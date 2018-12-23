using System;
using Voltaic.Logging;

namespace Pugster.Bridge.Logging
{
    public class DiscordLogManager : LogManager
    {
        public DiscordLogManager(LogSeverity logSeverity) : base(logSeverity)
        {
            Output += OnOutput;
        }

        private void OnOutput(LogMessage obj)
        {
            Console.WriteLine(obj);
        }
    }
}
