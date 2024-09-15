using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeFetcher.Properties;

namespace YoutubeFetcher.Data {
    public class Database {
        SQLiteAsyncConnection dbContext;

        async Task Init() {
            if (dbContext is not null) { return; }

            dbContext = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await dbContext.CreateTableAsync<Database>();
        }

        public async Task SaveYoutubeChannelId() {
            await Init();

        }
        public async Task SaveYoutubeChannelVideo() {
            await Init();
        }

        //
        public async Task GetSavedChannels() {
            await Init();

        }
        public async Task GetChannelVideos() {
            await Init();
        }



    }
}
