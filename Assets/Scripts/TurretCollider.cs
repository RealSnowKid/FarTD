using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretCollider : MonoBehaviour {
    private Turret root;
    [SerializeField] private GameObject canvas;

    public Text bulletCount;

    private Inventory inventory;

    public int bulletsAmountPerItem = 4;

    private bool isBuilt = false;
    private bool isConveyor = false;

    private void Start() {
        root = transform.parent.GetComponent<Turret>();
        canvas.SetActive(false);
        inventory = GameObject.Find("GUI").GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider col) {
        if (isConveyor) {
            if(col.GetComponent<OreDrop>() != null) {
                // can be optimized
                if(col.GetComponent<OreDrop>().Item.caption == "Bullet") {
                    if(root.bullets + bulletsAmountPerItem <= root.maxBullets) {
                        root.bullets += bulletsAmountPerItem;
                        Destroy(col.gameObject);
                    }
                }
            }
        } else {
            if (col.GetComponent<PlayerControl>() != null && isBuilt) {
                canvas.SetActive(true);
            }
        }

    }

    private void OnTriggerExit(Collider col) {
        if (isConveyor) {

        } else {
            if (col.GetComponent<PlayerControl>() != null && isBuilt) {
                canvas.SetActive(false);
            }
        }
    }

    public void LoadAmmo() {
        if(inventory.pickedItem.GetComponent<Item>().caption == "Bullet" && root.bullets + bulletsAmountPerItem <= root.maxBullets) {
            root.bullets += bulletsAmountPerItem;
            bulletCount.text = root.bullets.ToString();
            Destroy(inventory.pickedItem);
            root.ammoLight.color = Color.green;
        }
    }

    public void Build(bool isConveyor) {
        isBuilt = true;
        this.isConveyor = isConveyor;
        if (isConveyor) {
            GetComponent<BoxCollider>().size = new Vector3(.3f, .5f, .3f);
        }
    }
}
