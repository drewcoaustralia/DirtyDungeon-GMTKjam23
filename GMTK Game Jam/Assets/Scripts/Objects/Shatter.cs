using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Shatter : MonoBehaviour
{
    public GameObject shattered;
    public float force=100f;
    [Range(0f, 1f)] public float minUpwards = 0.6f;
    public List<AudioClip> shatterSFX;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!this.enabled) return;
        if (collision.gameObject.tag == "floor") ShatterThis();
    }

    void ShatterThis()
    {
        GameObject shatt = Instantiate(shattered, transform.position+(Vector3.up/2), transform.rotation);
        AudioSource src = shatt.AddComponent<AudioSource>();
        src.clip = shatterSFX[Random.Range (0, shatterSFX.Count)];
        src.PlayOneShot(src.clip, .25f);

        foreach (Transform child in shatt.transform)
        {
            Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
            MeshCollider col = child.gameObject.AddComponent<MeshCollider>();
            col.convex = true;
            // Vector3 frc = (rb.transform.position - transform.position).normalized * force; // add random angles?
            Vector3 frc = (Random.onUnitSphere).normalized; // add random angles?
            frc = new Vector3(frc.x, Mathf.Clamp(Mathf.Abs(frc.y), 0.6f, 1f), frc.z);
            rb.AddForce(frc * force);
            Debug.Log(frc);
        }
        Destroy(gameObject);
    }
}
