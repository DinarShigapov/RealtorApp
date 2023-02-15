using RealtorApp.Model;
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

namespace RealtorApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для RealtorListPage.xaml
    /// </summary>
    public partial class RealtorListPage : Page
    {
        public RealtorListPage()
        {
            InitializeComponent();
            LVRealtors.ItemsSource = App.DB.Realtor.ToList();
        }

        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RealtorAddPage(new Realtor() { IsDelete = false }));
        }

        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedRealtor = LVRealtors.SelectedItem as Realtor;
            if (selectedRealtor == null)
            {
                MessageBox.Show("Выберите клиента");
                return;
            }
            NavigationService.Navigate(new RealtorAddPage(selectedRealtor));
        }

        private void BRemove_Click(object sender, RoutedEventArgs e)
        {
            var selectedRealtor = LVRealtors.SelectedItem as Realtor;
            if (selectedRealtor == null)
            {
                MessageBox.Show("Выберите клиента");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить Риэлтора", "Подтверждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;

            selectedRealtor.IsDelete = true;
            App.DB.SaveChanges();
            LVRealtors.ItemsSource = App.DB.Client.Where(x => x.IsDelete == false).ToList();
        }
    }
}
