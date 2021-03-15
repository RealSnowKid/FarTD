using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tile : MonoBehaviour {

    public bool hasOre;

    void OnDrawGizmos() {
        //Handles.Label(transform.position, hasOre ? "true" : "false");
    }
}
