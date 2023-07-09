using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 11, -7);
    [SerializeField] private float lerpSpeed = 0.1f;

    void Start() {
        if (!player) {
            player = GameObject.Find("Player").transform;
        }

        transform.position = player.position + cameraOffset;
    }

    void Update() {
        transform.position = Vector3.Lerp(transform.position, player.position + cameraOffset, lerpSpeed * Time.deltaTime);
    }
}
