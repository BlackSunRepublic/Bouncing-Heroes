using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col) 
    {
        var _saund = col.gameObject.GetComponent<AudioSource>();

        if (_saund != null) _saund.Play();
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        var _saund = col.GetComponent<AudioSource>();

        if (_saund != null) _saund.Play();
    }
}
