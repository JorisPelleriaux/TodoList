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

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async Task InsertParent(Parent parent)
        {
            // This code inserts a new TodoItem into the database. When the operation completes
            // and Mobile Services has assigned an Id, the item is added to the CollectionView
            await ParentTable.InsertAsync(parent);
            //items.Add(todoItem);

            //await SyncAsync(); // offline sync
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var parent = new Parent { Id = "3", Name =  NameParents.Text, Email = EmailParents.Text, Phonenumber = PhonenumberParents.Text, Username = UsernameParents.Text};
            await InsertParent(parent);
        }
    }
}
