using Adressenbuch;
using Klassenbibliothek.Models;
using System;
using System.Collections;
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
using System.Windows.Shapes;

namespace AdressenWPF
{
    /// <summary>
    /// Interaktionslogik für Telefon.xaml
    /// </summary>
    public partial class Telefon : Window
    {
        SQLOps _sqlOps = new SQLOps(Funktionen.GetConnectionString());
        Person _p;

        public Telefon(Person p)
        {
            try
            {
                _p = p;
                InitializeComponent();
                tList.ItemsSource = null;
                tList.ItemsSource = _p.nummer;
                tList.Items.Refresh();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error occured: {ex.Message}");
            }
        }

        //private void eList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (tList.SelectedItem != null)
        //    {
        //        Tel n = (Tel)tList.SelectedItem;
        //        number.Text = n.nummer;
        //    }
        //}

        private void addNr(object sender, RoutedEventArgs e)
        {
            try
            {
                Tel n = new Tel();
                n.nummer = number.Text;
                _sqlOps.addTelNr(_p, n.nummer);
                _p.nummer = _sqlOps.GetTelNr(_p.id);
                showList(sender);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error occured: {ex.Message}");
            }
        }

        private void deleteNr(object sender, RoutedEventArgs e)
        {
            if (tList.SelectedItem != null)
            {
                Tel n = (Tel)tList.SelectedItem;
                _sqlOps.deleteTelNr(_p.id, n.id);
                _p.nummer = _sqlOps.GetTelNr(n.id);
                showList(sender);
            }
        }

        private void showList(object sender)
        {
            tList.ItemsSource = null;
            tList.ItemsSource = _p.nummer;
            tList.Items.Refresh();
        }
    }
}
