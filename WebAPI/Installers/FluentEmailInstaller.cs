using Application.Interfaces;
using Application.Services.Emails;

namespace WebAPI.Installers
{
    public class FluentEmailInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddFluentEmail(configuration["FluentEmail:FromEmail"], configuration["FluentEmail:FromName"])
                .AddRazorRenderer()
                .AddSmtpSender(configuration["FluentEmail:SmtpSender:Host"], 
                     int.Parse(configuration["FluentEmail:SmtpSender:Port"]!),
                               configuration["FluentEmail:SmtpSender:Username"],
                               configuration["FluentEmail:SmtpSender:Password"]);

            services.AddScoped<IEmailSenderService, EmailSenderService>();
        }
    }
}
