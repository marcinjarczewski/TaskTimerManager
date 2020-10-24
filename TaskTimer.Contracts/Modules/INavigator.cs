using TaskTimer.Contracts.Db;

namespace TaskTimer.Contracts
{
    public interface INavigator
    {
        void ShowDialog(string title, string message);

        DbProjectDto EditProject(DbProjectDto project = null);

        DbClientDto NewClient(DbClientDto client = null);

        DbTaskDto NewTimer(DbTaskDto client = null);


        bool ShowDialog(bool isConfirm, string title, string message);
    }
}
