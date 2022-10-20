using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Colectable : MonoBehaviour {

	bool eingesammelt = false;
	private void Awake()
	{
		if(Geschafft.geschafft.kristalle.Count == 0)
		{
			return;
		}
		else if (Geschafft.geschafft.kristalle[SceneManager.GetActiveScene().buildIndex - 2].eingesammelt)
		{
			gameObject.SetActive(false);
		}
	}
	private void FixedUpdate()
	{
		gameObject.transform.Rotate(new Vector3(0,Time.fixedDeltaTime*100,0));
	}
	private void Update()
	{
		if(GetComponent<AudioSource>().volume >= GameObject.Find("Main Camera").GetComponent<AudioSource>().volume/4)
		GetComponent<AudioSource>().volume = GameObject.Find("Main Camera").GetComponent<AudioSource>().volume/4;
		Colectet.eingesammelt = eingesammelt;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Spieler")
		{
			StartCoroutine(Disappear(GetComponent<AudioSource>().clip.length / GetComponent<AudioSource>().pitch));
		}
	}

	IEnumerator Disappear(float Cliplength)
	{
		GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
		eingesammelt = true;
		Colectet.eingesammelt = true;
		GetComponent<BoxCollider>().enabled = false;
		GetComponent<Transform>().GetChild(0).GetComponent<MeshRenderer>().enabled = false;
		GetComponent<Transform>().GetChild(1).GetComponent<MeshRenderer>().enabled = false;

		yield return new WaitForSeconds(Cliplength);

		Debug.Log("Disapperd!!");

		gameObject.SetActive(false);
	}
}

public static class Colectet
{
	public static bool eingesammelt;
}
