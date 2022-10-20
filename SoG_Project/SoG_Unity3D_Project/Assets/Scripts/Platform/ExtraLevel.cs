using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLevel : MonoBehaviour {

	int bonusStage;
	int nextWorld;
	public GameObject extra;
	public GameObject wand;

	public GameObject firstLevel;
	public GameObject sndLvl;
	public GameObject tutCanvas;

	private void Awake()
	{
		bonusStage = 0;
		for (int i = 0; i < 7; i++)
		{
			if (Geschafft.geschafft.perfekt[i].perfect)
			{
				bonusStage++;
			}
		}
		nextWorld = 0;
		for (int i = 0; i < 7; i++)
		{
			if (Geschafft.geschafft.perfekt[i].geschaft)
			{
				nextWorld++;
			}
		}
		
	}
	private void Start()
	{
		if (bonusStage == 7)
		{
			extra.SetActive(true);
		}
		else
			extra.SetActive(false);

		if (nextWorld == 7)
		{
			wand.SetActive(false);
		}
		else
			wand.SetActive(true);

		if(nextWorld > 0)
		{
			firstLevel.SetActive(false);
			sndLvl.SetActive(true);
			if(nextWorld > 1)
			{
				tutCanvas.SetActive(false);
			}
				
		}
	}
	//private void Update()
	//{
	//	Debug.Log(bonusStage);
	//}
}
