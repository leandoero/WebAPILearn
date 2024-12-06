using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;      //��� ������������ ���� �����������, ����� �� ������ �������� SwaggerDoc() � ������������� �������� � ��������� ��� API.

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
List<Products> products = new List<Products>()          //https://metanit.com/sharp/tutorial/4.5.php
{
    new Products{Id = 0, Name = "first", Description = "desk" },
    new Products{ Id = 1, Name = "second", Description = "desk"}
};

app.MapGet("/products", () => products);
app.MapGet("/products/{id}", (int ID) =>
{
    var product = products.FirstOrDefault(p => p.Id == ID);

    if (product == null)
    {
        throw new Exception("not found");       //����� ����������
    }
    else return product;   //Results.Ok(product) ����� ������� � ����� 200 (�����),
                           //���� ������� ������.
});


app.Run();  // ��������� API � ������������ ������� �� �������.

public class Products
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

}