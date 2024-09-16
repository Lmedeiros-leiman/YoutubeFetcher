using System;
using SQLite;

namespace YoutubeFetcher.Data.Models;

[Table("YoutubeVideo")]
public class YoutubeVideo(){
    [PrimaryKey]
    [AutoIncrement]
    [Column("id")]
    public int RowId {get; set;}

    [Column("videoId")]
    public string VideoId {get; set;} = string.Empty;

    [Column("channelId")]
    public string ChannelId {get; set;} = string.Empty;

    [Column("title")]
    public string Title {get; set;} = string.Empty;

    [Column("description")]
    public string Description {get; set;} = string.Empty;

    [Column("thumbnail")]
    public string Thumbnail {get; set;} = string.Empty;

    [Column("releaseDate")]
    public string ReleaseDate {get; set;} = string.Empty;
    
}
