using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tube.Data.Models;

namespace Tube.Data
{
    public class TubeDbContext : IdentityDbContext<TubeUser>
    {
        public TubeDbContext(DbContextOptions<TubeDbContext> options)
            : base(options)
        {

        }

        public TubeDbContext()
        {

        }

        public DbSet<Video> Videos { get; set; }

        public DbSet<Channel> Channels { get; set; }

        public DbSet<TubeUserChannel> TubeUserChannels { get; set; }

        public DbSet<VideoPlaylist> VideoPlaylists { get; set; }

        public DbSet<Playlist> Playlists { get; set; }

        public DbSet<History> Histories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TubeUserChannel>()
                .HasKey(tubeUserChannel => new { tubeUserChannel.TubeUserId, tubeUserChannel.ChannelId });
            builder.Entity<TubeUserChannel>()
                .HasOne(tubeUserChannel => tubeUserChannel.TubeUser)
                .WithMany(tubeUserChannel => tubeUserChannel.Subscriptions)
                .HasForeignKey(tubeUserChannel => tubeUserChannel.TubeUserId);
            builder.Entity<TubeUserChannel>()
                .HasOne(tubeUserChannel => tubeUserChannel.Channel)
                .WithMany(tubeUserChannel => tubeUserChannel.Subscribes)
                .HasForeignKey(tubeUserChannel => tubeUserChannel.ChannelId);

            builder.Entity<VideoPlaylist>()
                .HasKey(videoPlaylist => new { videoPlaylist.VideoId, videoPlaylist.PlaylistId });
            builder.Entity<VideoPlaylist>()
                .HasOne(videoPlaylist => videoPlaylist.Video)
                .WithMany(videoPlaylist => videoPlaylist.Playlists)
                .HasForeignKey(videoPlaylist => videoPlaylist.VideoId);
            builder.Entity<VideoPlaylist>()
                .HasOne(videoPlaylist => videoPlaylist.Playlist)
                .WithMany(videoPlaylist => videoPlaylist.Videos)
                .HasForeignKey(videoPlaylist => videoPlaylist.PlaylistId);

            builder.Entity<TubeUser>()
                .HasOne(tubeUser => tubeUser.Channel)
                .WithOne(channel => channel.TubeUser)
                .HasForeignKey<Channel>(channel => channel.TubeUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Channel>()
                .HasOne(channel => channel.TubeUser)
                .WithOne(user => user.Channel)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TubeUser>()
                .HasMany(tubeUser => tubeUser.Playlist)
                .WithOne(playlist => playlist.TubeUser)
                .HasForeignKey(playlist => playlist.TubeUserId);

            builder.Entity<TubeUser>()
                .HasMany(tubeUser => tubeUser.History)
                .WithOne(history => history.TubeUser)
                .HasForeignKey(history => history.TubeUserId);


            builder.Entity<Video>()
                .HasMany(video => video.Comments)
                .WithOne(comment => comment.Video)
                .HasForeignKey(comment => comment.VideoId);

            builder.Entity<Video>()
               .HasMany(video => video.Likes)
               .WithOne(vote => vote.Video)
               .HasForeignKey(vote => vote.VideoId);

            builder.Entity<Comment>()
               .HasMany(comment => comment.Likes)
               .WithOne(vote => vote.Comment)
               .HasForeignKey(vote => vote.CommentId);

            base.OnModelCreating(builder);  
        }
    }
}
