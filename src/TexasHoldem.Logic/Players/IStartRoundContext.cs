namespace TexasHoldem.Logic.Players
{
    using System.Collections.Generic;

    using TexasHoldem.Logic.Cards;
    using TexasHoldem.Logic.Pots;

    public interface IStartRoundContext
    {
        IReadOnlyCollection<Card> CommunityCards { get; }

        int CurrentPot { get; }

        int MoneyLeft { get; }

        GameRoundType RoundType { get; }

        Pot CurrentMainPot { get; }

        IReadOnlyCollection<Pot> CurrentSidePots { get; }
    }
}
