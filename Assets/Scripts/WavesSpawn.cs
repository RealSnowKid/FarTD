using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesSpawn : MonoBehaviour {
    [SerializeField] private Text wavesCounter;
    [SerializeField] private Text wavesTimer;

    [SerializeField] private Transform enemiesParent;
    [SerializeField] private Transform spawn;
    [SerializeField] private List<GameObject> spawnLocations;

    [SerializeField] private GameObject landEnemy;
    [SerializeField] private GameObject airEnemy;

    public List<GameObject> spawnedEnemies;

    private int nrLandEnemies = 1;
    private int nrAirEnemies = 0;

    private float waveDelay = 20f;

    private float timer;

    private bool isWave = false;

    private int waveCount = 0;

    public void SetSpawnLocation(GameObject spawn) {
        spawnLocations.Add(spawn);
    }

    public void Setup() {
        spawnLocations = new List<GameObject>();
        spawnedEnemies = new List<GameObject>();
        timer = waveDelay;
    }

    void Update() {
        if (!isWave) UpdateTimer();

        if(timer <= 0f && !isWave) {
            isWave = true;
            SpawnNextWave();
            timer = waveDelay;
        }
    }

    private void SpawnNextWave() {

        // randomize spawn location
        int rnd = Random.Range(0, 3);
        spawn.transform.position = spawnLocations[rnd].transform.position;

        waveCount++;
        wavesCounter.text = waveCount.ToString();

        for(int i=0; i<nrLandEnemies; i++) {
            GameObject enemy = Instantiate(landEnemy, spawn.position, Quaternion.Euler(new Vector3(-90f, 0f,0f)), enemiesParent);
            spawnedEnemies.Add(enemy);
        }
        for(int i=0; i<nrAirEnemies; i++) {
            GameObject enemy = Instantiate(airEnemy, spawn.position, Quaternion.Euler(new Vector3(-90f, 0f, 0f)), enemiesParent);
            spawnedEnemies.Add(enemy);
        }

        nrLandEnemies++;
        nrAirEnemies++;
    }

    public void Remove(GameObject obj) {
        spawnedEnemies.Remove(obj);
        if (spawnedEnemies.Count == 0) isWave = false;
    }

    private void UpdateTimer() {
        timer -= Time.deltaTime;

        int min = (int)(timer / 60);
        int sec = (int)(timer % 60);

        wavesTimer.text = min.ToString() + ":" + (sec / 10 > 0 ? sec.ToString() : "0" + sec.ToString());
    }
}
