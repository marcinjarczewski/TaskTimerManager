using TaskTimer.Contracts.Bootstrappers;

namespace TaskTimer.Contracts
{
    public class ShellMenuItem
    {
        public string Caption { get; set; }
        public IScreenViewModel ScreenViewModel { get; set; }
    }
}
