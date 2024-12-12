// See https://aka.ms/new-console-template for more information
using Azure;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SK.Core.Plugins;
using System.Text;

Console.WriteLine("Let's talk!");

var builder = Kernel.CreateBuilder();

//builder.AddOpenAIChatCompletion(
//    modelId: "gpt-4o-mini",
//    apiKey: "yourApiKey"
//    );

builder.AddOpenAIChatCompletion(
    modelId: "llama3.2:3b",
    endpoint: new Uri("http://localhost:11434/v1/"),
    apiKey: "AlgoDebeIr",
    orgId: "AlgoDebeIr"
);

//SuperHero Plugin
var heroInfo = new HeroInfo("10d9fe4acd0db9670f88108fa42cb221");
builder.Plugins.AddFromObject(heroInfo, "HeroInfo");

var kernel = builder.Build();

//History
var history = new ChatHistory();
OpenAIPromptExecutionSettings executionSettings = new() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };
var chat = kernel.GetRequiredService<IChatCompletionService>();
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

