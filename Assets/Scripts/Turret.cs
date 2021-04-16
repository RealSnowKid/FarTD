using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Building {
    public Transform body;
    public Transform gun;
    public Transform spawnLocation;

    public float radius;
    public float damage;
    public float shootDelay;
    [SerializeField] private GameObject bulletPrefab;

    public Transform target = null;

    private bool isShooting = false;

    private void Start() {
        GetComponent<SphereCollider>().radius = radius;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Enemy>() != null && target == null && !other.GetComponent<Enemy>().isGround) {
            target = other.transform.GetChild(1).GetChild(2);
            isShooting = true;
            InvokeRepeating("Shoot", 0, shootDelay);
        }
    }

    private void Update() {
        if (target != null)
            AimAt(target.position);

        if (isShooting && target == null) {
            isShooting = false;
            CancelInvoke("Shoot");
        }
    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, spawnLocation.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().damage = damage;

        bullet.GetComponent<Rigidbody>().AddForce(gun.transform.forward * 150f);
    }

    private void AimAt(Vector3 pos) {
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 playerFwd = new Vector2(transform.forward.x, transform.forward.z);

        float angle = Vector2.SignedAngle(new Vector2(pos.x, pos.z) - playerPos, playerFwd) + 90f;

        float diff = pos.y - gun.transform.position.y;
        float dist = (gun.transform.position - pos).magnitude;
        float angle2 = Mathf.Rad2Deg * Mathf.Asin(diff / dist);

        body.transform.rotation = Quaternion.Euler(new Vector3(-90f, angle, 0f));
        gun.transform.rotation = Quaternion.Euler(new Vector3(angle2 * -1, angle - 90f, 90f));
    }
}
