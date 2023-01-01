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

    public partial class addWindow : Window
    {
        status status = status.add;
        Person person;
        string gender;
        mainScreen parent;


        public addWindow(string gender,mainScreen parent)
        {
            InitializeComponent();
            FillCmb();
            this.parent = parent;
            this.gender = gender;
            person = new Person();
            person.gender = gender;
            if (gender == "m")
            {
                title.Content = "הוספת בחור";
                yeshivaOrSeminarLbl.Content = "ישיבה";
            }
            else 
            { 
                title.Content = "הוספת בחורה";
                yeshivaOrSeminarLbl.Content = "סמינר";
            }
        }

        public addWindow(object p,mainScreen parent)
        {
            InitializeComponent();
            FillCmb();
            this.parent = parent;
            person = (Person)p;
            status = status.update;
            gender = person.gender;
            if (gender == "בחור")
            {
                yeshivaOrSeminarLbl.Content = "ישיבה";
            }
            else
            {
                yeshivaOrSeminarLbl.Content = "סמינר";
            }
            FillFields();
        }

        private void FillCmb()
        {
            edaCmb.ItemsSource = people.peopleList.Select(p=>p.eda);
            motsaCmb.ItemsSource = people.peopleList.Select(p => p.motsa);
        }

        private void FillFields()
        {
            nameTxb.Text = person.name;
            ageTxb.Text = person.age;
            cityTxb.Text = person.city;
            phoneTxb.Text = person.phone;
            parentsTxb.Text = person.parents;
            friendsTxb.Text = person.friends;
            yeshivaOrSeminarTxb.Text = person.yeshivaOrSeminar;
            jobTxb.Text = person.job;
            emailTxb.Text = person.email;
            edaCmb.Text = person.eda;
            detailsTxb.Text = person.details;
            motsaCmb.Text = person.motsa;
            gender = person.gender;
            detailsTxb.Text=person.details;
            statusCmb.Text = person.status;
            if (person.learnOrWork.Contains("לומד"))
                learnCbx.IsChecked = true;
            if (person.learnOrWork.Contains("עובד"))
                workCbx.IsChecked = true;
            if (person.peaOrMitpachat.Contains("פאה"))
                peaCbx.IsChecked = true;
            if (person.peaOrMitpachat.Contains("מטפחת"))
                mitpachatCxb.IsChecked = true;

            if (person.imageUrl != null && person.imageUrl != "")
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(person.imageUrl, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
                imageBox.Source = bitmap;
            }
        }

        private void SaveP(object sender, RoutedEventArgs e)
        {
            if (status == status.add)
                SaveNew();
            else
                Update();
        }

        private void SaveNew()
        {
            person = new Person();
            person.id = people.peopleList.Max(p=>p.id)+"";
            SaveDetails();
            people.peopleList.Add(person);
            MessageBox.Show("נשמר בהצלחה!");
            this.Close();
        }

        private void Update()
        {
            people.peopleList.Remove(person);
            SaveDetails();
            people.peopleList.Add(person);
            MessageBox.Show("עודכן בהצלחה!");
            this.Close();
        }

        private void SaveDetails()
        {
            person.name = nameTxb.Text;
            person.email = emailTxb.Text;
            person.details = detailsTxb.Text;
            person.status = statusCmb.Text + "";
            person.phone = phoneTxb.Text;
            person.age = ageTxb.Text;
            person.city = cityTxb.Text;
            person.eda = edaCmb.Text;
            person.motsa = motsaCmb.Text;
            person.parents = parentsTxb.Text;
            person.job = jobTxb.Text;
            person.yeshivaOrSeminar = yeshivaOrSeminarTxb.Text;
            person.peaOrMitpachat = "";
            person.learnOrWork = "";
            person.gender = gender;
            person.friends = friendsTxb.Text;

            if (peaCbx.IsChecked == true)
                person.peaOrMitpachat += "פאה";
            if (mitpachatCxb.IsChecked == true)
                person.peaOrMitpachat += " מטפחת";
            if (learnCbx.IsChecked == true)
                person.learnOrWork += "לומד";
            if (workCbx.IsChecked == true)
                person.learnOrWork += " עובד";
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
