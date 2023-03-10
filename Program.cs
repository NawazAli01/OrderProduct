using ProductApis.Data.Repositories;
using ProductApis.Data;
using Zero.EFCoreSpecification;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSQLServerEFCoreSpecificationServices<ApplicationDbContext>
    (builder.Configuration.GetConnectionString("Db")!, typeof(EfRepository<>));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //The generated Swagger JSON file will have these properties.
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "MyApi",
        Description = "CentreServices Web API Sample Example"
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();