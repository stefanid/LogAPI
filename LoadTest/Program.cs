// See https://aka.ms/new-console-template for more information

using System.Text;
using Log_API;
using Log_API.Entities;
using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Plugins.Network.Ping;
using Newtonsoft.Json;

using var httpClient = new HttpClient();
Console.WriteLine("Specify amount of copies to be ran for performance test");
var copies = Console.ReadLine();
var copiesToInt = int.Parse(copies);

LogEntry entry = new LogEntry()
{
    ApplicationId = 1,
    Message = "Fatal error",
    Severity = Severity.High,
    TimeStamp = DateTimeOffset.Now.ToLocalTime(),
    TraceId = 1
};

var step = Step.Create("insert_logentries", async context =>
{
    try
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(entry), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("https://localhost:7259/FlatFile/log", stringContent);
        return response.IsSuccessStatusCode
            ? Response.Ok()
            : Response.Fail();
    }
    catch (Exception)
    {
        return Response.Fail();
    }
});


var scenario = ScenarioBuilder.CreateScenario("insert_logentries", step)
    .WithWarmUpDuration(TimeSpan.FromSeconds(1))
    .WithLoadSimulations(Simulation.KeepConstant(copies: copiesToInt, TimeSpan.FromSeconds(30)));

var pingPluginConfig = PingPluginConfig.CreateDefault(new[] { "nbomber.com" });
var pingPlugin = new PingPlugin(pingPluginConfig);

NBomberRunner
    .RegisterScenarios(scenario)
    .WithWorkerPlugins(pingPlugin)
    .Run();

Console.ReadKey();