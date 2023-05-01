using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SetCardsToGC))]
public class SetCardsToGCEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		SetCardsToGC code = (SetCardsToGC)target;

		GUILayout.Space(5);

		if (GUILayout.Button("Set", GUILayout.Height(30)))
			code.SetCards();
	}
}
