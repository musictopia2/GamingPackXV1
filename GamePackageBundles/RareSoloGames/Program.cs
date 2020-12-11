using GameLoaderBlazorLibrary;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Threading.Tasks;
namespace RareSoloGames
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.RegisterDefaultSinglePlayerProcesses<BasicViewModel>();
            await builder.Build().RunAsync();
        }
    }
}