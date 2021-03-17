using UnityEngine;
using UnityEngine.UI;

public class GunSwitcher : MonoBehaviour {
    public BuildGun build;
    public ShootGun shoot;

    public Text buildText;
    public Text shootText;

    private bool init = false;

    private void Start() {
        build = transform.GetChild(0).GetComponent<BuildGun>();
        shoot = transform.GetChild(0).GetComponent<ShootGun>();

        // ONLY FOR TESTING PURPOSES
        buildText = GameObject.Find("BuildingText").GetComponent<Text>();
        shootText = GameObject.Find("ShootingText").GetComponent<Text>();

        init = true;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1) || init) {
            shoot.Enable();
            build.Disable();

            shootText.color = Color.red;
            buildText.color = Color.white;
            init = false;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            shoot.Disable();
            build.Enable();

            shootText.color = Color.white;
            buildText.color = Color.red;
        }
    }

    public void UpdateBuildGun(GameObject obj) {
        build.ChangeBuildObject(obj);
    }
}
