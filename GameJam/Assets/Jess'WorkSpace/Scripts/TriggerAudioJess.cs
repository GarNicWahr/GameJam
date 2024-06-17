using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Assets.AudioManager;
using System.Runtime.CompilerServices;

public class TriggerAudioJess : MonoBehaviour
{
    
     public AudioManager audioManager;

    //public MusicPlayer musicPlayer;
    //public AudioPlayer audioPlayer;

    public string ClipToPlay = "none";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "entered Collider");

        audioManager.Play(ClipToPlay);
        //musicPlayer.PlayTrack();
        //audioPlayer.Play();
    }

}

   


