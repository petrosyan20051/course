﻿using db.Contexts;
using db.Interfaces;
using db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TypeId = int;

namespace db.Repositories {
    namespace db.Repositories {
        public class RateRepository : IRepository<Rate, TypeId>,
        IDeletable<TypeId>, IRecovarable<TypeId> {
            private readonly OrderDbContext _context;

            public RateRepository(OrderDbContext context) {
                _context = context;
            }

            public async Task<Rate?> GetByIdAsync(TypeId id) {
                return await _context.Rates.FirstOrDefaultAsync(o => o.Id == id);
            }

            public async Task<IEnumerable<Rate>?> GetAllAsync() {
                return await _context.Rates.ToListAsync();
            }

            public async Task AddAsync(Rate entity) {

                await EntityValidate(entity.Forename, entity.DriverId, entity.VehicleId, entity.MovePrice,
                    entity.IdlePrice, entity.WhoAdded, entity.WhenAdded, entity.Id, entity.WhoChanged,
                    entity.WhenChanged, entity.Note, entity.IsDeleted);

                await _context.Rates.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            public async Task AddAsync(string forename, TypeId driverId, TypeId vehicleId, int movePrice,
            int idlePrice, string whoAdded, DateTime whenAdded, string? whoChanged = null,
            TypeId? id = null, DateTime? whenChanged = null, string? note = null, DateTime? isDeleted = null) {

                await EntityValidate(forename, driverId, vehicleId, movePrice, idlePrice, whoAdded, whenAdded,
                    id, whoChanged, whenChanged, note, isDeleted);

                var entity = new Rate {
                    Forename = forename,
                    DriverId = driverId,
                    VehicleId = vehicleId,
                    MovePrice = movePrice,
                    IdlePrice = idlePrice,
                    WhoAdded = whoAdded,
                    WhenAdded = whenAdded,
                    WhoChanged = whoChanged,
                    WhenChanged = whenChanged,
                    Note = note,
                    IsDeleted = isDeleted
                };

                await _context.Rates.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            private async Task EntityValidate(string forename, TypeId driverId, TypeId vehicleId, int movePrice,
            int idlePrice, string whoAdded, DateTime whenAdded, TypeId? id, string? whoChanged = null,
            DateTime? whenChanged = null, string? note = null, DateTime? isDeleted = null) {

                if (forename.IsNullOrEmpty()) {
                    throw new ArgumentNullException("Forename must be no empty string");
                } else if (whoAdded.IsNullOrEmpty()) {
                    throw new ArgumentNullException("\"Who added\" must be no empty string");
                }

                if (!Rate.MovePriceValidate(movePrice))
                    throw new ArgumentException("Move price must be positive integer");
                else if (!Rate.IdlePriceValidate(idlePrice))
                    throw new ArgumentException("Idle price must be positive integer");

                if (await _context.Drivers.AnyAsync(d => d.Id == driverId) == false) {
                    throw new InvalidDataException($"Driver with id = {driverId} does not exist");
                } else if (await _context.TransportVehicles.AnyAsync(t => t.Id == vehicleId) == false) {
                    throw new InvalidDataException($"Transport vehicle with id = {vehicleId} does not exist");
                }

                if (id != 0) {
                    throw new InvalidDataException("Entity must contain zero ID. Auto generation of ID is used");
                } else if (id == null)
                    throw new DbUpdateException("Database has no available id for new entity");
            }

            public async Task UpdateAsync(Rate entity) {
                _context.Rates.Update(entity);
                await _context.SaveChangesAsync();
            }

            public async Task SoftDeleteAsync(TypeId id) {
                var entity = await GetByIdAsync(id);
                if (entity != null) {
                    entity.IsDeleted = DateTime.Now; // soft delete
                    entity.WhenChanged = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
            }

            public async Task DeleteAsync(TypeId id) {
                var entity = await GetByIdAsync(id);
                if (entity != null) {
                    _context.Rates.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task<bool> RecoverAsync(TypeId id) {
                var entity = await GetByIdAsync(id);
                if (entity?.Id != null) {
                    entity.IsDeleted = null;
                    entity.WhenChanged = DateTime.Now;
                    await _context.SaveChangesAsync();

                    return true;
                }
                return false;
            }

            /*public async Task<TypeId> NewIdToAddAsync() {
                var entities = await GetAllAsync();
                if (entities == null)
                    return 0; // entities are not found so can use id = 0

                // Get All deleted Ids in ascending order
                var Ids = entities
                    .Where(e => e.IsDeleted != null || e.IsDeleted is null)
                    .Select(e => e.Id)
                    .OrderBy(id => id)
                    .ToList();
                if (Ids.Last() == TypeId.MaxValue) {
                    return -1; // all seats are reserved
                }

                return Ids.Last() + 1; // maybe all seats are reserved
            }*/
        }
    }
}
