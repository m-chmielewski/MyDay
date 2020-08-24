using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MyDay.Client.Singletons;

namespace MyDay.Client
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      builder.RootComponents.Add<App>("app");

      builder.Services.AddHttpClient("MyDay.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
          .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

      // Supply HttpClient instances that include access tokens when making requests to the server project
      builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("MyDay.ServerAPI"));

      builder.Services.AddApiAuthorization();

      builder.Services.AddScoped<TasksClient>();
      builder.Services.AddScoped<PresentationController>();
      //builder.Services.AddTransient<TimerService>();

      await builder.Build().RunAsync();
    }
  }
}
