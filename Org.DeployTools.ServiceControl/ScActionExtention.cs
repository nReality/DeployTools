namespace Org.DeployTools.ServiceControl
{
    public static class ScActionExtention
    {
        internal static string GetServiceAction(this Options.ScAction action)
        {
            return action.ToString().ToLower();
        }
    }
}
