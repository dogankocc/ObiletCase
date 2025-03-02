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

// IHttpContextAccessor'� ekliyoruz
builder.Services.AddHttpContextAccessor();

// Session kullan�m�
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60); // Session s�resi
    options.Cookie.HttpOnly = true; // G�venlik i�in HTTPOnly yap�yoruz
    options.Cookie.IsEssential = false; // �erez kullanmak zorunlu de�il
    options.Cookie.Name = ".ObiletUserService";  
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.MaxAge = TimeSpan.FromMinutes(60);  // Cookie s�resi
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

// Session kullan�m�
app.UseSession();

app.UseMiddleware<GatewayMiddleware>();

app.Run();
