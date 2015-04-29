using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace KidsList
{
    public sealed partial class MainPage : Page
    {
        private bool Login = false;
        private bool IsParent = false;
        private MobileServiceCollection<Parent, Parent> parents;
        private IMobileServiceTable<Parent> ParentTable = App.MobileService.GetTable<Parent>();
        private MobileServiceCollection<Child, Child> children;
        private IMobileServiceTable<Child> ChildTable = App.MobileService.GetTable<Child>();
        public MainPage()
        {
            this.InitializeComponent();
        }
        private async Task ControleLogin()
        {
            try
            {
                parents = await ParentTable
                          .Where(Parent => Parent.Username == username.Text)
                          .ToCollectionAsync();


                children = await ChildTable
                          .Where(Child => Child.Username == username.Text)
                          .ToCollectionAsync();

                if (children.Count > 0)
                {
                    if (children[0].Username == username.Text && children[0].Password == password.Password)
                    {
                        Login = true;
                        IsParent = false;
                        await new MessageDialog("Welkom " + children[0].Name).ShowAsync();
                    }
                }
                if (children.Count <= 0)
                {
                    if (parents.Count > 0)
                    {
                        if (parents[0].Username == username.Text && parents[0].Password == password.Password)
                        {
                            Login = true;
                            IsParent = true;
                            await new MessageDialog("Welkom " + parents[0].Name).ShowAsync();
                        }
                    }
                    if (parents.Count <= 0)
                    {
                        await new MessageDialog("Wrong username or password").ShowAsync();
                    }
                    else
                    {
                        await new MessageDialog("something wrong, please try again").ShowAsync();
                    }
                }
                if (username.Text == null || password.Password == null)
                {
                    await new MessageDialog("Please enter a username and password").ShowAsync();
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            await ControleLogin();
            if (Login == true)
            {
                Frame.Navigate(typeof(ToDoList));
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ParentRegister));
        }
    }
}
