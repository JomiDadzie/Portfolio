using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
	public Text timer;

	float currentTime;
	string sec;

	float t;

	bool letsGo = false;

	private void Start()
	{
		TimerSeter.timerSet = false;
		letsGo = TimerSeter.timerSet;
	}
	private void Update()
	{
		//Debug.Log(ZeitenListeSeter.zeitenSeter[Application.loadedLevel - 2]);
		letsGo = TimerSeter.timerSet;
		if (letsGo)
		{
			FixTheTimer();
			sec = (t.ToString("f2"));
			timer.text = sec;
			TimerSeter.currentTime = t;
		}
	}
	public void FixTheTimer()
	{
		t += Time.deltaTime;

	}
	public void AufGehts()
	{
		TimerSeter.timerSet = true;
	}
}

public static class TimerSeter
{
	public static bool timerSet = false;
	public static float currentTime = 0;
}
