namespace TexasHoldem.AI.DummyPlayer
{
    using System;

    using TexasHoldem.Logic.Players;

    internal class AlwaysFoldDummyPlayer : BasePlayer
    {

        public AlwaysFoldDummyPlayer()
        {
            this.money = 1000;
        }

        public override string Name { get; } = "AlwaysFoldDummyPlayer_" + Guid.NewGuid();

        public override PlayerAction PostingBlind(IPostingBlindContext context)
        {
            return context.BlindAction;
        }

        public override PlayerAction GetTurn(IGetTurnContext context)
        {
            return PlayerAction.Fold();
        }
    }
}
