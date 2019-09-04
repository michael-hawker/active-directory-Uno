using Microsoft.Graph;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ActiveDirectoryUno
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public User Me
        {
            get { return (User)GetValue(MeProperty); }
            set { SetValue(MeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Me.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MeProperty =
            DependencyProperty.Register(nameof(Me), typeof(User), typeof(MainPage), new PropertyMetadata(null));

        public MainPage()
        {
            this.InitializeComponent();
        }

        async void OnGetData(object sender, RoutedEventArgs e)
        {
            try
            {
                Me = await App.Graph.Me.Request().GetAsync();
                slUser.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, 
                    async () => 
                    await new ContentDialog { 
                        Title = "Something went wrong with the API call", 
                        Content = ex.Message, 
                        CloseButtonText = "Dismiss" 
                    }.ShowAsync());
            }
        }
    }
}
