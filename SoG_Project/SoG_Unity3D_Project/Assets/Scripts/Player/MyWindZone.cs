using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyWindZone : MonoBehaviour {
	GameObject Player;
	WindZone myWindzone;
	float windStärke;

	float newValue;

	void Start ()
	{
		Player = GameObject.Find("Spieler");
		myWindzone = GetComponentInChildren<WindZone>();
	}
	

	void Update ()
	{
	
		windStärke = Rounding(Player.GetComponent<Rigidbody>().velocity.x, 100);
		WindZoneChange();
	}
	private void LateUpdate()
	{
		//<--------------------------------------------------Coroutine für ein Impact auf der Y axe
		if (windStärke > 0)
			transform.position = new Vector3(Player.transform.position.x + 1f, Player.transform.position.y + 0.1f, Player.transform.position.z);
		else if (windStärke < 0)
			transform.position = new Vector3(Player.transform.position.x - 1f, Player.transform.position.y + 0.1f, Player.transform.position.z);
		else
			transform.position = Player.transform.position;
	}
	void WindZoneChange()
	{

		if (Mathf.Abs(windStärke) > 4)
			myWindzone.windMain = Mathf.Abs(windStärke);
		else
			myWindzone.windMain = 0;
	}
	float Rounding(float value, float _nachkommastelle)
	{
		newValue = value;
		newValue = newValue * _nachkommastelle;
		newValue = Mathf.Round(newValue);
		value = newValue / _nachkommastelle;
		return value;
	}
}
