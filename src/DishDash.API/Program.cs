var builder = WebApplication.CreateBuilder(args);

// 1. LE DECIMOS A .NET QUE VAMOS A USAR CONTROLADORES
builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 2. ACTIVAMOS LA AUTORIZACIÓN (Por si la necesitan más adelante)
app.UseAuthorization();

// 3. MAPEAMOS LOS CONTROLADORES PARA QUE LA API SEPA DÓNDE BUSCARLOS
app.MapControllers();

app.Run();