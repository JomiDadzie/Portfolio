using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public static Menu menu;

	public GameObject StartFader;
	public GameObject StartMenu;
	public GameObject OptionMenu;

	public Button resume;
	public Button option;
	public Slider masterVol;

																																	//TRAIL MUSS UNBEDINGT GEFIXED WERDEN
	GameObject player;
	Animator animator;
	float myTime;
	public int lvl { get; set; }
	private void Awake()
	{
		player = GameObject.Find("Spieler");
		animator = gameObject.GetComponent<Animator>();
		myTime = Time.fixedDeltaTime;
		NichtJetzt.nj = false;
	}
	public void Resume()
	{
		player.GetComponent<Bewegung>().nichtBewegen = false;
		Time.timeScale = 1;
		StartFader.SetActive(false);
	}
	public void SpielSpeichern()
	{
		Debug.Log("Erfolgreich Gespeichert!");
		animator.SetTrigger("Saved");
		SaveLoad.Save();                                                //Abfragen ob sicher und animationen 
		SaveLoad.SaveOptions();
	}
	public void Optionen()
	{
		StartMenu.SetActive(false);
		OptionMenu.SetActive(true);
		masterVol.Select();

	}
	public void HauptMenu()
	{
		Time.timeScale = 1;
		Time.fixedDeltaTime = myTime;
		SceneManager.LoadScene("TitelScreen");
	}
	public void Back()
	{
		OptionMenu.SetActive(false);
		StartMenu.SetActive(true);
		resume.Select();
	}
	public void QuitGame()
	{
		Application.Quit();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Start"))
		{
			if (StartFader.activeInHierarchy)
			{
				Time.timeScale = 1;
				Time.fixedDeltaTime = myTime;
				StartMenu.SetActive(true);
				OptionMenu.SetActive(false);
				StartFader.SetActive(false);
				option.Select();
				player.GetComponent<Bewegung>().nichtBewegen = false;
				NichtJetzt.nj = false;
			}
			else if(!StartFader.activeInHierarchy)
			{
				TimeManager.SlowMotion();
				StartMenu.SetActive(true);
				OptionMenu.SetActive(false);
				StartFader.SetActive(true);
				resume.Select();
				player.GetComponent<Bewegung>().nichtBewegen = true;
				NichtJetzt.nj = true;
			}
		}
		if(player.GetComponent<Rigidbody>().isKinematic == true && player.GetComponent<Rewinder>().rewinding == false)
		{
			FadetoLevel(true);
		}
	}
	public void FadetoLevel(bool NewGame)
	{

		animator.SetTrigger("FadeOutGray");

	}
	public void OnFadeComplete()
	{
		SceneManager.LoadScene(lvl);		
	}

}
