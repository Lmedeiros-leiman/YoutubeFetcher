using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeFetcher.Resources.Models {
    public class YoutubeVideo {
        public required Google.Apis.YouTube.v3.Data.SearchResult videoData { get; set; }

        public bool IsDownloadable { get; set; } = false;


    }
}
