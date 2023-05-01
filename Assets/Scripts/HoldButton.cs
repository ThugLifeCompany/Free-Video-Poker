using UnityEngine;

public class HoldButton : MonoBehaviour
{
	[SerializeField] private int num;
	private bool isHolding;

	private GC m_GC;

	private void Start()
	{
		m_GC = GameObject.Find("GC").GetComponent<GC>();
	}

	private void OnMouseDown()
	{
		isHolding = !isHolding;
		m_GC.HoldCard(num, isHolding);
		GetComponent<SpriteRenderer>().color = isHolding ? Color.yellow : Color.grey;
	}

	public void ClearTemp()
	{
		isHolding = false;
		m_GC.HoldCard(num, false);
		GetComponent<SpriteRenderer>().color = Color.grey;
	}
}
