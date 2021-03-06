﻿using KidsList.Common;
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
    public sealed partial class ParentRegister : Page
    {
        private bool AlreadyExist = false;
        public string nummer { get; set; }

        private MobileServiceCollection<Parent, Parent> parents;
        private IMobileServiceTable<Parent> ParentTable = App.MobileService.GetTable<Parent>();
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public ParentRegister()
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

        private async Task InsertParent(Parent parent)
        {
            // This code inserts a new TodoItem into the database. When the operation completes
            // and Mobile Services has assigned an Id, the item is added to the CollectionView
            await ParentTable.InsertAsync(parent);
            //await SyncAsync(); // offline sync
        }

        private async void CreateNewParent()
        {
            if (NameParents.Text != "" && EmailParents.Text != "" && UsernameParents.Text != "" && PasswordParents.Password != "")
            {
                if (PasswordParents.Password.Equals(ConfPassword.Password))
                {
                    if (AlreadyExist == false)
                    {
                        nummer = Guid.NewGuid().ToString();

                        var parent1 = new Parent { Id = nummer, Name = NameParents.Text, Email = EmailParents.Text, Phonenumber = PhonenumberParents.Text, Username = UsernameParents.Text, Password = PasswordParents.Password };
                        await InsertParent(parent1);
                        Frame.Navigate(typeof(ChildRegister), nummer);
                    }
                    else if (AlreadyExist == true)
                        await new MessageDialog("Username already exists").ShowAsync();
                }
                else if (!PasswordParents.Password.Equals(ConfPassword.Password))
                {
                    await new MessageDialog("Your password and confirmation password do not match.").ShowAsync();
                }
            }
            else if (NameParents.Text == "" || EmailParents.Text == "" || UsernameParents.Text == "" || PasswordParents.Password == "")
            {
                await new MessageDialog("please fill in the required fields").ShowAsync();
            }
        }

        private async Task CheckAlreadyExists()
        {

            parents = await ParentTable
                          .Where(Parent => Parent.Username == UsernameParents.Text)
                          .ToCollectionAsync();

            if (parents.Count > 0 && parents[0].Username == UsernameParents.Text)
            {
                AlreadyExist = true;
            }
            else if (parents.Count <= 0)
                AlreadyExist = false;
        }

        private async void Next_Click(object sender, RoutedEventArgs e)
        {
            await CheckAlreadyExists();
            CreateNewParent();
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
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
