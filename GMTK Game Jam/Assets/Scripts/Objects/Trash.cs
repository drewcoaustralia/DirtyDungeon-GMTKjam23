using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {
    public void Clean() {
        foreach (var renderer in GetComponents<MeshRenderer>()) {
            renderer.enabled = false;
            //destroy after effect
        }
    }
}
