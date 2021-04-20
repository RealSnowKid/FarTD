using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretCollider : MonoBehaviour {
    private Turret root;
    [SerializeField] private GameObject canvas;

    public Text bulletCount;

    private Inventory inventory;

    public int bulletsAmoundPerItem = 4;

    private bool isBuilt = false;

    private void Start() {
        root = transform.parent.GetComponent<Turret>();
        canvas.SetActive(false);
        inventory = GameObject.Find("GUI").GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider col) {
        if(col.GetComponent<PlayerControl>() != null && isBuilt) {
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider col) {
        if (col.GetComponent<PlayerControl>() != null && isBuilt) {
            canvas.SetActive(false);
        }
    }

    public void LoadAmmo() {
        if(inventory.pickedItem.GetComponent<Item>().caption == "Bullet") {
            root.bullets += bulletsAmoundPerItem;
            bulletCount.text = root.bullets.ToString();
            Destroy(inventory.pickedItem);
        }
    }

    public void Build() {
        isBuilt = true;
    }
}
