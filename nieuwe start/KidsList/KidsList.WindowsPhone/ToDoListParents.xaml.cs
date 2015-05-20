using KidsList.Common;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace KidsList
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ToDoListParents : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private string IdParent;
        string LocalChildId;
        private MobileServiceCollection<TodoItem, TodoItem> items;
        private MobileServiceCollection<Child, Child> children;
        private IMobileServiceTable<Child> childrenTable = App.MobileService.GetTable<Child>();
        private IMobileServiceTable<TodoItem> todoTable = App.MobileService.GetTable<TodoItem>();

        public ToDoListParents()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        private async void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (amountKidsList.SelectedIndex == -1)
            {
                await new MessageDialog("Please select a child").ShowAsync();
            }
            else
            Frame.Navigate(typeof(VoiceRecorder));
        }

        private async Task InsertTodoItem(TodoItem todoItem)
        {
            // This code inserts a new TodoItem into the database. When the operation completes
            // and Mobile Services has assigned an Id, the item is added to the CollectionView
            await todoTable.InsertAsync(todoItem);
            //await SyncAsync(); // offline sync
        }

        private async Task RefreshTodoItems()
        {
            foreach (var child in children)
            {
                if (child.Name == amountKidsList.SelectedItem)
                {
                    LocalChildId = child.Id;
                }
            }

            MobileServiceInvalidOperationException exception = null;

            // This code refreshes the entries in the list view by querying the TodoItems table.
            // The query excludes completed TodoItems
            items = await todoTable
                .Where(todoItem => todoItem.Complete == false)
                .Where(todoItem => todoItem.IdChild == LocalChildId)
                .ToCollectionAsync();

            if (items.Count == 0)
            {
                await new MessageDialog("all tasks completed").ShowAsync();
            }
            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                ListItems.ItemsSource = items;
                this.ButtonSave.IsEnabled = true;
            }
        }

        private async Task UpdateCheckedTodoItem(TodoItem item)
        {
            // This code takes a freshly completed TodoItem and updates the database. When the MobileService 
            // responds, the item is removed from the list 
            await todoTable.UpdateAsync(item);
            items.Remove(item);
            ListItems.Focus(Windows.UI.Xaml.FocusState.Unfocused);

            // await SyncAsync(); // offline sync
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var todoItem = new TodoItem { Text = addTaskBox.Text, Time = choseTime.Time.ToString(), Date = choseDate.Date.ToString("dd-MM-yyyy"), IdChild = LocalChildId };
            await InsertTodoItem(todoItem);
            await RefreshTodoItems();
        }

        private async void CheckBoxComplete_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            TodoItem item = cb.DataContext as TodoItem;
            await UpdateCheckedTodoItem(item);
        }

        private async void amountKidsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ButtonSave.IsEnabled = true;
            await RefreshTodoItems();
        }

        private async void getChild()
        {
            children = await childrenTable
                   .Where(child => child.IdParent == IdParent)
                   .ToCollectionAsync();
            foreach (var c in children)
            {
                amountKidsList.Items.Add(c.Name);
            }
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            getChild();
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            IdParent = e.Parameter as string;
            this.navigationHelper.OnNavigatedTo(e);
            //await InitLocalStoreAsync(); // offline sync
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion



        #region Offline sync

        // private async Task InitLocalStoreAsync()
        // {
        //    if (!App.MobileService.SyncContext.IsInitialized)
        //    {
        //       var store = new MobileServiceSQLiteStore("localstore.db");
        //       store.DefineTable<TodoItem>();
        //       await App.MobileService.SyncContext.InitializeAsync(store);
        //    }
        //
        //    await SyncAsync();
        // }

        //  private async Task SyncAsync()
        // {
        //     await App.MobileService.SyncContext.PushAsync();
        //     await todoTable.PullAsync("todoItems", todoTable.CreateQuery());
        // }

        #endregion 
    }
}
