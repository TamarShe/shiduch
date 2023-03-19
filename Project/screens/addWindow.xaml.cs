using MahApps.Metro.Controls;
using Microsoft.Win32;
using Project.classes;
using Project.Classes.Person;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using static System.Net.Mime.MediaTypeNames;

namespace Project
{
    enum status
    {
        add,
        update
    }

    public partial class addWindow : MetroWindow
    {
        string[] migzar;
        string[] edot;
        string[] kisooyRosh;
        string[] statuses;
        status status = status.add;
        Person person;
        mainScreen parent;

        //הוספת חדש
        public addWindow(mainScreen parent,string gender)
        {
            InitializeComponent();
            this.parent = parent;
            this.migzar = parent.migzar;
            this.edot = parent.edot;
            this.kisooyRosh = parent.kisooyRosh;
            this.statuses = parent.statuses;
            person = new Person();
            person.gender = gender;
            InitDetails();
        }

        //עדכון בנאדם קיים
        public addWindow(object p,mainScreen parent)
        {
            InitializeComponent();
            this.parent = parent;
            this.migzar = parent.migzar;
            this.edot = parent.edot;
            this.kisooyRosh = parent.kisooyRosh;
            this.statuses = parent.statuses;
            person = (Person)p;
            status = status.update;
            InitDetails();
            //edaCmb.Text = person.eda;
            kisooyRoshCmb.Text = person.kisooyRosh;
            //migzarCmb.Text = person.migzar;
            statusCmb.Text = person.status;
            if (person.imageUrl != null && person.imageUrl != "")
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(person.imageUrl, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
                imageBox.Source = bitmap;
            }
        }

        //איתחול דברים בטופס
        private void InitDetails()
        {
            DataContext = person;
            statusCmb.ItemsSource = statuses;
            edaCmb.ItemsSource = edot;
            migzarCmb.ItemsSource= migzar;
            kisooyRoshCmb.ItemsSource = kisooyRosh;
        }

        private void SavePerson(object sender, RoutedEventArgs e)
        {
            try
            {
                person.kisooyRosh = kisooyRosh[kisooyRoshCmb.SelectedIndex];
            }
            catch (Exception ex) { }
            try
            {
                person.status = statuses[statusCmb.SelectedIndex];
            }

            catch (Exception ex) { }
            person.learnOrWork = "";
            if (learnCbx.IsChecked == true)
            {
                person.learnOrWork += "לומד ";
            }
            if (workCbx.IsChecked == true)
            {
                if (person.learnOrWork != "")
                {
                    person.learnOrWork += "ו";
                }
                person.learnOrWork += "עובד";
            }
            if (status == status.add)
            {
                person.id = people.peopleList.Max(p => p.id) + "";
                people.peopleList.Add(person);
            }
            else
            {
                people.peopleList.RemoveAll(p => p.id == person.id);
                people.peopleList.Add(person);
            }
            MessageBox.Show("נשמר בהצלחה!");
            this.Close();
        }

        void mouseEnter(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            b.Background = Brushes.LightBlue;
            b.Foreground = Brushes.Gray;
        }

        void mouseLeave(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            b.Background = Brushes.DeepSkyBlue;
            b.Foreground = Brushes.LightYellow;
        }

        private void changeImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                person.imageUrl = openFileDialog.FileName;
                bitmap.EndInit();
                imageBox.Source = bitmap;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            parent.RefreshDataGrid();
        }
    }
}
