using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Core : Damageable {
    private float health = 100f;
    private Slider slider;
    public GameObject player;
    public AudioSource coreDeathSound;

    private GameObject gameOverScreen;

    private GameObject gui;

    private void Start() {
        // temp
        gui = GameObject.Find("GUI");
        slider = gui.transform.GetChild(0).GetComponent<Slider>();

        gameOverScreen = gui.GetComponent<Inventory>().EndGameScreen;
    }

    private void Update()
    {
        // Debuggin destroy core
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            Damage(100);
        }
    }

    public override void Damage(float amount) {
        health -= amount;
        slider.value = health;
        if(health <= 0)
            Lose();
    }

    void Lose() {
        coreDeathSound.PlayOneShot(coreDeathSound.clip);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        gui.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.SetActive(false);

        GameObject cam = new GameObject("GameOver Cam");
        cam.AddComponent<AudioListener>();
        gameOverScreen.GetComponent<AudioSource>().Play(0);
        gameOverScreen.GetComponent<AudioSource>().loop = true;
        cam.transform.position = player.transform.position + new Vector3(0f, 20f, 0f);
        cam.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
        cam.AddComponent<Camera>();
    }
}
