using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Coinbag : MonoBehaviour, IInteractable
{
    public GameObject coin;
    public float force = 10f;
    private Collider col;
    public List<AudioClip> dispenseSFXlarge;
    public List<AudioClip> dispenseSFXsmall;
    public List<AudioClip> dispenseSFXfinal;
    AudioSource audioSource;
    [SerializeField] private GameObject largeStack;
    [SerializeField] private GameObject smallStack;
    public int coinCount = 5;
    public int smallCoinThreshold = 3;
    private float dispenseDuration;

    void Awake()
    {
        col = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        CheckModel();
    }

    public bool UsableWithObj(GameObject obj)
    {
        return false;
    }

    void CheckModel()
    {
        if (coinCount <= 0)
        {
            smallStack.SetActive(false);
            col.enabled = false;
            Destroy(gameObject, dispenseDuration);
        }
        else if (coinCount <= smallCoinThreshold)
        {
            largeStack.SetActive(false);
            smallStack.SetActive(true);
        }
    }

    public void Interact(GameObject source, GameObject obj=null)
    {
        GameObject newCoin = Instantiate(coin, transform.position+Vector3.up, Quaternion.identity, transform);
        if (coinCount > smallCoinThreshold)
        {
            if (dispenseSFXlarge.Count != 0)
            {
                int idx = Random.Range (0, dispenseSFXlarge.Count);
                audioSource.PlayOneShot(dispenseSFXlarge[idx]);
                dispenseDuration = dispenseSFXlarge[idx].length;
            }
        }
        else if (coinCount <= smallCoinThreshold && coinCount > 1)
        {
            if (dispenseSFXsmall.Count != 0)
            {
                int idx = Random.Range (0, dispenseSFXsmall.Count);
                audioSource.PlayOneShot(dispenseSFXsmall[idx]);
                dispenseDuration = dispenseSFXsmall[idx].length;
            }
        }
        else if (coinCount == 1)
        {
            if (dispenseSFXfinal.Count != 0)
            {
                int idx = Random.Range (0, dispenseSFXfinal.Count);
                audioSource.PlayOneShot(dispenseSFXfinal[idx]);
                dispenseDuration = dispenseSFXfinal[idx].length;
            }
        }
        coinCount--;
        Vector3 direction = Random.insideUnitSphere.normalized;
        direction = new Vector3(direction.x, Mathf.Clamp(Mathf.Abs(direction.y), 0.6f, 1f), direction.z);
        newCoin.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
        Debug.DrawRay(transform.position, direction * force, Color.yellow, 30f);
        CheckModel();
    }
}