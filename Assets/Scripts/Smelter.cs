using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : Building {
    public GameObject fuel;
    public GameObject input;
    public GameObject output;

    public float time = 5f;

    public GameObject gui;

    private bool isBuilt = false;

    public void Start() {
        // temporary solution again lol
        gui = GameObject.Find("Canvas").GetComponent<Inventory>().GetSmelteryGUI();
    }
    public void Build() {
        isBuilt = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<PlayerControl>() != null && isBuilt) {
            gui.SetActive(true);
            other.GetComponent<ClosestSmelter>().smelter = gameObject;

            // set apropriate items

            if (fuel != null) {
                fuel.GetComponent<Item>().PutDown(gui.transform.GetChild(2).gameObject);
                fuel.transform.position = fuel.GetComponent<Item>().tile.transform.position;
            }
            if (input != null) {
                input.GetComponent<Item>().PutDown(gui.transform.GetChild(1).gameObject);
                input.transform.position = input.GetComponent<Item>().tile.transform.position;
            }
            if (output != null) {
                output.GetComponent<Item>().PutDown(gui.transform.GetChild(3).gameObject);
                output.transform.position = output.GetComponent<Item>().tile.transform.position;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<PlayerControl>() != null && isBuilt) {
            gui.SetActive(false);
            other.GetComponent<ClosestSmelter>().smelter = null;

            if (fuel != null) fuel.transform.position = new Vector3(-20, -20, -20);
            if (input != null) input.transform.position = new Vector3(-20, -20, -20);
            if (output != null) output.transform.position = new Vector3(-20, -20, -20);
        }
    }
}
