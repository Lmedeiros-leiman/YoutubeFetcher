using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using YoutubeFetcher.Resources.Services;

namespace YoutubeFetcher {
    public partial class MainPage : ContentPage {
        //
        private readonly YoutubeApiService youtubeService;

        //
        private readonly CollectionView videoListShower;
        private readonly Button fetchButton;
        private readonly Entry channelNameBox;
        //

        private SearchResultSnippet? selectedChannel;



        //private readonly string localConfigurations = new ConfigurationBuilder().Adduse

        public MainPage() {
            InitializeComponent();

            youtubeService = new YoutubeApiService("AIzaSyAyPS6n-46jC54CTvTt7UiOXRIrONwcvlE", $"Youtube channel video fetcher | Maui app");
            videoListShower = this.FindByName<CollectionView>("videoCollectionShower");
            fetchButton = this.FindByName<Button>("SubmitButton");
            channelNameBox = this.FindByName<Entry>("InputChannelName");
            

        
        
        
        }


        private async void FetchYoutubeChannelVideos(object sender, EventArgs e) {

            fetchButton.IsEnabled = false;

            var channelList = youtubeService.FetchChannelsByName(channelNameBox.Text, 1);

            

            Trace.WriteLine("Showing channel found.");

            var channel = selectedChannel = channelList.Items[0].Snippet;
            bool answer = await DisplayAlert($"{channel.ChannelTitle} | {channel.ChannelId}",
                                             $"{channel.Description}",
                                             "Confirm",
                                             "Cancel");
            
            if (answer == true) {
                // fetch channel videos
                var channelVideos = youtubeService.FetchChannelVideos(selectedChannel.ChannelId);
                
                videoListShower.ItemsSource = channelVideos;


            } else {
                // cancel everything
                selectedChannel = null;
            }
        }

        private void CheckFetchButton(object sender, TextChangedEventArgs e) {
            var entry = sender as Entry;
            
            fetchButton.IsEnabled = entry.Text.Length > 4 ? true : false;
            fetchButton.IsEnabled = selectedChannel != null ? false : true;
            
        }

        private void fetchButton_Clicked(object sender, EventArgs e) {
            
        }
    }

}
