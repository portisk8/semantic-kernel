using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.Core.Planners
{
    public class EmailPlanner
    {
        [KernelFunction]
        [Description("Returns back the required steps necessary to author an email")]
        [return: Description("The list of steps needded to author an email.")]
        public async Task<string> PasosGenerarAsync(
            Kernel kernel,
            [Description("2 a 3 oraciones acerca de lo que trata tu email")] string topic,
            [Description("correos de los destinatarios")] string recipients
            )
        {
            var result = await kernel.InvokePromptAsync($""""
                I'm going to write an email about {topic} to {recipients}.
                Before I start, can you succinctly recommend the main points I should cover in a numbered list.
                I want to make sure I don't forget anything that would help my user's email sound more professional.
                """",
                new()
                {
                    {"topic", topic },
                    {"recipients", recipients}
                });
            return result.ToString();
        }

    }
}
