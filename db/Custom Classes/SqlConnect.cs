namespace db.Custom_Classes {
    public static class SqlConnect {
        public static string? GetConnectionString(string? ip, string? dbName, string? login, string? password, bool isTrustedConnection = false, bool trustServerSertificate = true) {
            string? connectionString = "";
                
            connectionString += $"Server={(ip == null ? "" : ip)};";
            connectionString += $"Database={(dbName == null ? "" : dbName)};";
            connectionString += $"User ID={(login == null ? "" : login)};";
            connectionString += $"Password={(password == null ? "" : password)};";
            connectionString += $"Trusted_Connection={(isTrustedConnection ? "True" : "False")};";
            connectionString += $"TrustServerCertificate={(trustServerSertificate ? "True" : "False")};";

            return connectionString;
        }
    }
}
