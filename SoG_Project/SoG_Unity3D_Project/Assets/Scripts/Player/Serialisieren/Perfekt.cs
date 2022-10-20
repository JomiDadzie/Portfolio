using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Perfekt 
{

	public static Perfekt current;

	public bool geschaft;
	public int currentLevel;
	public bool perfect;

	public Perfekt(bool beendet,bool perfec, int currentlvl)
	{
		geschaft = beendet;
		perfect = perfec;
		currentLevel = currentlvl;
	}
}
[System.Serializable]
public class HighScore
{
	public static HighScore current;

	public float score;

	public HighScore(float highscore)
	{
		score = highscore;
	}
}
[System.Serializable]
public class MyLastLevel
{
	public static MyLastLevel current;

	public float x;
	public float y;

	public MyLastLevel(float _x, float _y)
	{
		x = _x;
		y = _y;
	}
}

[System.Serializable]
public class MyOptions
{
	public static MyOptions current;

	public float music;
	public float sfx;

	public MyOptions(float _music, float _sfx)
	{
		music = _music;
		sfx = _sfx;
	}
}
[System.Serializable]
public class Kristalle
{
	public static Kristalle current;

	public bool eingesammelt;

	public Kristalle(bool _eingesammelt)
	{
		eingesammelt = _eingesammelt;
	}
}
