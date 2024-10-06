using System.Windows;

namespace PhoneBookDesktopApplication
{
    /// <summary>
    /// Логика взаимодействия для AddPhoneBookEntry.xaml
    /// </summary>
    public partial class AddPhoneBookEntry : Window
    {
        public AddPhoneBookEntry()
        {
            InitializeComponent();
        }

        public AddPhoneBookEntry(PhoneBook phoneBook) : this()
        {
            cancelBtn.Click += delegate { this.DialogResult = false; };

            okBtn.Click += delegate
            {
                phoneBook.Surname = txtSurname.Text;
                phoneBook.Name = txtName.Text;
                phoneBook.Patronymic = txtPatronimyc.Text;
                phoneBook.NumberPhone = txtNumberPhone.Text;
                phoneBook.Address = txtAddress.Text;
                phoneBook.Description = txtDescription.Text;
                this.DialogResult = !false;
            };
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void okBtn_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
