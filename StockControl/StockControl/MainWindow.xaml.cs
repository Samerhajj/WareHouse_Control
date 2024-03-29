﻿using System;
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
using System.IO;
using System.Collections.ObjectModel;

namespace StockControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HomePage homePage;
        OrderPage orderPage;
        DepartmentsPage departmentsPage;
        EmployeePage employeePage;
        ProductsPage productsPage;
        AboutUsPage aboutUsPage;
        SettingsControl settingsPage;
        public int lastChosenPage { get; set; } = 0;

        public MainWindow()
        {
            InitializePages();
            InitializeComponent();
            MoveCursorMenu(0);
            ChooseTab(0);
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);
            ChooseTab(index);
        }//Takes the index of the selected ListViewMenu and opens the correct tab 
        private void MoveCursorMenu(int index)
        {
            TransitionSlideHome.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, 117.5 + (60 * index), 0, 0);
        }//Changes the GridCursor to the intended index (place)
        public void ChooseTab(int index)
        {
            switch (index)
            {
                case 0:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(homePage);
                    lastChosenPage = 0;
                    break;
                case 1:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(orderPage);
                    lastChosenPage = 1;
                    break;
                case 2:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(departmentsPage);
                    departmentsPage.ClearSelection();
                    lastChosenPage = 2;
                    break;
                case 3:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(employeePage);
                    employeePage.ClearSelection();
                    lastChosenPage = 3;
                    break;
                case 4:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(productsPage);
                    productsPage.ClearSelection();
                    lastChosenPage = 4;
                    break;
                case 5:
                    GridPrincipal.Children.Clear();
                    GridPrincipal.Children.Add(aboutUsPage);
                    lastChosenPage = 5;
                    break;
               
                default:
                    break;
            }
            
        }//Shows the correct tab according to the index
        private void InitializePages()
        {
            homePage = new HomePage();
            departmentsPage = new DepartmentsPage();
            orderPage = new OrderPage();
            employeePage = new EmployeePage();
            productsPage = new ProductsPage();
            aboutUsPage = new AboutUsPage();
            settingsPage = new SettingsControl(this);
        }//Initializes all the pages
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Data.StoreAll();
            });
            Application.Current.Shutdown();
        }//Stores the data and closes all the application
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            GridPrincipal.Children.Clear();
            GridPrincipal.Children.Add(settingsPage);
            settingsPage.initializeParams();
            ListViewMenu.IsEnabled = false;
            backArrowBTN.Visibility = Visibility.Visible;
        }//Opens the settings tab
        private void backArrowBTN_Click(object sender, RoutedEventArgs e)
        {
            ChooseTab(lastChosenPage);
            ListViewMenu.IsEnabled = true;
            backArrowBTN.Visibility = Visibility.Hidden;
        }//Allows the user to come back after opening the settings(Shows a new button to go back)
    }
}
