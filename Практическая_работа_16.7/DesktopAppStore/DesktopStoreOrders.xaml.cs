using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace DesktopAppStore
{
    /// <summary>
    /// Логика взаимодействия для DesktopStoreOrders.xaml
    /// </summary>
    public partial class DesktopStoreOrders : Window
    {
        public DesktopStoreOrders(string selectClients)
        {
            InitializeComponent();
            this.selectClients = selectClients;
            Preparing(selectClients);
        }

        public string selectClients = "";
        public string s = "";

        SqlConnection con;
        SqlDataAdapter da;
        DataTable dt;
        DataRowView row;

        private void Preparing(string selectClients)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = @"DESKTOP-26BRAK1\SQLEXPRESS",
                InitialCatalog = "DBOrders",
                IntegratedSecurity = false,
                Pooling = true,
                UserID = "ConnectNameStore",
                Password = "store1236547890"
            };

            dt = new DataTable();
            da = new SqlDataAdapter();
            con = new SqlConnection(connectionStringBuilder.ConnectionString);
            string data = "Orders";
            var sql = "";

            //Выборка заказов по клиенту
            if (selectClients == "full")
            {
                sql = $@"SELECT * FROM {data} Order By {data}.ID";
                da.SelectCommand = new SqlCommand(sql, con);
                da.Fill(dt);
                gridView.DataContext = dt.DefaultView;
                //t = false;
            }
            else
            {
                string yy = $"{selectClients}";
                string yyy = $"@{yy}";
                sql = $@"SELECT * FROM {data} WHERE {data}.Email='{selectClients}' Order By {data}.ID";
                da.SelectCommand = new SqlCommand(sql, con);
                da.Fill(dt);
                gridView.DataContext = dt.DefaultView;
            }

            //Добавление клиента в таблицу БД
            sql = @"INSERT INTO Orders (Email, Код_товара, Наименование_товара) VALUES (@Email, @Код_товара, @Наименование_товара)
                                SET @ID = @@IDENTITY;";
            da.InsertCommand = new SqlCommand(sql, con);

            da.InsertCommand.Parameters.Add("@ID", SqlDbType.Int, 4, "ID").Direction = ParameterDirection.Output;
            da.InsertCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 20, "Email");
            da.InsertCommand.Parameters.Add("@Код_товара", SqlDbType.Int, 4, "Код_товара");
            da.InsertCommand.Parameters.Add("@Наименование_товара", SqlDbType.NVarChar, 20, "Наименование_товара");

            //Удаление записи по заказу в таблице БД
            sql = @"DELETE FROM Orders WHERE ID = @ID;";

            da.DeleteCommand = new SqlCommand(sql, con);
            da.DeleteCommand.Parameters.Add("@ID", SqlDbType.Int, 4, "ID");
        }

        //Начало редактирование записи
        private void GridViewCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
           row = (DataRowView)gridView.SelectedItem;
           row.BeginEdit();
           da.Update(dt);
        }

        //Окончание редактирование записи
        private void GridViewCurrentCellChanged(object sender, EventArgs e)
        {
           if (row == null) return;
           row.EndEdit();
           da.Update(dt);
        }

        //Пункт контекстного меню "Удалить"
        private void MenuItemDeleteClick(object sender, RoutedEventArgs e)
        {
           row = (DataRowView)gridView.SelectedItem;
           row.Row.Delete();
           da.Update(dt);
        }

        //Пункт контекстного меню "Добавить"
        private void MenuItemAddClick(object sender, RoutedEventArgs e)
        {
           if (selectClients == "full")
           {
               DataRow r = dt.NewRow();
               OrderAddWindow add = new OrderAddWindow(r);
               add.ShowDialog();

               if(add.DialogResult.Value)
               {
                   dt.Rows.Add(r);
                   da.Update(dt);
               }
           }
           else
           {
               MessageBox.Show("В режиме выборки по клиенту добавление нового заказа невозможно");
           }
        }
    }
}
