using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinArea : MonoBehaviour
{
    public GameObject vfx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pushable"))
        {
            PlayParticleEffect(other.gameObject);
        }
    }

    void PlayParticleEffect(GameObject other)
    {
        GameObject particle = Instantiate(vfx.transform, other.transform).gameObject;
        Destroy(other.gameObject, 0.15f);
    }
}
