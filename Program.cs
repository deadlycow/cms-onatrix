using Azure.Communication.Email;
using Onatrix.Interfaces;
using Onatrix.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(x => new EmailClient(builder.Configuration["ACS:ConnectionString"]));
builder.Services.AddTransient<IFormService, FormService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddComposers()
    .Build();

WebApplication app = builder.Build();

app.UseCors(options =>
{
  options.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader();
});


await app.BootUmbracoAsync();


app.UseUmbraco()
    .WithMiddleware(u =>
    {
      u.UseBackOffice();
      u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
      u.UseBackOfficeEndpoints();
      u.UseWebsiteEndpoints();
    });

await app.RunAsync();
