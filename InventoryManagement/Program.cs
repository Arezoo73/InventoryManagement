using InventoryManagement.Core.IServices;
using InventoryManagement.Core.Services;
using InventoryManagement.Data.DatabaseContexts;
using InventoryManagement.Data.IRepository;
using InventoryManagement.Data.Repository;
using InventoryManagement.Web;
using Microsoft.EntityFrameworkCore;
using OrderProcessing.Core.IServices;
using OrderProcessing.Core.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
builder.Services.AddScoped<IInventoryItemService, InventoryItemService>();
builder.Services.AddScoped<IProcessItemQuantityService, ProcessItemQuantityService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DataContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
