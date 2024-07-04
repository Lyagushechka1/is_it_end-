using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Game_
{
    public class Card
    {
        public int Value { get; set; }
        public string Suit { get; set; }
        public Card(int value, string suit)
        {
            Value = value;
            Suit = suit;
        }
        public override string ToString()
        {
            return $"{Value} {Suit}";
        }
    }
    public class Deck
    {
        List<Card> cards = new List<Card>();
        public Deck()
        {
            string[] suits = { "❤️", "♦️", "♣️", "♠️" };
            for (int i = 0; i <= 13; i++)
            {
                foreach (string suit in suits)
                {
                    cards.Add(new Card(i, suit));
                }
            }
            Shuffle();
        }
        public void Shuffle()
        {
            Random random = new Random();
            cards = cards.OrderBy(c => random.Next()).ToList();
        }
        public List<Card> Deal(int numberOfCards)
        {
            List<Card> hand = cards.Take(numberOfCards).ToList();
            cards.RemoveRange(0, numberOfCards);
            return hand;
        }
    }
    public class WarGame
    {
        private List<Card> player1Deck;
        private List<Card> player2Deck;

        public WarGame()
        {
            Deck deck = new Deck();
            player1Deck = new List<Card>(deck.Deal(26));
            player2Deck = new List<Card>(deck.Deal(26));
        }

        public (Card, Card, string) PlayRound()
        {
            if (player1Deck.Count == 0 || player2Deck.Count == 0)
            {
                return (null, null, "Game over!");
            }

            Card player1Card = player1Deck[0];
            Card player2Card = player2Deck[0];
            player1Deck.RemoveAt(0);
            player2Deck.RemoveAt(0);
            string result;
            // это сложно объяснить, тут все видно
            if (player1Card.Value > player2Card.Value)
            {
                player1Deck.Add(player1Card);
                player1Deck.Add(player2Card);
                result = "Player 1 wins this round!";
            }
            else if (player1Card.Value < player2Card.Value)
            {
                player2Deck.Add(player1Card);
                player2Deck.Add(player2Card);
                result = "Player 2 wins this round!";
            }
            else
            {
                result = "War!";
                if (player1Deck.Count < 2 || player2Deck.Count < 2)
                {
                    result += " Not enough cards to continue the war. Game over!";
                    player1Deck.Clear();
                    player2Deck.Clear();
                }
                else
                {
                    List<Card> warPile = new List<Card> { player1Card, player2Card };
                    //---------------------------------------------//
                    Card player1WarDown = player1Deck[0];
                    Card player2WarDown = player2Deck[0];
                    player1Deck.RemoveAt(0);
                    player2Deck.RemoveAt(0);
                    // берет и удаляет верхнюю карту
                    //---------------------------------------------//
                    Card player1WarUp = player1Deck[0];
                    Card player2WarUp = player2Deck[0];
                    player1Deck.RemoveAt(0);
                    player2Deck.RemoveAt(0);
                    // берет верхнию следующую карту для сравнения и удаляет её
                    //---------------------------------------------//
                    warPile.Add(player1WarDown);
                    warPile.Add(player2WarDown);
                    warPile.Add(player1WarUp);
                    warPile.Add(player2WarUp);

                    if (player1WarUp.Value > player2WarUp.Value)
                    {
                        player1Deck.AddRange(warPile);//AddRange - переносит элемент с одной колекциы в другую
                        result += " Player 1 wins the war!";
                    }
                    else if (player1WarUp.Value < player2WarUp.Value)
                    {
                        player2Deck.AddRange(warPile);
                        result += " Player 2 wins the war!";
                    }
                    else
                    {
                        result += " The war resulted in a tie. No one wins this war.";
                    }
                }
            }

            return (player1Card, player2Card, result);
        }

        public int GetPlayer1DeckCount()
        {
            return player1Deck.Count;
        }

        public int GetPlayer2DeckCount()
        {
            return player2Deck.Count;
        }
    }
}