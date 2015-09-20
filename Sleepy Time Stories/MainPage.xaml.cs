using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Sleepy_Time_Stories
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }


        private void btnReminder_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //ShowToast(10, "10s timeout toast from Jay!");
        }
        private void ShowToast(int timeoutInSeconds, string text)
        {
            var toastTemplate = ToastTemplateType.ToastImageAndText01;

            var toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            var toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(text));

            ////Andreas Hammar 2014-10-08 08:39: note! does not work on windows phone
            //var toastImageAttributes = toastXml.GetElementsByTagName("image");
            //((XmlElement)toastImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/jay_transparent.png");
            //((XmlElement)toastImageAttributes[0]).SetAttribute("alt", "jay");

            var toast = new ToastNotification(toastXml);

            if (timeoutInSeconds >= 0)
                toast.ExpirationTime = DateTime.Now.AddSeconds(timeoutInSeconds);


            ToastNotificationManager.CreateToastNotifier().Show(toast);

        }

        private void about_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(About));
        }

        private void stories_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StoryList));
        }

        
    }
}
