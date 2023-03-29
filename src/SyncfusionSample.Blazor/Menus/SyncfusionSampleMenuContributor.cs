using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SyncfusionSample.Localization;
using SyncfusionSample.MultiTenancy;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace SyncfusionSample.Blazor.Menus;

public class SyncfusionSampleMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public SyncfusionSampleMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<SyncfusionSampleResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                SyncfusionSampleMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home"
            )
        );
        context.Menu.AddItem(
                new ApplicationMenuItem(
                    "ManagementStore",
                    l["Menu:Asset Management"],
                    icon: "fa fa-tasks"
                ).AddItem(
                    new ApplicationMenuItem(
                        "ManagementStore.Products",
                        l["Product Management"],
                        url: "/products"
                    )
                ).AddItem(
                    new ApplicationMenuItem(
                        "ManagementStore.Projects",
                        l["Project Management"],
                        url: "/projects"
                    )
                ));
        context.Menu.AddItem(
                new ApplicationMenuItem(
                    "ManagementStore",
                    l["Menu:Charts"],
                    icon: "fa fa-chart-bar"
                ).AddItem(
                    new ApplicationMenuItem(
                        "Management.Products",
                        l["PieChart Statistics"],
                        url: "/pie"
                    )
                ).AddItem(
                    new ApplicationMenuItem(
                        "AssetManagement.Products",
                        l["Pyramid Statistics"],
                        url: "/pyramid"
                    )
                ).AddItem(
                    new ApplicationMenuItem(
                        "AssetManagement.Products",
                        l["Funnel Statistics"],
                        url: "/funnel"
                    )
                )); 
            

        var administration = context.Menu.GetAdministration();

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        return Task.CompletedTask;
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();

        var authServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem(
            "Account.Manage",
            accountStringLocalizer["MyAccount"],
            $"{authServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
            icon: "fa fa-cog",
            order: 1000,
            null).RequireAuthenticated());

        return Task.CompletedTask;
    }
}
