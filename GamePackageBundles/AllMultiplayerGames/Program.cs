using GameLoaderBlazorLibrary;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MultiplayerGamesBlazorLoaderLibrary;
using System.Threading.Tasks;
namespace AllMultiplayerGames
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.RegisterDefaultMultiplayerProcesses<BasicViewModel>();
            await builder.Build().RunAsync();
        }
    }
}