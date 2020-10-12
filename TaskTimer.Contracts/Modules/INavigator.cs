using TaskTimer.Contracts.Db;

namespace TaskTimer.Contracts
{
    public interface INavigator
    {
        void ShowDialog(string title, string message);

        DbProjectDto EditProject(DbProjectDto project = null);

        DbClientDto NewClient(DbClientDto client = null);
    }
}
