
*********Regenerate DB **************************************************************
1) add following in AppRepository.cs
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlite("Data Source=AppDatabase.db");
}


2) Delate Migrations folder from project if exist
3) open progect folder in file explorer type cmd in file path
4) Type - "dotnet ef migrations add InitialCreate"
5) Type - "dotnet ef database update"
6) Delate Migrations folder 
7) Delate above methode from AppRepository
****************************************************************************************