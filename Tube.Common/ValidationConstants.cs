using System;
using System.Collections.Generic;
using System.Text;

namespace Tube.Common
{
    public class ValidationConstants
    {
        public const string regex = "^[A-Za-z]+$";
        public const string regexErrorMessage = "Please enter text only.";

        // Channel
        public const int channelNameMinimumLegth = 5;
        public const int channelNameMaximumLength = 50;
        public const string channelNameErrorMessage = "The Channel Name must be at least 5 and at max 50 characters long.";

        
        public const int channelDescriptionMinimumLength = 5;
        public const int channelDescriptionMaximumLegth = 200;
        public const string channelDescriptionErrorMessage = "The Channel Description must be at least 5 and at max 200 characters long.";

        //Comment
        public const int commentTextMinimumLength = 1;
        public const int commentTextMaximumLength = 75;
        public const string commentTextErrorMessage = "The Comment must be at least 1 and at max 50 characters long.";

        //Playlist
        public const int playlistNameMinimumLength = 5;
        public const int playlistNameMaximumLength = 25;
        public const string playlistNameErrorMessage = "The Playlist Name must be at least 5 and at max 25 characters long.";

        //TubeUser
        public const int tubeUserNameMinimumLegth = 3;
        public const int tubeUserNameMaximumLength = 20;
        public const string tubeUserFirstNameErrorMessage = "First Name must be at least 3 and at max 20 characters long.";
        public const string tubeUserLastNameErrorMessage = "Last Name must be at least 3 and at max 20 characters long.";

        //Video
        public const int videoNameMinimumLength = 3;
        public const int videoNameMaximumLength = 100;
        public const string videoNameErrorMessage = "The Video Name must be at least 3 and at max 50 characters long.";
    }
}
