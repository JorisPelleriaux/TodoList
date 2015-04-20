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

            parents.Add(parent);

            //await SyncAsync(); // offline sync
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {


            var parent = new Parent { Name = NameParents.Text, Email = EmailParents.Text, Phonenumber = PhonenumberParents.Text, Username = UsernameParents.Text };
            await InsertParent(parent);
            Frame.Navigate(typeof(ChildRegister));

        }


        private async Task RefreshParents()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {

                // This code refreshes the entries in the list view by querying the TodoItems table.
                // The query excludes completed TodoItems
                parents = await ParentTable
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                /*ListItems.ItemsSource = items;
                this.ButtonSave.IsEnabled = true;*/
            }

        }
    }
}
