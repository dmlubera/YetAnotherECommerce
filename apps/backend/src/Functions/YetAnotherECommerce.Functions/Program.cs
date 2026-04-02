using Azure.Communication.Email;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YetAnotherECommerce.Functions.Builders;
using YetAnotherECommerce.Functions.Services;
using YetAnotherECommerce.Functions.Settings;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddOptions<EmailNotificationsSettings>()
    .Configure<IConfiguration>((settings, configuration) =>
        configuration.GetSection("EmailNotificationsSettings").Bind(settings));

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services
    .AddSingleton<IEmailMessageBuilder, CompleteRegistrationEmailMessageBuilder>()
    .AddSingleton<IEmailMessageBuilderFactory, EmailMessageBuilderFactory>();

if (builder.Configuration["EmailProvider"] == "SMTP")
{
    builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();
}
else
{
    builder.Services
        .AddSingleton<IEmailSender, AzureCommunicationServicesSender>()
        .AddSingleton(new EmailClient(builder.Configuration["CommunicationServicesConnectionString"]));
}

builder.Build().Run();