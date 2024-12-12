using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Text;

namespace SK.Core.Plugins
{
    public class EmailPlugin
    {
        public EmailPlugin()
        {
        }

        [KernelFunction("SendEmail")]
        [Description("Envia emails a destinatarios")]
        public async Task<string> SendEmailAsync(
                    [Description("Asunto del correo")] string subject,
                    [Description("Cuerpo del correo")] string body,
                    [Description("Destinatarios del correo separados por \",\"")] string to)
        {
            try
            {
                var str = new StringBuilder();
                str.AppendLine($"Destinatario/s: {to}");
                str.AppendLine($"Asunto: {subject}");
                str.AppendLine($"Cuerpo: {body}");

                Console.WriteLine(str.ToString());
                return "Correo enviado!";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar el email: {ex.Message}") ;
                return "Error al enviar correo!";
            }
        }
    }
}
