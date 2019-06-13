using Ninject;
using Ninject.Extensions.Conventions;

namespace FlashcardsClient
{
    public static class Program
    {
        public static void Main()
        {
            var container = new StandardKernel();
            container.Bind(configure => configure
                .FromThisAssembly()
                .SelectAllClasses()
                .BindAllBaseClasses());
            var ui = container.Get<ConsoleUi>();

            ui.Run();
        }
    }
}