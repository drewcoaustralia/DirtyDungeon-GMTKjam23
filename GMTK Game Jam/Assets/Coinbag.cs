using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Coinbag : MonoBehaviour, IInteractable
{
    public GameObject coin;
    public float force = 10f;
    private Collider col;
    public AudioClip dispenseSFX;
    AudioSource audioSource;

    void Awake()
    {
        col = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Interact(GameObject source)
    {
        GameObject newCoin = Instantiate(coin, transform.position+Vector3.up, Quaternion.identity, transform);
        audioSource.PlayOneShot(dispenseSFX);
        Vector3 direction = Random.insideUnitSphere.normalized;
        direction = new Vector3(direction.x, Mathf.Clamp(Mathf.Abs(direction.y), 0.6f, 1f), direction.z);
        newCoin.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
        Debug.DrawRay(transform.position, direction * force, Color.yellow, 30f);
    }
}