using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLoopAfterEntry : MonoBehaviour {
    public float loop_start;
    public float loop_end;
    AudioSource audiosource;

	void Start () {
        audiosource = GetComponent<AudioSource>();
	}
	
	void Update () {
        if (audiosource.time >= loop_end)
            audiosource.time = loop_start;
	}
}
