using System.Windows;


namespace PhoneBookDesktopApplication
{
    /// <summary>
    /// Логика взаимодействия для EditPhoneBookEntry.xaml
    /// </summary>
    public partial class EditPhoneBookEntry : Window
    {
        public EditPhoneBookEntry()
        {
            InitializeComponent();
        }

        public EditPhoneBookEntry(PhoneBook phoneBook) : this()
        {
            txtSurname.Text = phoneBook.Surname;
            txtName.Text = phoneBook.Name;
            txtPatronimyc.Text = phoneBook.Patronymic;
            txtNumberPhone.Text = phoneBook.NumberPhone;
            txtAddress.Text = phoneBook.Address;
            txtDescription.Text = phoneBook.Description;

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
    }
}
