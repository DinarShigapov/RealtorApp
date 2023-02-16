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
    /// Логика взаимодействия для RealtorAddPage.xaml
    /// </summary>
    public partial class RealtorAddPage : Page
    {
        Realtor contextRealtor = new Realtor();
        public RealtorAddPage(Realtor realtor)
        {
            InitializeComponent();
            contextRealtor = realtor;
            DataContext = contextRealtor;
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {

            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(contextRealtor.MiddleName) == true)
            {
                errorMessage += "Введите фамилию\n";
            }
            if (string.IsNullOrWhiteSpace(contextRealtor.FirstName) == true)
            {
                errorMessage += "Введите имя\n";
            }
            if (string.IsNullOrWhiteSpace(contextRealtor.LastName) == true)
            {
                errorMessage += "Введите отчество\n";
            }

            if (contextRealtor.DealShare < 0 || contextRealtor.DealShare > 100)
            {
                errorMessage += "Доля 0 - 100\n";
                TBDealShare.Text = "0";
            }

            if (string.IsNullOrWhiteSpace(errorMessage) == false)
            {
                MessageBox.Show(errorMessage);
                return;
            }


            if (contextRealtor.Id == 0)
            {
                App.DB.Realtor.Add(contextRealtor);
                MessageBox.Show($"Новый риэлтор " +
                $"{contextRealtor.MiddleName} " +
                $"{contextRealtor.FirstName.ToCharArray()[0]}. " +
                $"{contextRealtor.LastName.ToCharArray()[0]}. был успешно добавлен");
            }
            else
            {
                MessageBox.Show($"Риэлтор " +
                $"{contextRealtor.MiddleName} " +
                $"{contextRealtor.FirstName.ToCharArray()[0]}. " +
                $"{contextRealtor.LastName.ToCharArray()[0]}. был сохранен");
            }
            App.DB.SaveChanges();

            NavigationService.Navigate(new RealtorListPage());
        }

        private void TBDealShare_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            if (Regex.IsMatch(e.Text, @"[0-9.]") == false)
                e.Handled = true;
            if (e.Text == "." && textBox.Text.Contains('.'))
                e.Handled = true;
            if (textBox.Text != "" && textBox.Text.Last() == '.' && e.Text != ".")
                textBox.Text += e.Text;
            textBox.CaretIndex = textBox.Text.Length;
        }

        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RealtorListPage());
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}
