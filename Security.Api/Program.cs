using Autofac;
using Autofac.Extensions.DependencyInjection;
using Security.Api.Infrastructure.AutofacModules;
using Security.Api.Infrastructure.Delegates;
using Security.Api.Infrastructure.Extensions;
using Security.Api.Middlewares;
using Security.Application;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    //    .AddCors()
    //.AddCustomMvc()
    .AddCustomAuthentication(builder.Configuration)
    .AddSwaggerDoc(builder.Configuration);

    builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
    builder.Services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    //builder.Services.AddHttpClient<IExternalService, ExternalService>(s => s.BaseAddress = new Uri(builder.Configuration["MsExternalService"])).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

    builder.Services.AddControllers();
};

// Configure the HTTP request pipeline.

builder.Services.AddApplicationLayer();

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(b => b.RegisterModule(new ApplicationModule(builder.Configuration["ConnectionSting"], builder.Configuration["Jwt:Key"], builder.Configuration["Jwt:DurationInMinutes"], builder.Configuration["Jwt:Issuer"], builder.Configuration["TimeZone"], builder.Configuration["Jwt:Audience"])));
builder.Host.ConfigureContainer<ContainerBuilder>(b => b.RegisterModule(new MediatorModule()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerDoc(builder.Configuration);
}

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

app.Run();

