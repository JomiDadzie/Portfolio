using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class TitelBild : MonoBehaviour {

	public Button loadbutton;
    public Animator animator;

    bool neuesSpiel = false;

	private void Awake()
	{
		SaveLoad.LoadOptions();
	}
	public void NeuesSpiel()
    {
		FadetoLevel(true);
		GameObject.Find("Highscore").GetComponent<ZeitenListe>().NewGame();
		Geschafft.geschafft.NewGame();
    }
    public void LadeSpiel()
    {
		SaveLoad.Load();
		Geschafft.geschafft.LoadGame();
		GameObject.Find("Highscore").GetComponent<ZeitenListe>().LoadGame();
		FadetoLevel(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void FadetoLevel(bool NewGame)
    {
        neuesSpiel = NewGame;
        animator.SetTrigger("NewGameFader");
        
    }
	public void SpeicherOptionen()
	{

	}
    public void OnFadeComplete()
    {
        if (neuesSpiel)
        {
            SceneManager.LoadScene(1);
        }
    }

    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.sog"))
        {

			loadbutton.interactable = true;
        }
        else
        {
			loadbutton.interactable = false;
		}

    }

}
