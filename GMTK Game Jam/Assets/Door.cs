using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour
{
    public string keyColor;
    public AudioClip openSFX;
    AudioSource audioSource;
    private bool startedOpening = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (startedOpening) return;
        GameObject obj = col.gameObject;
        Key key = obj.GetComponent<Key>();
        if (key == null) return;
        if (key.doorColor == keyColor)
        {
            key.Open();
            Open();
        }
    }

    void Open()
    {
        startedOpening = true;
        audioSource.PlayOneShot(openSFX);
        //animation stuff
        Destroy(gameObject, openSFX.length);
    }
}
