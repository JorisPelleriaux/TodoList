﻿using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace KidsList
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChildRegister : Page
    {
        private string IdParent;
        private MobileServiceCollection<Child, Child> children;
        private IMobileServiceTable<Child> ChildTable = App.MobileService.GetTable<Child>();

        public ChildRegister()
        {
            this.InitializeComponent();
            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
             IdParent = e.Parameter as string;
        }


        private async Task InsertChild(Child child)
        {
            // This code inserts a new TodoItem into the database. When the operation completes
            // and Mobile Services has assigned an Id, the item is added to the CollectionView
            await ChildTable.InsertAsync(child);

            //parents.Add(parent);

            //await SyncAsync(); // offline sync
        }
        private async void SaveToDatabase()
        {
            if (Password_Child.Password.Equals(Confirm_passwordCild.Password))
            {
                if (NameChild.Text == "" || UsernameChild.Text == "" || Password_Child.Password == "")
                {
                    await new MessageDialog("please fill in the required fields").ShowAsync();
                }
                else if (NameChild.Text != "" || UsernameChild.Text != "" || Password_Child.Password != "")
                {
                    var child = new Child { IdParent = IdParent, Name = NameChild.Text, Username = UsernameChild.Text, Password = Password_Child.Password };
                    await InsertChild(child);
                    
                }
            }

            else if (!Password_Child.Password.Equals(Confirm_passwordCild.Password))
            {
                await new MessageDialog("Your password and confirmation password do not match.").ShowAsync();
            }  
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
             SaveToDatabase();
             Frame.Navigate(typeof(MainPage));
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SaveToDatabase();
            Frame.Navigate(typeof(ChildRegister), IdParent);
        }
    }
}
