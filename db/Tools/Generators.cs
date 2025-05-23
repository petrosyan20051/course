using db.Models;

namespace db.Tools {
    public class Generators {
        public static ICollection<Customer> GenerateCustomers(int count) {
            var firstNames = new[] { "Иван", "Алексей", "Дмитрий", "Сергей", "Андрей", "Михаил", "Артем", "Николай", "Павел", "Егор" };
            var lastNames = new[] { "Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов", "Васильев", "Попов", "Соколов", "Михайлов", "Новиков" };
            var domains = new[] { "gmail.com", "mail.ru", "yandex.ru", "outlook.com", "hotmail.com" };

            var customers = new List<Customer>();
            var random = new Random();

            for (int i = 0; i < count; i++) {
                var firstName = firstNames[random.Next(firstNames.Length)];
                var lastName = lastNames[random.Next(lastNames.Length)];
                var email = $"{firstName.ToLower()}.{lastName.ToLower()}@{domains[random.Next(domains.Length)]}";
                var phone = $"+7{random.Next(900, 1000)}{random.Next(1000000, 9999999)}";

                customers.Add(new Customer {
                    Id = i + 1,
                    Forename = firstName,
                    Surname = lastName,
                    PhoneNumber = phone,
                    Email = email,
                    WhoAdded = "System",
                    WhenAdded = DateTime.Now.AddDays(-random.Next(1, 365)),
                    WhoChanged = "System",
                    WhenChanged = DateTime.Now.AddDays(-random.Next(1, 365)),
                    Note = $"Клиент {firstName} {lastName}"
                });
            }

            return customers;
        }
    }
}
