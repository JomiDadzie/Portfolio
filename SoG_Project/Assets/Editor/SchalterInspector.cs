using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

[CustomEditor(typeof(Schalter))]
[CanEditMultipleObjects]
public class SchalterInspector : Editor {

	Schalter schalter;
	SerializedProperty BewegbareWand;
	SerializedProperty schalterZeit;
	SerializedProperty deaktivierBar;
	SerializedProperty benutzeWand;
	SerializedProperty EndPunkt;
	SerializedProperty benutzeShade;
	SerializedProperty SpielerForm;
	SerializedProperty Klein;
	SerializedProperty Groß;
	private void OnEnable()
	{
		schalter = (Schalter)target;
		BewegbareWand = serializedObject.FindProperty("bewegendeWand");
		EndPunkt = serializedObject.FindProperty("EndPunkt");
		schalterZeit = serializedObject.FindProperty("schalterZeit");
		deaktivierBar = serializedObject.FindProperty("deaktivierBar");
		benutzeWand = serializedObject.FindProperty("benutzeWand");
		benutzeShade = serializedObject.FindProperty("benutzeShade");
		SpielerForm = serializedObject.FindProperty("SpielerForm");
		Klein = serializedObject.FindProperty("Klein");
		Groß = serializedObject.FindProperty("Groß");
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		if (Klein.boolValue)
			Groß.boolValue = false;
		if (Groß.boolValue)
			Klein.boolValue = false;
		EditorGUILayout.BeginVertical();

		EditorGUILayout.PropertyField(deaktivierBar);
		EditorGUILayout.PropertyField(benutzeWand);
		if (benutzeWand.boolValue)
		{
			EditorGUILayout.PropertyField(benutzeShade);
			EditorGUILayout.PropertyField(BewegbareWand);
			EditorGUILayout.PropertyField(EndPunkt);
			EditorGUILayout.PropertyField(schalterZeit);
			if(deaktivierBar.boolValue)
			EditorGUILayout.HelpBox("Den Weg zurück entspricht der Schalter Zeit x 2!", MessageType.Info);
		}
		EditorGUILayout.PropertyField(SpielerForm);
		if (SpielerForm.boolValue)
		{
			EditorGUILayout.PropertyField(Klein);
			EditorGUILayout.PropertyField(Groß);
		}
		EditorGUILayout.EndVertical();
		serializedObject.ApplyModifiedProperties();
	}

}
