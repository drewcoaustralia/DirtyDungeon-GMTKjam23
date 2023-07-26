using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCameraFix : MonoBehaviour
{
    [SerializeField]private Transform player;
    public float offset = 0f;

    // TODO: Add lerping

    void Awake()
    {
        if (player == null) player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (player.position.z > transform.position.z + offset) transform.localScale = new Vector3(1,0.25f,1);
        else transform.localScale = new Vector3(1,1,1);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, 0.25f);
        Gizmos.DrawLine(transform.position, transform.position + (offset * Vector3.forward));
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + (offset * Vector3.forward), 0.25f);
    }
}
