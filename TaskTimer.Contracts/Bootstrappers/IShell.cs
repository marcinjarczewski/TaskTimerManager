
using System.Collections.Generic;
using System.Reflection;

namespace TaskTimer.Contracts.Bootstrappers
{
    public interface IShell
    {
        IList<ShellMenuItem> MenuItems { get; }
        IModule LoadModule(Assembly assembly);
    }
}
