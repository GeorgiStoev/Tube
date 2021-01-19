using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tube.Common;
using Tube.Services;
using Tube.Web.InputModels.Comment;

namespace Tube.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IChannelService channelService;
        private readonly IUserService userService;
        private readonly IHistoryService historyService;

        public CommentsController(ICommentService commentService, IChannelService channelService, IUserService userService, IHistoryService historyService)
        {
            this.commentService = commentService;
            this.channelService = channelService;
            this.userService = userService;
            this.historyService = historyService;
        }

        [Authorize]
        public IActionResult Add()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(CreateCommentInputModel createCommentInputModel, string id)
        {
            if (!ModelState.IsValid)
            {
                this.RedirectToAction(nameof(Add));
            }

            var username = this.User.Identity.Name;
            var channel = this.channelService.GetChannelByTubeUserName(username);

            var channelPicUrl = string.Empty;
            if (channel == null)
            {
                channelPicUrl = GlobalConstants.dafaultChannelPicUrl;
            }
            else
            {
                channelPicUrl = channel.ChannelPicUrl;
            }

            var user = this.userService.GetTubeUserByUsername(username);
            this.historyService.DeleteLastHistory(user.Id);
            username = user.FirstName + " " + user.LastName;

            this.commentService.CreateComment(createCommentInputModel.Text, username, id, channelPicUrl); ;

            return this.RedirectToAction("Watch", "Videos", new { id });
        }

        [Authorize]
        public IActionResult Like(string id)
        {
            var comment = this.commentService.GetCommentById(id);
            var username = this.User.Identity.Name;
            var user = this.userService.GetTubeUserByUsername(username);
            var userId = user.Id;

            this.historyService.DeleteLastHistory(userId);
            comment = this.commentService.Like(comment, username);

            return this.RedirectToAction("Watch", "Videos" , new { id = comment.VideoId });
        }

        [Authorize]
        public IActionResult UnLike(string id)
        {
            var comment = this.commentService.GetCommentById(id);
            var username = this.User.Identity.Name;
            var user = this.userService.GetTubeUserByUsername(username);
            var userId = user.Id;

            this.historyService.DeleteLastHistory(userId);
            this.commentService.UnLike(comment, username);

            return this.RedirectToAction("Watch", "Videos", new { id = comment.VideoId });
        }
    }
}