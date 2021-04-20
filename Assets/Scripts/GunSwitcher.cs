using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class GunSwitcher : MonoBehaviour {
    public BuildGun build;
    public ShootGun shoot;
    public DestroyGun destroy;

    public Text buildText;
    public Text shootText;
    public Text mineText;
    public Text destroyText;

    private bool init = false;
    public bool isMining = false;

    [SerializeField] private GameObject colorObject;
    [SerializeField] private Light colorLight;

    private void Start() {
        build = transform.GetChild(0).GetComponent<BuildGun>();
        shoot = transform.GetChild(0).GetComponent<ShootGun>();
        destroy = transform.GetChild(0).GetComponent<DestroyGun>();

        // ONLY FOR TESTING PURPOSES
        buildText = GameObject.Find("BuildingText").GetComponent<Text>();
        shootText = GameObject.Find("ShootingText").GetComponent<Text>();
        mineText = GameObject.Find("MiningText").GetComponent<Text>();
        destroyText = GameObject.Find("DestroyingText").GetComponent<Text>();

        build.surface = (NavMeshSurface) GameObject.Find("NavMesh").GetComponents(typeof(NavMeshSurface))[0];

        init = true;
    }

    private short temp = 0;

    public void OpenedInvenory() {
        if (shoot.gunEnabled) {
            temp = 1;
        } else if (build.gunEnabled) {
            temp = 2;
        } else if (destroy.gunEnabled) {
            temp = 3;
        } else if (isMining) {
            temp = 4;
        }
        shoot.Disable();
        build.Disable();
        isMining = false;
        destroy.Disable();
    }

    public void ClosedInventory() {
        if(temp == 1) {
            shoot.Enable();
        } else if(temp == 2) {
            build.Enable();
            //UpdateBuildGun(build.buildObject);
        } else if(temp == 3) {
            destroy.Enable();
        } else if(temp == 4) {
            isMining = true;
        }

        temp = 0;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1) || init) {
            shoot.Enable();
            build.Disable();
            isMining = false;
            destroy.Disable();

            shootText.color = Color.red;
            buildText.color = Color.white;
            mineText.color = Color.white;
            destroyText.color = Color.white;
            init = false;

            colorObject.GetComponent<Renderer>().material.color = Color.white;
            colorLight.color = Color.white;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            shoot.Disable();
            build.Enable();
            isMining = false;
            destroy.Disable();

            shootText.color = Color.white;
            buildText.color = Color.red;
            mineText.color = Color.white;
            destroyText.color = Color.white;
            colorObject.GetComponent<Renderer>().material.color = Color.yellow;
            colorLight.color = Color.yellow;
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            shoot.Disable();
            build.Disable();
            isMining = true;
            destroy.Disable();

            shootText.color = Color.white;
            buildText.color = Color.white;
            mineText.color = Color.red;
            destroyText.color = Color.white;
            colorObject.GetComponent<Renderer>().material.color = Color.green;
            colorLight.color = Color.green;
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            shoot.Disable();
            build.Disable();
            isMining = false;
            destroy.Enable();

            shootText.color = Color.white;
            buildText.color = Color.white;
            mineText.color = Color.white;
            destroyText.color = Color.red;
            colorObject.GetComponent<Renderer>().material.color = Color.red;
            colorLight.color = Color.red;
        }
    }

    public void UpdateBuildGun(GameObject obj) {
        build.ChangeBuildObject(obj);
    }
}
