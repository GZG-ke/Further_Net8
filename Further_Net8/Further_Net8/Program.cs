using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Further_Net8.Extensions;
using Further_Net8.Filter;
using Further_Net8_Common;
using Further_Net8_Common.Core;
using Further_Net8_Common.HttpContextUser;
using Further_Net8_Common.Hubs;
using Further_Net8_Common.Serilog.Utility;
using Further_Net8_Extensions.ServiceExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Further_Net8_Common.Helper;
using Further_Net8_Extensions.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule<AutofacModuleRegister>();
        builder.RegisterModule<AutofacPropertityModuleReg>();
    })
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        hostingContext.Configuration.ConfigureApplication();
    });
builder.Services.AddSignalR().AddNewtonsoftJsonProtocol();
builder.ConfigureApplication();
builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

builder.Services.AddControllers(o =>
{
    o.Filters.Add(typeof(GlobalExceptionsFilter));
    //o.Conventions.Insert(0, new GlobalRouteAuthorizeConvention());
    o.Conventions.Insert(0, new GlobalRoutePrefixFilter(new RouteAttribute(RoutePrefix.Name)));
})
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
        //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
        //将long类型转为string
        options.SerializerSettings.Converters.Add(new NumberConverter(NumberConverterShip.Int64));
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
AutoMapperConfig.RegisterMappings();

// 配置
builder.Services.AddSingleton(new AppSettings(builder.Configuration));
builder.Services.AddAllOptionRegister();

// 缓存
builder.Services.AddCacheSetup();

// ORM
builder.Services.AddSqlsugarSetup();

builder.Services.AddDbSetup();
//注册服务
builder.Services.AddInitializationHostServiceSetup();

builder.Host.AddSerilogSetup();
// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "Blog.Core",
            ValidAudience = "wr",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sdfsdfsrty45634kkhllghtdgdfss345t678fs"))
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Client", policy => policy.RequireClaim("iss", "Blog.Core").Build());
    options.AddPolicy("SuperAdmin", policy => policy.RequireRole("SuperAdmin").Build());
    options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("SuperAdmin", "System"));

    options.AddPolicy("Permission", policy => policy.Requirements.Add(new PermissionRequirement()));
});
builder.Services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();
builder.Services.AddSingleton(new PermissionRequirement());

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IAspNetUser, AspNetUser>();

var app = builder.Build();
app.ConfigureApplication();
app.UseApplicationSetup();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
}
app.UseExceptionHandlerMiddle();
app.UseRequestResponseLogMiddle();
app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = SerilogRequestUtility.HttpMessageTemplate;
    options.GetLevel = SerilogRequestUtility.GetRequestLevel;
    //options.EnrichDiagnosticContext = SerilogRequestUtility.EnrichFromRequest;
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/api/chatHub");

app.Run();