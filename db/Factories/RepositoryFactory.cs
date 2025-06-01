using db.Contexts;
using db.Repositories;
using System.Reflection;

namespace db.Factories {
    public class RepositoryFactory<TRepository> where TRepository : class {
        private readonly OrderDbContext _context;

        public RepositoryFactory(OrderDbContext context) {
            _context = context;
        }

        public TRepository? CreateRepository() {
            return typeof(TRepository)
                .GetConstructor(new Type[] { typeof(OrderDbContext) })?
                .Invoke(new object[] { _context }) as TRepository;
        }
    }

    public class RepositoryFactory {
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
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                binder: null,
                types: new[] { typeof(OrderDbContext) },
                modifiers: null
            );

            return constructorGeneric?.Invoke(new object[] { _context });
        }

    }
}
