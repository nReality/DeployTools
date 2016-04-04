namespace Org.DeployTools.ServiceControl
{
    public static class ScActionExtention
    {
        internal static string GetServiceAction(this Options.ScAction action)
        {
            if (action == Options.ScAction.WaitStop)
                return Options.ScAction.Stop.GetServiceAction();
            return action.ToString().ToLower();
        }
    }
}
