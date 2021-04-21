using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float damage;

    public void SetDamage(float dmg) {
        damage = dmg;
    }

    public void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.GetComponent<Enemy>() == null) {
            GetComponent<ParticleSystem>().Play();

            GetComponent<Rigidbody>().isKinematic = true;
            Destroy(GetComponent<MeshRenderer>());
            Destroy(GetComponent<Collider>());

            Destroy(gameObject, 0.5f);
        }
    }
}
