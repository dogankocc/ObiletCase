using AutoMapper;
using ObiletService.Core.Application.Features.Queries.BusLocation.List;
using ObiletService.Core.Application.Features.Queries.BusLocation.Search;
using ObiletService.Core.Application.Mapping;
using ObiletService.Middleware;
using ObiletService.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetBusLocationQueryHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(SearchBusLocationQueryHandler).Assembly));

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new GeneralMappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


builder.Services.AddHttpClient();

builder.Services.AddDistributedMemoryCache(); // Bellekte session saklamak için

// Session kullanýmý
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "ObiletServiceSession";
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Session süresi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;  
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.AddSingleton<IdentityClientService>();
builder.Services.AddScoped<SessionService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

// Session kullanýmý
app.UseSession();

app.UseAuthorization();

app.MapControllers();

//app.MapFallbackToFile("~/Clients/obiletclient.client/index.html");

app.UseMiddleware<GatewayMiddleware>();

app.Run();
