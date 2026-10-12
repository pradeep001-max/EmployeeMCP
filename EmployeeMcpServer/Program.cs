using EmployeeMcpServer.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// Read appsettings.json from the folder next to the built program,
// no matter which directory VS Code starts the process from
var builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
{
    Args = args,
    ContentRootPath = AppContext.BaseDirectory
});

// stdout is the MCP protocol channel, so all logs must go to stderr
builder.Logging.ClearProviders();
builder.Logging.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Trace;
});

// Typed HttpClient for the Employee API
var baseUrl = builder.Configuration["EmployeeApi:BaseUrl"]
    ?? throw new InvalidOperationException("EmployeeApi:BaseUrl is missing in appsettings.json");

builder.Services.AddHttpClient<IEmployeeApiClient, EmployeeApiClient>(client =>
{
    client.BaseAddress = new Uri(baseUrl);
    client.Timeout = TimeSpan.FromSeconds(15);
});

// MCP server: stdio transport, tools discovered from this assembly
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();