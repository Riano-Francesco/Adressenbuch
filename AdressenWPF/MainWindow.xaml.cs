﻿using Klassenbibliothek.Models;
using Adressenbuch;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Net;
using AdressenWPF;

namespace Adressen;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    Adresse persons = new Adresse();
    SQLOps _sqlOps = new SQLOps(Funktionen.GetConnectionString());

    public MainWindow()
    {
        InitializeComponent();
        pList.ItemsSource = persons.people;
    }

    private void Save_Click(object sender, EventArgs e)
    {
        persons.SaveToFile("Adressen.txt");
        persons.people.Clear();
        MessageBox.Show("Daten gespeichert.");
    }
    private void Load_Click(object sender, EventArgs e)
    {
        //pList.Items.Clear();
        persons.LoadFromFile("Adressen.txt");
        pList.ItemsSource = persons.people;
        pList.Items.Refresh();
    }

    private void Load_DB(object sender, RoutedEventArgs e)
    {
        persons.people = _sqlOps.GetPeople();

        foreach (var p in persons.people)
        {
            p.mail = _sqlOps.GetMails(p.id);
            p.nummer = _sqlOps.GetTelNr(p.id);
        }

        pList.ItemsSource = null;
        pList.ItemsSource = persons.people;
        pList.Items.Refresh();
    }

    private void Save_DB(object sender, RoutedEventArgs e)
    {
        foreach (var p in persons.people)
        {
            if (!_sqlOps.checkID(p))
            {
                _sqlOps.CreateContact(p);
            } 
            else
            {
                _sqlOps.UpdatePerson(p);
            }
        }
        Load_DB(sender, e);
    }

    private void AddPerson(object sender, RoutedEventArgs e)
    {
        Person newp = new Person();

        newp.vorname = vName.Text;
        newp.nachname = nName.Text;
        newp.strasse = street.Text;
        newp.hausnr = hausnr.Text;
        newp.plz = postal.Text;
        newp.ort = city.Text;

        persons.people.Add(newp);

        vName.Text = "";
        nName.Text = "";
        street.Text = "";
        hausnr.Text = "";
        postal.Text = "";
        city.Text = "";
    }

    //private void pList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    if (pList.SelectedItem != null)
    //    {
    //        Person p = (Person)pList.SelectedItem;
    //        vName.Text = p.vorname;
    //        nName.Text = p.nachname;
    //        street.Text = p.strasse;
    //        hausnr.Text = p.hausnr;
    //        postal.Text = p.plz;
    //        city.Text = p.ort;
            
    //    }
    //}

    private void Email_Button(object sender, RoutedEventArgs e)
    {
        if (pList.SelectedItem == null)
        {
            MessageBox.Show("Bitte Person wählen!");
            return;
        }

            Email email = new Email((Person)pList.SelectedItem);
            email.Show();
        
    }
    private void Tel_Button(object sender, RoutedEventArgs e)
    {
        if (pList.SelectedItem == null)
        {
            MessageBox.Show("Bitte Person wählen!");
            return;
        }
        Telefon telefon = new Telefon((Person)pList.SelectedItem);
        telefon.Show();
    }
}