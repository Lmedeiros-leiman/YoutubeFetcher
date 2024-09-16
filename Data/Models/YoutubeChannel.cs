using System;
using Google.Apis.YouTube.v3.Data;
using SQLite;

namespace YoutubeFetcher.Data.Models;

[Table("channels")]
public class YoutubeChannel {
    
    [PrimaryKey]
    [AutoIncrement]
    [Column("id")]
    public int RowId {get; set;}

    [Column("ChannelId")]
    public string ChannelId {get; set;} = string.Empty;

    [Column("title")]
    public  string ChannelTitle {get; set;} = string.Empty;
    [Column("description")]
    public  string Description {get; set;} = string.Empty;
    
    [Column("thumbnailUrl")]
    public string Thumbnail {get; set;} = string.Empty;
    
    //
    //



    //
    //
    [Column("creationDate")]
    public  string PublishedAt {get; set;} = string.Empty;
}
