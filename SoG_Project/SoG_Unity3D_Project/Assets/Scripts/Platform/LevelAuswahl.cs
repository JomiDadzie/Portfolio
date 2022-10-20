using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelAuswahl : MonoBehaviour
{
	public static LevelAuswahl levelAuswhal;

    public int Level;
	//bool eingtreten = false;

	Text zeitText;
	public string lvlname;
	Text levelNameText;

	GameObject perfectText;
	GameObject pressUp;
	GameObject levelInfos;
	Button eintreten;
	Button cancel;
	GameObject player;
	Menu fader;
	string spieler;



	private void Awake()
	{
		fader = GameObject.Find("Canvas").GetComponent<Menu>();
		levelInfos = GameObject.Find("LevelKarte");
		pressUp = GameObject.Find("PressUp");
		eintreten = GameObject.Find("Enter").GetComponent<Button>();
		cancel = GameObject.Find("Abbrechen").GetComponent<Button>();
		player = GameObject.FindGameObjectWithTag("Player");
		zeitText = GameObject.Find("ZeitText").GetComponent<Text>();
		levelNameText = GameObject.Find("LevelNameText").GetComponent<Text>();
		perfectText = GameObject.Find("PerfektText");
		spieler = player.tag;
	}
	private void Start()
    {
		pressUp.SetActive(false);
		
		perfectText.SetActive(false);
		levelInfos.SetActive(false);
	}

	private void Update()
	{
		if (Geschafft.geschafft.perfekt[Level-2].geschaft == true && Geschafft.geschafft.perfekt[Level - 2].perfect == false)
		{
			gameObject.GetComponent<Renderer>().material.color = Color.gray;
		}
		if (Geschafft.geschafft.perfekt[Level - 2].perfect == true)
		{
			gameObject.GetComponent<Renderer>().material.color = Color.white;
		}

		if (Input.GetButtonDown("Cancel") && levelInfos.activeInHierarchy)
			Abbrechen();
		if (NichtJetzt.nj)
			Abbrechen2();
		
	}
	private void OnTriggerEnter(Collider other)
    {
		if (other.tag == spieler)
        {
			levelNameText.text = lvlname;
			pressUp.SetActive(true);
			zeitText.text = ZeitenListeSeter.zeitenSeter[Level - 2].ToString("f2");
			if (Geschafft.geschafft.perfekt[Level - 2].perfect)
			{
				perfectText.SetActive(true);
			}
			
			fader.lvl = Level;
        }
    }
    private void OnTriggerStay(Collider other)
    {
		Debug.Log(Geschafft.geschafft.perfekt[Level - 2].perfect);
		levelNameText.text = lvlname;
		if (other.tag ==  spieler)//player.GetComponent<Bewegung>().bewegen == false)
        {
			if (Input.GetAxis("Vertical") > 0.5f && NichtJetzt.nj == false)
			{
				
				pressUp.SetActive(false);
				levelInfos.SetActive(true);
				eintreten.Select();
				player.GetComponent<Bewegung>().nichtBewegen = true;
				player.GetComponent<Rigidbody>().velocity = new Vector3(0, player.GetComponent<Rigidbody>().velocity.y, player.GetComponent<Rigidbody>().velocity.z);
				
			}
			
			if (player.GetComponent<Rigidbody>().isKinematic == true && player.GetComponent<Rewinder>().rewinding == false)
			{
				LastLevelPlayed.letztesLevel = new Vector3(gameObject.GetComponent<Transform>().GetChild(1).transform.position.x, gameObject.GetComponent<Transform>().GetChild(1).transform.position.y, -0.5f);
				Geschafft.geschafft.hierWarIch = new MyLastLevel(gameObject.GetComponent<Transform>().GetChild(1).transform.position.x, gameObject.GetComponent<Transform>().GetChild   (1).transform.position.y);			//Schreibt auf dem Serializer den standort

			}
		}

    }
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == spieler)
		{
			//Time.timeScale = 1;
			player.GetComponent<Bewegung>().nichtBewegen = false;
			pressUp.SetActive(false);
			levelInfos.SetActive(false);
			perfectText.SetActive(false);
			cancel.Select();
		}

	}

	public void Eintreten()
	{
		//Time.timeScale = 1;
		player.GetComponent<Rigidbody>().isKinematic = true;
	}
	public void Abbrechen()
	{
		//Time.timeScale = 1;
		player.GetComponent<Bewegung>().nichtBewegen = false;
		pressUp.SetActive(true);
		levelInfos.SetActive(false);
		perfectText.SetActive(false);
		cancel.Select();
	}
	public void Abbrechen2()
	{
		pressUp.SetActive(true);
		levelInfos.SetActive(false);
		perfectText.SetActive(false);
	}
}
public static class NichtJetzt
{
	public static bool nj = false;
}