using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

  private Rigidbody rb;

  [SerializeField] private float speed = 70f;
  [SerializeField] private float jumpStrength = 10f;
  [SerializeField] private Transform visualBody;
  [SerializeField] private float airControl = 0.4f;

  private Vector3 playerInput;
  private bool jumpPressed;
  private bool isGrounded = true;

  void Awake() {
    rb = GetComponent<Rigidbody>();
  }

  void Update() {
    playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

    jumpPressed |= Input.GetButtonDown("Jump") && isGrounded;

    if (rb.velocity.magnitude > 1f) {
      var lookRotation = Vector3.Scale(rb.velocity, new Vector3(1, 0, 1));
      if (lookRotation != Vector3.zero) {
        visualBody.rotation = Quaternion.LookRotation(lookRotation);
      }
    }
  }

  void FixedUpdate() {
    var airControlInfluence = isGrounded ? 1f : airControl;
    if (playerInput.magnitude < 0.5f) {
      rb.velocity = Vector3.Scale(rb.velocity, new Vector3(0.91f, 1, 0.91f));
    }
    rb.velocity += playerInput * (speed * airControlInfluence);

    var somethingBelow = Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.down, out RaycastHit hit, 0.2f);
    isGrounded = somethingBelow;

    if (jumpPressed) {
      rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
      jumpPressed = false;
    }
  }
}
