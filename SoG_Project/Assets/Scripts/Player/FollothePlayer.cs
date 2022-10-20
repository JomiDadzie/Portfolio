using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollothePlayer : MonoBehaviour {

	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Spieler");
	}
	
	// Update is called once per frame
	void Update () {

		if(player.GetComponent<Bewegung>().Kern == 1)
		gameObject.transform.position = new Vector3(player.transform.position.x,player.transform.position.y  + 0.7f, player.transform.position.z);
		else if (player.GetComponent<Bewegung>().Kern == -1)
			gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.7f, player.transform.position.z);

	}
}
