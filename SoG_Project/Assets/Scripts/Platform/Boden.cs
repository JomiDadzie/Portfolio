using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boden : MonoBehaviour {

    GameObject spieler;

    private void Start()
    {
        spieler = GameObject.FindGameObjectWithTag("Player");
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == spieler)
        {
            Wandsprung.status = 0;
			if (!Wandsprung.direction && Wandsprung.otherDirection)
			{
				spieler.GetComponent<Rigidbody>().velocity = new Vector3(0, spieler.GetComponent<Rigidbody>().velocity.y, spieler.GetComponent<Rigidbody>().velocity.z);
			}
			if (spieler.GetComponent<Rigidbody>().velocity.y > -15 && spieler.GetComponent<Rigidbody>().velocity.y < -2 && Wandsprung.otherDirection && spieler.GetComponent<Bewegung>().inLuft == true)
			{
				spieler.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, spieler.GetComponent<Rigidbody>().velocity.z);
			}

		}
    }
    void OnCollisionStay(Collision col)
    {
        if (col.gameObject == spieler)
        {
            Wandsprung.status = 0;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject == spieler)
        {
            Wandsprung.status = 0;
        }
    }
}
