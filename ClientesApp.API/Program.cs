using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Adicionar as configurações para o Swagger (Documentação da API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Executando a documentação do Swagger configurado
app.UseSwagger();
app.UseSwaggerUI();

//Executando a documentação do Scalar
app.MapScalarApiReference(op => { op.WithTheme(ScalarTheme.BluePlanet); });

app.UseAuthorization();

app.MapControllers();

app.Run();
