using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;      //Это пространство имен потребуется, когда вы будете вызывать SwaggerDoc() и предоставлять сведения о заголовке для API.
using Products;

var builder = WebApplication.CreateBuilder(args);       //The web application class used to configure the HTTP pipeline, and routes.
/*Create Builder it is Initializes a new instance of the WebApplicationBuilder class with preconfigured defaults.
через билдер мы можем подключить например энтити фреймворк или сваггер потому что у него есть builder.Services*/

builder.Services.AddEndpointsApiExplorer();
/*Метод AddEndpointsApiExplorer() используется в ASP.NET Core для подключения генератора документации API (например, Swagger)
 * при работе с минимальным API или эндпоинтами, определенными без использования контроллеров. 
 * Этот метод регистрирует специальный сервис, который позволяет генерировать метаданные об определенных маршрутах
 * и параметрах минимального API для дальнейшего отображения в документации.
*/

/*
 builder.Services.AddEndpointsApiExplorer() и другие сервисы должны быть добавлены до вызова builder.Build() и до того,
как вы начинаете настраивать маршруты через app.MapGet() и другие методы.
После того, как вы вызываете builder.Build(), коллекция сервисов становится доступной для использования, и ее больше нельзя изменять.
 */
builder.Services.AddSwaggerGen(options =>         //добавить свагер     //options — это экземпляр класса SwaggerUIOptions.
                                                  //Этот объект содержит свойства и методы, которые позволяют детально настроить Swagger UI.
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",

    });
});

var app = builder.Build();
/*
 После вызова builder.Build() коллекция сервисов становится неизменяемой, и вы не сможете добавить новые сервисы или изменить существующие.
Поэтому все регистрации сервисов, таких как коллекции, базы данных или другие зависимости, должны быть выполнены до этого этапа.
*/
if (app.Environment.IsDevelopment())            //https://metanit.com/sharp/aspnet6/2.17.php
{
    /*Использование app.Environment.IsDevelopment() после того, как приложение построено, позволяет настроить Swagger только в среде разработки.
     * Здесь важно, чтобы весь код для добавления сервисов был до builder.Build(), чтобы избежать ошибки.*/

    app.UseSwagger();               //https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-8.0&tabs=visual-studio
    app.UseSwaggerUI(options =>           //генератор Swagger, который создает SwaggerDocumentобъекты напрямую из ваших маршрутов, контроллеров и моделей.
                                          //Обычно он сочетается с промежуточным программным обеспечением конечной точки Swagger для автоматического предоставления Swagger JSON.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API ");
    });
}

/*
GET	    Возвращает данные
POST	Отправляет данные для создания ресурса
PUT	    Отправляет данные для обновления ресурса
DELETE	Удаляет ресурс
 */

Persons persons = new Persons();
app.MapGet("/person", persons.GetPersons);
app.MapGet("/person/{id}", persons.GetPerson);
app.MapPost("/add", persons.CreatePerson);
app.MapPut("/update/{id}", persons.UpdatePerson);
app.MapDelete("/delete/{id}", persons.DeletePerson);

app.Run();  // запускает API и прослушивает запросы от клиента.