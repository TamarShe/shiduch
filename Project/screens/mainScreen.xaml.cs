using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Project.Classes.Person;
using Project.classes;
using System.Data;
using System.ComponentModel;
using System.Reflection;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using Project.Classes;

namespace Project
{
    public partial class mainScreen : MetroWindow
    {
        public string[] migzar;
        public string[] edot;
        public string[] kisooyRosh = new string[] { "פאה", "מטפחת", "לא משנה" };
        public string[] statuses = new string[] { "רווק/ה", "גרוש/ה", "אלמן/ה" };
        string remarks = "";
        List<Person> filteredPeople = people.peopleList;

        public mainScreen()
        {
            InitializeComponent();
            ExcelManager.ReadExcel();
            this.remarks = File.ReadAllText("..//..//..//remarks.txt");
            statusTxb.Text = this.remarks;
            dataGrid.ItemsSource = people.peopleList;
            this.FillCmb();
        }

        public void FillCmb()
        {
            migzar = people.peopleList.Select(p => p.migzar).Distinct().ToArray();
            edot = people.peopleList.Select(p => p.eda).Distinct().ToArray();
            migzarCmb.ItemsSource = migzar;
            edaCmb.ItemsSource = edot;
            kisooyCmb.ItemsSource = kisooyRosh;
            statusCmb.ItemsSource = statuses;
        }


        void openAddWindowF(object sender, RoutedEventArgs e)
        {
            addWindow addWindow = new addWindow(this, "נקבה");
            addWindow.Show();
            dataGrid.DataContext = people.peopleList;
            FillCmb();
        }

        void openAddWindowM(object sender, RoutedEventArgs e)
        {
            addWindow addWindow = new addWindow(this, "זכר");
            addWindow.Show();
            dataGrid.DataContext = people.peopleList;
            FillCmb();
        }


        //פתיחת טופס עדכון על בנאדם נבחר
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            addWindow addWindow = new addWindow(dataGrid.SelectedItem, this);
            addWindow.Show();
            FillCmb();

        }

        private void openMenu(object sender, RoutedEventArgs e)
        {
            contextMenu.IsOpen = true;
        }

        //מחיקת בנאדם
        private async void deletePerson(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("למחוק?", "אישור מחיקה", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Person p = (Person)dataGrid.SelectedItem;
                people.peopleList.Remove(p);
                if (System.Windows.MessageBox.Show(p.name + " נמחק בהצלחה") == MessageBoxResult.OK)
                {
                    RefreshDataGrid();
                }
            }
        }

        //עריכת בנאדם
        private void editPerson(object sender, RoutedEventArgs e)
        {
            addWindow addWindow = new addWindow(dataGrid.SelectedItem, this);
            addWindow.Show();
            FillCmb();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ExcelManager.WriteExcel();
            File.WriteAllText("..//..//..//remarks.txt", this.remarks);
            var jsonString = JsonConvert.SerializeObject(people.peopleList);
            File.WriteAllText("..//..//..//people.txt", jsonString);
        }

        private void FilterTable(object sender, EventArgs e)
        {
            filteredPeople = people.peopleList;

            int minAge = 0;
            int maxAge = 0;

            try
            {
                minAge = Convert.ToInt32(ageTxb.Text.Substring(5, 2));
            }
            catch (Exception ex) { }
            try
            {
                maxAge = Convert.ToInt32(ageTxb.Text.Substring(11, 2));
            }
            catch (Exception ex) { }


            if (freeSearchTxb != null && dataGrid != null)
            {
                if (freeSearchTxb.Text != "")
                {
                    filteredPeople = SmartSearch.Search(filteredPeople, freeSearchTxb.Text);
                }

                if (maleRdn.IsChecked == true)
                    filteredPeople = filteredPeople.Where(p => p.gender == "זכר").ToList();

                if (fmaleRdn.IsChecked == true)
                    filteredPeople = filteredPeople.Where(p => p.gender == "נקבה").ToList();

                if (kisooyCmb.SelectedIndex != -1)
                {
                    var a = kisooyRosh[kisooyCmb.SelectedIndex];
                    if (a != "לא משנה")
                        filteredPeople = filteredPeople.Where(p => p.kisooyRosh.Equals(a)).ToList();
                    else
                    {
                        filteredPeople = filteredPeople.Where(p => p.kisooyRosh.Equals("פאה") || p.kisooyRosh.Equals("מטפחת") || p.kisooyRosh.Equals("לא משנה")).ToList();
                    }
                }

                if (migzarCmb.SelectedIndex != -1)
                {
                    var a = migzar[migzarCmb.SelectedIndex];
                    filteredPeople = filteredPeople.Where(p => p.migzar.Equals(a)).ToList();
                }

                if (edaCmb.SelectedIndex != -1)
                {
                    var a = edot[edaCmb.SelectedIndex];
                    filteredPeople = filteredPeople.Where(p => p.eda.Equals(a)).ToList();
                }

                if (statusCmb.SelectedIndex != -1)
                {
                    var a = statuses[statusCmb.SelectedIndex];
                    filteredPeople = filteredPeople.Where(p => p.status.Equals(a)).ToList();
                }

                if (minAge != 0)
                {
                    filteredPeople = filteredPeople.Where(p => Convert.ToInt32(p.age).CompareTo(minAge) >= 0).ToList();
                }

                if (maxAge != 0)
                {
                    filteredPeople = filteredPeople.Where(p => Convert.ToInt32(p.age).CompareTo(maxAge) <= 0).ToList();
                }

                dataGrid.ItemsSource = filteredPeople;
            }
        }

        public void RefreshDataGrid()
        {
            dataGrid.ItemsSource = null;
            this.filteredPeople = people.peopleList;
            dataGrid.ItemsSource = filteredPeople;
            FilterTable(new object(), new EventArgs());
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

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            //ניקוי כל החיפוש
            maleRdn.IsChecked = false;
            fmaleRdn.IsChecked = false;
            ageTxb.Text = "";
            edaCmb.Text = "";
            migzarCmb.Text = "";
            kisooyCmb.Text = "";
            statusCmb.Text = "";
            freeSearchTxb.Text = "";
            dataGrid.ItemsSource = people.peopleList;
        }

        private void clearFreeSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            freeSearchTxb.Text = "";
            FilterTable(sender, e);
        }

        private void clearGroupBoxBtn_Click(object sender, RoutedEventArgs e)
        {
            maleRdn.IsChecked = false;
            fmaleRdn.IsChecked = false;
            ageTxb.Text = "";
            edaCmb.Text = "";
            migzarCmb.Text = "";
            kisooyCmb.Text = "";
            statusCmb.Text = "";
            dataGrid.ItemsSource = people.peopleList;
        }

        private void statusTxb_TextChanged(object sender, TextChangedEventArgs e)
        {
            File.WriteAllText("..//..//..//remarks.txt", statusTxb.Text);
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            addWindow addWindow = new addWindow(dataGrid.SelectedItem, this);
            addWindow.Show();
        }
    }
}

