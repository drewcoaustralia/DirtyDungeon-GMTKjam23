using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyColor;
    public Color color;

    // TODO change colour

    public void Open()
    {
        gameObject.GetComponent<Pickuppable>().PutDown();
    }
}
