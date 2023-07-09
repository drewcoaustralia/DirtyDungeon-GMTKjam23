using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void Interact(GameObject source, GameObject obj=null);
    public bool UsableWithObj(GameObject obj);
}
