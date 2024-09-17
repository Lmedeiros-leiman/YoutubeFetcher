using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using YoutubeFetcher.Data.Models;
using YoutubeFetcher.Properties;
using YoutubeFetcher.Resources.Services;

namespace YoutubeFetcher {
    public partial class MainPage : ContentPage {
        public static object? selectedChannel = null; 
        public List<YoutubeVideo> youtubeVideos = []; 
        private readonly Button fetchVideosButton;
        private readonly CollectionView youtubeVideosCollection;
        //private readonly string localConfigurations = new ConfigurationBuilder().Adduse

        public MainPage() {
            InitializeComponent();
            Trace.WriteLine("Loading main page.");

            fetchVideosButton = this.FindByName<Button>("FetchChannelButton");
            youtubeVideosCollection = this.FindByName<CollectionView>("SavedChannelsCollection");
        }
        private void FetchChannelVideos(object sender, EventArgs e) {
            if (selectedChannel == null) { return; }
            var channel = selectedChannel as YoutubeChannel;
            fetchVideosButton.IsEnabled = false;
            
            var youtubeService = new YoutubeApiService( Constants.YoutubeApiKey , $"Youtube channel video fetcher | Maui app");
            var videos = youtubeService.FetchChannelVideos(channel.ChannelId, int.MaxValue);

            youtubeVideos = [];
            for (int i = 0; i < videos.Items.Count; i++ ) {

                YoutubeVideo video = new() {
                    VideoId = videos.Items[i].Id.VideoId,
                    ChannelId = videos.Items[i].Snippet.ChannelId,
                    Title = videos.Items[i].Snippet.Title,
                    Description = videos.Items[i].Snippet.Description,
                    Thumbnail = videos.Items[i].Snippet.Thumbnails.Default__.Url,
                    ReleaseDate = videos.Items[i].Snippet.PublishedAtRaw
                    };


                youtubeVideos.Add(video);
            }

            youtubeVideosCollection.ItemsSource = youtubeVideos;
            fetchVideosButton.IsEnabled = true;
        }

        private void UpdateMainPage(object sender, EventArgs e) {
            var channel = selectedChannel as YoutubeChannel;
            if (channel == null) { return;}
            Trace.WriteLine("Updating main page");

            youtubeVideos = [];
            youtubeVideosCollection.ItemsSource = youtubeVideos;

            if (channel.GetType().ToString().Equals("YoutubeFetcher.Data.Models.YoutubeChannel") ) {
                fetchVideosButton.Text = $"Click to fetch {channel.ChannelTitle} videos";
                fetchVideosButton.IsEnabled = true;
            } else {
                fetchVideosButton.Text = $"Select a channel to fetch videos.";
                

                fetchVideosButton.IsEnabled = false;
            }

        }
    }

}
