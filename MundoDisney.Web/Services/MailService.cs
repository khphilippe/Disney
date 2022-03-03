using Microsoft.Extensions.Configuration;
using MundoDisney.Commonn.Request;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Web.Services
{
    public class MailService : IMailService
    {
        
        private readonly IConfiguration _configuration;


        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        
        public async Task SendMail(string email)
        {

            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("kevensherbyson@gmail.com", "kev");
            var subject = "Correo de bienvenida";
            var to = new EmailAddress(email);
            var plainTextContent = "gracias por registrarte!";
           var htmlContent = "<strong> Gracias por registrarte, bienvenido aL MundoDisney!</strong>" +
            "<img src=https://media-exp1.licdn.com/dms/image/C4E1BAQEDDjuh9HQchg/company-background_10000/0/1610631110628?e=2159024400&v=beta&t=00JMFny1Y6JiSd8rpPDIfJ_6vNH6NhtCK_yban1zy3c style= width:300px height: 300px> ";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response =   await client.SendEmailAsync(msg);
           if(response.IsSuccessStatusCode ==false)
            {
                return; 
            }
        }
        
    }
}
