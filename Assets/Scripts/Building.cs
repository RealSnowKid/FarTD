using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Damageable {
    public float health = 100f;

    public override void Damage(float amount) {
        health -= amount;
        if (health <= 0) {
            if(gameObject.GetComponent<Crafter>() != null)
                Destroy(transform.parent.gameObject);
            else
                Destroy(gameObject);
        }
    }
}
