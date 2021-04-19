using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public GameObject playerUI;
    public Camera menuCamera;

    public MapGeneration mapAgent;
    public WavesSpawn wavesAgent;

    public GameObject settingsMenu;

    private int mapSize = 100;
    public Text sizeLabel;

    public void StartGame() {
        playerUI.SetActive(true);
        menuCamera.gameObject.SetActive(false);
        gameObject.SetActive(false);

        mapAgent.mapX = mapSize;
        mapAgent.mapY = mapSize;

        mapAgent.BuildMap();

        wavesAgent.gameObject.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ToggleSettings(bool open) {
        settingsMenu.SetActive(open);
    }

    public void ChangeMapSise(float value) {
        mapSize = (int)value;
        sizeLabel.text = value.ToString();
    }

    public void Update() {
        if(menuCamera.enabled)
            menuCamera.transform.Rotate(new Vector3(0f, Time.deltaTime, 0f));
    }

    public void ChangeDifficulty(int difficulty) {
        mapAgent.ChangeDifficulty(difficulty);
        wavesAgent.ChangeDifficulty(difficulty);
    }

    public void ReloadScene() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
