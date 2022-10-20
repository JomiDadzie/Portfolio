using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveLoad
{

	public static void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/savedGames.sog");
		bf.Serialize(file, Geschafft.geschafft.perfekt);
		bf.Serialize(file, GameObject.Find("Highscore").GetComponent<ZeitenListe>().score);
		bf.Serialize(file, Geschafft.geschafft.hierWarIch);
		bf.Serialize(file, Geschafft.geschafft.kristalle);
		file.Close();


	}
	public static void Load()
	{
		if (File.Exists(Application.persistentDataPath + "/savedGames.sog"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.sog", FileMode.Open);
			Geschafft.geschafft.perfekt = (List<Perfekt>)bf.Deserialize(file);
			GameObject.Find("Highscore").GetComponent<ZeitenListe>().score = (List<HighScore>)bf.Deserialize(file);
			Geschafft.geschafft.hierWarIch = (MyLastLevel)bf.Deserialize(file);
			Geschafft.geschafft.kristalle = (List<Kristalle>)bf.Deserialize(file);
			file.Close();


		}
	}
	public static void SaveOptions()
	{
		Geschafft.geschafft.meineOptionen.music = MetaSound.music;
		Geschafft.geschafft.meineOptionen.sfx = MetaSound.sfx;

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/savedOptions.sog");
		bf.Serialize(file, Geschafft.geschafft.meineOptionen);
		file.Close();


	}
	public static void LoadOptions()
	{
		if (File.Exists(Application.persistentDataPath + "/savedOptions.sog"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedOptions.sog", FileMode.Open);
			Geschafft.geschafft.meineOptionen = (MyOptions)bf.Deserialize(file);
			file.Close();
			
				MetaSound.music = Geschafft.geschafft.meineOptionen.music;
				MetaSound.sfx = Geschafft.geschafft.meineOptionen.sfx;
			

		}
	}
}
