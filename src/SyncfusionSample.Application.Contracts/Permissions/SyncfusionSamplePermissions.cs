namespace SyncfusionSample.Permissions;

public static class SyncfusionSamplePermissions
{
    public const string GroupName = "SyncfusionSample";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public static class Products
    {
        public const string Default = GroupName + ".Products";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
