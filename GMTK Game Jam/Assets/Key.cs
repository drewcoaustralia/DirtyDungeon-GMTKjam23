using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string doorColor;

    public void Open()
    {
        gameObject.GetComponent<Pickuppable>().PutDown();
    }
}
