namespace TexasHoldem.AI.DummyPlayer
{
    using System;

    using TexasHoldem.Logic.Players;

    internal class AlwaysCallDummyPlayer : BasePlayer
    {
        public AlwaysCallDummyPlayer()
        {
            this.money = 1000;
        }

        public override string Name { get; } = "AlwaysCallDummyPlayer_" + Guid.NewGuid();

        public override PlayerAction PostingBlind(IPostingBlindContext context)
        {
            return context.BlindAction;
        }

        public override PlayerAction GetTurn(IGetTurnContext context)
        {
            return PlayerAction.CheckOrCall();
        }
    }
}
