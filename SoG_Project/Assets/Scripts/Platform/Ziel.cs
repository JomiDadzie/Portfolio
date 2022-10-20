using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class Ziel : MonoBehaviour {

	public GameObject EndScreen;
	
	AudioSource cam;

	bool funktionBenutzt = false;
	int aktuellesLevel;

	float aktuellerScore;
	float besterScore;
	float inTime;
	float myTime;
	float slowTime;

	Text perfectText;
	Text zeitText;
	Text finishText;

	private void Awake()
	{
		myTime = Time.fixedDeltaTime;
		finishText = GameObject.Find("FinshedText").GetComponent<Text>();
		zeitText = GameObject.Find("ZeitText").GetComponent<Text>();
		perfectText = GameObject.Find("PerfectText").GetComponent<Text>();
		EndScreen.SetActive(false);
		cam = GameObject.Find("Main Camera").GetComponent<AudioSource>();
	}
	private void Start()
	{
		aktuellesLevel = SceneManager.GetActiveScene().buildIndex - 2;
		besterScore = ZeitenListeSeter.zeitenSeter[SceneManager.GetActiveScene().buildIndex - 2];
		inTime = GameObject.Find("Highscore").GetComponent<ZeitenListe>().zeiten[aktuellesLevel];
		
	}
	private void Update()
	{
		if(gameObject.GetComponent<AudioSource>().volume >= cam.volume/4)
		gameObject.GetComponent<AudioSource>().volume = cam.volume/4;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Spieler")
		{
			print("Ziel");
			other.GetComponent<Bewegung>().nichtBewegen = true;
			ZielErreicht();		
		}
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.name == "Spieler")
		{
			if(Geschafft.geschafft.perfekt[aktuellesLevel].geschaft == false && Geschafft.geschafft.perfekt[aktuellesLevel].perfect == false)
			{
				if(!funktionBenutzt)
				LevelBeendet();
				Geschafft.geschafft.LevelGeschaft();
				
			}
			if (Geschafft.geschafft.perfekt[aktuellesLevel].perfect == false && aktuellerScore < inTime)
			{
				if(!funktionBenutzt)
				LevelBeendet();
				Geschafft.geschafft.LevelPerfekt();
			}
			if (aktuellerScore < besterScore)
			{
				ZeitenListeSeter.zeitenSeter.RemoveAt(aktuellesLevel);
				ZeitenListeSeter.zeitenSeter.Insert(aktuellesLevel, aktuellerScore);
				GameObject.Find("Highscore").GetComponent<ZeitenListe>().NewScore();
			}
			if (aktuellerScore < inTime && funktionBenutzt == false)
				LevelBeendet();
		}
	}
	public void LevelAuswahl()
	{
		MenuLevelSeter.notNowSeter = true;
		Time.timeScale = 1;
		Time.fixedDeltaTime = myTime;
		SceneManager.LoadScene("WeltenAuswahl");
	}
	public void Nochmal()
	{
		Time.timeScale = 1;
		Time.fixedDeltaTime = myTime;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	void LevelBeendet()
	{
		if (Geschafft.geschafft.perfekt[aktuellesLevel].geschaft == false)
		{
			finishText.text = "Level Complete";
			if (aktuellerScore < inTime)
			{
				GetComponent<AudioSource>().PlayOneShot(Sounds.Goal);
				perfectText.text = "Perfect";
			}
			else
			{
				perfectText.text = "";
			}
		}
		else if (Geschafft.geschafft.perfekt[aktuellesLevel].geschaft == true)
		{
			finishText.text = "Level Finished";
			if (aktuellerScore < inTime)
			{
				GetComponent<AudioSource>().PlayOneShot(Sounds.Goal);
				perfectText.text = "Perfect";
			}
			else
			{
				perfectText.text = "";
			}
		}
		if (!Geschafft.geschafft.kristalle[SceneManager.GetActiveScene().buildIndex - 2].eingesammelt)
		{
			Geschafft.geschafft.kristalle[SceneManager.GetActiveScene().buildIndex - 2].eingesammelt = Colectet.eingesammelt;
		}
		funktionBenutzt = true;
	}

	public void ZielErreicht()
	{
		MenuLevelSeter.notNowSeter = false;
		TimerSeter.timerSet = false;
		aktuellerScore = TimerSeter.currentTime;
		zeitText.text = aktuellerScore.ToString("f2");		
		EndScreen.SetActive(true);
		GameObject.Find("Button").GetComponent<Button>().Select();
		Time.fixedDeltaTime = Time.fixedDeltaTime / 10;
		TimeManager.SlowMotion();
		cam.Stop();
		GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
	}
}
