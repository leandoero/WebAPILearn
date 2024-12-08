using System.Data.SqlTypes;
using Products;

//https://learn.microsoft.com/en-us/training/modules/build-web-api-minimal-database/
//https://learn.microsoft.com/en-us/training/modules/build-web-api-minimal-database/5-exercise-use-sqlite-database
//https://www.youtube.com/watch?v=M3_Jt_qlr9w
namespace myDbName;
public class myDb : DbContext        //DbContext предоставляет функциональность для взаимодействия с базой данных
{
    public myDb(DbContextOptions options):base(options) { }
   /* DbContextOptions — это класс, содержащий параметры конфигурации для DbContext, такие как строка подключения, 
      используемый провайдер базы данных(например, SQL Server, SQLite и т.д.), настройки кэширования и другие параметры.
      base(options) — вызывает конструктор базового класса DbContext и передает ему объект настроек.
      Это позволяет вашему контексту работать с базой данных, используя указанные параметры конфигурации.
    */
    public DbSet<Person> Persons { get; set; }
    //DbSet<T> представляет собой коллекцию сущностей определенного типа(T), которые будут управляться Entity Framework Core.
    //В данном случае это коллекция сущностей типа Person.
}


/*

After all:

dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0


создание миграции
dotnet ef migrations add InitialCreate
применить миграцию к базе данных
dotnet ef database update


 */

