	using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Bewegung : MonoBehaviour
{

	public static Bewegung bewegung;

	GameObject spielerCanvas;
	Rigidbody Player;
	//TRAIL MUSS UNBEDINGT GEFIXED WERDEN und DiE KOMMICHEN LAGS IN DER LUFT
	Vector3 newScale;
	AudioSource Sound;
	Color newColour;    // NOCH NIHT GEKLÄRT GANZ UNTEN IST NEW COLOR MOPRHING

	//public float fallspeed = 50f;
	//<-----------------------------------------------------------------------------------Morphing beinhaltet die breite

	#region(Private)
	public float Ges = 15f;
	public float jumpSpeed = 650f;      // ACHTUNG IM ALTEN PROJEKT 600 und 300!!!!
	public float W_jumpSpeed = 325f;  // UNBEDINGT AUFPASSEN BEIM ÜBETRAGEN!!!
	public float wachstum = 10f;
	float farbwechsel = 10;
	#endregion

	public bool inLuft = true;
	bool richtung = false;
	bool andereRichtung = false;
	public int Kern = 1;
	public int WallJump = 0;

	public bool nichtBewegen { get; set; }
	public bool nichtJumpen { get; set; }
	

	private void Awake()
	{
		nichtBewegen = false;
		nichtJumpen = false;
		spielerCanvas = GameObject.Find("SpielerCanvas");
		
	}
	void Start()
	{
		//spielerCanvas = GameObject.Find("SpielerCanvas");
		Sound = gameObject.GetComponent<AudioSource>();
		Player = gameObject.GetComponent<Rigidbody>();
		newScale = gameObject.transform.localScale;
		newColour = gameObject.GetComponent<Renderer>().material.color;
		if (Geschafft.geschafft.perfekt[8].geschaft == true || SceneManager.GetActiveScene().buildIndex == 10)
		{
			gameObject.GetComponent<Rewinder>().enabled = true;
			spielerCanvas.SetActive(true);

		}
		else
		{
			gameObject.GetComponent<Rewinder>().enabled = false;
			spielerCanvas.SetActive(false);
		}

	}
	#region Collision
	void OnCollisionEnter(Collision collision)
	{
		WallJump = Wandsprung.status;
		if (Kern == 1 && inLuft == true)
		{
			Sound.PlayOneShot(Sounds.SoundColSmall);
		}
		if (Kern == -1 && inLuft == true)
		{
			Sound.PlayOneShot(Sounds.SoundColBig);
		}
		WasserDistanz.wasserlaufen = false;
	}
	void OnCollisionStay(Collision collision)
	{
		WallJump = Wandsprung.status;
		if (collision.gameObject == true)
		{
			inLuft = false;
		}
		if (Kern == -1)
		{
			inLuft = false;
		}
	}
	void OnCollisionExit(Collision collision)
	{
		inLuft = true;
		WasserDistanz.wasserlaufen = true;
	}
	#endregion
	void FixedUpdate()
	{
		//	Debug.Log(spielerCanvas);
		Morphing();
		#region(Bewegung)
		if (!nichtBewegen)
		{
				if (Kern == 1)
				{
					Player.AddForce(Ges * Input.GetAxis("Horizontal"), 0, 0, ForceMode.Force);
				}
				if (Kern == -1)
				{
					Player.AddForce(Ges * 2f * Input.GetAxis("Horizontal"), 0, 0, ForceMode.Force);        // Noch nicht zu frieden wie es funkioniert
				}
		}
		#endregion
	}
	void Update()
	{
		#region RayCast
		RaycastHit hit;
		if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hit, 2.5f) || Physics.Raycast(gameObject.transform.position, Vector3.up, out hit, 2.5f))
		{
			if (hit.transform.tag == "Shades" || hit.transform.tag == "Boden")
			{
				richtung = true;
				
			}
			if (hit.transform.name == "WasserTrigger")
			{
				Wandsprung.wasserGrind = true;
			}
		}
		else if (!Physics.Raycast(gameObject.transform.position, Vector3.down, out hit, 2.5f) && !Physics.Raycast(gameObject.transform.position, Vector3.up, out hit, 2.5f))
		{
			richtung = false;
			Wandsprung.wasserGrind = false;
		}

		if (Physics.Raycast(gameObject.transform.position, Vector3.left, out hit, 2.5f) || Physics.Raycast(gameObject.transform.position, Vector3.right, out hit, 2.5f))
		{
			if (hit.transform.tag == "Shades" || hit.transform.tag == "Boden")
			{
				andereRichtung = true;
			}
			if (hit.transform.name == "WasserTrigger")
			{
				Wandsprung.wasserGrind = false;
			}

		}
		else if(!Physics.Raycast(gameObject.transform.position, Vector3.left, out hit, 2.5f) && !Physics.Raycast(gameObject.transform.position, Vector3.right, out hit, 2.5f))
		{
			andereRichtung = false;
			
		}
		Wandsprung.direction = richtung;
		Wandsprung.otherDirection = andereRichtung;
		#endregion
		ColourChanging();		
		#region Jump
		if (!nichtBewegen && gameObject.GetComponent<Rigidbody>().isKinematic == false && !nichtJumpen)
		{
			if (Input.GetButtonDown("Jump") && inLuft == false)
			{
				inLuft = true;
				switch (Kern)
				{
					case 1:
						Player.mass = 3;
						Player.AddForce(0, jumpSpeed * 1.75f, 0);
						Sound.PlayOneShot(Sounds.SoundLittle);
						break;
					case -1:
						Player.mass = 1;
						if (Input.GetButton("Fire1") && WallJump == 0)
						{
							Player.AddForce(0, jumpSpeed * 1.2f, 0);
							Sound.PlayOneShot(Sounds.SoundRollingBig);
						}
						else
						{
							Player.AddForce(0, jumpSpeed * 0.75f, 0);
							Sound.PlayOneShot(Sounds.SoundBIg);
						}
						break;
				}
				switch (WallJump)
				{
					case 0:
						W_jumpSpeed = 325;
						jumpSpeed = 650;
						WallJump = 0;
						inLuft = false;
						break;

					case 1:
						Player.velocity = new Vector3(Player.velocity.x, 0, 0);
						if (Kern == 1)
						{
							Player.AddForce(W_jumpSpeed * 10f, 0, 0);
						}
						if (Kern == -1)
						{
							Player.AddForce(W_jumpSpeed * 4, 0, 0);
						}
						break;

					case 2:
						Player.velocity = new Vector3(Player.velocity.x, 0, 0);
						if (Kern == 1)
						{
							Player.AddForce(-W_jumpSpeed * 10, 0, 0);
						}
						if (Kern == -1)
						{
							Player.AddForce(-W_jumpSpeed * 4f, 0, 0);
						}
						break;

					case 3:
						Player.velocity = new Vector3(Player.velocity.x, 0, 0);
						if (Kern == 1)
						{
							Player.AddForce(0, -W_jumpSpeed * 70, 0);
						}
						if (Kern == -1)
						{
							Player.AddForce(0, -W_jumpSpeed * 25, 0);
						}
						break;
					case 4:
						if(Kern == 1)
						{

						}
						if(Kern == -1)
						{																															 //WasserDRECK!!!!
							Player.AddForce(0,W_jumpSpeed * (WasserDistanz.distanz/2.5f),0);														 //WasserDRECK!!!!
							if(Input.GetAxis("Horizontal")> 0.1)																					 //WasserDRECK!!!!
							Player.velocity = new Vector3(Player.velocity.x + 20 * Input.GetAxis("Horizontal"),Player.velocity.y,0);				 //WasserDRECK!!!!
							if (Input.GetAxis("Horizontal") < 0.1)																					 //WasserDRECK!!!!
								Player.velocity = new Vector3(Player.velocity.x + 20 * Input.GetAxis("Horizontal"), Player.velocity.y, 0);			 //WasserDRECK!!!!
						}
						break;
				}
				Kern *= -1;
			}
		}
		#endregion
		#region Stomp
		if (!nichtBewegen && gameObject.GetComponent<Rigidbody>().isKinematic == false)
		{
			if (Input.GetButton("Stomp") && Kern == 1 && inLuft == true)
			{
				if (Player.velocity.y > -25)
					Player.velocity = new Vector3(Player.velocity.x / 2, -25, 0);
				Kern = -1;
			}
		}
		#endregion
		#region(MasseImFlug)
		if (inLuft == true && Kern == -1 && Player.mass < 13)
		{
			Player.mass += 1f * Time.deltaTime;
		}
		if (inLuft == false && Kern == -1)
		{
			Player.mass = 3;
		}
		#endregion                                                                                     //FRAG WÜRDIG OB ICH ES BEHALTE
	}
	#region Lerp
	void Morphing()
	{
		Vector3 klein = new Vector3(1, 1, 1);
		Vector3 groß = new Vector3(2, 2, 2);

		if (Kern == 1)
		{
			newScale = klein;
		}
		else if (Kern == -1)
		{
			newScale = groß;
		}

		gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, newScale, wachstum * Time.deltaTime);
	}
	void ColourChanging()
	{
		Color colourA = Color.white;


		Color colourB = Color.black;


		if (Kern == 1)
		{
			newColour = colourA;
		}

		if (Kern == -1)
		{
			newColour = colourB;
		}

		gameObject.GetComponent<Renderer>().material.color = Color.Lerp(gameObject.GetComponent<Renderer>().material.color, newColour, farbwechsel * Time.deltaTime);
	}
	#endregion
}

public class Wandsprung
{
	public static int status = 0;
	public static bool direction = false;
	public static bool otherDirection = false;
	public static bool wasserGrind = false;
}
