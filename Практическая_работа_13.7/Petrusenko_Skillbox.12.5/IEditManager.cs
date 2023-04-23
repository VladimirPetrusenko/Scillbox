using System.Windows.Controls;

namespace Petrusenko_Skillbox._11._6
{
    interface IEditManager
    {
        void EditClientsList(DataGrid dataGrid);
        void AddClientsList(DataGrid dataGrid);
        void DeleteClientsList(DataGrid dataGrid);
    }
}
