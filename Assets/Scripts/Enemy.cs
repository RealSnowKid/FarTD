using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    private WavesSpawn master;
    public float health = 100f;

    public void SetScript(WavesSpawn ws) {
        master = ws;
    }

    public void SetTarget(GameObject target) {
        GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
    }

    public void Damage(float amount) {
        health -= amount;
        if (health <= 0f) Die();
    }

    private void Die() {
        master.Remove(gameObject);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.GetComponent<Bullet>() != null) {
            Damage(collision.gameObject.GetComponent<Bullet>().damage);
        }
    }
}
