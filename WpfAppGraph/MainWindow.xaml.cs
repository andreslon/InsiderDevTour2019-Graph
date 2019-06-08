using Microsoft.Graph;
using MyGraph;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string ClientId { get; set; } = "{CLIENT_ID_2}";
        // TODO: Connect to Graph
        private Connector connector { get; set; }

        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register(nameof(UserName), typeof(string), typeof(MainWindow), new PropertyMetadata("Not Logged In"));

        public ObservableCollection<Event> CalendarEvents
        {
            get { return (ObservableCollection<Event>)GetValue(CalendarEventsProperty); }
            set { SetValue(CalendarEventsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CalendarEvents.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CalendarEventsProperty =
            DependencyProperty.Register(nameof(CalendarEvents), typeof(ObservableCollection<Event>), typeof(MainWindow), new PropertyMetadata(new ObservableCollection<Event>()));

        public MainWindow()
        {
            InitializeComponent();
            connector = new Connector(ClientId);
            DataContext = this;

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO: Initialize UI Component's Data From Graph
            UserName = await connector.GetUserNameAsync();


            await LoadEvents();
            
        }

        private async Task LoadEvents()
        {
            CalendarEvents.Clear();
            var events = await connector.GetCalendarEventsAsync();
            foreach (var ev in events)
            {
                CalendarEvents.Add(ev);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dasdsa = connector.AddEventAsync("Remate Insider Dev Tour");
            await LoadEvents();
        }
    }
}
