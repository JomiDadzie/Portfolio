using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schalter : MonoBehaviour {

	GameObject spieler;
	Vector3 trigger;
	Vector3 triggerAktiviert;
	AudioSource cam;

	public Material rot;
	public Material gruen;
	public Material orange;
	public Material blau;

	public GameObject SchalterBoden;
	public GameObject bewegendeWand;
	public GameObject EndPunkt;
	public float schalterZeit = 5;

	Vector3 startPunkt;
	Vector3 endPunkt;
	
	float distanz;

	public bool deaktivierBar = false;	
	public bool benutzeWand = false;
	public bool benutzeShade = false;
	public bool SpielerForm = false;
	public bool Klein = false;
	public bool Groß = false;

	

	bool aktiviert = false;
	bool deaktivieren = false;

	private void Start()
	{
		spieler = GameObject.Find("Spieler");
		cam = GameObject.Find("Main Camera").GetComponent<AudioSource>();
		trigger = gameObject.transform.localPosition;
		triggerAktiviert = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 0.4f,gameObject.transform.localPosition.z);
		if (benutzeWand)
		{
			endPunkt = EndPunkt.transform.position;
			startPunkt = bewegendeWand.transform.position;
			distanz = Vector3.Distance(endPunkt,startPunkt);
		}
		if (!deaktivierBar && !benutzeShade)
		{
			gameObject.GetComponent<MeshRenderer>().material = blau;
		}
		if (benutzeShade)
		{
			gameObject.GetComponent<MeshRenderer>().material = bewegendeWand.GetComponent<MeshRenderer>().material;	
		}
		if (deaktivierBar && !benutzeShade)
		{
			bewegendeWand.GetComponent<MeshRenderer>().material = rot;
		}
		if (spieler)
		{
			if(Groß)
			SchalterBoden.GetComponent<MeshRenderer>().material.color = Color.black;
			if(Klein)
			SchalterBoden.GetComponent<MeshRenderer>().material.color = Color.white;
		}
	}
	private void OnCollisionEnter(Collision col)
	{
		if (SpielerForm == false || (Klein==false && Groß == false))
		{
			if (col.gameObject == spieler)
			{				
				if (!deaktivieren)
				{
					if(!aktiviert)
					gameObject.GetComponent<AudioSource>().PlayOneShot(Sounds.Trigger);
					aktiviert = true;
				}
			}
		}
		else if (SpielerForm == true && Klein == true)
		{
			if (col.gameObject == spieler && spieler.GetComponent<Bewegung>().Kern == 1)
			{	//Wenn Klein				
				if (!deaktivieren)
				{
					if(!aktiviert)
					gameObject.GetComponent<AudioSource>().PlayOneShot(Sounds.Trigger);
					aktiviert = true;
				}
			}
		}
		else if (SpielerForm == true && Groß == true)
		{
			if (col.gameObject == spieler && spieler.GetComponent<Bewegung>().Kern == -1)
			{   //Wenn groß				
				if (!deaktivieren)
				{
					if(!aktiviert)
					gameObject.GetComponent<AudioSource>().PlayOneShot(Sounds.Trigger);
					aktiviert = true;
				}
			}
		}
	}
	private void Update()
	{
		gameObject.GetComponent<AudioSource>().volume = cam.volume;
	}

	private void FixedUpdate()
	{
		if (deaktivieren && deaktivierBar)
			Deaktivieren();
		if (aktiviert)
			Aktiviert();
	}

	void Aktiviert()
	{
		gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, triggerAktiviert, 0.4f / (schalterZeit / Time.fixedDeltaTime));
		gameObject.GetComponent<MeshRenderer>().material = gruen;
		if (gameObject.transform.localPosition.y <= triggerAktiviert.y)
		{
			gameObject.transform.localPosition = triggerAktiviert;
			deaktivieren = true;
			aktiviert = false;
			if(deaktivierBar)
			gameObject.GetComponent<MeshRenderer>().material = orange;

		}
		if (benutzeWand)
		{
			
			if (Vector3.Distance(gameObject.transform.localPosition, endPunkt) != 0)
				bewegendeWand.transform.position = Vector3.MoveTowards(bewegendeWand.transform.position, endPunkt, distanz / (schalterZeit / Time.fixedDeltaTime));
		}

	}
	void Deaktivieren()
	{
		gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, trigger, 0.4f / (schalterZeit / Time.fixedDeltaTime));
		if(gameObject.transform.localPosition.y >= trigger.y - 0.001)
		{
			gameObject.transform.localPosition = trigger;
			deaktivieren = false;
			if(!benutzeShade)
			gameObject.GetComponent<MeshRenderer>().material = rot;
			else
				gameObject.GetComponent<MeshRenderer>().material = bewegendeWand.GetComponent<MeshRenderer>().material;

		}
		if (benutzeWand)
		{
			
			if (Vector3.Distance(gameObject.transform.localPosition, startPunkt) != 0)
				bewegendeWand.transform.position = Vector3.MoveTowards(bewegendeWand.transform.position, startPunkt, distanz / (schalterZeit / Time.fixedDeltaTime));
		}

	}
}
