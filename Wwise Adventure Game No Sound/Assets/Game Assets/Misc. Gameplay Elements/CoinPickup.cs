////////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2018 Audiokinetic Inc. / All Rights Reserved
//
////////////////////////////////////////////////////////////////////////

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPickup : MonoBehaviour {

    public bool playSpawnSoundAtSpawn = true;
    public AudioClip coinSound;

	void Start(){
        if (playSpawnSoundAtSpawn){
            gameObject.AddComponent<AudioSource>();
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(coinSound);
            // HINT: You might want to play the Coin pickup sound here
        }
	}

	public void AddCoinToCoinHandler(){
		InteractionManager.SetCanInteract(this.gameObject, false);
		GameManager.Instance.coinHandler.AddCoin ();
		//Destroy (gameObject, 0.1f); //TODO: Pool instead?
	}
}
