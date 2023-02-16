using RealtorApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            if (CheckEmail(contextClient.Email) == false)
                return;

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

        private bool CheckEmail(string textBox) 
        {
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            if (!Regex.IsMatch(textBox, pattern, RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Email не соответствует формату\n***@***.**");
                return false;
            }
            return true;
        }

        private void TBLostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (sender as TextBox).Text;
            if (CheckEmail(textBox) == false)
                return;
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
