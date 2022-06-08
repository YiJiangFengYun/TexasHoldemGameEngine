﻿namespace TexasHoldem.Logic.GameMechanics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TexasHoldem.Logic.Players;

    public class TexasHoldemGame : ITexasHoldemGame
    {
        protected static readonly int[] SmallBlinds =
            {
                1, 2, 3, 5, 10, 15, 20, 25, 30, 40, 50, 60, 80, 100, 150, 200, 300,
                400, 500, 600, 800, 1000, 1500, 2000, 3000, 4000, 5000, 6000, 8000,
                10000, 15000, 20000, 30000, 40000, 50000, 60000, 80000, 100000,
            };

        private readonly ICollection<InternalPlayer> allPlayers;

        public TexasHoldemGame(IPlayer firstPlayer, IPlayer secondPlayer)
            : this(new[] { firstPlayer, secondPlayer })
        {
            if (firstPlayer == null)
            {
                throw new ArgumentNullException(nameof(firstPlayer));
            }

            if (secondPlayer == null)
            {
                throw new ArgumentNullException(nameof(secondPlayer));
            }

            // Ensure the players have unique names
            if (firstPlayer.Name == secondPlayer.Name)
            {
                throw new ArgumentException($"Both players have the same name: \"{firstPlayer.Name}\"");
            }
        }

        public TexasHoldemGame(IList<IPlayer> players)
            : this((ICollection<IPlayer>)players)
        {
            // Ensure the players have unique names
            var duplicateNames = players.GroupBy(x => x)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key.Name);

            if (duplicateNames.Count() > 0)
            {
                throw new ArgumentException($"Players have the same name: \"{string.Join(" ", duplicateNames.ToArray())}\"");
            }
        }

        private TexasHoldemGame(ICollection<IPlayer> players)
        {
            if (players == null)
            {
                throw new ArgumentNullException(nameof(players));
            }

            if (players.Count < 2 || players.Count > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(players), "The number of players must be from 2 to 10");
            }

            this.allPlayers = new List<InternalPlayer>(players.Count);
            foreach (var item in players)
            {
                this.allPlayers.Add(new InternalPlayer(item));
            }

            this.HandsPlayed = 0;
        }

        public int HandsPlayed { get; private set; }

        public IPlayer Start()
        {
            var playerNames = this.allPlayers.Select(x => x.Name).ToList().AsReadOnly();
            foreach (var player in this.allPlayers)
            {
                player.StartGame(new StartGameContext(playerNames));
            }

            this.PlayGame();

            var winner = this.allPlayers.WithMoney().FirstOrDefault();
            foreach (var player in this.allPlayers)
            {
                player.EndGame(new EndGameContext(winner.Name));
            }

            return winner;
        }

        private void PlayGame()
        {
            var shifted = this.allPlayers.ToList();

            // While at least two players have money
            while (this.allPlayers.WithMoney().Count() > 1)
            {
                this.HandsPlayed++;

                // Every 10 hands the blind increases
                // var smallBlind = SmallBlinds[(this.HandsPlayed - 1) / 10];
                var smallBlind = SmallBlinds[0];

                // Players are shifted in order of priority to make a move
                shifted = shifted.WithMoney().ToList();
                shifted.Add(shifted.First());
                shifted.RemoveAt(0);

                // Rotate players
                IHandLogic hand = new HandLogic(shifted, this.HandsPlayed, smallBlind);

                hand.Play();

            }
        }
    }
}
