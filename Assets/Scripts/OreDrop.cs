using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreDrop : MonoBehaviour {
    public Ore ore;

    private void OnCollisionEnter(Collision collision) {
        // if we hit a tile, initiate self destruction
        if(collision.collider.gameObject.GetComponent<Tile>() != null) {
            Destroy(gameObject, 5f);
        }
    }
}
