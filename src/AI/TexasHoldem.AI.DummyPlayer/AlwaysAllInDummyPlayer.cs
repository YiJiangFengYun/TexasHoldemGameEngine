namespace TexasHoldem.AI.DummyPlayer
{
    using System;

    using TexasHoldem.Logic.Players;

    internal class AlwaysAllInDummyPlayer : BasePlayer
    {

        public AlwaysAllInDummyPlayer()
        {
            this.money = 1000;
        }

        public override string Name { get; } = "AlwaysAllInDummyPlayer_" + Guid.NewGuid();

        public override PlayerAction PostingBlind(IPostingBlindContext context)
        {
            return context.BlindAction;
        }

        public override PlayerAction GetTurn(IGetTurnContext context)
        {
            if (context.MoneyLeft > 0)
            {
                return PlayerAction.Raise(context.MoneyLeft - context.MoneyToCall);
            }
            else
            {
                return PlayerAction.CheckOrCall();
            }
        }
    }
}
