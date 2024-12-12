// See https://aka.ms/new-console-template for more information
using Microsoft.SemanticKernel;



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
//Console.WriteLine("Hello, World!");
var response = await kernel.InvokePromptAsync("Dame un chiste de microsoft");
Console.WriteLine(response);
