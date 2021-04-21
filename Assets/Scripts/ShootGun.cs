using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour {
    public bool gunEnabled = false;
    public AudioClip fireSound;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnLocation;

    private float speed = 150f;

    public float damage = 5f;

    public void Enable() {
        gunEnabled = true;
    }

    public void Disable() {
        gunEnabled = false;
    }

    public void Update() {
        if(Input.GetButtonDown("Fire1") && gunEnabled) {
            GameObject bullet = Instantiate(bulletPrefab, spawnLocation.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(fireSound, spawnLocation.position, 0.04f);

            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
            bullet.GetComponent<Bullet>().SetDamage(damage);

            Destroy(bullet, 5);
        }
    }
}
