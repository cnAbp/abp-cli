using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Cli;
using Volo.Abp.DependencyInjection;
using Volo.Abp.IO;
using Volo.Abp.ProjectBuilding;

namespace CnAbp.Cli
{
    [Dependency(ReplaceServices = true)]
    public class CnAbpTemplateStore : ITemplateStore, ITransientDependency
    {
        public CnAbpTemplateStore()
        {
            Logger = NullLogger<AbpIoTemplateStore>.Instance;
        }

        public ILogger<AbpIoTemplateStore> Logger { get; set; }

        public async Task<TemplateFile> GetAsync(string templateName, string version)
        {
            var localCacheFolder = Path.Combine(CliPaths.TemplateCachePath, version);
            DirectoryHelper.CreateIfNotExists(localCacheFolder);

            var localCacheFile = Path.Combine(localCacheFolder, templateName + ".zip");
            if (File.Exists(localCacheFile))
            {
                Logger.LogInformation("Using cached template: " + templateName + ", version: " + version);
                return new TemplateFile(File.ReadAllBytes(localCacheFile));
            }

            Logger.LogInformation("Downloading template: " + templateName + ", version: " + version);

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(5);
                var downloadUrl = "https://cn.abp.io/downloads/templates/" + version + "/" + templateName + ".zip";
                var fileContents = await client.GetByteArrayAsync(downloadUrl);
                File.WriteAllBytes(localCacheFile, fileContents);
                return new TemplateFile(fileContents);
            }
        }
    }
}