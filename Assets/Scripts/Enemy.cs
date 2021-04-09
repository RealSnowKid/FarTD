using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private WavesSpawn master;

    public float health = 100f;

    public void SetScript(WavesSpawn ws) {
        master = ws;
    }

    public void Damage(float amount) {
        health -= amount;
        if (health <= 0f) Die();
    }

    private void Die() {
        master.Remove(gameObject);
        Destroy(gameObject);
    }
}
