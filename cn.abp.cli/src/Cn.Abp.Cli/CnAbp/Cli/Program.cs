using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Volo.Abp;
using Volo.Abp.Cli;
using Volo.Abp.Threading;

namespace CnAbp.Cli
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(Path.Combine(CliPaths.CliLogPath, "cn.abp-cli-logs.txt"))
                .WriteTo.Console()
                .CreateLogger();

            using (var application = AbpApplicationFactory.Create<CnAbpCliModule>(
                options =>
                {
                    options.UseAutofac();
                    options.Services.AddLogging(c => c.AddSerilog());
                }))
            {
                application.Initialize();

                AsyncHelper.RunSync(
                    () => application.ServiceProvider
                        .GetRequiredService<CliService>()
                        .RunAsync(args)
                );

                application.Shutdown();
            }
        }
    }
}