using Service_Api;
using Service_Api.BusinessLogicLayer;
using Service_Api.BusinessLogicLayer.Interfaces;
using ServiceData.DatabaseLayer;
using ServiceData.DatabaseLayer.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICustomerGroup, CustomerGroupDatabaseAccess>();
builder.Services.AddScoped<ICustomerGroupData, CustomerGroupDataControl>();
builder.Services.AddScoped<IDiscount, DiscountDatabaseAccess>();
builder.Services.AddScoped<IDiscountData, DiscountDataControl>();
builder.Services.AddScoped<IProduct, ProductDatabaseAccess>();
builder.Services.AddScoped<IProductData,  ProductDataControl>();


builder.Services.AddControllers();


// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
