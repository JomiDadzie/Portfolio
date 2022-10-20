using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLevelSpawner : MonoBehaviour {

	GameObject spur;
	GameObject spieler;
	GameObject cam;

	// Use this for initialization
	private void Awake()
	{
		cam = GameObject.Find("Main Camera");
		spieler = GameObject.Find("Spieler");
		spur = GameObject.Find("Trail");
		gameObject.transform.position = LastLevelPlayed.letztesLevel;
		
	}
	void Start()
	{
		spur.SetActive(false);
		cam.transform.position = new Vector3(LastLevelPlayed.letztesLevel.x, LastLevelPlayed.letztesLevel.y, cam.transform.position.z);
		spieler.transform.position = new Vector3(LastLevelPlayed.letztesLevel.x, LastLevelPlayed.letztesLevel.y, spieler.transform.position.z);
		spur.SetActive(true);

	}

}

public static class LastLevelPlayed
{
	public static Vector3 letztesLevel = new Vector3(-5.5f,-8,-0.5f); 
	
}
