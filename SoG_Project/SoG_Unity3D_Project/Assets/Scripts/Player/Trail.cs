using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
	public Material schwarz;
	public Material weis;

	GameObject thePlayer;

	void Start()
	{
		thePlayer = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{
		if (ZeitManip.record || ZeitManip.rewind)
		{
			if (gameObject.GetComponent<TrailRenderer>().material.color == Color.gray)
			{
				gameObject.GetComponent<TrailRenderer>().time = 12f;
			}
			else
				gameObject.GetComponent<TrailRenderer>().time = 0f;
			gameObject.GetComponent<TrailRenderer>().material.color = Color.gray;
			gameObject.GetComponent<TrailRenderer>().endWidth = 1;
			gameObject.GetComponent<TrailRenderer>().startWidth = thePlayer.GetComponent<Transform>().transform.localScale.x;
		}
		else
		{
			gameObject.GetComponent<TrailRenderer>().time = 0.5f;
			gameObject.GetComponent<TrailRenderer>().endWidth = 0;
			gameObject.GetComponent<TrailRenderer>().startWidth = thePlayer.GetComponent<Transform>().transform.localScale.x;
			Changing();
		}

		


	}
	void Changing()
	{

		if (thePlayer.GetComponent<Bewegung>().Kern == 1)
		{
			gameObject.GetComponent<TrailRenderer>().material = schwarz;
		}

		if (thePlayer.GetComponent<Bewegung>().Kern == -1)
		{
			gameObject.GetComponent<TrailRenderer>().material = weis;

		}

	}


}
