using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApi1
{
    [Authorize]
    public class MySignalRHub:Hub
    {

        public override Task OnConnectedAsync()
        {
            var data = Context.User.Identity.IsAuthenticated;


            //if(data)
            //{
            //    throw new Exception("Fcking code");
            //}

            return base.OnConnectedAsync();
        }

        //public async Task SendMessage()
        //{
        //    await Clients.All.SendAsync("GetMessageFromFub", " This is SignalR message");
        //}
    }
}
