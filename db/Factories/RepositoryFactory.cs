using db.Contexts;
using db.Interfaces;
using db.Repositories;
using db.Repositories.db.Repositories;

using TypeId = int;

namespace db.Factories {
    public class RepositoryFactory {
        private readonly OrderDbContext _context;

        public RepositoryFactory(OrderDbContext context) {
            _context = context;
        }

        public IRecovarable<TypeId>? CreateRecoverableRepository(string entityName) {
            if (entityName == "Credential")
                return new CredentialRepository(_context);
            else if (entityName == "Customer")
                return new CustomerRepository(_context);
            else if (entityName == "Driver")
                return new DriverRepository(_context);
            else if (entityName == "Order")
                return new OrderRepository(_context);
            else if (entityName == "Rate")
                return new RateRepository(_context);
            else if (entityName == "TransportVehicle")
                return new TransportVehicleRepository(_context);
            return null;
        }

        public IDeletable<TypeId>? CreateDeletableRepository(string entityName) {
            if (entityName == "Credential")
                return new CredentialRepository(_context);
            else if (entityName == "Customer")
                return new CustomerRepository(_context);
            else if (entityName == "Driver")
                return new DriverRepository(_context);
            else if (entityName == "Order")
                return new OrderRepository(_context);
            else if (entityName == "Rate")
                return new RateRepository(_context);
            else if (entityName == "TransportVehicle")
                return new TransportVehicleRepository(_context);
            return null;
        }



        #region Deprecated

        /*public TRepository? CreateRepository() {
            return typeof(TRepository)
                .GetConstructor(new Type[] { typeof(OrderDbContext) })?
                .Invoke(new object[] { _context }) as TRepository;
        }*/

        #endregion
    }

    #region Deprecated 
    /*public class RepositoryFactory {
       private readonly OrderDbContext _context;

       public RepositoryFactory(OrderDbContext context) {
           _context = context;
       }

       public object? CreateRepository(Type repositoryType) {
           // Проверяем, можно ли создать экземпляр
           //if (!typeof(IRepository).IsAssignableFrom(repositoryType)) {
           //    return null;
           //}

           var constructorGeneric = repositoryType.GetConstructor(
               BindingFlags.Instance | BindingFlags.Public,
               //binder: null,
               types: new[] { typeof(OrderDbContext) }
               //modifiers: null
           );

           return constructorGeneric?.Invoke(new object[] { _context });
       }

   }*/
    #endregion
}
