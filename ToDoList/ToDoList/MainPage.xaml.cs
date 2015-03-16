using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ToDoList.Resources;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.ObjectModel;

namespace ToDoList
{
    public class PARENTS
    {
        public int ID { get; set; }

        public string NAME { get; set; }

        public char USERNAME { get; set; }
        public char PASSWORD { get; set; }
    }

    public sealed partial class MainPage : PhoneApplicationPage
    {
        private MobileServiceCollection<PARENTS, PARENTS> items;
        private IMobileServiceTable<PARENTS> todoTable =
            App.MobileService.GetTable<PARENTS>();
        public MainPage()
        {
            this.InitializeComponent();
        }
        private async void InsertTodoItem(PARENTS parent)
        {
            await todoTable.InsertAsync(parent);
            items.Add(parent);
        }
        
        private async void UpdateCheckedTodoItem(PARENTS item)
        {
            await todoTable.UpdateAsync(item);
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            int nummer = 2;
            var todoItem = new PARENTS { ID = nummer, NAME = tekst.Text};
           
            InsertTodoItem(todoItem);
        }






       /* private MobileServiceCollection<PARENTS, PARENTS> items;
        MobileServiceClient client = new MobileServiceClient(
            "https://kidslist.azure-mobile.net/",
           "LIvhiOaaECUZQfozduynGmJGyhazay72"
            );

        IMobileServiceTable<PARENTS> parents = App.MobileService.GetTable<PARENTS>();
 
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
 
            var parent = new PARENTS { NAME = "naam"};
            InsertTodoItem(parent);
        }
        private async void InsertTodoItem(PARENTS parent)
        {
            await parents.InsertAsync(parent);
            items.Add(parent);
        }*/

    }
}