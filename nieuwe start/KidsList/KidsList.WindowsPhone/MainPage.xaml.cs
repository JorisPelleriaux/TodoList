using Windows.UI.Xaml.Controls;

namespace KidsList
{
    public sealed partial class MainPage : Page
    {
        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ParentRegister));
        }
    }
}
