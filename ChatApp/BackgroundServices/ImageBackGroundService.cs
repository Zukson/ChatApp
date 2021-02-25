using ChatApp.Files;
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
        public ImageBackGroundService(Channel<ImageMessage>channel, IImageManager imageManager)
        {
            _channelReader = channel.Reader;
            _imageManager = imageManager;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await _channelReader.WaitToReadAsync())
            {
                var message = await _channelReader.ReadAsync();

                string temporaryImagePath = _imageManager.GetAvatarPath(message.Filename, message.Username, ImageType.TemporaryAvatar);
                string avatarPath = _imageManager.GetAvatarPath(message.Filename, message.Username, ImageType.Avatar);
                string thumbnailPath = _imageManager.GetAvatarPath(message.Filename, message.Username, ImageType.Thumbnail);
                await _imageManager.SaveImageAsync(temporaryImagePath, avatarPath, ImageSizes.AvatarSize);
                await _imageManager.SaveImageAsync(temporaryImagePath, thumbnailPath, ImageSizes.ThumbnailSize);

                 _imageManager.DeleteImage(temporaryImagePath);



            }
            
        }
    }
}
