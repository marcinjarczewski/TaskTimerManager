using TaskTimer.Contracts.Db;

namespace TaskTimer.Contracts
{
    public interface INavigator
    {
        void ShowDialog(string title, string message);

        DbClientDto NewClient(DbClientDto client = null);

        DbTaskDto NewTimer(DbTaskDto timer, bool showInvoices);

        bool ShowDialog(bool isConfirm, string title, string message);
    }
}
