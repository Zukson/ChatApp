using ChatApp.Data;
using ChatApp.Files;
using ChatApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChatApp.BackgroundServices
{
    public class ImageBackGroundService : BackgroundService
    {
       private readonly ChannelReader<ImageMessage> _channelReader;
        private readonly IImageManager _imageManager;

        private readonly IServiceProvider _provider;
        public ImageBackGroundService(Channel<ImageMessage>channel, IImageManager imageManager, IServiceProvider provider)
        {
            _channelReader = channel.Reader;
            _imageManager = imageManager;
            _provider = provider;
           

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await _channelReader.WaitToReadAsync())
            {
                try
                {

                    using var scope = _provider.CreateScope();

                    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
                       
                var message = await _channelReader.ReadAsync();

                    string temporaryImagePath = _imageManager.GetAvatarPath(message.Filename, message.Username, ImageType.TemporaryAvatar);
                    string avatarPath = _imageManager.GetAvatarPath(message.Filename, message.Username, ImageType.Avatar);
                    string thumbnailPath = _imageManager.GetAvatarPath(message.Filename, message.Username, ImageType.Thumbnail);
                    await _imageManager.SaveImageAsync(temporaryImagePath, avatarPath, ImageSizes.AvatarSize);
                    await _imageManager.SaveImageAsync(temporaryImagePath, thumbnailPath, ImageSizes.ThumbnailSize);

                 var user  = await db.ChatUsers.FindAsync(message.Username);
                    user.AvatarPath = avatarPath;
                    await db.SaveChangesAsync();
                    _imageManager.DeleteImage(temporaryImagePath);

                }

                catch
                {
                    //TODO add error handlign

                }
                



            }
            
        }
    }
}
