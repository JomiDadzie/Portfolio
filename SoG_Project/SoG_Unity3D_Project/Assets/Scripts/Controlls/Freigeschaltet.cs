using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freigeschaltet : MonoBehaviour {

	public static Freigeschaltet freischalten;

	public List<GameObject> portale = new List<GameObject>();
	public List<GameObject> portale2 = new List<GameObject>();
	public List<string> portalname = new List<string>();
	public List<string> portal2name = new List<string>();

	GameObject ich;

	private void Awake()
	{
		portale = new List<GameObject>(GameObject.FindGameObjectsWithTag("Portal"));
		portale2 = new List<GameObject>(GameObject.FindGameObjectsWithTag("Portal2"));
	}

	private void Start()
	{
		#region world1
		//bool[] offen = new bool[Geschafft.geschafft.perfekt.Count];
		foreach (GameObject i in portale)
		{
			portalname.Add(i.name);
		}
		portalname.Sort();
		#endregion
		#region world2
		foreach (GameObject i in portale2)
		{
			portal2name.Add(i.name);
		}
		portal2name.Sort();
		#endregion
		if (Geschafft.geschafft.fertig) // in fortschritt reinschreiben
		{
			#region World1
			for (int i = 0; i < portale.Count - 1; i++)
			{

				if (Geschafft.geschafft.perfekt[i].geschaft)
				{
					ich = GameObject.Find(portalname[i + 1]);
					//print(portalname[i + 1] + "Ich bin freigeschaltet");
					ich.SetActive(true);
				}
				else
				{
					ich = GameObject.Find(portalname[i + 1]);
					//print(portalname[i + 1] + "Ich bin  NICHT freigeschaltet");
					ich.SetActive(false);
				}
			}
			#endregion
			#region World2
			for (int i = 0; i < portale2.Count - 1; i++)
			{

				if (Geschafft.geschafft.perfekt[i + portale.Count].geschaft)
				{
					ich = GameObject.Find(portal2name[i + 1]);
					//print(portalname[i + 1] + "Ich bin freigeschaltet");
					ich.SetActive(true);
				}
				else
				{
					ich = GameObject.Find(portal2name[i + 1]);
					//print(portalname[i + 1] + "Ich bin  NICHT freigeschaltet");
					ich.SetActive(false);
				}
			}
			#endregion
		}
		if (Geschafft.geschafft.laden && !Geschafft.geschafft.fertig)
		{
			#region World1
			for (int i = 0; i < portale.Count - 1; i++)
			{
			
				if (Geschafft.geschafft.perfekt[i].geschaft)
				{
					ich = GameObject.Find(portalname[i + 1]);
					ich.SetActive(true);
				}
				else
				{
					ich = GameObject.Find(portalname[i + 1]);
					ich.SetActive(false);
				}
			}
			#endregion
			#region World2
			for (int i = 0; i < portale2.Count - 1; i++)
			{

				if (Geschafft.geschafft.perfekt[i + portale.Count].geschaft)
				{
					ich = GameObject.Find(portal2name[i + 1]);
					ich.SetActive(true);
				}
				else
				{
					ich = GameObject.Find(portal2name[i + 1]);
					ich.SetActive(false);
				}
			}
			#endregion
			Geschafft.geschafft.fertig = true;
		}
		else if (!Geschafft.geschafft.laden && !Geschafft.geschafft.fertig)
		{
			#region World1
			for (int i = 0; i < portale.Count - 1; i++)
			{

				ich = GameObject.Find(portalname[i + 1]);
				ich.SetActive(false);				
			}
			#endregion
			#region World2
			for (int i = 0; i < portale2.Count - 1; i++)
			{		
				ich = GameObject.Find(portal2name[i + 1]);
				ich.SetActive(false);			
			}
			#endregion
			Geschafft.geschafft.fertig = true;
		}
	}


}
