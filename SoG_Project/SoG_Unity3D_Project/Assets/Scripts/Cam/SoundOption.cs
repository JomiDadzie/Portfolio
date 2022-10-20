using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class SoundOption : MonoBehaviour {

	AudioSource music;
	AudioSource sfx;

	public Slider musicSlider;
	public Slider sfxSlider;

	float musicVol = 1;
	float sfxVol = 3;
	private void Awake()
	{

		music = GameObject.Find("Main Camera").GetComponent<AudioSource>();
		if(GameObject.Find("Spieler") != null)
		sfx = GameObject.Find("Spieler").GetComponent<AudioSource>();

	}
	private void Start()
	{
		musicVol = MetaSound.music / 100;
		music.volume = MetaSound.music/ 100;
		if (GameObject.Find("Spieler") != null)
		{
			sfxVol = MetaSound.sfx / 100;
			sfx.volume = MetaSound.sfx / 100;
		}
		musicSlider.value = MetaSound.music;
		sfxSlider.value = MetaSound.sfx;
	}
	public void SetMusic(float volume)
	{
		musicVol = volume/100;
		MetaSound.music = volume;
		
	}
	public void SetSFX(float volume)
	{
		
			if (GameObject.Find("Spieler") != null)
				sfxVol = volume/100;
			MetaSound.sfx = volume;
		
	}
	private void Update()
	{
		//Debug.Log(MetaSound.music);
		if (musicSlider.gameObject.activeInHierarchy)
		{
			music.volume = musicVol;
			if (GameObject.Find("Spieler") != null)
			{
				Debug.Log("SFX Volume: " + sfx.volume + "    Music Volume: " + music.volume);
				sfx.volume = sfxVol;
			}
		}
	}
}
