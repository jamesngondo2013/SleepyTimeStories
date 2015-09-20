using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Media.SpeechSynthesis;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Sleepy_Time_Stories
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StoryPage : Page
    {
        public string myStory;
        public MediaElement media = new MediaElement();
        public SpeechSynthesizer ttssynthesizer = new SpeechSynthesizer();
      

        public StoryPage()
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
           
            Globalvars._Items = (string)e.Parameter;
            // txItem.Text = (string)e.Parameter;

            XDocument LoadData = XDocument.Load("Stories.xml"); //xml file name
            var SearchData = from c in LoadData.Descendants("Messg")//xml tags
                             where c.Attribute("Story").Value == (string)e.Parameter
                             select new InspirationalMsg()
                             {

                                 InspMessage = c.Attribute("InspMessage").Value,

                             };

            displayMsg.ItemsSource = SearchData;
            InspirationalMsg message = new InspirationalMsg();
            myStory = SearchData.FirstOrDefault().InspMessage;
            //txResult.Text = SearchData.FirstOrDefault().ToString();
            //txResult.Text = SearchData.ElementAt(0).ToString();
            displayMsg.Visibility = Visibility.Visible;

            
            
        }

        public class InspirationalMsg
        {
            string story;
            string msg;
            int id;

            public string Story
            {
                get { return story; }
                set { story = value; }
            }

            public string InspMessage
            {
                get { return msg; }
                set { msg = value; }
            }
           
            public int Id
            {
                get { return id; }
                set { id = value; }
            }
            public override string ToString()
            {
                return msg;
                //return base.ToString();
            }

        }

       
        public async void showError()
        {
            var dialog = new Windows.UI.Popups.MessageDialog("Please enter some text!");
            await dialog.ShowAsync();

        }
        public async void SpeakText(string TTS)
        {
            //Set the Voice/Speaker
            using (var Speaker = new SpeechSynthesizer())
            {
                Speaker.Voice = (SpeechSynthesizer.AllVoices.First(x => x.Gender == VoiceGender.Female));

                ttssynthesizer.Voice = Speaker.Voice;
            }

            SpeechSynthesisStream ttsStream = await ttssynthesizer.SynthesizeTextToStreamAsync(TTS);

            //play the speech
            media.SetSource(ttsStream, " ");

        }

        private void btnPlay_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            if (myStory.Length > 0)
            {
                SpeakText(myStory);
            }
            else
            {
                showError();

            }
        }

        private void btnStop_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            media.Stop();
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(StoryList));
        }

        
       


    }
}
