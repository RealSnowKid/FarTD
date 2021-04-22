using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Smelter : Building {
    public GameObject fuel;
    public GameObject input;
    public GameObject output;

    public float time = 4.5f;

    public GameObject gui;

    private bool isBuilt = false;

    [SerializeField] private Transform itemsParent;

    [SerializeField] private GameObject ironiumItem;
    [SerializeField] private GameObject zoniumItem;

    [SerializeField] private GameObject ironiumSmelt;
    [SerializeField] private GameObject zoniumSmelt;
    [SerializeField] private GameObject ventiumSmelt;
    [SerializeField] private GameObject memiumSmelt;
    [SerializeField] private GameObject unobtaniumSmelt;

    [SerializeField] private GameObject ironiumSmeltBlock;
    [SerializeField] private GameObject zoniumSmeltBlock;
    [SerializeField] private GameObject ventiumSmeltBlock;
    [SerializeField] private GameObject memiumSmeltBlock;
    [SerializeField] private GameObject unobtaniumSmeltBlock;

    public bool isConveyor = false;
    [SerializeField] private Transform spawnTransform;
    private bool hasFuel = false;
    [SerializeField] private AudioSource smeltingSound;

    public void Start() {
        // temporary solution again lol
        if (!isConveyor) {
            gui = GameObject.Find("GUI").GetComponent<Inventory>().GetSmelteryGUI();
            itemsParent = gui.transform.parent.GetChild(4);
        }
    }
    public void Build() {
        isBuilt = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerControl>() != null && isBuilt && !isConveyor) {
            gui.SetActive(true);
            other.GetComponent<ClosestSmelter>().smelter = gameObject;

            // set apropriate items

            if (fuel != null) {
                fuel.GetComponent<Item>().PutDown(gui.transform.GetChild(2).gameObject);
                fuel.transform.position = fuel.GetComponent<Item>().tile.transform.position;
                gui.transform.GetChild(2).GetComponent<InventoryTile>().item = fuel;
            }
            if (input != null) {
                input.GetComponent<Item>().PutDown(gui.transform.GetChild(1).gameObject);
                input.transform.position = input.GetComponent<Item>().tile.transform.position;
                gui.transform.GetChild(1).GetComponent<InventoryTile>().item = input;
            }
            if (output != null) {
                output.GetComponent<Item>().PutDown(gui.transform.GetChild(3).gameObject);
                output.transform.position = output.GetComponent<Item>().tile.transform.position;
                gui.transform.GetChild(3).GetComponent<InventoryTile>().item = output;
            }
            playerIn = true;
        } else if (other.GetComponent<OreDrop>() != null && isBuilt && isConveyor && !ACR_running) {

            switch (other.GetComponent<OreDrop>().ore) {
                case Ore.Ironium:
                    if (input == null) {
                        Destroy(other.gameObject);
                        input = ironiumSmelt;
                    }
                    break;
                case Ore.Zonium:
                    if (input == null) {
                        Destroy(other.gameObject);
                        input = zoniumSmelt;
                    }
                    break;
                case Ore.Instabilium:
                    if (!hasFuel) {
                        Destroy(other.gameObject);
                        hasFuel = true;
                    }
                    break;
                case Ore.Ventium:
                    if(input == null) {
                        Destroy(other.gameObject);
                        input = ventiumSmelt;
                    }
                    break;
                case Ore.Memium:
                    if(input == null) {
                        Destroy(other.gameObject);
                        input = memiumSmelt;
                    }
                    break;
                case Ore.Unobtanium:
                    if(input == null) {
                        Destroy(other.gameObject);
                        input = unobtaniumSmelt;
                    }
                    break;
            }

            if (input != null && hasFuel) 
                StartCoroutine(SmeltAuto(input));
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<PlayerControl>() != null && isBuilt && !isConveyor) {
            gui.SetActive(false);
            other.GetComponent<ClosestSmelter>().smelter = null;

            if (fuel != null) {
                fuel.transform.position = new Vector3(-20, -20, -20);
                fuel.GetComponent<Item>().tile = null;
                gui.transform.GetChild(2).GetComponent<InventoryTile>().item = null;
            }
            if (input != null) {
                input.transform.position = new Vector3(-20, -20, -20);
                input.GetComponent<Item>().tile = null;
                gui.transform.GetChild(1).GetComponent<InventoryTile>().item = null;
            }
            if (output != null) {
                output.transform.position = new Vector3(-20, -20, -20);
                output.GetComponent<Item>().tile = null;
                gui.transform.GetChild(3).GetComponent<InventoryTile>().item = null;
            }
            playerIn = false;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.GetComponent<PlayerControl>() != null && isBuilt && !isConveyor) {
            gui.transform.GetChild(4).GetComponent<Slider>().value = count / time;
            if (input != null && fuel != null) {
                if (input.GetComponent<Item>().isOre && fuel.GetComponent<Item>().isBurnable && output == null)
                    if(!CR_running) StartCoroutine(Smelt());
            }
        }
    }

    bool playerIn = false;
    bool CR_running = false;
    public GameObject ingot = null;
    float count = 0f;

    private IEnumerator Smelt() {
        CR_running = true;
        bool isIronium = false;
        bool isZonium = false;

        if (input.GetComponent<Item>().caption == "Ironium Ore")
            isIronium = true;
        else if (input.GetComponent<Item>().caption == "Zonium Ore")
            isZonium = true;

        Destroy(input);
        Destroy(fuel);
        smeltingSound.Play(0);
        smeltingSound.loop = true;

        while (count < time) {
            yield return new WaitForSeconds(.2f);
            count += .2f;
        }
        count = 0;

        if (isIronium) ingot = Instantiate(ironiumItem, itemsParent);
        else if (isZonium) ingot = Instantiate(zoniumItem, itemsParent);

        if (ingot != null) {
            ingot.GetComponent<Item>().inventory = gui.transform.parent.parent.GetComponent<Inventory>();
            ingot.GetComponent<Item>().label = gui.transform.parent.GetChild(5).GetComponent<Text>();

            if (playerIn) {
                ingot.GetComponent<Item>().tile = gui.transform.GetChild(3).gameObject;
                gui.transform.GetChild(3).GetComponent<InventoryTile>().item = ingot;
                ingot.transform.position = gui.transform.GetChild(3).position;
            }

            output = ingot;
        } else
            Debug.LogError("ingot not found tf?");
        smeltingSound.Stop();
        CR_running = false;
    }

    bool ACR_running = false;

    private IEnumerator SmeltAuto(GameObject obj) {
        ACR_running = true;
        smeltingSound.Play(0);
        smeltingSound.loop = true;
        yield return new WaitForSeconds(time);
        GameObject newObj = Instantiate(obj, spawnTransform.position, spawnTransform.rotation);
        Debug.Log(newObj.name);
        input = null;
        hasFuel = false;
        smeltingSound.Stop();
        ACR_running = false;
    }
}
