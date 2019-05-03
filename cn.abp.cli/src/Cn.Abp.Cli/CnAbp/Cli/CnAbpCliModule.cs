using Volo.Abp.Autofac;
using Volo.Abp.Cli;
using Volo.Abp.Modularity;

namespace CnAbp.Cli
{
    [DependsOn(
        typeof(AbpCliCoreModule),
        typeof(AbpAutofacModule)
    )]
    public class CnAbpCliModule : AbpModule
    {

    }
}