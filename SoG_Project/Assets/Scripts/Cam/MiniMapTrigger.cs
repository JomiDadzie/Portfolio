using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapTrigger : MonoBehaviour
{

	public GameObject player;
	public Camera cam;
	public GameObject camPunkt;

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject == player)
		{
			cam.transform.position = camPunkt.transform.position;
		}
	}

}