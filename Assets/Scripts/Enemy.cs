using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    private WavesSpawn master;

    public float health = 100f;
    public float damage = 5f;
    public float attackDelay = 1;

    private Animator animator;

    private GameObject target = null;

    private void Start() {
        animator = GetComponent<Animator>();
    }

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

    public void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.GetComponent<Bullet>() != null) {
            Damage(collision.gameObject.GetComponent<Bullet>().damage);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider col) {
        if(col.GetComponent<Core>() != null) {
            GetComponent<NavMeshAgent>().enabled = false;
            animator.SetBool("isAttacking", true);
            //transform.LookAt(col.transform);

            target = col.gameObject;
            InvokeRepeating("DealDamage", 0, attackDelay);
        }
    }

    private void OnTriggerExit(Collider col) {
        if (col.GetComponent<Core>() != null) {
            GetComponent<NavMeshAgent>().enabled = true;
            animator.SetBool("isAttacking", false);

            target = null;
            CancelInvoke("DealDamage");
        }
    }

    void DealDamage() {
        target.GetComponent<Core>().Damage(damage);
    }
}
