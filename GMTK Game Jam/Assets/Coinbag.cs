using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coinbag : MonoBehaviour, IInteractable
{
    public GameObject coin;
    public float force = 10f;
    private Collider col;

    void Awake()
    {
        col = GetComponent<Collider>();
    }

    public void Interact(GameObject source)
    {
        GameObject newCoin = Instantiate(coin, transform.position, Quaternion.Euler(90,0,0), transform);
        Vector3 direction = Random.insideUnitSphere.normalized;
        direction = new Vector3(direction.x, Mathf.Clamp(Mathf.Abs(direction.y), 0.6f, 1f), direction.z);
        newCoin.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
        Debug.DrawRay(transform.position, direction * force, Color.yellow, 30f);
    }
}
