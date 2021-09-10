using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ClinicaVeterinaria.Helpers
{
    public class SMSHelper : ISMSHelper
    {
        public Response SendSMS(string to, string body)
        {
            // Processo que faz com que a mensagem seja enviada para o numero selecionado
            string accountSid = "AC8fba6feefb41ec8d969e69d11b1d3bb8";
            string authToken = "22c407ebe8adfb185d599a4f06b354d0";
            //string accountSid = Environment.GetEnvironmentVariable("AC8fba6feefb41ec8d969e69d11b1d3bb8");
            //string authToken = Environment.GetEnvironmentVariable("22c407ebe8adfb185d599a4f06b354d0");

            // Initialize the Twilio client
            TwilioClient.Init(accountSid, authToken);

            try
            {
                var message = MessageResource.Create(
                body: body,
                from: new Twilio.Types.PhoneNumber("+12282850475"),
                to: new Twilio.Types.PhoneNumber(to));
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.ToString()
                };
            }

            return new Response
            {
                IsSuccess = true
            };
        }
    }
}
