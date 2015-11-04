using Org.DeployTools.Shared;

namespace Org.DeployTools.BacPackage
{
    class Program
    {
        static void Main(string[] args)
        {
            SafeMain.ParseArgumentsAndRunInTryCatch<Options>(args, Run);
        }

        private static void Run(Options options)
        {
        }
    }
}
