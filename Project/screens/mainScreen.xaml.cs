using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Project.Classes;
using Project.Classes.Person;
using System.Text.Json;
using Project.classes;
using System.Data;
using System.ComponentModel;
using System.Reflection;

namespace Project
{
    class Filter
    {
        public string name { get; set; }
        public bool isSelected { get; set; }
        public Filter(string name, bool isSelcted)
        {
            this.name = name;
            this.isSelected = isSelected;
        }

        
    }


    public partial class mainScreen : Window
    {
        List<Filter> eFilters = new List<Filter>();
        List<Filter> mFilters = new List<Filter>();

        List<Person> filteredPeople = people.peopleList;
        DataTable table;

        public mainScreen()
        {
            InitializeComponent();
            string peopleAsJsonString = File.ReadAllText("..//..//..//people.json");
            people.peopleList = JsonSerializer.Deserialize<List<Person>>(peopleAsJsonString);
            dataGrid.ItemsSource = people.peopleList;
            // dataGrid.Columns.RemoveAt(1);
            people.peopleList.ForEach(p => eFilters.Add(new Filter(p.eda, false)));
            people.peopleList.ForEach(p => mFilters.Add(new Filter(p.motsa, false)));
            Resources["edot"] = eFilters;
            Resources["motsa"] = mFilters;
        }

        void updatePerson(object sender, RoutedEventArgs e)
        {
            addWindow addWindow = new addWindow(dataGrid.SelectedItem,this);
            addWindow.Show();
        }

        void openAddWindowF(object sender, RoutedEventArgs e)
        {
            addWindow addWindow = new addWindow("בחורה",this);
            addWindow.Show();
            dataGrid.DataContext = people.peopleList;
        }

        void openAddWindowM(object sender, RoutedEventArgs e)
        {
            addWindow addWindow = new addWindow("בחור", this);
            addWindow.Show();
            dataGrid.DataContext = people.peopleList;
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

        void mouseEnter1(object sender, RoutedEventArgs e)
        {
            MenuItem b = (MenuItem)sender;
            b.Background = Brushes.LightBlue;
        }

        void mouseLeave1(object sender, RoutedEventArgs e)
        {
            MenuItem b = (MenuItem)sender;
            b.Background = Brushes.LightYellow;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            addWindow addWindow = new addWindow(dataGrid.SelectedItem, this);
            addWindow.Show();
        }

        private void openMenu(object sender, RoutedEventArgs e)
        {
            contextMenu.IsOpen = true;
        }

        private async void deletePerson(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("למחוק את הבנאדם?", "אישור מחיקה",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Person p = (Person)dataGrid.SelectedItem;
                people.peopleList.Remove(p);
                if (MessageBox.Show(p.name + " נמחק בהצלחה") == MessageBoxResult.OK)
                {
                    RefreshDataGrid();
                }

            }
        }

        private void editPerson(object sender, RoutedEventArgs e)
        {
            addWindow addWindow = new addWindow(dataGrid.SelectedItem, this);
            addWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            File.WriteAllText("..//..//..//people.json", Newtonsoft.Json.JsonConvert.SerializeObject(people.peopleList));
        }

        private void fCbx_Click(object sender, RoutedEventArgs e)
        {
            FilterTable();
        }

        private void mCbx_Click(object sender, RoutedEventArgs e)
        {
            FilterTable();
        }

        private void peaCbx_Click(object sender, RoutedEventArgs e)
        {
            FilterTable();

        }

        private void mitpachatCbx_Click(object sender, RoutedEventArgs e)
        {
            FilterTable();

        }

        private void workCbx_Click(object sender, RoutedEventArgs e)
        {
            FilterTable();

        }

        private void learnCbx_Click(object sender, RoutedEventArgs e)
        {
            FilterTable();

        }

        private void FilterTable()
        {
            filteredPeople = people.peopleList;

            if (freeSearchTxb.Text != "חיפוש")
            {
                filteredPeople = SmartSearch.Search(filteredPeople, freeSearchTxb.Text);
            }

            if (fCbx.IsChecked == true && mCbx.IsChecked==false) filteredPeople = filteredPeople.Where(p => p.gender == "בחורה").ToList();
            if (mCbx.IsChecked == true && fCbx.IsChecked==false) filteredPeople = filteredPeople.Where(p => p.gender == "בחור").ToList();

            if (peaCbx.IsChecked == true) filteredPeople = filteredPeople.Where(p => p.peaOrMitpachat.Contains("פאה")).ToList();
            if (mitpachatCbx.IsChecked == true) filteredPeople = filteredPeople.Where(p => p.peaOrMitpachat.Contains("מטפחת")).ToList();

            if (learnCbx.IsChecked == true) filteredPeople = filteredPeople.Where(p => p.learnOrWork.Contains("לומד")).ToList();
            if (workCbx.IsChecked == true) filteredPeople = filteredPeople.Where(p => p.learnOrWork.Contains("עובד")).ToList();
           
            mFilters.Where(mo => mo.isSelected == true)
                 .ToList()
                 .ForEach(m => filteredPeople = filteredPeople.Where(p => p.motsa.Contains(m.name)).ToList());

            eFilters.Where(ed => ed.isSelected == true)
                 .ToList()
                 .ForEach(e => filteredPeople = filteredPeople.Where(p => p.eda.Contains(e.name)).ToList());

            dataGrid.ItemsSource = filteredPeople;
        }

        private void edaListBox(object sender, RoutedEventArgs e)
        {
            FilterTable();
        }

        private void motsaListBox(object sender, RoutedEventArgs e)
        {
            FilterTable();
        }

        private void freeSearchTxb_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            freeSearchTxb.TextChanged -= freeSearchTxb_TextChanged;

            if (freeSearchTxb.Text == "חיפוש")
            {
                freeSearchTxb.Text = "";
                freeSearchTxb.Foreground = Brushes.Black;
                freeSearchTxb.FontStyle = FontStyles.Normal;
            }
            freeSearchTxb.TextChanged += freeSearchTxb_TextChanged;
        }

