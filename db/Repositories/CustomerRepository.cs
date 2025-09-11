﻿using db.Contexts;
using db.Interfaces;
using db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TypeId = int;

namespace db.Repositories {
    public class CustomerRepository : IRepository<Customer, TypeId>,
        IDeletable<TypeId>, IRecovarable<TypeId> {
        private readonly OrderDbContext _context;

        public CustomerRepository(OrderDbContext context) {
            _context = context;
        }

        public async Task<Customer?> GetByIdAsync(TypeId id) {
            return await _context.Customers.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Customer>?> GetAllAsync() {
            return await _context.Customers.ToListAsync();
        }

        public async Task AddAsync(Customer entity) {

            await EntityValidate(entity.Forename, entity.Surname, entity.PhoneNumber, entity.Email,
                entity.WhoAdded, entity.WhenAdded, entity.Id, entity.WhoChanged, entity.WhenChanged,
                entity.Note, entity.isDeleted);

            await _context.Customers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(string forename, string surname, string phoneNumber, string email, 
            string whoAdded, DateTime whenAdded, TypeId? id = null, string? whoChanged = null, 
            DateTime? whenChanged = null, string? note = null, DateTime? isDeleted = null) {

            await EntityValidate(forename, surname, phoneNumber, email, whoAdded, whenAdded, id,
                whoChanged, whenChanged, note, isDeleted);

            var entity = new Customer {
                Forename = forename,
                Surname = surname,
                PhoneNumber = phoneNumber,
                Email = email,
                WhoAdded = whoAdded,
                WhenAdded = whenAdded,
                WhoChanged = whoChanged,
                WhenChanged = whenChanged,
                Note = note,
                isDeleted = isDeleted
            };

            await _context.Customers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        private async Task EntityValidate(string forename, string surname, string phoneNumber, string email,
            string whoAdded, DateTime whenAdded, TypeId? id = null, string? whoChanged = null,
            DateTime? whenChanged = null, string? note = null, DateTime? isDeleted = null) {

            if (forename.IsNullOrEmpty()) {
                throw new ArgumentNullException("Forename must be no empty string");
            } else if (surname.IsNullOrEmpty()) {
                throw new ArgumentNullException("Surname must be no empty string");
            } else if (phoneNumber.IsNullOrEmpty()) {
                throw new ArgumentNullException("Phone number must be no empty string");
            } else if (email.IsNullOrEmpty()) {
                throw new ArgumentNullException("Email must be no empty string");
            } else if (whoAdded.IsNullOrEmpty()) {
                throw new ArgumentNullException("\"Who added\" must be no empty string");
            }

            if (!Customer.PhoneNumberValidate(phoneNumber)) {
                throw new InvalidDataException("Ivalid phone number");
            } else if (!Customer.EmailValidate(email)) {
                throw new InvalidDataException("Ivalid phone email");
            }

            if (id != 0) {
                throw new InvalidDataException("Entity must contain zero ID. Auto generation of ID is used");
            } else if (id == null && await NewIdToAddAsync() == -1)
                throw new DbUpdateException("Database has no available id for new entity");
        }

        public async Task UpdateAsync(Customer entity) {
            _context.Customers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(TypeId id) {
            var entity = await GetByIdAsync(id);
            if (entity != null) {
                entity.isDeleted = DateTime.Now; // soft delete
                entity.WhenChanged = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(TypeId id) {
            var entity = await GetByIdAsync(id);
            if (entity != null) {
                _context.Customers.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TypeId> NewIdToAddAsync() {
            var entities = await GetAllAsync();
            if (entities == null)
                return 0; // entities are not found so can use id = 0

            // Get All deleted Ids in ascending order
            var Ids = entities
                .Where(e => e.isDeleted != null || e.isDeleted is null)
                .Select(e => e.Id)
                .OrderBy(id => id)
                .ToList();
            if (Ids.Last() == TypeId.MaxValue) {
                return -1; // all seats are reserved
            }

            return Ids.Last() + 1; // maybe all seats are reserved
        }

        public async Task<bool> RecoverAsync(TypeId id) {
            var entity = await GetByIdAsync(id);
            if (entity != null) {
                entity.isDeleted = null;
                entity.WhenChanged = DateTime.Now;
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}
