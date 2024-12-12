﻿// See https://aka.ms/new-console-template for more information
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SK.Core.Planners;
using SK.Core.Plugins;

Console.WriteLine("Let's talk!");

var builder = Kernel.CreateBuilder();

//builder.AddOpenAIChatCompletion(
//    modelId: "gpt-4o-mini",
//    apiKey: "yourApiKey"
//    );

builder.AddOpenAIChatCompletion(
    modelId: "mistral:latest",
    endpoint: new Uri("http://localhost:11434/v1/"),
    apiKey: "AlgoDebeIr",
    orgId: "AlgoDebeIr"
);

//SuperHero Plugin
var heroInfo = new HeroInfo("10d9fe4acd0db9670f88108fa42cb221");
var emailPlugin = new EmailPlugin();
builder.Plugins.AddFromObject(heroInfo, "HeroInfo");
builder.Plugins.AddFromType<EmailPlanner>();
//builder.Plugins.AddFromObject(emailPlugin);

var kernel = builder.Build();
kernel.ImportPluginFromObject(emailPlugin, "SendEmail");
//History
string SYSTEM_PROMPT = """
            Eres un asistente especializado en envio de emails. 
            Debes mantener un tono profesional pero amigable, y ayudar a los usuarios a:

            1. Enviar un mail:
               - Asunto
               - Cuerpo
               - Destinatarios (correos)

            Proceso para enviar un mail:
            1. Recopila toda la información necesaria:
               - Asunto (requerido)
               - Cuerpo (requerido)
               - Destinatarios (correos) (requerido)
            2. Pregunta al usuario si los datos son correctos
            3. Envia el mail con SendEmail
            4. Confirma el envío exitoso

            Si falta información:
            - Solicita los datos faltantes uno a uno
            - Guía al usuario con ejemplos si es necesario
            
            No envies emails si los datos están incompletos.
            Siempre valida con el usuario que los datos estén correctos antes de enviar el email.
            """;
var history = new ChatHistory(SYSTEM_PROMPT);
OpenAIPromptExecutionSettings executionSettings = new() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };
var chat = kernel.GetRequiredService<IChatCompletionService>();
//history.AddSystemMessage()
while (true)
{
    Console.Write("Q:");
    var input = Console.ReadLine();
    if (string.IsNullOrEmpty(input))
    {
        break;
    }
    history.AddUserMessage(input);
    //Sin Stream//
    var response = await chat.GetChatMessageContentsAsync(history, executionSettings, kernel);
    Console.WriteLine($"R: {response[^1]}");
    history.Add(response[^1]);
    //Con Stream//
    //var result = chat.GetStreamingChatMessageContentsAsync(history, executionSettings, kernel);
    //var sb = new StringBuilder();
    //await foreach (var item in result)
    //{
    //    sb.Append(item);
    //    Console.Write(item.Content);
    //}
    Console.WriteLine();
    //history.AddAssistantMessage(sb.ToString());
}