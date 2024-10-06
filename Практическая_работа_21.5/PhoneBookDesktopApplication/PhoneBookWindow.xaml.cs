using System.Collections.Generic;
using System.Windows;

namespace PhoneBookDesktopApplication
{
    /// <summary>
    /// Логика взаимодействия для PhoneBookWindow.xaml
    /// </summary>
    public partial class PhoneBookWindow : Window
    {
        public PhoneBookApi phoneBookApi;
        public delegate void HandleChange();
        public event HandleChange HandleChangeEvent;
        string role;

        public PhoneBookWindow(PhoneBookApi phoneBookApi, IEnumerable<PhoneBook> phoneBooks, string role)
        {
            InitializeComponent();
            this.phoneBookApi = phoneBookApi;
            viewPhoneBookEntries.ItemsSource = phoneBooks;
            this.role = role;
            InitMode(this.role);
        }

        public void InitMode(string mode)
        {
            if (mode == "view")
            {
                Add_Button.IsEnabled = false;
                Edit_Button.IsEnabled = false;
                Delete_Button.IsEnabled = false;
                Users_Button.IsEnabled = false;
            }
            if (mode == "user")
            {
                Add_Button.IsEnabled = true;
                Edit_Button.IsEnabled = false;
                Delete_Button.IsEnabled = false;
                Users_Button.IsEnabled = false;
            }
            if (mode == "admin")
            {
                Add_Button.IsEnabled = true;
                Edit_Button.IsEnabled = true;
                Delete_Button.IsEnabled = true;
                Users_Button.IsEnabled = true;
            }
        }

        private async void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            PhoneBook newPhoneBookEntry = new PhoneBook();
            AddPhoneBookEntry AddPhoneBookEntryWindow = new AddPhoneBookEntry(newPhoneBookEntry);
            AddPhoneBookEntryWindow.ShowDialog();

            if (AddPhoneBookEntryWindow.DialogResult.Value)
            {
                await phoneBookApi.AddPhoneBookEntry(newPhoneBookEntry);
            }
            HandleChangeEvent();
        }

        private async void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            PhoneBook phoneBookEntry = (PhoneBook)(viewPhoneBookEntries.SelectedItem);
            EditPhoneBookEntry EditPhoneBookEntryWindow = new EditPhoneBookEntry(phoneBookEntry);
            EditPhoneBookEntryWindow.ShowDialog();

            if (EditPhoneBookEntryWindow.DialogResult.Value)
            {
                await phoneBookApi.EditPhoneBookEntry(phoneBookEntry);
            }
            HandleChangeEvent();
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            PhoneBook phoneBookEntry = (PhoneBook)(viewPhoneBookEntries.SelectedItem);
            await phoneBookApi.DeletePhoneBookEntry(phoneBookEntry);
            HandleChangeEvent();
        }

        private void Users_Button_Click(object sender, RoutedEventArgs e)
        {
            UsersManager UsersManagerWindow = new UsersManager();
            UsersManagerWindow.ShowDialog();
        }
    }
}
