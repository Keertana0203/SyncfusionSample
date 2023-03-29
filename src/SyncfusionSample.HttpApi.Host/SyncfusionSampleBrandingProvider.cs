using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace SyncfusionSample;

[Dependency(ReplaceServices = true)]
public class SyncfusionSampleBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "SyncfusionSample";
}
