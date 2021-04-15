using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEnemyHitbox : MonoBehaviour {
    private Enemy root;

    private void Start() {
        root = transform.parent.parent.GetComponent<Enemy>();
    }

    private void OnCollisionEnter(Collision collision) {
        root.OnCollisionEnter(collision);
        Debug.Log("a");
    }
}
