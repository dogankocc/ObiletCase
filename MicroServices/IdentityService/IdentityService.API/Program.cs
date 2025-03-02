using IdentityService.API.Service;

var builder = WebApplication.CreateBuilder(args);

// gRPC servisini ekle
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<IdentityServiceImpl>();  // Servisi register et
app.MapGet("/", () => "IdentityService gRPC Çalýþýyor!");

app.Run();
