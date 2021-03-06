using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    private WavesSpawn master;

    public float health = 100f;
    public float damage = 5f;
    public float attackDelay = 1;
    public AudioClip DeathSound;

    private Animator animator;

    private GameObject playerCore = null;
    private GameObject target = null;

    private bool isAttacking = false;
    public bool isGround = false;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void SetScript(WavesSpawn ws) {
        master = ws;
    }
    public void SetTarget(GameObject core) {
        playerCore = core;
        GetComponent<NavMeshAgent>().SetDestination(playerCore.transform.position);
    }

    public void Damage(float amount) {
        health -= amount;
        if (health <= 0f) Die();
    }

    private void Die() {
        AudioSource.PlayClipAtPoint(DeathSound, transform.position);
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
        if(col.GetComponent<Damageable>() != null && !isAttacking) {

            if(col.GetComponent<ConveyorMovement>() != null && col.GetComponent<ConveyorMovement>().subPart) {
                return;
            }
            
            if (col.GetComponent<Smelter>() != null && col.GetComponent<Smelter>().isConveyor) {
                target = col.transform.parent.gameObject;
            } else {
                target = col.gameObject;
            }

            isAttacking = true;
            GetComponent<NavMeshAgent>().enabled = false;
            animator.SetBool("isAttacking", true);

            if (isGround) {
                transform.LookAt(target.transform);
            }

            if (col.GetComponent<Wall>() != null)
                col.GetComponent<Wall>().SetAttacker(gameObject);

            InvokeRepeating("DealDamage", 0, attackDelay);
        }
    }

    private void OnTriggerExit(Collider col) {
        if (col.GetComponent<Damageable>() != null) {
            StopAttacking();
        }
    }

    public void StopAttacking() {
        CancelInvoke("DealDamage");
        animator.SetBool("isAttacking", false);

        target = null;
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<NavMeshAgent>().SetDestination(playerCore.transform.position);
        isAttacking = false;
    }

    void DealDamage() {
        try {
            target.GetComponent<Damageable>().Damage(damage);
        } catch (SystemException) {
            StopAttacking();
        }
    }

    private void Update() {
        /*
        Vector3 lookPos = playerCore.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        */
    }
}
