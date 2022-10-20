using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Rewinder : MonoBehaviour {

	public bool rewinding = false;		
	bool recording = false;
	bool stopJetzt = false;
	bool coolingDown = false;
	bool stopRewind = false;
	bool gedrückt = false;

	float currentMass;
	Vector3 currentVelo;
	float myTime = 0;
	float faster = 0;
	float coolDownTime = 5;
	List<PointsInTime> points = new List<PointsInTime>();

	Slider slider;
	// Use this for initialization
	void Start ()
	{
		rewinding = false;
		recording = false;
		slider = GameObject.Find("RewindCoolDown").GetComponent<Slider>();
		myTime = Time.fixedDeltaTime;
		faster = Time.fixedDeltaTime / 4;
		slider.maxValue = coolDownTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log(coolDownTime);
		if (Time.timeScale < 1)
			stopRewind = true;
		else
			stopRewind = false;

		if (coolDownTime >= 5)
			coolingDown = false;

		if (!stopRewind)
		{
			ZeitManip.record = recording;
			ZeitManip.rewind = rewinding;

			if (!rewinding)
				Time.fixedDeltaTime = myTime;
			//Debug.Log(Time.fixedDeltaTime);
			if (Input.GetButtonDown("Rewind") && rewinding == false && recording == false && stopJetzt == false && coolingDown == false	)
			{
				currentMass = gameObject.GetComponent<Rigidbody>().mass;
				currentVelo = gameObject.GetComponent<Rigidbody>().velocity;
				gedrückt = true;
			}
			if (Input.GetButton("Rewind") && rewinding == false && stopJetzt == false && coolingDown == false && gedrückt == true )
			{
				recording = true;
			}
			if (gedrückt)
			{
				if (Input.GetButtonUp("Rewind") && coolingDown == false)
				{
					stopJetzt = true;
					gedrückt = false;
				}
			}

			if ((points.Count >= 5f / Time.fixedDeltaTime) || stopJetzt == true)				//die dauer fürs Aufzeichenen
			{
				recording = false;
				rewinding = true;
				coolingDown = true;

			}

		}
		slider.value = coolDownTime;
	}
	private void FixedUpdate()
	{
		if (!stopRewind)
		{
			if (recording == true)
			{

				StartRecord();
			}
			else if (recording == false && rewinding == true)
			{

				gameObject.GetComponent<Rigidbody>().isKinematic = true;
				gameObject.GetComponent<Bewegung>().nichtBewegen = true;
				
			}
			if (rewinding)
			{
				Time.fixedDeltaTime = faster;
				Rewind();
			}
			if (coolDownTime < 5 && recording == false && rewinding == false)
			{
				coolDownTime += 4 * Time.fixedDeltaTime ;
			}
		}
		
	}

	public void StartRecord()
	{
		points.Insert(0,new PointsInTime(gameObject.transform.position, gameObject.transform.rotation, gameObject.GetComponent<Bewegung>().Kern));
		coolDownTime -= 1 * Time.fixedDeltaTime;
	}

	public void Rewind()
	{
		if (points.Count > 0)
		{
			gameObject.transform.position = points[0].poistion;
			gameObject.transform.rotation = points[0].rotation;
			gameObject.GetComponent<Bewegung>().Kern = points[0].playerState;
			points.RemoveAt(0);
		}
		else
		{
			Time.fixedDeltaTime = myTime;
			rewinding = false;
			stopJetzt = false;
			if (MenuLevelSeter.letsGo == true || SceneManager.GetActiveScene().buildIndex == 2)
			{
				gameObject.GetComponent<Bewegung>().nichtBewegen = false;					//Fix This for start
			}			
			gameObject.GetComponent<Rigidbody>().isKinematic = false;
			gameObject.GetComponent<Rigidbody>().mass = currentMass;
			gameObject.GetComponent<Rigidbody>().velocity = currentVelo;
			
		}
	}
	void CoolDown()
	{
		if (coolDownTime < 4 && coolingDown == true)
			coolDownTime += 1 * Time.deltaTime;
	}


}

public class ZeitManip
{
	public static bool record;
	public static bool rewind;
}
