using UnityEngine;

public class SetCardsToGC : MonoBehaviour
{
	public Sprite[] geshniz;
	public Sprite[] khesht;
	public Sprite[] del;
	public Sprite[] pik;

	private GC m_GC;

	public void SetCards()
	{
		m_GC = GetComponent<GC>();

		for (int j = 0; j < 13; j++)
		{
			m_GC.cards[0].cards[j].card = geshniz[j];
			m_GC.cards[0].cards[j].cardNumber = j + 2;
			m_GC.cards[0].cards[j].CardGenres = CardGenres.Gishniz;
		}

		for (int j = 0; j < 13; j++)
		{
			m_GC.cards[1].cards[j].card = khesht[j];
			m_GC.cards[1].cards[j].cardNumber = j + 2;
			m_GC.cards[1].cards[j].CardGenres = CardGenres.Khesht;
		}

		for (int j = 0; j < 13; j++)
		{
			m_GC.cards[2].cards[j].card = del[j];
			m_GC.cards[2].cards[j].cardNumber = j + 2;
			m_GC.cards[2].cards[j].CardGenres = CardGenres.Del;
		}

		for (int j = 0; j < 13; j++)
		{
			m_GC.cards[3].cards[j].card = pik[j];
			m_GC.cards[3].cards[j].cardNumber = j + 2;
			m_GC.cards[3].cards[j].CardGenres = CardGenres.Pik;
		}

		Debug.Log("Success!");
	}
}
