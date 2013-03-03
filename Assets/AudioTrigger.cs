using System;
using UnityEngine;

public class AudioTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
       if (other.tag == "Player") {
     //    particleSystem.Play();
         audio.Play();
       }
    }

    void OnTriggerExit(Collider other) {
       if (other.tag == "Player") {
       //  particleSystem.Stop ();
         audio.Stop();
       }
    }
}