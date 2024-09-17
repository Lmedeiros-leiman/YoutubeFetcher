using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using YoutubeFetcher.Data;
using YoutubeFetcher.Data.Models;
using YoutubeFetcher.Properties;
using YoutubeFetcher.Resources.Services;

namespace YoutubeFetcher;

public partial class ManageChannels : ContentPage {

	private readonly YoutubeApiService youtubeService;
	private readonly IDatabase dbcontext;
	//
	private readonly Button fetchButton;
	private readonly Entry channelNameBox;
	private readonly CollectionView channelCollection;
	//
	private List<YoutubeChannel> savedYoutubeChannels;

	public ManageChannels() {
		InitializeComponent();
		
		dbcontext = new Database();

		youtubeService = new YoutubeApiService( Constants.YoutubeApiKey , $"Youtube channel video fetcher | Maui app");
		savedYoutubeChannels = [];

		fetchButton = this.FindByName<Button>("SubmitButton");
		channelNameBox = this.FindByName<Entry>("InputChannelName");
		channelCollection = this.FindByName<CollectionView>("SavedChannelsCollection");

		// fetches the saved youtube channels.
		savedYoutubeChannels = dbcontext.GetSavedChanels();
		channelCollection.ItemsSource = savedYoutubeChannels;


	}




	private async void FetchYoutubeChannelVideos(object sender, EventArgs e) {
		fetchButton.IsEnabled = false;

		var channelList =  youtubeService.FetchChannelsByName(channelNameBox.Text, 1);
		channelNameBox.Text = string.Empty;
		channelNameBox.IsReadOnly = true;


		Trace.WriteLine("Showing channel found.");
		var selectedChannel = channelList.Items[0].Snippet;
		if (dbcontext.GetYoutubeChannelById(selectedChannel.ChannelId) != null) {

			await DisplayAlert($"Channel already on the list.", $"The channel {selectedChannel.Title} is already on the list", "Confirm");
			channelNameBox.IsReadOnly = false;
			return;
		}

		bool answer = await DisplayAlert($"{selectedChannel.ChannelTitle} | {selectedChannel.ChannelId}", $"{selectedChannel.Description}", "Confirm", "Cancel");
		if (answer) {
			// saves the channel into the database
			YoutubeChannel channel = new() {
				ChannelId = selectedChannel.ChannelId,
				ChannelTitle = selectedChannel.ChannelTitle,
				Description = selectedChannel.Description,
				PublishedAt = selectedChannel.PublishedAtRaw,
				Thumbnail = selectedChannel.Thumbnails.Default__.Url,
			};
			dbcontext.SaveYoutubeChannel(channel);
			savedYoutubeChannels.Add(channel);
			channelCollection.ItemsSource = savedYoutubeChannels;

		} else {
			// cancels everything
		}


		
		channelNameBox.IsReadOnly = false;
	}

	private void EnableSubmitButton(object sender, TextChangedEventArgs e) {
		var entry = sender as Entry;

		fetchButton.IsEnabled = !string.IsNullOrEmpty(entry?.Text.Trim());
		//fetchButton.IsEnabled = selectedChannel != null ? false : true;
	}

    private void SelectedChannel(object sender, SelectionChangedEventArgs e) {
		Trace.WriteLine("Changing selected channel.");
		MainPage.selectedChannel = e.CurrentSelection[0];
	}

    

}