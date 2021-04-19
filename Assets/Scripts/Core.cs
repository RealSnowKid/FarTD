using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Core : Damageable {
    private float health = 100f;
    private Slider slider;

    private void Start() {
        // temp
        slider = GameObject.Find("GUI").transform.GetChild(0).GetComponent<Slider>();
    }

    public override void Damage(float amount) {
        health -= amount;
        slider.value = health;
        if(health <= 0)
            Lose();
    }

    void Lose() {
        Debug.Log("ded");
    }
}
