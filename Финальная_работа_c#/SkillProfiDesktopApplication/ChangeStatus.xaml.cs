using System.Windows;

namespace SkillProfiDesktopApplication
{
    /// <summary>
    /// Логика взаимодействия для ChangeStatus.xaml
    /// </summary>
    public partial class ChangeStatus : Window
    {
        private RequestServiceApi requestServiceApi;
        private Request request;

        public ChangeStatus(RequestServiceApi requestServiceApi, Request request)
        {
            InitializeComponent();
            this.request = request;
            txtId.Text = request.Id.ToString();
            txtGuestName.Text = request.GuestName;
            txtEmail.Text = request.Email;
            txtRequestText.Text = request.RequestText;
            StatusComboBox.SelectedIndex = DetermineStatus(request.Status);
            txtCreatedAt.Text = request.CreatedAt.ToString();

            this.requestServiceApi = requestServiceApi;
        }

        private int DetermineStatus(string status)
        {
            switch(status)
            {
                case "Received": return 0;
                case "InProgress": return 1;
                case "Completed": return 2;
                case "Declined": return 3;
                case "Canceled": return 4;
                default: return 0;
            }
        }

        private async void okBtn_Click(object sender, RoutedEventArgs e)
        {
            this.request.Status = StatusComboBox.Text;
            await requestServiceApi.ChangeRequestStatus(this.request);
            this.DialogResult = !false;
            //this.Close();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
