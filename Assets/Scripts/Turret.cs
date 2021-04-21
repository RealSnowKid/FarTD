using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour {
    public Transform body;
    public Transform gun;
    public Transform spawnLocation;

    public float health;
    public float radius;
    public float damage;
    public float shootDelay;
    public int maxBullets = 12;
    [SerializeField] private GameObject bulletPrefab;
    public AudioClip gunFireSound;

    public Transform target = null;

    private bool isShooting = false;

    public bool isAir = false;
    public bool isConveyor = false;

    public int bullets = 0;

    [SerializeField] private Text bulletLabel;

    public Light ammoLight;

    public void Build() {
        transform.GetChild(1).GetComponent<TurretCollider>().Build(isConveyor);
    }

    private void Start() {
        GetComponent<SphereCollider>().radius = radius;
    }

    public bool visible = false;

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Enemy>() != null && target == null) {
            if (isAir) {
                if (!other.GetComponent<Enemy>().isGround) {
                    target = other.transform.GetChild(1).GetChild(2);
                }
            } else {
                if (other.GetComponent<Enemy>().isGround) {
                    target = other.transform.GetChild(1).GetChild(2);
                }
            }

            if(target != null) {
                isShooting = true;
                InvokeRepeating("Shoot", 0, shootDelay);
            }
        }
    }

    private void Update() {
        if (target != null) {
            AimAt(target.position, isAir);
        }

        if (isShooting && target == null) {
            isShooting = false;
            CancelInvoke("Shoot");
        }
    }

    void Shoot() {
        if(bullets <= 0) {
            Debug.LogWarning("turret out of ammo");
            ammoLight.color = Color.red;
        } else {
            GameObject bullet = Instantiate(bulletPrefab, spawnLocation.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().damage = damage;

            bullet.GetComponent<Rigidbody>().AddForce((isAir ? gun.transform.forward : -body.transform.right) * 250f);
            AudioSource.PlayClipAtPoint(gunFireSound, transform.position);
            Destroy(bullet, 5);
            bullets--;

            bulletLabel.text = bullets.ToString();
        }
    }

    private void AimAt(Vector3 pos, bool isAir) {
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 playerFwd = new Vector2(transform.forward.x, transform.forward.z);

        float angle = Vector2.SignedAngle(new Vector2(pos.x, pos.z) - playerPos, playerFwd) + 90f;

        body.transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));

        if (isAir) {
            float diff = pos.y - gun.transform.position.y;
            float dist = (gun.transform.position - pos).magnitude;

            float angle2 = Mathf.Rad2Deg * Mathf.Asin(diff / dist);

            gun.transform.rotation = Quaternion.Euler(new Vector3(angle2 * -1, angle - 90f, 90f));
        }
    }
}
