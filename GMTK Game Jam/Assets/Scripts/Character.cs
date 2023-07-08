using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

  private Rigidbody rb;

  [SerializeField] private float speed = 100f;
  [SerializeField] private Transform visualBody;

  private Vector3 playerInput;

  void Awake() {
    rb = GetComponent<Rigidbody>();
  }

  void Update() {
    playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

    if (rb.velocity.magnitude > 1f) {
      visualBody.rotation = Quaternion.LookRotation(rb.velocity);
    }
  }

  void FixedUpdate() {
    rb.velocity += playerInput * speed;
  }
}
