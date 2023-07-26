using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuAnimator : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        GetComponent<InteractionController>().enabled = false;
        GetComponent<BasicController>().enabled = false;
        anim.SetTrigger("StartSweepingMenu");
    }
}
