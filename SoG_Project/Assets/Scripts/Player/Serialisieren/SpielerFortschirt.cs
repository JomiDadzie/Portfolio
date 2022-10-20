using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//[System.Serializable]
public class SpielerFortschirt
{
	//public string ;
	//public List<LevelListen> fortschrit = new List<LevelListen>();
	public string playerName;
	public string levelName;
	public int levelIndex;

	public SpielerFortschirt(string levelN, int levelIn)
	{
		levelName = levelN;
		levelIndex = levelIn;

		//return Fortschrit();
	}
}