using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wall : Damageable {
    public float health = 100f;
    private NavMeshSurface surface;

    private GameObject attacker = null;
    
    public void SetAttacker(GameObject go) {
        attacker = go;
    }

    public void SetSurface(NavMeshSurface surface) {
        this.surface = surface;
    }

    public override void Damage(float amount) {
        health -= amount;
        if (health <= 0f)
            DestroyWall();
    }

    private void DestroyWall() {
        gameObject.layer = 11;
        surface.UpdateNavMesh(surface.navMeshData);
        attacker.GetComponent<Enemy>().StopAttacking();
        Destroy(gameObject);
    }
}
