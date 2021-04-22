using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject GUI;
    public WavesSpawn wavesSpawn;
    public List<GameObject> Pages = new List<GameObject>();
    private GameObject currentPage;

    private void Awake()
    {
        currentPage = Pages[0];
        currentPage.SetActive(true);
    }

    public void ChangePage(int pageNumber)
    {
        currentPage.SetActive(false);
        currentPage = Pages[pageNumber];
        currentPage.SetActive(true);
    }

    public void CloseTutorial()
    {
        currentPage.SetActive(false);
        wavesSpawn.PauseUnpause(false);
        GUI.GetComponent<Inventory>().enabled = true;
        GUI.GetComponent<Inventory>().player.GetComponent<PlayerControl>().enabled = true;
        GUI.GetComponent<Inventory>().player.GetComponent<GunSwitcher>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }
}
