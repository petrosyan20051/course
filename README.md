1. Check .NET version and SDK's. Install SDK and GIT from official resources.

dotnet --version
dotnet --list-sdks

NOTE: all commands are entered in powershell.

2. Create folder for your project. Go to your project folder.

3. Create ASP.NET WEB API project

dotnet new webapi -o DbAPI -f net8.0
cd DbAPI

4. Install required packages.

dotnet add package Microsoft.EntityFrameworkCore --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.20

5. Clone repository in any place where "course" directory does not exist (or it is empty)

git clone https://github.com/petrosyan20051/course.git
cd course

6. Switch DBAPI branch

git switch DBAPI

7. Cut and paste all files (except ".git" folder) from repos to <your_project_folder_name> folder. Accept rewriting case neccessary.

8. Enjoy development.