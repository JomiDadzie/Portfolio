using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RWand : MonoBehaviour {

    GameObject spieler;
	Color startFarbe;

	private void Start()
    {
        spieler = GameObject.FindGameObjectWithTag("Player");
		startFarbe = gameObject.GetComponent<MeshRenderer>().material.color;
	}
    void OnCollisionEnter(Collision col)
    {
		if (col.gameObject == spieler)
		{
			Wandsprung.status = 2;
			if (!Wandsprung.direction && Wandsprung.otherDirection)
			{
				spieler.GetComponent<Rigidbody>().velocity = new Vector3(0, spieler.GetComponent<Rigidbody>().velocity.y, spieler.GetComponent<Rigidbody>().velocity.z);
			}
			if (spieler.GetComponent<Rigidbody>().velocity.y > -15 && spieler.GetComponent<Rigidbody>().velocity.y < -2 && Wandsprung.otherDirection && spieler.GetComponent<Bewegung>().inLuft == true)
			{
				spieler.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, spieler.GetComponent<Rigidbody>().velocity.z);
			}
			if(Wandsprung.status == 2)
			gameObject.GetComponent<MeshRenderer>().material.color = new Color(gameObject.GetComponent<MeshRenderer>().material.color.r + 0.15f, gameObject.GetComponent<MeshRenderer>().material.color.g + 0.15f, gameObject.GetComponent<MeshRenderer>().material.color.b + 0.15f);
		}
	}
    void OnCollisionStay(Collision col)
    {
        if (col.gameObject == spieler)
        {
			
            Wandsprung.status = 2;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject == spieler)
        {
            Wandsprung.status = 0;
			gameObject.GetComponent<MeshRenderer>().material.color = startFarbe;
		}
    }
}
