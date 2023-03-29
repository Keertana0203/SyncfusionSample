using SyncfusionSample.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace SyncfusionSample.Permissions;

public class SyncfusionSamplePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //var myGroup = context.AddGroup(SyncfusionSamplePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(SyncfusionSamplePermissions.MyPermission1, L("Permission:MyPermission1"));
        var productGroup = context.AddGroup(SyncfusionSamplePermissions.GroupName, L("Permission:ProductManagement"));

        var productsPermission = productGroup.AddPermission(SyncfusionSamplePermissions.Products.Default, L("Permission:Products"));
        productsPermission.AddChild(SyncfusionSamplePermissions.Products.Create, L("Permission:Products.Create"));
        productsPermission.AddChild(SyncfusionSamplePermissions.Products.Edit, L("Permission:Products.Edit"));
        productsPermission.AddChild(SyncfusionSamplePermissions.Products.Delete, L("Permission:Products.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SyncfusionSampleResource>(name);
    }
}
