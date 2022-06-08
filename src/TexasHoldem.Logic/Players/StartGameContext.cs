namespace TexasHoldem.Logic.Players
{
    using System.Collections.Generic;

    public class StartGameContext : IStartGameContext
    {
        public StartGameContext(IReadOnlyCollection<string> playerNames)
        {
            this.PlayerNames = playerNames;
        }

        public IReadOnlyCollection<string> PlayerNames { get; }
    }
}
