using Microsoft.EntityFrameworkCore;
using TrabalhoAPI.Data;
using TrabalhoAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar Entity Framework com SQLite
builder.Services.AddDbContext<ControleInternoContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? 
    "Data Source=ControleInterno.db"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Swagger sempre habilitado para facilitar testes
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Controle Interno v1");
    c.RoutePrefix = "swagger"; // Define a rota como /swagger
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Rota raiz que redireciona para o Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

// Garantir que o banco de dados seja criado e populado
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ControleInternoContext>();
    context.Database.EnsureCreated();
    
    // Popular o banco com dados iniciais se estiver vazio
    if (!context.Politicas.Any())
    {
        SeedData.Initialize(context);
    }
}

app.Run();
