using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLoopAfterEntry : MonoBehaviour {
    public float loop_start;
    public float loop_end;
    public bool fading = false; // Sets the music to the fading ending and stops looping
    AudioSource audiosource;

	void Start () {
        audiosource = GetComponent<AudioSource>();
	}
	
	void Update () {
        if (!fading)
        {
            if (audiosource.time >= loop_end)
                audiosource.time = loop_start;
        }
        else
        {
            if (audiosource.time < loop_end)
                audiosource.time = loop_end;
        }
	}
}
