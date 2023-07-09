using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void Awake()
    {
        if (transform.parent != null) {
            Physics.IgnoreCollision(GetComponent<Collider>(), transform.parent.GetComponent<Collider>());
            transform.SetParent(null, true);
        }
    }
}
