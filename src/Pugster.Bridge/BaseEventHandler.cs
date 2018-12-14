using Microsoft.AspNetCore.SignalR.Client;
using System.IO;

namespace Pugster.Bridge
{
    public abstract class BaseEventHandler
    {
        protected readonly HubConnection _hub;

        public BaseEventHandler(string hubUrl)
        {
            _hub = new HubConnectionBuilder()
                .WithUrl(Path.Combine("https://localhost:44393/", hubUrl))
                .Build();
        }
    }
}
