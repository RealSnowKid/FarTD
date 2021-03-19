using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMovement : MonoBehaviour {
    private float speed = 2f;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.collider.gameObject.GetComponent<OreDrop>() != null) {
            Vector3 pos = rb.position;

            rb.position += transform.forward * speed * Time.fixedDeltaTime * -1;
            rb.MovePosition(pos);
        }
    }
}
