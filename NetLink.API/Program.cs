using System.Text;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MQTTnet.AspNetCore;
using NetLink.API.Data;
using NetLink.API.Exceptions;
using NetLink.API.Hubs;
using NetLink.API.Mapping;
using NetLink.API.Repositories;
using NetLink.API.Services;
using NetLink.API.Services.Auth;
using NetLink.API.Services.MQTT;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JWT");
var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]!);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddDbContext<NetLinkDbContext>(options => { options.UseSqlServer("name=ConnectionStrings:DefaultConnection"); });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetLink API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5169);
    options.ListenAnyIP(7260, listenOptions => { listenOptions.UseHttps(); });
    options.ListenAnyIP(1883, listenOptions => { listenOptions.UseMqtt(); });
});

builder.Services.AddHostedMqttServer(optionsBuilder =>
{
    optionsBuilder
        .WithDefaultEndpoint()
        .WithDefaultEndpointPort(1883)
        .WithDefaultEndpointBoundIPAddress(System.Net.IPAddress.Any);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowDev",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });

    options.AddPolicy("AllowPortal",
        policyBuilder =>
        {
            policyBuilder.WithOrigins("https://portal.netlink-solution.com")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("firebaseServiceAcc.json")
});

builder.Services.AddMqttConnectionHandler();
builder.Services.AddConnections();
builder.Services.AddSignalR();

builder.Services.AddControllers(options => { options.Filters.Add<NetLinkExceptionFilter>(); });
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IDeveloperRepository, DeveloperRepository>();
builder.Services.AddScoped<IEndUserRepository, EndUserRepository>();
builder.Services.AddScoped<ISensorRepository, SensorRepository>();

builder.Services.AddScoped<ISensorOperationsService, SensorOperationsService>();
builder.Services.AddScoped<IDeveloperService, DeveloperService>();
builder.Services.AddScoped<IEndUserService, EndUserService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IGroupingService, GroupingService>();
builder.Services.AddScoped<IFirebaseService, FirebaseService>();
builder.Services.AddSingleton(FirebaseAuth.DefaultInstance);
builder.Services.AddSingleton<IMqttService, MqttService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowDev");
}

app.MapConnectionHandler<MqttConnectionHandler>(
    "/mqtt",
    httpConnectionDispatcherOptions => httpConnectionDispatcherOptions.WebSockets.SubProtocolSelector =
        protocolList => protocolList.FirstOrDefault() ?? string.Empty);

var mqttService = app.Services.GetRequiredService<IMqttService>();
app.UseMqttServer(server =>
{
    server.ValidatingConnectionAsync += mqttService.ValidateConnection;
    server.ClientConnectedAsync += mqttService.OnClientConnected;
    server.InterceptingPublishAsync += mqttService.OnMessageReceived;
});

app.UseCors("AllowPortal");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<SensorHub>("/sensorHub");

app.Run();