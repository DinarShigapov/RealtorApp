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
    /// Логика взаимодействия для ClientAddPage.xaml
    /// </summary>
    public partial class ClientAddPage : Page
    {

        Client contextClient = new Client();

        public ClientAddPage(Client client)
        {
            InitializeComponent();
            this.contextClient = client;
            DataContext = contextClient;
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(contextClient.Phone) == true 
                && string.IsNullOrWhiteSpace(contextClient.Email) == true)
            {
                MessageBox.Show("Укажите почту или телефон");
                return;
            }

            if (contextClient.Email.Last() == '@' || contextClient.Email.Last() == '.')
            {
                MessageBox.Show("Неправильная почта");
            }

            if (contextClient.Id == 0)
            {
                App.DB.Client.Add(contextClient);
                MessageBox.Show($"Новый клиент " +
                $"{contextClient.MiddleName} " +
                $"{contextClient.FirstName.ToCharArray()[0]}. " +
                $"{contextClient.LastName.ToCharArray()[0]}. был успешно добавлен");
            }
            else
            {
                MessageBox.Show($"Клиент " +
                $"{contextClient.MiddleName} " +
                $"{contextClient.FirstName.ToCharArray()[0]}. " +
                $"{contextClient.LastName.ToCharArray()[0]}. был сохранен");
            }
            App.DB.SaveChanges();

            NavigationService.Navigate(new ClientListPage());
        }

        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientListPage());
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "@" && TBEmail.Text.Contains('@'))
                e.Handled = true;
            if (e.Text == "." && TBEmail.Text.Contains('.'))
                e.Handled = true;
            if (TBEmail.Text != string.Empty)
            {
                if (TBEmail.Text.Last() == '@' && e.Text == ".")
                    e.Handled = true;
            }

        }
    }
}
