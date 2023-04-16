using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
namespace AddressBook.Api.Hubs
{
    public class ContactHub : Hub
    {
        public async Task SendMessage(string message, string contact)
        {
            await Clients.All.SendAsync(message, contact);
        }
    }
}
