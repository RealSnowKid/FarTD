using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum Ore {
    None,
    Ironium,
    Zonium,
    Ventium,
    Memium,
    Unobtanium,
    Instabilium
}

public class Tile : MonoBehaviour {
    public Ore oreType = 0;

    //happens only in the editor
    void OnDrawGizmos() {
        if (oreType == Ore.Ironium) {
            Handles.Label(transform.position, "Ironium");
        } else if (oreType == Ore.Zonium) {
            Handles.Label(transform.position, "Zonium");
        } else if (oreType == Ore.Ventium) {
            Handles.Label(transform.position, "Ventium");
        } else if (oreType == Ore.Memium) {
            Handles.Label(transform.position, "Memium");
        } else if (oreType == Ore.Unobtanium) {
            Handles.Label(transform.position, "Unobtanium");
        } else if (oreType == Ore.Instabilium) {
            Handles.Label(transform.position, "Instabilium");
        }
    }

    public void UpdateVisuals() {
        if (oreType == Ore.Ironium) {
            GetComponent<Renderer>().material.color = new Color(0.8f, 0.5f, 0.1f);
        } else if (oreType == Ore.Zonium) {
            GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f);
        } else if (oreType == Ore.Ventium) {
            GetComponent<Renderer>().material.color = new Color(0.3f, 0.7f, 0.2f);
        } else if (oreType == Ore.Memium) {
            GetComponent<Renderer>().material.color = new Color(0.4f, 0.9f, 0.9f);
        } else if (oreType == Ore.Unobtanium) {
            GetComponent<Renderer>().material.color = new Color(0.2f, 0f, 0.5f);
        } else if (oreType == Ore.Instabilium) {
            GetComponent<Renderer>().material.color = new Color(1f, 1f, 0.7f);
        } else {
            GetComponent<Renderer>().material.color = new Color(0.7f, 0.7f, 0.6f);
        }
    }
}
