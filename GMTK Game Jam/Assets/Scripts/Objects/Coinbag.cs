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
    [SerializeField] private GameObject largeStack;
    [SerializeField] private GameObject smallStack;
    public int coinCount = 5;
    public int smallCoinThreshold = 3;

    void Awake()
    {
        col = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        CheckModel();
    }

    void CheckModel()
    {
        if (coinCount <= 0)
        {
            smallStack.SetActive(false);
            col.enabled = false;
            Destroy(gameObject, dispenseSFX.length);
        }
        else if (coinCount <= smallCoinThreshold)
        {
            largeStack.SetActive(false);
            smallStack.SetActive(true);
        }
    }

    public void Interact(GameObject source)
    {
        coinCount--;
        GameObject newCoin = Instantiate(coin, transform.position+Vector3.up, Quaternion.identity, transform);
        audioSource.PlayOneShot(dispenseSFX);
        Vector3 direction = Random.insideUnitSphere.normalized;
        direction = new Vector3(direction.x, Mathf.Clamp(Mathf.Abs(direction.y), 0.6f, 1f), direction.z);
        newCoin.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
        Debug.DrawRay(transform.position, direction * force, Color.yellow, 30f);
        CheckModel();
    }
}