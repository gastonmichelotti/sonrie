using netCoreNew.Enum;
using netCoreNew.ViewModels;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace netCoreNew.Services
{
    public class NotificadorService
    {
        //public static bool SendEmail(SendEmailVM model)
        //{
        //    try
        //    {
        //        var client = new SmtpClient("smtp.zoho.com");

        //        client.UseDefaultCredentials = false;
        //        client.Port = 587;
        //        client.EnableSsl = true;
        //        client.DeliveryFormat = SmtpDeliveryFormat.International;
        //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        client.Credentials = new NetworkCredential(Valores.Email, "Kapica1987$");

        //        var mailMessage = new MailMessage();

        //        mailMessage.From = new MailAddress(Valores.Email, Enum.Valores.Nombre);

        //        mailMessage.ReplyToList.Add(Valores.Email);

        //        foreach (var item in model.To)
        //        {
        //            mailMessage.To.Add(item);
        //        }

        //        mailMessage.Subject = model.Motivo;

        //        var builder = new StringBuilder();

        //        var uploads = "./wwwroot/Templates/Emails";
        //        var filePath = Path.Combine(uploads, "Aviso.html");

        //        using (var reader = File.OpenText(filePath))
        //        {
        //            builder.Append(reader.ReadToEnd());
        //        }

        //        builder = Convertir(builder, model);

        //        mailMessage.Body = builder.ToString();

        //        mailMessage.IsBodyHtml = true;

        //        client.Send(mailMessage);

        //        return true;
        //    }
        //    catch (Exception)
        //    {

        //    }

        //    return false;
        //}

        //public static StringBuilder Convertir(StringBuilder builder, SendEmailVM model)
        //{
        //    builder.Replace("{{email-value}}", Valores.Email);
        //    builder.Replace("{{telefono-value}}", Valores.Telefono);

        //    builder.Replace("{{titulo-value}}", model.Motivo);
        //    builder.Replace("{{texto-value}}", model.Texto);

        //    builder.Replace("{{boton1-value}}", model.TextoBoton);
        //    builder.Replace("{{link1-value}}", model.Link);

        //    builder.Replace("{{logo-value}}", $"{Enum.Valores.Url}/img/logo.png");
        //    builder.Replace("{{show-logo-value}}", "block");

        //    builder.Replace("{{show-button-value}}", model.Boton ? "block" : "none");

        //    return builder;
        //}
    }
}