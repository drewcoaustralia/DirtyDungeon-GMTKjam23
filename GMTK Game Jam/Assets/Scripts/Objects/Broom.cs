using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : MonoBehaviour
{
    public Renderer rend;

    public void Pickup()
    {
        rend.enabled = false;
    }

    public void PutDown()
    {
        rend.enabled = true;
    }
}
