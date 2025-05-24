using Microsoft.EntityFrameworkCore;

public abstract class BaseDbContext : DbContext {
    protected BaseDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // Общие конфигурации для всех сущностей
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
            // Настройка для всех сущностей с полем Id
            if (entityType.FindProperty("Id") != null) {
                modelBuilder.Entity(entityType.ClrType)
                    .Property("Id")
                    .ValueGeneratedOnAdd();
            }

            // Автоматическая регистрация всех IEntityTypeConfiguration
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }

    // Общие методы для всех контекстов
    public async Task<int> ExecuteInTransactionAsync(Func<Task> action) {
        using var transaction = await Database.BeginTransactionAsync();
        try {
            await action();
            await transaction.CommitAsync();
            return 1;
        } catch {
            await transaction.RollbackAsync();
            throw;
        }
    }
}