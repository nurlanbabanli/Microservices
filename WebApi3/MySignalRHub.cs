using Microsoft.AspNetCore.SignalR;

namespace WebApi3
{
    public class MySignalRHub:Hub
    {
        public override Task OnConnectedAsync()
        {
            var data = Context.User.Identity.IsAuthenticated;

            if(data)
            {
                throw new Exception("This is the end");
            }

            return base.OnConnectedAsync();
        }
    }
}
