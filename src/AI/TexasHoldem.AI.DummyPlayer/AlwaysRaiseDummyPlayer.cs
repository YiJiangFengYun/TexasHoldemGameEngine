namespace TexasHoldem.AI.DummyPlayer
{
    using System;

    using TexasHoldem.Logic.Players;

    internal class AlwaysRaiseDummyPlayer : BasePlayer
    {
        public AlwaysRaiseDummyPlayer()
        {
            this.money = 1000;
        }

        public override string Name { get; } = "AlwaysRaiseDummyPlayer_" + Guid.NewGuid();

        public override PlayerAction PostingBlind(IPostingBlindContext context)
        {
            return context.BlindAction;
        }

        public override PlayerAction GetTurn(IGetTurnContext context)
        {
            return PlayerAction.Raise(context.MoneyLeft - context.MoneyToCall);
        }
    }
}
