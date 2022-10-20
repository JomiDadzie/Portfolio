using UnityEngine.SceneManagement;
using UnityEngine;

public class Crusher : MonoBehaviour {

	GameObject player;
	private void Awake()
	{
		player = GameObject.Find("Spieler");
		gameObject.transform.position = gameObject.GetComponentInParent<Transform>().parent.transform.position;
		gameObject.transform.localScale = new Vector3(1,1,1);
	}
	void Start ()
	{
		gameObject.GetComponent<BoxCollider>().size = new Vector3(0.1f,0.85f,1);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject == player && Mathf.Abs(player.GetComponent<Rigidbody>().velocity.x) < 15 && Mathf.Abs(player.GetComponent<Rigidbody>().velocity.y) < 15)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
