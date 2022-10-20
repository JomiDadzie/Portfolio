using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZeitenListe : MonoBehaviour
{
	public static ZeitenListe zeitenListe; 

	public List<float> zeiten = new List<float>();
	public List<HighScore> score = new List<HighScore>();

	int levelAnzahl;
	private void Awake()
	{
		levelAnzahl = SceneManager.sceneCountInBuildSettings - 2;
		if (zeiten.Count < levelAnzahl)
		{
			int diff = 0;
			diff = levelAnzahl - zeiten.Count;
			for (int i = 0; i < diff; i++)
			{
				zeiten.Add(20);
			}
		}
	}
	private void Start()
	{	
		for (int i = 0; i < zeiten.Count; i++)
		{
			score.Add(new HighScore(zeiten[i]));
			ZeitenListeSeter.zeitenSeter.Add(zeiten[i]);
		}
	}
	public void NewGame()
	{
		score.Clear();
		ZeitenListeSeter.zeitenSeter.Clear();
		for (int i = 0; i < zeiten.Count; i++)
		{
			score.Add(new HighScore(zeiten[i]));
			ZeitenListeSeter.zeitenSeter.Add(zeiten[i]);

		}
	}
	public void LoadGame()
	{

		if (score.Count < zeiten.Count)
		{
			int diff = 0;
			int count = score.Count;
			diff = zeiten.Count - score.Count;
			for (int i = 0; i < diff; i++)
			{
				score.Add(new HighScore(zeiten[i + count]));
			}
			
		}		
		for (int i = 0; i < zeiten.Count; i++)
		{
			
			if (score[i].score < zeiten[i])
			{
				ZeitenListeSeter.zeitenSeter.RemoveAt(i);
				ZeitenListeSeter.zeitenSeter.Insert(i, score[i].score);
			}
			else
			{
				ZeitenListeSeter.zeitenSeter.RemoveAt(i);
				ZeitenListeSeter.zeitenSeter.Insert(i, score[i].score);
			}
		}
	}
	public void NewScore()
	{
		score.RemoveAt(SceneManager.GetActiveScene().buildIndex - 2);
		score.Insert(SceneManager.GetActiveScene().buildIndex - 2, new HighScore (ZeitenListeSeter.zeitenSeter[SceneManager.GetActiveScene().buildIndex - 2]));
	}
}
public static class ZeitenListeSeter
{
	public static List<float> zeitenSeter = new List<float>();
}