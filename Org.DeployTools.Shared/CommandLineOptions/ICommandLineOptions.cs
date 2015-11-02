namespace Org.DeployTools.Shared.CommandLineOptions
{
    public interface ICommandLineOptions
    {
        void GuardArgumentsValid();
        void Setup();
    }
}
