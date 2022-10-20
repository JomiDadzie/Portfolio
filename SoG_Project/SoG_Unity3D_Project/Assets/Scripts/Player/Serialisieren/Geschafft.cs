using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Geschafft : MonoBehaviour
{
	public static Geschafft geschafft;

	int levelAnzahl;// MINUS ALLE SZENEN DIE BIS EINSCHLIEßLICH LEVELSELECET SIND!!!!!!!

	public MyOptions meineOptionen;
	public MyLastLevel hierWarIch;
	public List<Kristalle> kristalle = new List<Kristalle>();
	public List<Perfekt> perfekt = new List<Perfekt>();
	public bool laden = false;
	public bool fertig { get; set; }

	private void Awake()
	{
		levelAnzahl = SceneManager.sceneCountInBuildSettings - 2;
		if (geschafft == null)
		{
			DontDestroyOnLoad(gameObject);
			geschafft = this;
		}
		else
		{
			Destroy(gameObject);
		}		
	}
	public void LoadGame()
	{
		#region Fortschritt
		if (perfekt.Count < levelAnzahl )								// wenn neue level hinzugefügt werden
		{
			int diff = 0;
			int count = perfekt.Count;
			diff = levelAnzahl - perfekt.Count;
			for(int i = 0; i < diff; i++)
			{
				perfekt.Add(new Perfekt(false,false, i + count));
			}

			
		}
		for (int i = 0; i < levelAnzahl; i++)
		{
			if(perfekt[i].geschaft == true)
			{
				if(perfekt[i].perfect == true)
				{
					//Eventuelle Hier zusammen zählen wieviele perfekt sind
				}
			}
			else
			{
				//if statment wenn spiel geladen wird wreden alle werte eingetragen
				perfekt.RemoveAt(i);
				perfekt.Insert(i,(new Perfekt(false,false, i + 2)));// Plus ALLE SZENEN DIE BIS EINSCHLIEßLICH LEVELSELECET SIND!!!!!!!
				

			}
		}
		#endregion
		#region Kristalle
		if (kristalle.Count < levelAnzahl)                                // wenn neue level hinzugefügt werden
		{
			int diff = 0;
			diff = levelAnzahl - kristalle.Count;
			for (int i = 0; i < diff; i++)
			{
				kristalle.Add(new Kristalle(false));
			}
		}
		#endregion
		#region Positio
		if (hierWarIch.x != 0 && hierWarIch.y != 0)
		LastLevelPlayed.letztesLevel = new Vector3(hierWarIch.x,hierWarIch.y, -0.5f);			//Läd den standort von dem zuletzt gespielten level
		else
		LastLevelPlayed.letztesLevel = new Vector3(-5.5f, -8, -0.5f);
		#endregion		
		laden = true;
		
	}
	public void NewGame()
	{		
		perfekt.Clear();
		kristalle.Clear();
		for (int i = 0; i < levelAnzahl; i++)
		{													//if statment wenn spiel geladen wird wreden alle werte eingetragen
			perfekt.Add(new Perfekt(false,false, i + 2));   // Plus ALLE SZENEN DIE AbGEZOGEN WURDEN!!!!!!!
			kristalle.Add(new Kristalle(false));
		}
		LastLevelPlayed.letztesLevel = new Vector3(-5.5f, -8, -0.5f);
		laden = false;
	}
	public void LevelGeschaft()												//BEdinung für perfekt
	{
		perfekt.RemoveAt(SceneManager.GetActiveScene().buildIndex - 2); // MINUS ALLE SZENEN DIE BIS EINSCHLIEßLICH LEVELSELECET SIND!!!!!!!
		perfekt.Insert(SceneManager.GetActiveScene().buildIndex - 2, new Perfekt(true,false, SceneManager.GetActiveScene().buildIndex));
	}
	public void LevelPerfekt()
	{	
		perfekt.RemoveAt(SceneManager.GetActiveScene().buildIndex - 2); // MINUS ALLE SZENEN DIE BIS EINSCHLIEßLICH LEVELSELECET SIND!!!!!!!
		perfekt.Insert(SceneManager.GetActiveScene().buildIndex - 2, new Perfekt(true, true, SceneManager.GetActiveScene().buildIndex));	
	}
	public void Eingesammelt()
	{
		kristalle.RemoveAt(SceneManager.GetActiveScene().buildIndex - 2); // MINUS ALLE SZENEN DIE BIS EINSCHLIEßLICH LEVELSELECET SIND!!!!!!!
		kristalle.Insert(SceneManager.GetActiveScene().buildIndex - 2, new Kristalle(true));
	}


}
