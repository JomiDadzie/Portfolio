using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour {

	
    public static AudioClip SoundLittle;
    public static AudioClip SoundBIg;
    public static AudioClip SoundColSmall;
    public static AudioClip SoundColBig;
    public static AudioClip SoundRollingBig;
	public static AudioClip Goal;
	public static AudioClip Trigger;
	

	public AudioClip JumpS;
    public AudioClip JumpB;
    public AudioClip ColS;
    public AudioClip ColB;
    public AudioClip RollB;
	public AudioClip ziel;
	public AudioClip _trigger;
	
	void Awake ()
    {
        SoundLittle = JumpS;
        SoundBIg = JumpB;
        SoundColSmall = ColS;
        SoundColBig = ColB;
        SoundRollingBig = RollB;
		Goal = ziel;
		Trigger = _trigger;
	
	}
}
