using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorMovement : MonoBehaviour {
    private float speed = 2f;

    private void OnCollisionStay(Collision collision) {
        if (collision.collider.gameObject.GetComponent<OreDrop>() != null) {
            collision.collider.gameObject.transform.Translate(transform.forward * speed * Time.fixedDeltaTime, Space.World);
        }
    }
}
