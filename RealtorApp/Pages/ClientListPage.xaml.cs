using System;
using System.Collections.Generic;
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
using RealtorApp.Model;

namespace RealtorApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientListPage.xaml
    /// </summary>
    public partial class ClientListPage : Page
    {
        public ClientListPage()
        {
            InitializeComponent();
            LVClients.ItemsSource = App.DB.Client.Where(x => x.IsDelete == false).ToList();
        }

        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientAddPage(new Client() { IsDelete = false }));
        }

        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedClient = LVClients.SelectedItem as Client;
            if (selectedClient == null)
            {
                MessageBox.Show("Выберите клиента", "Подсказка", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            NavigationService.Navigate(new ClientAddPage(selectedClient));
        }

        private void BRemove_Click(object sender, RoutedEventArgs e)
        {
            var selectedClient = LVClients.SelectedItem as Client;
            if (selectedClient == null)
            {
                MessageBox.Show("Выберите сотрудника");
                return;
            }
            selectedClient.IsDelete = true;
            App.DB.SaveChanges();
            LVClients.ItemsSource = App.DB.Client.Where(x => x.IsDelete == false).ToList();
        }
    }
}
