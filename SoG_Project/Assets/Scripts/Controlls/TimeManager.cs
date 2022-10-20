using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeManager {


	public static void SlowMotion()
	{
		Time.timeScale = 0.05f;
		//Time.fixedDeltaTime = Time.timeScale * Time.fixedDeltaTime;
	}

	public static void NormalTime()
	{
		Time.timeScale = 1;
		//Time.fixedDeltaTime = 0.2;
	}
}
