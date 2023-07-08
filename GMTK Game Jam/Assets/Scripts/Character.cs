using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

  private Rigidbody rb;

  [SerializeField] private float speed = 100f;
  [SerializeField] private Transform visualBody;

  void Awake() {
    rb = GetComponent<Rigidbody>();
  }

  void Update() {
    if (Input.GetKey(KeyCode.W)) {
      rb.velocity += Vector3.forward * speed * Time.deltaTime;
    }
    if (Input.GetKey(KeyCode.S)) {
      rb.velocity += Vector3.back * speed * Time.deltaTime;
    }
    if (Input.GetKey(KeyCode.A)) {
      rb.velocity += Vector3.left * speed * Time.deltaTime;
    }
    if (Input.GetKey(KeyCode.D)) {
      rb.velocity += Vector3.right * speed * Time.deltaTime;
    }

    if (rb.velocity.magnitude > 0.4f) {
      visualBody.rotation = Quaternion.LookRotation(rb.velocity);
    }
  }
}
