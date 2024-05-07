using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;
using Adressenbuch;
using Klassenbibliothek.Models;

namespace AdressenWPF
{
    /// <summary>
    /// Interaktionslogik für Email.xaml
    /// </summary>
    public partial class Email : Window
    {
        SQLOps _sqlOps = new SQLOps(Funktionen.GetConnectionString());
        Person _p;

        public Email(Person p)
        {
            try
            {
                _p = p;
                InitializeComponent();
                eList.ItemsSource = null;
                eList.ItemsSource = _p.mail;
                eList.Items.Refresh();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error occured: {ex.Message}");
            }
        }

        private void eList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (eList.SelectedItem != null)
            {
                Mail m = (Mail)eList.SelectedItem;
                mailbox.Text = m.mail;
            }
        }

        private void addEmail(object sender, RoutedEventArgs e)
        {
            try
            {
                Mail m = new Mail();
                m.mail = mailbox.Text;
                _sqlOps.addMail(_p, m.mail);
                _p.mail = _sqlOps.GetMails(_p.id);
                showList(sender);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error occured: {ex.Message}");
            }
        }

        private void deleteEmail(object sender, RoutedEventArgs e)
        {
            if (eList.SelectedItem != null)
            {
                Mail m = (Mail)eList.SelectedItem;
                _sqlOps.deleteMail(_p.id, m.id);
                _p.mail = _sqlOps.GetMails(m.id);
                showList(sender);
            }
        }

        private void showList(object sender)
        {
            eList.ItemsSource = null;
            eList.ItemsSource = _p.mail;
            eList.Items.Refresh();
        }
    }


}
