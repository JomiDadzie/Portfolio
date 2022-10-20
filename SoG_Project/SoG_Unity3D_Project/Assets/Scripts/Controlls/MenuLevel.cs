using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuLevel : MonoBehaviour {

	public static Menu menu;

	public GameObject StartFader;
	public GameObject StartMenu;
	public GameObject OptionMenu;

	public Button resume;
	public Button option;
	public Slider musicVol;
																																	//TRAIL MUSS UNBEDINGT GEFIXED WERDEN
	GameObject player;
	Animator animator;

	float myTime;
	bool bereit = false;
	public int lvl { get; set; }
	private void Awake()
	{
		myTime = Time.fixedDeltaTime;
		player = GameObject.Find("Spieler");
		animator = gameObject.GetComponent<Animator>();

	}
	private void Start()
	{
		MenuLevelSeter.letsGo = false;
		MenuLevelSeter.notNowSeter = true;
	}
	public void Optionen()
	{
		StartMenu.SetActive(false);
		OptionMenu.SetActive(true);
		musicVol.Select();
	}
	public void QuitLevel()
	{
		Time.timeScale = 1;
		Time.fixedDeltaTime = myTime;
		SceneManager.LoadScene("WeltenAuswahl");
	}
	public void RestartLevel()
	{
		Time.timeScale = 1;
		Time.fixedDeltaTime = myTime;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void QuitGame()
	{
		Application.Quit();
	}
	public void Weiter()
	{
		if (bereit)
		{
			player.GetComponent<Bewegung>().nichtBewegen = false;
		}
		Time.timeScale = 1;
		StartFader.SetActive(false);
	}
	public void Back()
	{
		OptionMenu.SetActive(false);
		StartMenu.SetActive(true);
		resume.Select();
	}

	private void Update()
	{
		if (MenuLevelSeter.notNowSeter)
		{
			if (Input.GetButtonDown("Start"))
			{
				if (StartFader.activeInHierarchy)
				{

					Time.timeScale = 1;
					StartMenu.SetActive(true);
					OptionMenu.SetActive(false);
					StartFader.SetActive(false);
					option.Select();
					if (bereit)
					{
						player.GetComponent<Bewegung>().nichtBewegen = false;
					}
				}
				else if (!StartFader.activeInHierarchy)
				{
					Time.timeScale = 0;
					StartFader.SetActive(true);
					OptionMenu.SetActive(false);
					StartMenu.SetActive(true);					
					resume.Select();
					GameObject.Find("ResumeButton").GetComponent<Button>().Select();
					if (bereit)
					{
						player.GetComponent<Bewegung>().nichtBewegen = true;
					}
				}
			}
		}
		if (player.GetComponent<Rigidbody>().isKinematic == true && SceneManager.GetActiveScene().buildIndex == 2)
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

	public void StayReady()
	{
		player.GetComponent<Bewegung>().nichtBewegen = true;
	}
	public void LetsGO()
	{
		player.GetComponent<Bewegung>().nichtBewegen = false;
		bereit = true;
		MenuLevelSeter.letsGo = true;
	}
}

public class MenuLevelSeter
{
	public static bool notNowSeter = true;
	public static bool letsGo = false;
}
