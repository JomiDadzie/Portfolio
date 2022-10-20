using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Feuer : MonoBehaviour {
	
	GameObject spieler;
	ParticleSystem.EmissionModule myChild;
	ParticleSystem.CollisionModule particaleCollision;

	Color currentColor;
	bool tod = false;
	bool andereFarbe = false;
	public float pulseSpeed = 2f;
	private void Awake()
	{
		myChild = GetComponentInChildren<ParticleSystem>().emission;
		particaleCollision = GetComponentInChildren<ParticleSystem>().collision;
		currentColor = GetComponent<MeshRenderer>().material.color;
	}
	private void Start()
	{
		OnFire.onfire = false;
		particaleCollision.enabled = false;
		myChild.rateOverTime = transform.localScale.x * 3;
		spieler = GameObject.Find("Spieler");
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject == spieler && spieler.GetComponent<Bewegung>().Kern == 1)
		{
			//<-------PlaySound
			OnFire.onfire = true;
			StartCoroutine(RestartLevel());
		}
		if (collision.gameObject == spieler)
		particaleCollision.enabled = true;
	}
	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject == spieler)
		{
			OnFire.onfire = false;
			particaleCollision.enabled = false;

		}
	}

	IEnumerator RestartLevel()
	{

		spieler.GetComponent<Bewegung>().nichtBewegen = true;
		spieler.GetComponent<Rigidbody>().isKinematic = true;
		tod = true;
		//spieler.GetComponent<MeshRenderer>().material.color = Color.Lerp(spieler.GetComponent<MeshRenderer>().material.color, Color.red, 1 * Time.deltaTime);
		yield return new WaitForSeconds(1f);

		OnFire.onfire = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	private void Update()
	{
		if(tod)
		spieler.GetComponent<MeshRenderer>().material.color = Color.Lerp(spieler.GetComponent<MeshRenderer>().material.color, Color.red, 3 * Time.deltaTime);
		ColourChanging();
	}
	void ColourChanging()
	{
		
		Color schwarz = new Color(0.243f,0.092f,0.092f,1);
		if(GetComponent<MeshRenderer>().material.color.r >= 0.49f)
		{

			andereFarbe = true;
		}
		if (GetComponent<MeshRenderer>().material.color.r <= 0.253f)
		{

			andereFarbe = false;
		}
		if(andereFarbe)
		GetComponent<MeshRenderer>().material.color = Color.Lerp(GetComponent<MeshRenderer>().material.color,schwarz, pulseSpeed*Time.deltaTime);
		else
			GetComponent<MeshRenderer>().material.color = Color.Lerp(GetComponent<MeshRenderer>().material.color, currentColor, pulseSpeed * Time.deltaTime);

	}
}
public static class OnFire
{
	public static bool onfire = false;
}
