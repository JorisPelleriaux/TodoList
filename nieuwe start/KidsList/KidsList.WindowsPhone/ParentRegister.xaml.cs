using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Text;
using Windows.UI.Popups;






// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace KidsList
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

   
    
    public sealed partial class ParentRegister : Page
    {
        private bool AlreadyExist = false;
        public string nummer { get; set; }
        public string test;
        private MobileServiceCollection<Parent, Parent> parents;
        private IMobileServiceTable<Parent> ParentTable = App.MobileService.GetTable<Parent>();
        public ParentRegister()
        {
            this.InitializeComponent();
        }


        private async Task InsertParent(Parent parent)
        {
            // This code inserts a new TodoItem into the database. When the operation completes
            // and Mobile Services has assigned an Id, the item is added to the CollectionView
            await ParentTable.InsertAsync(parent);

            //parents.Add(parent);

            //await SyncAsync(); // offline sync
        }

        private async void CheckAlreadyExists()
        {
            AlreadyExist = false;
            parents = await ParentTable
                          .Where(Parent => Parent.Username == UsernameParents.Text)
                          .ToCollectionAsync();

            if (parents[0].Username == UsernameParents.Text)
            {
                AlreadyExist = true;
            }
            else if (parents[0].Username != UsernameParents.Text)
                AlreadyExist = false;
        }
        private async void Next_Click(object sender, RoutedEventArgs e)
        {
            CheckAlreadyExists();
             if (PasswordParents.Password.Equals(ConfPassword.Password))
            {
                if (NameParents.Text == "" || EmailParents.Text == "" || UsernameParents.Text == "" || PasswordParents.Password == "")
                {
                    await new MessageDialog("please fill in the required fields").ShowAsync();
                }

                if (NameParents.Text != "" || EmailParents.Text != "" || UsernameParents.Text != "")
                {
                    if (AlreadyExist == true)
                    {
                        await new MessageDialog("Username already exists").ShowAsync();
                        
                    }
                    if (AlreadyExist == false)
                    {
                        nummer = Guid.NewGuid().ToString();

                        var parent1 = new Parent { Id = nummer, Name = NameParents.Text, Email = EmailParents.Text, Phonenumber = PhonenumberParents.Text, Username = UsernameParents.Text, Password = PasswordParents.Password };
                        await InsertParent(parent1);
                        Frame.Navigate(typeof(ChildRegister), nummer);
                    }
                }
            }
            else if (!PasswordParents.Password.Equals(ConfPassword.Password))
            {
                await new MessageDialog("Your password and confirmation password do not match.").ShowAsync();
            } 
        }

        private void BackParents_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
           
    }
}
