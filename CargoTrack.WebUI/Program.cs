using CargoTrack.Business;
using CargoTrack.Business.Services.Abouts;
using CargoTrack.Business.Services.Branches;
using CargoTrack.Business.Services.Cities;
using CargoTrack.DataAccess.Context;
using CargoTrack.DataAccess.Repositories.Abouts;
using CargoTrack.DataAccess.Repositories.Branches;
using CargoTrack.DataAccess.Repositories.Cities;
using CargoTrack.Entity.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
//registration
//eagerloading
//lazyloading
//Extension Method?

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

    options.UseLazyLoadingProxies();
});

builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<AppDbContext>();
// Add services to the container.
//IOC Container
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Login/Index";
    config.LogoutPath = "/Login/Logout";
    config.AccessDeniedPath = "/ErrorPages/AccessDenied";
    config.Cookie.Name = "CargoTrackCookie";
});


builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssembly(typeof(BusinessAssembly).Assembly);
//Assembly =katman

                //.AddValidatorsFromAssemblyContaining<BusinessAssembly>();

builder.Services.AddScoped<IAboutRepository,AboutRepository>();
builder.Services.AddScoped<IBranchRepository,BranchRepository>();   
builder.Services.AddScoped<ICityRepository,CityRepository>();

builder.Services.AddScoped<IAboutService,AboutService>();
builder.Services.AddScoped<IBranchService,BranchService>();
builder.Services.AddScoped<ICityService,CityService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Uygulama baþlarken Seed Data ekleme iþlemi
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Veritabanýnýn var olduðundan emin olun (Migration kullanýyorsanýz context.Database.Migrate() yapýn)
    context.Database.EnsureCreated();

    // Eðer veritabanýnda hiç þehir yoksa ekle //buralar biz projeyi bitirdiðimizde kaldýrýlacak
    if (!context.Cities.Any())
    {
        var cities = new List<City>
        {
            new City { Id = Guid.NewGuid(), Name = "Ýstanbul" },
            new City { Id = Guid.NewGuid(), Name = "Ankara" },
            new City { Id = Guid.NewGuid(), Name = "Ýzmir" },
            new City { Id = Guid.NewGuid(), Name = "Bursa" },
            new City { Id = Guid.NewGuid(), Name = "Antalya" },
            new City { Id = Guid.NewGuid(), Name = "Adana" },
            new City { Id = Guid.NewGuid(), Name = "Konya" },
            new City { Id = Guid.NewGuid(), Name = "Þanlýurfa" },
            new City { Id = Guid.NewGuid(), Name = "Gaziantep" },
            new City { Id = Guid.NewGuid(), Name = "Kocaeli" },
            new City { Id = Guid.NewGuid(), Name = "Mersin" },
            new City { Id = Guid.NewGuid(), Name = "Diyarbakýr" },
            new City { Id = Guid.NewGuid(), Name = "Hatay" },
            new City { Id = Guid.NewGuid(), Name = "Kayseri" },
            new City { Id = Guid.NewGuid(), Name = "Samsun" },
            new City { Id = Guid.NewGuid(), Name = "Balýkesir" },
            new City { Id = Guid.NewGuid(), Name = "Kahramanmaraþ" },
            new City { Id = Guid.NewGuid(), Name = "Van" },
            new City { Id = Guid.NewGuid(), Name = "Aydýn" },
            new City { Id = Guid.NewGuid(), Name = "Tekirdað" }
        };

        context.Cities.AddRange(cities);
        context.SaveChanges();
    }


    

    if (!context.Roles.Any())
    {
        var roles = new List<AppRole>
            {
                new AppRole{Name="Admin"},
                new AppRole{Name="Manager"},
                new AppRole{Name="User"},

            };
        context.Roles.AddRange(roles);
        context.SaveChanges();
    }
    if (!context.Cargos.Any())
    {
        var cargo = new Cargo
        {
            Id = Guid.NewGuid(),
            SenderId = Guid.Parse("ba17a101-be9f-4d26-19ee-08df085ce0b8"),
            ReceiverId = Guid.Parse("2866ab9b-9fff-4d4a-19ef-08df085ce0b8"),
            OriginBranchId = Guid.Parse("772fb50a-22de-4a3c-b093-201ad2c5512b"),
            DestinationBranchId = Guid.Parse("2464587a-51f5-48a0-9416-abb369fa197e"),
            TrackCode = "CT202609081234",
            ShipmentDate = DateTime.Now,
            ArrivalDate = DateTime.Now.AddDays(2),
            Weight = 2.5,
            CargoType = CargoTrack.Entity.Entities.Enums.CargoType.Standart,
            CargoStatus = CargoTrack.Entity.Entities.Enums.CargoStatus.DispatchedFromTransferCenter
        };

        context.Add(cargo);
        context.SaveChanges();
    }

}

//app.MapGet("/", () => "Uygulama çalýþýyor ve Seed Data kontrol edildi!");

app.Run();
