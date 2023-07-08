using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterableCoinPot : MonoBehaviour
{
    public GameObject shattered;
    public float shatterForce=10f;
    public GameObject coin;
    public float coinForce = 5f;
    private Collider col;
    public int numCoins = 3;

    void Awake()
    {
        col = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        ShatterThis();
    }

    void ShatterThis()
    {
        GameObject shatt = Instantiate(shattered, transform.position, transform.rotation);
        foreach(Rigidbody rb in shatt.GetComponentsInChildren<Rigidbody>())
        {
            // Vector3 frc = (rb.transform.position - transform.position).normalized * force; // add random angles?
            Vector3 frc = (Random.onUnitSphere).normalized;
            frc = new Vector3(frc.x, Mathf.Clamp(Mathf.Abs(frc.y), 0.6f, 1f), frc.z) * shatterForce;
            rb.AddForce(frc);
        }
        for (int i=0; i<numCoins; i++)
        {
            GameObject newCoin = Instantiate(coin, transform.position, Quaternion.Euler(90,0,0), transform);
            Vector3 direction = Random.insideUnitSphere.normalized;
            direction = new Vector3(direction.x, Mathf.Clamp(Mathf.Abs(direction.y), 0.6f, 1f), direction.z);
            newCoin.GetComponent<Rigidbody>().AddForce(direction * coinForce, ForceMode.Impulse);
        }
        Destroy(gameObject);
    }
}
