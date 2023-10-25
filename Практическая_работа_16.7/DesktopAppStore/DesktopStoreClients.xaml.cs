using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;


namespace DesktopAppStore
{
    /// <summary>
    /// Логика взаимодействия для DesktopStoreClients.xaml
    /// </summary>
    public partial class DesktopStoreClients : Window
    {
        public DesktopStoreClients()
        {
            InitializeComponent();
            Preparing();
        }

        SqlConnection con;
        SqlDataAdapter da;
        DataTable dt;
        DataRowView row;

        private void Preparing()
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = @"DESKTOP-26BRAK1\SQLEXPRESS",
                InitialCatalog = "DBClients",
                IntegratedSecurity = false,
                Pooling = true,
                UserID = "ConnectNameStore",
                Password = "store1236547890"
            };

            dt = new DataTable();
            da = new SqlDataAdapter();
            con = new SqlConnection(connectionStringBuilder.ConnectionString);
            string data = "Clients";

            //Выборка из таблицы БД
            var sql = $@"SELECT * FROM {data} Order By {data}.ID";
            da.SelectCommand = new SqlCommand(sql, con);

            //Добавление клиента в таблицу БД
            sql = @"INSERT INTO Clients (Фамилия, Имя, Отчество, Номер_телефона, Email) VALUES (@Фамилия, @Имя, @Отчество, @Номер_телефона, @Email)
                                SET @ID = @@IDENTITY;";
            da.InsertCommand = new SqlCommand(sql, con);

            da.InsertCommand.Parameters.Add("@ID", SqlDbType.Int, 4, "ID").Direction = ParameterDirection.Output;
            da.InsertCommand.Parameters.Add("@Фамилия", SqlDbType.NVarChar, 20, "Фамилия");
            da.InsertCommand.Parameters.Add("@Имя", SqlDbType.NVarChar, 20, "Имя");
            da.InsertCommand.Parameters.Add("@Отчество", SqlDbType.NVarChar, 20, "Отчество");
            da.InsertCommand.Parameters.Add("@Номер_телефона", SqlDbType.NVarChar, 20, "Номер_телефона");
            da.InsertCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 20, "Email");

            //Обновление записи по клиенту в таблице БД
            sql = @"UPDATE Clients SET Фамилия = @Фамилия, Имя = @Имя, Отчество = @Отчество, Номер_телефона = @Номер_телефона, Email = @Email Where ID = @ID;";

            da.UpdateCommand = new SqlCommand(sql, con);

            da.UpdateCommand.Parameters.Add("@ID", SqlDbType.Int, 4, "ID").SourceVersion = DataRowVersion.Original;
            da.UpdateCommand.Parameters.Add("@Фамилия", SqlDbType.NVarChar, 20, "Фамилия");
            da.UpdateCommand.Parameters.Add("@Имя", SqlDbType.NVarChar, 20, "Имя");
            da.UpdateCommand.Parameters.Add("@Отчество", SqlDbType.NVarChar, 20, "Отчество");
            da.UpdateCommand.Parameters.Add("@Номер_телефона", SqlDbType.NVarChar, 20, "Номер_телефона");
            da.UpdateCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 20, "Email");

            //Удаление записи по клиенту в таблице БД

            sql = @"DELETE FROM Clients WHERE ID = @ID;";
            
            da.DeleteCommand = new SqlCommand(sql, con);
            da.DeleteCommand.Parameters.Add("@ID", SqlDbType.Int, 4, "ID");
            
            da.Fill(dt);
            gridView.DataContext = dt.DefaultView;
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
            DataRow r = dt.NewRow();
            MessageBox.Show(r[4].ToString());
            ClientAddWindow add = new ClientAddWindow(r);
            add.ShowDialog();

            if(add.DialogResult.Value)
            {
                dt.Rows.Add(r);
                da.Update(dt);
            }
        }
        
        //Выборка заказов по клиенту
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            row = (DataRowView)gridView.SelectedItem;
            MessageBox.Show(row[5].ToString());
            if (row != null)
            {
                DesktopStoreOrders DesktopStoreOrdersWindow = new DesktopStoreOrders(row[5].ToString());
                DesktopStoreOrdersWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите клиента, по которому нужно получить выборку заказов");
            }
        }
    }
}
