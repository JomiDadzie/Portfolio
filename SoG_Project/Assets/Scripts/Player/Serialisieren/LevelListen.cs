using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelListen : MonoBehaviour
{	
	//public static LevelListen lvllisten;
	//int AnzhalderScenen = 0;
	///////////////////////////////////////////////////////////////////////////////////
	//// Um die Level auf den Plakten nieder zu schreiben wird dieses Script benötigt//
	///////////////////////////////////////////////////////////////////////////////////
	//public string levelName;
	//public int levelIndex;

	//public LevelListen(string levelN, int levelIn)
	//{
	//	levelName = levelN;
	//	levelIndex = levelIn;
	//}
	//private void Awake()
	//{
	//	AnzhalderScenen = SceneManager.sceneCountInBuildSettings;
	//																											///////////////////////////////////////////////////////////////////
	//																											// DIESE LISTE SOLLTE ERST AB DEM ERSTEN LEVEL HOCHGEZÄHLT WERDEN//
	//																											///////////////////////////////////////////////////////////////////
	//	List<SpielerFortschirt> fortschrit = new List<SpielerFortschirt>();

	//	int[] SceneIndex = new int[SceneManager.sceneCountInBuildSettings];
	//	string[] SceneName = new string[SceneManager.sceneCountInBuildSettings];

	//	for (int i = 0; i < AnzhalderScenen; i++)
	//	{
	//		SceneName[i] = SceneUtility.GetScenePathByBuildIndex(i).Substring(0, SceneUtility.GetScenePathByBuildIndex(i).Length - 6).Substring(SceneUtility.GetScenePathByBuildIndex(i).LastIndexOf('/') + 1);
	//		SceneIndex[i] =  i;
	//		fortschrit.Add(new SpielerFortschirt(SceneName[i],SceneIndex[i]));

	//		//Debug.Log("Name: " + SceneName[i] + ", Count: " + SceneIndex[i]);
			
	//		Debug.Log("List: " + fortschrit[i].levelName + " ist Level: " + fortschrit[i].levelIndex);
	//	}
	//	fortschrit.RemoveRange(0,2);
	//	//Debug.Log(fortschrit.Count);
	//	//for (int i = 0; i < fortschrit.Count; i++)
	//	//{
	//	//	Debug.Log("List: " + fortschrit[i].levelName + " = " + fortschrit[i].levelIndex);
	//	//}
	//}

}
