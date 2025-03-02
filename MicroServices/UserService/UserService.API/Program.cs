using UserService.API.Middleware;
using UserService.API.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// **Memory Cache Kullanarak IDistributedCache Servisini Ekle**
builder.Services.AddDistributedMemoryCache();

// IHttpContextAccessor'ý ekliyoruz
builder.Services.AddHttpContextAccessor();

// Session kullanýmý
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // Session süresi
    options.Cookie.HttpOnly = true; // Güvenlik için HTTPOnly yapýyoruz
    options.Cookie.IsEssential = false; // Çerez kullanmak zorunlu deðil
    options.Cookie.Name = ".ObiletUserService";  
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.MaxAge = TimeSpan.FromMinutes(60);  // Cookie süresi
});


builder.Services.AddScoped<IdentityClientService>();
builder.Services.AddScoped<SessionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Session kullanýmý
app.UseSession();

app.UseMiddleware<GatewayMiddleware>();

app.Run();
