// See https://aka.ms/new-console-template for more information
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Text;

//Console.WriteLine("Hello, World!");
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
var kernel = builder.Build();

//History
var history = new ChatHistory();
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
    //var response = await chat.GetChatMessageContentsAsync(history);
    //Console.WriteLine($"R: {response[^1]}");
    //history.Add(response[^1]);
    //Con Stream//
    var result = chat.GetStreamingChatMessageContentsAsync(history);
    var sb = new StringBuilder();
    await foreach (var item in result)
    {
        sb.Append(item);
        Console.Write(item.Content);
    }
    Console.WriteLine();
    history.AddAssistantMessage(sb.ToString());
}