using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{

	GameObject Player;
    GameObject Cam;

	Vector3 velocity = Vector3.zero;
    Vector3 newPosition;

	
	float normalSize = 10;
	public float smooth = 3f;
    float offset = 3f;


	void Start()
    {
        Cam = GameObject.Find("Main Camera");
        newPosition = Cam.transform.position;
        Player = GameObject.FindGameObjectWithTag("Player");
        Cam.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Cam.transform.position.z);
		
    }


    void Update()
    {
		PositionChanging();			
    }
    void PositionChanging()
    {
    
        Vector3 PositionB = new Vector3(Player.transform.position.x, Player.transform.position.y + offset, Cam.transform.position.z);   
        newPosition = PositionB;
		if (Player.GetComponent<Rigidbody>().velocity.y < -15)
		{
			Cam.transform.position = Vector3.SmoothDamp(Cam.transform.position, newPosition,ref velocity, 10 * Time.fixedDeltaTime);
			if ( 10f < Mathf.Abs(Player.GetComponent<Rigidbody>().velocity.y) * 0.2f)
			{
				Cam.GetComponent<Camera>().orthographicSize = Mathf.Lerp(Cam.GetComponent<Camera>().orthographicSize, Mathf.Abs(Player.GetComponent<Rigidbody>().velocity.y) * 0.20f, 3 * Time.deltaTime);
			}
		}
		if (Player.GetComponent<Rigidbody>().velocity.y > 30)
		{
			Cam.transform.position = Vector3.SmoothDamp(Cam.transform.position, newPosition, ref velocity, 15 * Time.fixedDeltaTime);
			if (10f < Mathf.Abs(Player.GetComponent<Rigidbody>().velocity.y) * 0.2f)
			{
				Cam.GetComponent<Camera>().orthographicSize = Mathf.Lerp(Cam.GetComponent<Camera>().orthographicSize, Mathf.Abs(Player.GetComponent<Rigidbody>().velocity.y) * 0.20f, 3 * Time.deltaTime);
			}
		}
		if (Mathf.Abs(Player.GetComponent<Rigidbody>().velocity.x) > 20)
		{
			Cam.transform.position = Vector3.SmoothDamp(Cam.transform.position, newPosition,ref velocity, 15f * Time.fixedDeltaTime);
			if (10f < Mathf.Abs(Player.GetComponent<Rigidbody>().velocity.x) * 0.5f)
			{
				Cam.GetComponent<Camera>().orthographicSize = Mathf.Lerp(Cam.GetComponent<Camera>().orthographicSize, Mathf.Abs(Player.GetComponent<Rigidbody>().velocity.x) * 0.5f, 3 * Time.deltaTime);
			}
		}
		else
		{
			Cam.GetComponent<Camera>().orthographicSize = Mathf.Lerp(Cam.GetComponent<Camera>().orthographicSize, normalSize, 1 * Time.deltaTime);
			Cam.transform.position = Vector3.SmoothDamp(Cam.transform.position, newPosition,ref velocity, 20 * Time.fixedDeltaTime);
		}

    }

}

