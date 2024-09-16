using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeFetcher.Resources.Services {
    public class YoutubeApiService(string API_KEY, string applicationName) {
        public readonly YouTubeService youtubeApiObject = new YouTubeService(new Google.Apis.Services.BaseClientService.Initializer() {
            ApiKey = API_KEY,
            ApplicationName = applicationName
        });

        public SearchListResponse FetchChannelsByName(string channelName, int ammount = 20) {
            Trace.WriteLine($"Fetching youtube channels by {channelName}...");

            var searchRequest = youtubeApiObject.Search.List("snippet");
            searchRequest.Q = channelName;
            searchRequest.Type = "channel";
            searchRequest.MaxResults = ammount;

            Trace.WriteLine($"Chanels found by {channelName}.");
            return searchRequest.Execute();
        }
        public SearchListResponse FetchChannelVideos(string chanelId, int maxAmmount = 20) {
            Trace.WriteLine($"Fetching chanel id {chanelId} uploaded videos...");

            var searchRequest = youtubeApiObject.Search.List("snippet");
            searchRequest.ChannelId = chanelId;
            searchRequest.Type = "video";
            searchRequest.MaxResults = maxAmmount;

            Trace.WriteLine($"Found chanel id {chanelId} uploaded videos.");
            return searchRequest.Execute();
        }
    }
}

