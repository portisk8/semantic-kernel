// See https://aka.ms/new-console-template for more information
using Microsoft.SemanticKernel;

Console.WriteLine("Let's talk!");

var builder = Kernel.CreateBuilder();


builder.AddOpenAIChatCompletion(/*<...service configuration>*/, serviceId: "service-1");
builder.AddAzureOpenAIChatCompletion(/*<...service configuration>*/, serviceId: "service-2");

Kernel kernel = builder.Build();

ChatCompletionAgent agent =
	new()
	{
		Name = "<agent name>",
		Instructions = "<agent instructions>",
		Kernel = kernel,
		Arguments = // Specify the service-identifier via the KernelArguments
		  new KernelArguments(
			new OpenAIPromptExecutionSettings()
			{
				ServiceId = "service-2" // The target service-identifier.
			});
	};