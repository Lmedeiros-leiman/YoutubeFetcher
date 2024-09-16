using System;
using SQLite;

namespace YoutubeFetcher.Data.Models;

[Table("YoutubeVideo")]
public class YoutubeVideo(){
    [PrimaryKey]
    [AutoIncrement]
    [Column("id")]
    public string RowId {get; set;} = string.Empty;

    public string VideoId {get; set;} = string.Empty;
    public string ChannelId {get; set;} = string.Empty;
    
}
