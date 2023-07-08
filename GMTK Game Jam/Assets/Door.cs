using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string keyColor;

    void OnCollisionEnter(Collision col)
    {
        GameObject obj = col.gameObject;
        Key key = obj.GetComponent<Key>();
        if (key == null) return;
        if (key.doorColor == keyColor)
        {
            key.Open();
            Open();
        }
    }

    void Open()
    {
        Destroy(gameObject);
    }
}
