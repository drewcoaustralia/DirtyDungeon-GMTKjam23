using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinArea : MonoBehaviour
{
    public GameObject vfx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Trash>(out Trash trash)) {
            trash.Clean();
            PlayParticleEffect(other.gameObject);
        }
    }

    void PlayParticleEffect(GameObject other)
    {
        GameObject particle = Instantiate(vfx.transform, other.transform).gameObject;
    }
}
