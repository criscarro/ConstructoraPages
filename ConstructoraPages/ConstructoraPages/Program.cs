
using ConstructoraPages.Data;
using ConstructoraPages.Repository;
using ConstructoraPages.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("defaultConnection")));

builder.Services.AddScoped<ObraRepository>();
builder.Services.AddScoped<ObraService>();
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<EmpleadoRepository>();
builder.Services.AddScoped<EmpleadoService>();
builder.Services.AddScoped<ContratistaRepository>();
builder.Services.AddScoped<ContratistaService>();
builder.Services.AddScoped<ProveedorRepository>();
builder.Services.AddScoped<ProveedorService>();
builder.Services.AddScoped<MaterialRepository>();
builder.Services.AddScoped<MaterialService>();
builder.Services.AddScoped<GastoRepository>();
builder.Services.AddScoped<GastoService>();
builder.Services.AddScoped<EtapaObraRepository>();
builder.Services.AddScoped<EtapaObraService>();
builder.Services.AddScoped<AsignacionEmpleadoRepository>();
builder.Services.AddScoped<AsignacionEmpleadoService>();
builder.Services.AddScoped<ContratistaObraRepository>();
builder.Services.AddScoped<ContratistaObraService>();
builder.Services.AddScoped<CompraMaterialRepository>();
builder.Services.AddScoped<CompraMaterialService>();
builder.Services.AddScoped<DetalleCompraRepository>();
builder.Services.AddScoped<DetalleCompraService>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("PermitirFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