        private void freeSearchTxb_TextChanged(object sender, TextChangedEventArgs e)
        {
            freeSearchTxb.TextChanged-= freeSearchTxb_TextChanged;
            if (freeSearchTxb.Text == "")
            {
                freeSearchTxb.Text = "חיפוש";
                freeSearchTxb.Foreground = Brushes.Gray;
                freeSearchTxb.FontStyle = FontStyles.Italic;
            }
            else
            {
                if (freeSearchTxb.Text.Contains("חיפוש")&& freeSearchTxb.Text!="חיפוש")
                {
                    freeSearchTxb.Text=freeSearchTxb.Text.Replace("חיפוש", "");
                    freeSearchTxb.Foreground = Brushes.Black;
                    freeSearchTxb.FontStyle = FontStyles.Normal;
                }
            }
            freeSearchTxb.TextChanged += freeSearchTxb_TextChanged;

            FilterTable();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            freeSearchTxb.Text = "חיפוש";
            freeSearchTxb.Foreground = Brushes.Gray;
            freeSearchTxb.FontStyle = FontStyles.Italic;
        }

        public void RefreshDataGrid()
        {
            dataGrid.ItemsSource = null;
            this.filteredPeople = people.peopleList;
            dataGrid.ItemsSource = filteredPeople;
            FilterTable();
        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string displayName = GetPropertyDisplayName(e.PropertyDescriptor);
            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
            }
        }

        public static string GetPropertyDisplayName(object descriptor)
        {

            PropertyDescriptor pd = descriptor as PropertyDescriptor;
            if (pd != null)
            {
                DisplayNameAttribute dn = pd.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
                if (dn != null && dn != DisplayNameAttribute.Default)
                {
                    return dn.DisplayName;
                }
            }
            else
            {
                PropertyInfo pi = descriptor as PropertyInfo;
                if (pi != null)
                {
                    Object[] attributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    for (int i = 0; i < attributes.Length; ++i)
                    {
                        DisplayNameAttribute dn = attributes[i] as DisplayNameAttribute;
                        if (dn != null && dn != DisplayNameAttribute.Default)
                        {
                            return dn.DisplayName;
                        }
                    }
                }
            }
            return null;
        }
    }
}
