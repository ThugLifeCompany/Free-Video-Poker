using UnityEngine;
using UnityEngine.UI;
using System;

public class GC : MonoBehaviour
{
	[SerializeField] private int money;
	[SerializeField] private int bet;
	[SerializeField] private Sprite defaultCard;
	[SerializeField] private SpriteRenderer[] cardPlaces;
	public CardGenre[] cards;
	[SerializeField] private CardDetails[] playerCards = new CardDetails[5];

	[Header("UIs")]
	[SerializeField] private GameObject deal_Button;
	[SerializeField] private GameObject bet_Button;
	[SerializeField] private InputField bet_Field;
	[SerializeField] private Text result_Text;
	[SerializeField] private Text money_Text;

	private void Update()
	{
		money_Text.text = "$" + money;
	}

	public void DealFirstCards()
	{
		if (bet_Field.text == string.Empty || bet_Field.text == "0")
		{
			money_Text.text = "Invalid Betting, Enter your Bet";
		}

		bet = int.Parse(bet_Field.text);

		if (bet > money)
		{
			result_Text.text = "not enoght money!";
			return;
		}
		else
			money -= bet;

		bet_Field.interactable = false;

		for (int i = 0; i < 5; i++)
		{
			X:
			var genre = UnityEngine.Random.Range(0, 4);
			var number = UnityEngine.Random.Range(0, 13);

			if (cards[genre].cards[number].isUsed)
				goto X;

			playerCards[i] = cards[genre].cards[number];
			cardPlaces[i].sprite = playerCards[i].card;
			playerCards[i].isUsed = true;
		}
	}

	public void HoldCard(int num, bool hold) => playerCards[num].isHolding = hold;

	public void DealSecondCards()
	{
		for (int i = 0; i < 5; i++)
		{
			if (playerCards[i].isHolding)
				continue;
			X:			var genre = UnityEngine.Random.Range(0, 4);
			var number = UnityEngine.Random.Range(0, 13);

			if (cards[genre].cards[number].isUsed)
				goto X;

			playerCards[i] = cards[genre].cards[number];
			cardPlaces[i].sprite = playerCards[i].card;
		}
		
		FinalResults();
	}

	private void FinalResults()
	{
		/* video poker ratio
		 * Royal Flush = 250
		 * Straight Flush = 50
		 * Four of a Kind = 25
		 * Full House = 9
		 * Flush = 6
		 * Straight = 4
		 * Three of a Kind = 3
		 * Two Pair = 2
		 * Jacks or Better = 1
		 */

		bool flush = true;
		bool straight = true;
		bool fourOfAKind = false;
		bool threeOfAKind = false;
		bool twoPairs = false;
		CardDetails[] sortedCards = playerCards;
		Array.Sort(sortedCards, (a, b) => a.cardNumber.CompareTo(b.cardNumber));
		var firstCardGenre = playerCards[0].CardGenres;

		int J = 0;
		int Q = 0;
		int K = 0;
		int A = 0;

		//Get Results
		for (int i = 0; i < 5; i++)
		{
			//Flush Checker
			flush &= playerCards[i].CardGenres == firstCardGenre;

			//Straight Checker
			straight &= sortedCards[i].cardNumber == sortedCards[0].cardNumber + i;

			//Jacks or Better Checker
			if (playerCards[i].cardNumber == 11)
				J++;
			if (playerCards[i].cardNumber == 12)
				Q++;
			if (playerCards[i].cardNumber == 13)
				K++;
			if (playerCards[i].cardNumber == 14)
				A++;
		}

		//each of a kind Pairs Checker
		bool pair = false;
		for (int i = 0; i < 13; i++)
		{
			int tempx = 0;			for (int j = 0; j < 5; j++)
			{
				if (playerCards[j].cardNumber == i + 2)
					tempx++;
			}

			fourOfAKind |= tempx == 4;
			threeOfAKind |= tempx == 3;

			if (tempx == 2)
			{
				if (!pair)					pair = true;
				else
					twoPairs = true;
			}
		}

		//Final Decision
		if (straight && flush && sortedCards[0].cardNumber == 10)
		{
			SetReward(bet * 250, "Royal Flush!");
			return;
		}

		if (straight && flush)
		{
			SetReward(bet * 50, "Straight Flush!");
			return;
		}

		if (fourOfAKind)
		{
			SetReward(bet * 25, "Four of a Kind!");
			return;
		}

		if (threeOfAKind && pair)
		{
			SetReward(bet * 9, "Full House!");
			return;
		}

		if (flush)
		{
			SetReward(bet * 6, "Flush!");
			return;
		}
		
		if (straight)
		{
			SetReward(bet * 4, "Straight!");
			return;
		}

		if (threeOfAKind)
		{
			SetReward(bet * 3, "Three of a Kind!");
			return;
		}

		if (twoPairs)
		{
			SetReward(bet * 2, "Two Pairs!");
			return;
		}

		if (J >= 2 || Q >= 2 || K >= 2 || A >= 2)
		{
			SetReward(bet, "Jacks or Better!");
			return;
		}

		result_Text.text = "You Lost " + bet;
	}

	private void SetReward(int reward, string message)
	{
		money += reward;
		result_Text.text = message + " You Won $" + reward;
	}

	public void PlayAgain()
	{
		deal_Button.SetActive(true);
		bet_Button.SetActive(true);
		bet_Field.interactable = true;
		result_Text.text = "Welcome to Video Poker";

		for (int i = 0; i < 5; i++)
		{
			cardPlaces[i].sprite = defaultCard;
			cardPlaces[i].transform.GetChild(0).GetComponent<HoldButton>().ClearTemp();
		}
	}

	public void GetExtraMoney()
	{
		if (money < 1000)
			money = 1000;
		else
			result_Text.text = "You have enoght Money!";
	}
}
