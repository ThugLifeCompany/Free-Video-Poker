using UnityEngine;

public enum CardGenres { Gishniz, Khesht, Del, Pik }

[System.Serializable]
public class CardDetails
{
	public Sprite card;
	public int cardNumber;
	public bool isUsed;
	public bool isHolding;
	public CardGenres CardGenres;
}

[System.Serializable]
public class CardGenre
{
	public CardDetails[] cards;
}
