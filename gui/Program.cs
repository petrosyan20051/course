using gui.Services;

AuthService authService = new AuthService();
try {
    //var response = await authService.LoginAsync("puzan58", "JcGDN9ST5KEG!#");
    var response1 = await authService.RegisterAsync(new DbAPI.DTO.RegisterPrompt {
        UserName = "puzan59",
        Password = "JcGDN9ST5KEG!",
        RegisterRights = db.Interfaces.IInformation.UserRights.Admin,
        WhoRegister = "admin"
    });
    Console.WriteLine(response1.UserName);
} catch (Exception ex) {
    Console.WriteLine($"Error: {Environment.NewLine}{Environment.NewLine}{ex.Message}");
}

