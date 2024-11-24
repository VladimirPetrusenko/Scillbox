using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace SkillProfiDesktopApplication
{
    /// <summary>
    /// Логика взаимодействия для Requests.xaml
    /// </summary>
    public partial class Requests : Window
    {
        public RequestServiceApi requestServiceApi;
        public RequestFilter requestFilter;
        private ObservableCollection<Request> _requests;
        private List<Request> requestsFromService;

        private DateTime zeroDate = new DateTime(0001, 1, 1, 0, 0, 0);

        public Requests(RequestServiceApi requestServiceApi, IEnumerable<Request> requestsFromService)
        {
            InitializeComponent();
            StatusComboBox.SelectedIndex = 0;
            this.requestServiceApi = requestServiceApi;
            this.requestsFromService = (List<Request>)requestsFromService;

            _requests = new ObservableCollection<Request>();
            requestFilter = new RequestFilter();

            CreateRequestsCollection();

            viewRequests.ItemsSource = _Requests;
        }

        public ObservableCollection<Request> _Requests
        {
            get { return _requests; }
            set
            {
                _requests = value;
                OnPropertyChanged(nameof(_Requests));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            Request selectedRequest = (Request)(viewRequests.SelectedItem);
            ChangeStatus changeStatusWindow = new ChangeStatus(requestServiceApi, selectedRequest);
            changeStatusWindow.ShowDialog();
            if (changeStatusWindow.DialogResult.Value)
            {
                MessageBox.Show("Статус изменен");
                requestsFromService = (List<Request>)(await requestServiceApi.GetRequests(requestFilter));
                
                CreateRequestsCollection();
            }
        }

        private async void StartDate_Button_Click(object sender, RoutedEventArgs e)
        {
            string StartDatePickerContent = Start_DatePicker.Text;
            string EndDatePickerContent = End_DatePicker.Text;
            string statusComboBoxContent = StatusComboBox.Text;

            DateTime startDateTime;
            DateTime endDateTime;

            if (StartDatePickerContent == "")
            {
                startDateTime = zeroDate;
            }
            else
            {
                startDateTime = Convert.ToDateTime(StartDatePickerContent);
                MessageBox.Show(startDateTime.ToString());
            }

            if (EndDatePickerContent == "")
            {
                endDateTime = zeroDate;
            }
            else
            {
                endDateTime = Convert.ToDateTime(EndDatePickerContent);
                MessageBox.Show(endDateTime.ToString());
            }

            requestFilter.RequestStatus = statusComboBoxContent;

            if (statusComboBoxContent != "Все")
            {
                requestFilter.RequestStatus = statusComboBoxContent;
            }
            else
            {
                requestFilter.RequestStatus = null;
            }

            requestFilter.StartDate = startDateTime;
            requestFilter.EndDate = endDateTime;

            if (requestFilter.StartDate == zeroDate && requestFilter.EndDate == zeroDate && statusComboBoxContent == "Все")
            {
                requestsFromService = (List<Request>)(await requestServiceApi.GetAllRequests());

                CreateRequestsCollection();
            }
            else
            {
                requestsFromService = (List<Request>)(await requestServiceApi.GetRequests(requestFilter));

                MessageBox.Show(requestsFromService.Count().ToString());

                CreateRequestsCollection();
            }
        }

        private async void Day_Button_Click(object sender, RoutedEventArgs e)
        {
            requestFilter.StartDate = DateTime.Today;
            requestFilter.EndDate = DateTime.Today.AddDays(1).AddTicks(-1); // до конца дня
            requestFilter.RequestStatus = null;

            requestsFromService = (List<Request>)(await requestServiceApi.GetRequests(requestFilter));

            MessageBox.Show(requestsFromService.Count().ToString());

            CreateRequestsCollection();
        }

        private async void Yesterday_Button_Click(object sender, RoutedEventArgs e)
        {
            requestFilter.StartDate = DateTime.Today.AddDays(-1);
            requestFilter.EndDate = DateTime.Today.AddTicks(-1); // до конца дня
            requestFilter.RequestStatus = null;

            requestsFromService = (List<Request>)(await requestServiceApi.GetRequests(requestFilter));

            MessageBox.Show(requestsFromService.Count().ToString());

            CreateRequestsCollection();
        }

        private async void Week_Button_Click(object sender, RoutedEventArgs e)
        {
            requestFilter.StartDate = DateTime.Today.AddDays(-7);
            requestFilter.EndDate = DateTime.Today; // до конца дня
            requestFilter.RequestStatus = null;

            requestsFromService = (List<Request>)(await requestServiceApi.GetRequests(requestFilter));

            MessageBox.Show(requestsFromService.Count().ToString());

            CreateRequestsCollection();
        }

        private async void Month_Button_Click(object sender, RoutedEventArgs e)
        {
            requestFilter.StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            requestFilter.EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month,
                             DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)).AddTicks(-1); // до конца дня
            requestFilter.RequestStatus = null;

            requestsFromService = (List<Request>)(await requestServiceApi.GetRequests(requestFilter));

            MessageBox.Show(requestsFromService.Count().ToString());

            CreateRequestsCollection();
        }

        public void CreateRequestsCollection()
        {
            _requests.Clear();

            for (int i = 0; i < this.requestsFromService.Count(); i++)
            {
                _requests.Add(new Request(this.requestsFromService[i].Id, this.requestsFromService[i].GuestName,
                    this.requestsFromService[i].Email, this.requestsFromService[i].RequestText,
                    this.requestsFromService[i].Status, this.requestsFromService[i].CreatedAt));
            }
        }
    }
}
