using Microsoft.WindowsAzure.MobileServices;
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
        private bool AlreadyExist = false;
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

        private async Task CheckAlreadyExists()
        {
            children = await ChildTable
                          .Where(Child => Child.Username == UsernameChild.Text)
                          .ToCollectionAsync();

            if (children.Count > 0 && children[0].Username == UsernameChild.Text)
                AlreadyExist = true;
            
            else if (children.Count == 0)
                AlreadyExist = false;
        }

        private async Task InsertChild(Child child)
        {
            // This code inserts a new TodoItem into the database. When the operation completes
            // and Mobile Services has assigned an Id, the item is added to the CollectionView
            //Save child to database
            await ChildTable.InsertAsync(child);
            //await SyncAsync(); // offline sync
        }

        private async void CreateNewChild()
        {
            if (NameChild.Text != "" && UsernameChild.Text != "" && Password_Child.Password != "")
            {
                if (Password_Child.Password.Equals(Confirm_passwordCild.Password))
                {
                    if (AlreadyExist == false)
                    {
                        //Create a new child
                        var child = new Child { IdParent = IdParent, Name = NameChild.Text, Username = UsernameChild.Text, Password = Password_Child.Password };
                        await InsertChild(child);
                    }
                    else if (AlreadyExist == true)
                        await new MessageDialog("Username already exists").ShowAsync();
                }
                else if (!Password_Child.Password.Equals(Confirm_passwordCild.Password))
                {
                    await new MessageDialog("Your password and confirmation password do not match.").ShowAsync();
                }
            }
            else if (NameChild.Text == "" || UsernameChild.Text == "" || Password_Child.Password == "")
            {
                await new MessageDialog("please fill in the required fields").ShowAsync();
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
             CreateNewChild();
            // Go back to the login screen
             Frame.Navigate(typeof(MainPage));
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CreateNewChild();
            Frame.Navigate(typeof(ChildRegister), IdParent);
        }
    }
}
