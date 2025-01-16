
using backend.Application;
using backend.Infrastructure;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<RegistrarVentaHandler>();
builder.Services.AddScoped<IRegistrarVenta, RegistrarVentaHandler>();
builder.Services.AddScoped<RegistrarVentaCommand>();

builder.Services.AddTransient<RegistrarClienteHandler>();
builder.Services.AddScoped<IRegistrarCliente, RegistrarClienteHandler>();
builder.Services.AddScoped<RegistrarClienteCommand>();

builder.Services.AddTransient<RegistrarCafeHandler>();
builder.Services.AddScoped<IRegistrarCafe, RegistrarCafeHandler>();
builder.Services.AddScoped<RegistrarCafeCommand>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
