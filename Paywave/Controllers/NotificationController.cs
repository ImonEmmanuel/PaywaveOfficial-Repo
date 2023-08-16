using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaywaveAPICore.Authentication;
using PaywaveAPICore.Processor;
using PaywaveAPIData.Response.Auth;
using PaywaveAPIData.Response;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PaywaveAPIData.Model;

namespace Paywave.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        private NotificationProcessor _notificationProcessor;
        public NotificationController(NotificationProcessor notificationProcessor)
        {
            _notificationProcessor = notificationProcessor;
        }

        [HttpGet("GetNotifications/{userId}")]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<Notification>>), 200)]
        public IActionResult GetNotifications()
        {
            var userId = User.FindFirstValue("ID").ToString();
            if (userId is null)
            {
                return Unauthorized("Invalid Token");
            }
            var message = _notificationProcessor.GetNotifications(userId);
            return HandleActionResult(message);
        }
        [HttpPut("{notificationId}/[action]")]
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        public IActionResult UpdateReadStatus([Required][FromRoute(Name = "notificationId")] string notificationId, [Required] bool isRead)
        {
            var userId = User.FindFirstValue("ID").ToString();
            if (userId is null)
            {
                return Unauthorized("Invalid Token");
            }
            return HandleActionResult(_notificationProcessor.UpdateReadStatus(userId, notificationId, isRead));
        }
        [HttpDelete("{notificationId}")]
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        public IActionResult DeleteNotification([Required][FromRoute] string notificationId)
        {
            var userId = User.FindFirstValue("ID").ToString();
            if (userId is null)
            {
                return Unauthorized("Invalid Token");
            }
            return HandleActionResult(_notificationProcessor.DeleteNotification(notificationId, userId));
        }
    }
}
