using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;
using TimeTable.Modules.TotalGrid.Views;

namespace TimeTable
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog.AddModule(new ModuleInfo
            {
                ModuleName = typeof(TimeTable.Modules.TotalGrid.TotalGridModule).Name,
                ModuleType = typeof(TimeTable.Modules.TotalGrid.TotalGridModule).AssemblyQualifiedName
            });
            ModuleCatalog.AddModule(new ModuleInfo
            {
                ModuleName = typeof(TimeTable.Modules.SelectGrid.SelectGridModule).Name,
                ModuleType = typeof(TimeTable.Modules.SelectGrid.SelectGridModule).AssemblyQualifiedName
            });
            ModuleCatalog.AddModule(new ModuleInfo
            {
                ModuleName = typeof(TimeTable.Modules.ShowTime.ShowTimeModule).Name,
                ModuleType = typeof(TimeTable.Modules.ShowTime.ShowTimeModule).AssemblyQualifiedName
            });

            ModuleCatalog.AddModule(new ModuleInfo
            {
                ModuleName = typeof(TimeTable.Modules.Menu.MenuModule).Name,
                ModuleType = typeof(TimeTable.Modules.Menu.MenuModule).AssemblyQualifiedName
            });
        }
    }
}
