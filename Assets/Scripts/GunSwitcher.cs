using UnityEngine;

public class GunSwitcher : MonoBehaviour {
    private BuildGun build;

    private void Start() {
        build = GetComponent<BuildGun>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            build.Disable();
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            build.Enable();
        }
    }
}
