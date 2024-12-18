using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReactFlight.Server.BLDataRepository.BLData;
using ReactFlight.Server.BLDataRepository.ILData;
using ReactFlight.Server.BussinessCore.AbstractFlight;
using ReactFlight.Server.BussinessCore.CoreFlight;
using ReactFlight.Server.InfraLayer.DATA;
using ReactFlight.Server.InfraLayer.Product.Flight;
using ReactFlight.Server.Model.Common;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
/*------------------------ Add services to the container.-----------------------------*/
builder.Services.AddControllers();
builder.Services.AddCors(policy =>
{
    policy.AddPolicy(MyAllowSpecificOrigins, policy =>
                     {
                         policy.AllowAnyOrigin().
                                        AllowAnyHeader().
                                            AllowAnyMethod();
                     });
});
builder.Services.AddHttpContextAccessor();
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});
var configurationroot = (ConfigurationRoot)new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
string connectionSection = (configurationroot.GetValue<string>("ServiceMode") ?? string.Empty).Equals(MYEnum.ServiceMode.TEST.ToString(), StringComparison.OrdinalIgnoreCase) ? "TestConnection" : "LiveConnection";
builder.Services.AddScoped<SHELLEntity>();
builder.Services.AddScoped<BrightsunEntity>();
builder.Services.AddScoped<IBEEntity>();
builder.Services.AddPooledDbContextFactory<SHELLEntity>(options => options.UseSqlServer(configurationroot.GetSection("ConnectionString").GetSection(connectionSection)["ShellEntity"]));
builder.Services.AddPooledDbContextFactory<BrightsunEntity>(options => options.UseSqlServer(configurationroot.GetSection("ConnectionString").GetSection(connectionSection)["BrightsunEntity"]));
builder.Services.AddPooledDbContextFactory<IBEEntity>(options => options.UseSqlServer(configurationroot.GetSection("ConnectionString").GetSection(connectionSection)["IBEEntity"]));
builder.Services.AddScoped<ILDataContext, BLDataContext>();
builder.Services.AddScoped<DLAirport>();
builder.Services.AddScoped<ILAirport, BLAirport>();
builder.Services.AddScoped<ILFlight, BLFlight>();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors(MyAllowSpecificOrigins);
/*----------------------------- Configure the HTTP request pipeline.------------------*/
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseHsts();
app.UseAuthorization();
//app.UseSession();
app.UseRouting();
app.MapControllers();
app.MapFallbackToFile("/index.html");
app.Run();

/*----------------------------- END -------------------------------------------------*/