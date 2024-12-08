using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;      //��� ������������ ���� �����������, ����� �� ������ �������� SwaggerDoc() � ������������� �������� � ��������� ��� API.
using Products;

var builder = WebApplication.CreateBuilder(args);       //The web application class used to configure the HTTP pipeline, and routes.
/*Create Builder it is Initializes a new instance of the WebApplicationBuilder class with preconfigured defaults.
����� ������ �� ����� ���������� �������� ������ ��������� ��� ������� ������ ��� � ���� ���� builder.Services*/

builder.Services.AddEndpointsApiExplorer();
/*����� AddEndpointsApiExplorer() ������������ � ASP.NET Core ��� ����������� ���������� ������������ API (��������, Swagger)
 * ��� ������ � ����������� API ��� �����������, ������������� ��� ������������� ������������. 
 * ���� ����� ������������ ����������� ������, ������� ��������� ������������ ���������� �� ������������ ���������
 * � ���������� ������������ API ��� ����������� ����������� � ������������.
*/

/*
 builder.Services.AddEndpointsApiExplorer() � ������ ������� ������ ���� ��������� �� ������ builder.Build() � �� ����,
��� �� ��������� ����������� �������� ����� app.MapGet() � ������ ������.
����� ����, ��� �� ��������� builder.Build(), ��������� �������� ���������� ��������� ��� �������������, � �� ������ ������ ��������.
 */
builder.Services.AddSwaggerGen(options =>         //�������� ������     //options � ��� ��������� ������ SwaggerUIOptions.
                                                  //���� ������ �������� �������� � ������, ������� ��������� �������� ��������� Swagger UI.
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
 ����� ������ builder.Build() ��������� �������� ���������� ������������, � �� �� ������� �������� ����� ������� ��� �������� ������������.
������� ��� ����������� ��������, ����� ��� ���������, ���� ������ ��� ������ �����������, ������ ���� ��������� �� ����� �����.
*/
if (app.Environment.IsDevelopment())            //https://metanit.com/sharp/aspnet6/2.17.php
{
    /*������������� app.Environment.IsDevelopment() ����� ����, ��� ���������� ���������, ��������� ��������� Swagger ������ � ����� ����������.
     * ����� �����, ����� ���� ��� ��� ���������� �������� ��� �� builder.Build(), ����� �������� ������.*/

    app.UseSwagger();               //https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-8.0&tabs=visual-studio
    app.UseSwaggerUI(options =>           //��������� Swagger, ������� ������� SwaggerDocument������� �������� �� ����� ���������, ������������ � �������.
                                          //������ �� ���������� � ������������� ����������� ������������ �������� ����� Swagger ��� ��������������� �������������� Swagger JSON.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API ");
    });
}

/*
GET	    ���������� ������
POST	���������� ������ ��� �������� �������
PUT	    ���������� ������ ��� ���������� �������
DELETE	������� ������
 */

Persons persons = new Persons();
app.MapGet("/person", persons.GetPersons);
app.MapGet("/person/{id}", persons.GetPerson);
app.MapPost("/add", persons.CreatePerson);
app.MapPut("/update/{id}", persons.UpdatePerson);
app.MapDelete("/delete/{id}", persons.DeletePerson);

app.Run();  // ��������� API � ������������ ������� �� �������.