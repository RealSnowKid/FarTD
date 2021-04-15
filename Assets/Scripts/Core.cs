using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Core : MonoBehaviour {
    private float health = 100f;
    private Slider slider;

    private void Start() {
        // temp
        slider = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Slider>();
    }

    public void Damage(float amount) {
        health -= amount;
        slider.value = health;
        if(health <= 0)
            Lose();
    }

    void Lose() {
        Debug.Log("ded");
    }
}
