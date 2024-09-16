using Google.Apis.YouTube.v3.Data;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeFetcher.Data.Models;
using YoutubeFetcher.Properties;

namespace YoutubeFetcher.Data {

    public interface IDatabase {

        //
        // Save
        public YoutubeChannel SaveYoutubeChannel(YoutubeChannel channel);
        public Task<YoutubeChannel> SaveYoutubeChannelAsync(YoutubeChannel channel);


        //
        // Get
        public List<YoutubeChannel> GetSavedChanels();
        public Task<List<YoutubeChannel>> GetSavedChanelsAsync();
        public YoutubeChannel? GetYoutubeChannelById(string channelId);
        public Task<YoutubeChannel?> GetYoutubeChannelByIdAsync(string channelId);
        public List<YoutubeVideo> GetChannelVideos(YoutubeChannel channel);
        public Task<List<YoutubeVideo>> GetChannelVideosAsync(YoutubeChannel channel);
        
    }
    public class Database : IDatabase {
        private readonly SQLiteAsyncConnection dbContextAsync;
        private readonly SQLiteConnection dbContext;

        public Database() {
            dbContextAsync = new(Constants.DatabasePath, Constants.Flags);
            dbContext = new(Constants.DatabasePath, Constants.Flags);

            if (dbContext is null) {
                throw new Exception("DATABASE NOT LOADED!");
            }

            // creates the tables for our objects.
            //

            dbContext.CreateTable<YoutubeChannel>();
            dbContext.CreateTable<YoutubeVideo>();

        }
        


        public YoutubeChannel SaveYoutubeChannel(YoutubeChannel channel) {
            dbContext.Insert(channel);
            return channel;
        }
        public async Task<YoutubeChannel> SaveYoutubeChannelAsync(YoutubeChannel channel) {
            await dbContextAsync.InsertAsync(channel);
            return channel;
        }
        
        
        public YoutubeChannel? GetYoutubeChannelById(string channelId) {
            return dbContext.Table<YoutubeChannel>().FirstOrDefault(c => c.ChannelId == channelId);
        }
        public async Task<YoutubeChannel?> GetYoutubeChannelByIdAsync(string channelId) {
            return await dbContextAsync.Table<YoutubeChannel>().FirstOrDefaultAsync(c => c.ChannelId == channelId);
        }
        
        
        public List<YoutubeVideo> GetChannelVideos(YoutubeChannel channel) {
            return dbContext.Table<YoutubeVideo>().Where(v => v.ChannelId == channel.ChannelId).ToList(); 
        }
        public async Task<List<YoutubeVideo>> GetChannelVideosAsync(YoutubeChannel channel) {
            return await dbContextAsync.Table<YoutubeVideo>().Where(v => v.ChannelId == channel.ChannelId).ToListAsync();
        }

        public List<YoutubeChannel> GetSavedChanels() {
            return dbContext.Table<YoutubeChannel>().ToList();
        }

        public async Task<List<YoutubeChannel>> GetSavedChanelsAsync() {
            return await dbContextAsync.Table<YoutubeChannel>().ToListAsync();
        }
    }
}
