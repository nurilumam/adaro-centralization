using AdaroConnect.Application.Core.Extensions;
using Microsoft.AspNetCore.Authentication.Negotiate;


#region Constants

string AppSettingsFileName = "appSettings.json";
string UserSecretId = "6EE22606-D56C-4FC3-A363-5C58E3ED1371";

#endregion

#region Properties


#endregion


var builder = WebApplication.CreateBuilder(args);

IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile(AppSettingsFileName, true, true)
    .AddUserSecrets(UserSecretId, true)
    .AddEnvironmentVariables();

IConfiguration Configuration = configurationBuilder.Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
//   .AddNegotiate();

builder.Services.AddAdaroConnectSampleCore(Configuration);

//builder.Services.AddAuthorization(options =>
//{
//    // By default, all incoming requests will be authorized according to the default policy.
//    options.FallbackPolicy = options.DefaultPolicy;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//public static IHostBuilder CreateHostBuilder(string[] args) =>
//        Host.CreateDefaultBuilder(args)
//            .ConfigureWebHostDefaults(webBuilder =>
//            {
//                webBuilder
//                .UseNLog()
//                //.UseContentRoot(Directory.GetCurrentDirectory())
//                //.UseKestrel()
//                .UseIISIntegration()
//                .UseIIS()
//                .UseStartup<Startup>();
//                //.Build();
//            });


app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
