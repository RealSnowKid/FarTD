using UnityEngine;

public class DestroyGun : MonoBehaviour {
    public bool gunEnabled = false;

    private float distance = 5f;
    private RaycastHit hitInfo;

    private GameObject building;

    public void Enable() {
        gunEnabled = true;
    }

    public void Disable() {
        gunEnabled = false;
    }

    private void Update() {
        if (gunEnabled) {
            // if he look at a building
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, distance)) {
                if(hitInfo.transform.GetComponent<Building>() != null) {
                    building = hitInfo.transform.gameObject;
                } else {
                    building = null;
                }
            } else {
                building = null;
            }
        }

        if(Input.GetButtonDown("Fire1") && building != null) {
            Destroy(building);
        }
    }
}
