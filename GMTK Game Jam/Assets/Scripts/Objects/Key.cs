using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyColor;

    public void Open()
    {
        gameObject.GetComponent<Pickuppable>().PutDown();
    }
}
