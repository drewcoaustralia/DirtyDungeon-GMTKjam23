using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessMaker : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;

    public float minInterval;
    public float maxInterval;
    private float currentInterval;
    private float lastThrown = 0f;

    public List<float> times;
    private bool chaos = false;

    public List<GameObject> mess;
    public List<GameObject> messChaos;

    void Awake()
    {
        currentInterval = Random.Range(minInterval, maxInterval);
    }

    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(minX,maxX), Random.Range(minY,maxY), Random.Range(minZ,maxZ));
    }

    void DropMess(Vector3 position)
    {
        if (mess.Count != 0)
        {
            int idx = Random.Range (0, mess.Count);
            GameObject.Instantiate(mess[idx], position, Random.rotation);
        }
    }

    void Update()
    {
        if (!chaos && times.Count <= 0)
        {
            chaos = true;
            foreach (GameObject obj in messChaos)
            {
                mess.Add(obj);
            }
        }

        if (!chaos && Time.time >= times[0])
        {
            DropMess(new Vector3(Random.Range(-3,1),2,-5));
            times.RemoveAt(0);
        }

        if (chaos && Time.time - lastThrown >= currentInterval)
        {
            DropMess(RandomPosition());
            lastThrown = Time.time;
            currentInterval = Random.Range(minInterval, maxInterval);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        var p1 = new Vector3(minX, minY, minZ);
        var p2 = new Vector3(maxX, minY, minZ);
        var p3 = new Vector3(maxX, minY, maxZ);
        var p4 = new Vector3(minX, minY, maxZ);

        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);

        var p5 = new Vector3(minX, maxY, minZ);
        var p6 = new Vector3(maxX, maxY, minZ);
        var p7 = new Vector3(maxX, maxY, maxZ);
        var p8 = new Vector3(minX, maxY, maxZ);

        Gizmos.DrawLine(p5, p6);
        Gizmos.DrawLine(p6, p7);
        Gizmos.DrawLine(p7, p8);
        Gizmos.DrawLine(p8, p5);

        Gizmos.DrawLine(p1, p5);
        Gizmos.DrawLine(p2, p6);
        Gizmos.DrawLine(p3, p7);
        Gizmos.DrawLine(p4, p8);
        }
}
