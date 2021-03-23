using UnityEngine;
using UnityEngine.UI;

public class GunSwitcher : MonoBehaviour {
    public BuildGun build;
    public ShootGun shoot;

    public Text buildText;
    public Text shootText;
    public Text mineText;

    private bool init = false;
    public bool isMining = false;

    private void Start() {
        build = transform.GetChild(0).GetComponent<BuildGun>();
        shoot = transform.GetChild(0).GetComponent<ShootGun>();

        // ONLY FOR TESTING PURPOSES
        buildText = GameObject.Find("BuildingText").GetComponent<Text>();
        shootText = GameObject.Find("ShootingText").GetComponent<Text>();
        mineText = GameObject.Find("MiningText").GetComponent<Text>();

        init = true;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1) || init) {
            shoot.Enable();
            build.Disable();
            isMining = false;

            shootText.color = Color.red;
            buildText.color = Color.white;
            mineText.color = Color.white;
            init = false;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            shoot.Disable();
            build.Enable();
            isMining = false;

            shootText.color = Color.white;
            buildText.color = Color.red;
            mineText.color = Color.white;
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            shoot.Disable();
            build.Disable();
            isMining = true;

            shootText.color = Color.white;
            buildText.color = Color.white;
            mineText.color = Color.red;
        }
    }

    public void UpdateBuildGun(GameObject obj) {
        build.ChangeBuildObject(obj);
    }
}
