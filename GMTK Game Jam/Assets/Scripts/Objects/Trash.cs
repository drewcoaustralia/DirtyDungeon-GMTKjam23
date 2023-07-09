using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TaskInstance))]
public class Trash : MonoBehaviour {
    public void Clean() {
        GetComponent<TaskInstance>().Complete();
        foreach (var renderer in GetComponents<MeshRenderer>()) {
            renderer.enabled = false;
        }
    }
}
