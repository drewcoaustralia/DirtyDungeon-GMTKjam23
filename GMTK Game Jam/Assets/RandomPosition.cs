using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    void Awake()
    {
        foreach (Transform child in transform)
        {
            child.position = new Vector3(Random.Range(-5f, 5f), Random.Range(1f, 50f), Random.Range(-5f, 5f));
        }
    }
}
