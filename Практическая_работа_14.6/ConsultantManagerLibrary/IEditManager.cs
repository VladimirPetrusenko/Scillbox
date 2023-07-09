using System.Windows.Controls;

namespace ConsultantManagerLibrary
{
    public interface IEditManager
    {
        void EditClientsList(DataGrid dataGrid);
        void AddClientsList(DataGrid dataGrid);
        void DeleteClientsList(DataGrid dataGrid);
    }
}
