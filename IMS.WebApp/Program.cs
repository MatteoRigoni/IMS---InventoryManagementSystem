using IMS.Plugins.EFCore;
using IMS.UseCases;
using IMS.UseCases.Interfaces;
using IMS.UseCases.Inventories;
using IMS.UseCases.Inventories.Interfaces;
using IMS.UseCases.PluginInterfaces;
using IMS.UseCases.Products;
using IMS.UseCases.Products.Interfaces;
using IMS.UseCases.Reports;
using IMS.UseCases.Reports.Interfaces;
using IMS.WebApp.Areas.Identity;
using IMS.WebApp.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<WeatherForecastService>();

//builder.Services.AddDbContext<IMSContext>(options =>
//{
//    options.UseInMemoryDatabase("IMS");
//});
builder.Services.AddDbContext<IMSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryManagement")));

// DI repositories
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInventoryTransactionRepository, InventoryTransactionRepository>();
builder.Services.AddScoped<IProductTransactionRepository, ProductTransactionRepository>();

// DI use cases
builder.Services.AddScoped<IViewInventoriesByNameUseCase, ViewInventoriesByNameUseCase>();
builder.Services.AddScoped<IAddInventoryUseCase, AddInventoryUseCase>();
builder.Services.AddScoped<IEditInventoryUseCase, EditInventoryUseCase>();
builder.Services.AddScoped<IGetInventoryByIdUseCase, GetInventoryByIdUseCase>();
builder.Services.AddScoped<IViewProductsByNameUseCase, ViewProductsByNameUseCase>();
builder.Services.AddScoped<IViewProductByIdUseCase, ViewProductByIdUseCase>();
builder.Services.AddScoped<IAddProductUseCase, AddProductUseCase>(); 
builder.Services.AddScoped<IEdiProductUseCase, EdiProductUseCase>(); 
builder.Services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
builder.Services.AddScoped<IPurchaseInventoryUseCase, PurchaseInventoryUseCase>();
builder.Services.AddScoped<IProduceProductUseCase, ProduceProductUseCase>();
builder.Services.AddScoped<IValidateEnoughInventoriesFOrProductingUseCase, ValidateEnoughInventoriesFOrProductingUseCase>();
builder.Services.AddScoped<ISellProductUseCase, SellProductUseCase>();
builder.Services.AddScoped<IGetInventoryByIdUseCase, GetInventoryByIdUseCase>();
builder.Services.AddScoped<IGetInventoriesTransactionsUseCase, GetInventoriesTransactionsUseCase>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var imsContext = scope.ServiceProvider.GetRequiredService<IMSContext>();
imsContext.Database.EnsureDeleted();
imsContext.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
