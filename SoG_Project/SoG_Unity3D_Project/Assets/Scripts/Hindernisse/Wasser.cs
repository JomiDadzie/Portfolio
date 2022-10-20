using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasser : MonoBehaviour
{
	GameObject player;
	GameObject wasserOberFläche;

	Rigidbody myRigidbody;

	ParticleSystem.MainModule mainParticle;
	ParticleSystem.EmissionModule emissionParticale;
	ParticleSystem.SizeBySpeedModule sbsParticale;
	ParticleSystem.ShapeModule shapeParticle;

	public float bigDrag;
	public float smallDrag;
	public float angularDrag;
	public float myforce;
	
	#region WaterGrind
	bool stop = true;
	bool coroutine = false;
	bool walkOnWater = false;
	#endregion

	float myDrag;
	float myTime;

	private void Awake()
	{
		if(gameObject.GetComponent<MeshRenderer>().enabled == true)
		gameObject.GetComponent<MeshRenderer>().enabled = false;
		wasserOberFläche = GameObject.Find("Surface");
		player = GameObject.Find("Spieler");
		mainParticle = GetComponentInChildren<ParticleSystem>().main;
		emissionParticale = GetComponentInChildren<ParticleSystem>().emission;
		sbsParticale = GetComponentInChildren<ParticleSystem>().sizeBySpeed;
		shapeParticle = GetComponentInChildren<ParticleSystem>().shape;
	}

	private void Start()
	{
		myTime = 1 / Time.fixedDeltaTime;
		SetPlayerValues();
		SetParticleValues();
	}
	#region Updates
	private void Update()
	{
		WasserDistanz.distanz = Vector3.Distance(wasserOberFläche.transform.position, player.transform.position);

		Debug.Log("walk: " + Wandsprung.wasserGrind + " dir: " + Wandsprung.direction + " coroutine: " + coroutine + " Distanz: " + WasserDistanz.distanz);
	}
	private void FixedUpdate()
	{
		
			if (coroutine && Wandsprung.wasserGrind == true && Wandsprung.otherDirection == false && Wandsprung.wasserGrind == true && stop == false)
				StartCoroutine(GehZurOberF());
		if (!stop)
		{
			if (walkOnWater && Wandsprung.otherDirection == false && WasserDistanz.wasserlaufen)
				player.transform.position = new Vector3(player.transform.position.x, wasserOberFläche.transform.position.y + 0.3f, -0.5f);
		}

	}
	#endregion
	#region Trigger
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
		{
			stop = false;
		}
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject == player)
		{
			InWater();
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player)
		{
			Resetten();
		}
	}
	#endregion
	#region Values
	void SetParticleValues()
	{
		emissionParticale.rateOverTime = transform.localScale.x * 2;
		shapeParticle.scale = new Vector3(0.5f,1,1);
		mainParticle.startLifetime = new ParticleSystem.MinMaxCurve(0.1f, transform.localScale.y/4);
		mainParticle.startSpeed = new ParticleSystem.MinMaxCurve(0.1f, transform.localScale.y/4);
		mainParticle.startSize = new ParticleSystem.MinMaxCurve(0.1f, transform.localScale.y/4);
		sbsParticale.range = new Vector2(0.1f, transform.localScale.y / 4);
	}
	void SetPlayerValues()
	{
		myRigidbody = player.GetComponent<Rigidbody>();
		myDrag = player.GetComponent<Rigidbody>().drag;
	}
	#endregion
	#region MyFunctions
	void InWater()
	{
		
		wasserOberFläche.transform.position = new Vector3(player.transform.position.x,wasserOberFläche.transform.position.y, -0.5f);//damit die distanz vom spieler zur oberfläche gleich bleibt
		if (player.GetComponent<Bewegung>().Kern == 1)									  //wenn klein
		{
			if(!walkOnWater)
			myRigidbody.AddForce(0, myforce, 0);										  //gibt es auftriebt																	  
			if (Wandsprung.otherDirection == false || Mathf.Abs(Input.GetAxis("Horizontal")) < 0.1f)										  //wenn rechts und links kein rycast
				myRigidbody.useGravity = false;											  //
			else																		  //
			{																			  //
				stop = true;															  //kein grinden
				myRigidbody.useGravity = true;											  //damit der spieler abtuacht
			}
			if (Mathf.Abs(myRigidbody.velocity.y) < 1 && WasserDistanz.distanz < 1)		  //wenn der spieler nah der wasser oberfläche ist
			{																			  //
				if(!coroutine && !walkOnWater)											  //und er nicht grindet oder schon die coroutine ausführt
				coroutine = true;														  //soll er anfangen zu grinden
			}
			if(Wandsprung.otherDirection == true && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)	 //dammit mann seine geschwindigkeit abbremsen kann wenn man in der nähe einer wand ist
			{ 																						 //dammit mann seine geschwindigkeit abbremsen kann wenn man in der nähe einer wand ist
				myRigidbody.drag = 4 * Mathf.Abs(Input.GetAxis("Horizontal"));						 //dammit mann seine geschwindigkeit abbremsen kann wenn man in der nähe einer wand ist
			}																						 //dammit mann seine geschwindigkeit abbremsen kann wenn man in der nähe einer wand ist
			if(Wandsprung.otherDirection == false)													 //dammit mann seine geschwindigkeit abbremsen kann wenn man in der nähe einer wand ist
			{																						 //dammit mann seine geschwindigkeit abbremsen kann wenn man in der nähe einer wand ist
				myRigidbody.drag = smallDrag;														 //dammit mann seine geschwindigkeit abbremsen kann wenn man in der nähe einer wand ist
			}
														  
			
		
		}
																	  
		if (player.GetComponent<Bewegung>().Kern == -1)									  
		{
			Wandsprung.status = 4;                                                        //Script Bewegung
			walkOnWater = false;														  //damit ich auch aufhören kann wenn cih B drücke
			myRigidbody.useGravity = true;                                                //Kein PLan zu müde
			coroutine = false;
			myRigidbody.drag = bigDrag;													  //
			myRigidbody.drag = smallDrag;												  //
		}																				  //
	}
	void Resetten()																		 // Alle werte zurück setzen;
	{																					 //
		stop = true;																	 //
		coroutine = false;																 //
		walkOnWater = false;															 //
		Wandsprung.status = 0;															 //
		Wandsprung.wasserGrind = false;													 //
		myRigidbody.useGravity = true;													 //
		myRigidbody.drag = myDrag;														 //

	}
	IEnumerator GehZurOberF()															 
	{

		player.transform.position = Vector3.MoveTowards(player.transform.position, wasserOberFläche.transform.position, 0.5f /( 1  / Time.fixedDeltaTime));

		yield return new WaitForSeconds(0.5f / (1 / Time.fixedDeltaTime));

		walkOnWater = true;


	}
	#endregion

}

public static class WasserDistanz
{
	public static float distanz;
	public static bool wasserlaufen;
}
