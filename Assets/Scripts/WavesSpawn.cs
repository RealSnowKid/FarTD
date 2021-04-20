using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WavesSpawn : MonoBehaviour {
    [SerializeField] private Text wavesCounter;
    [SerializeField] private Text wavesTimer;

    [SerializeField] private Text scoreCounter;
    [SerializeField] private Text difficultyLabel;

    [SerializeField] private Transform enemiesParent;
    [SerializeField] private Transform spawn;
    [SerializeField] private List<GameObject> spawnLocations;

    [SerializeField] private GameObject landEnemy;
    [SerializeField] private GameObject airEnemy;

    private List<GameObject> spawnedEnemies;

    private GameObject playerCore;
    private GameObject compass;

    private int nrLandEnemies = 1;
    private int nrAirEnemies = 0;

    private float waveDelay = 20f;

    private float timer;

    private bool isWave = false;

    private int waveCount = 0;

    private int difficulty;

    public void ChangeDifficulty(int difficulty) {
        switch (difficulty) {
            case 0:
                nrLandEnemies = 0;
                nrAirEnemies = 0;
                difficultyLabel.text = "Easy";
                difficultyLabel.color = new Color(0, 1, 0.1951792f);
                break;
            case 1:
                nrLandEnemies = 1;
                nrAirEnemies = 0;
                difficultyLabel.text = "Medium";
                difficultyLabel.color = new Color(0, 0.8145137f, 1);
                break;
            case 2:
                nrLandEnemies = 1;
                nrAirEnemies = 1;
                difficultyLabel.text = "Hard";
                difficultyLabel.color = new Color(1, 0, 0.01010323f);
                break;
        }

        this.difficulty = difficulty;
    }

    public void SetSpawnLocation(GameObject spawn) {
        spawnLocations.Add(spawn);
        spawn.transform.parent = enemiesParent;
    }

    public void Setup() {
        spawnLocations = new List<GameObject>();
        spawnedEnemies = new List<GameObject>();
        timer = waveDelay;
    }

    public void SetCompass(GameObject comp) {
        compass = comp;
    }

    public void SetCore(GameObject core) {
        playerCore = core;
    }

    void Update() {
        if (!isWave) UpdateTimer();

        if(timer <= 0f && !isWave) {
            isWave = true;
            SpawnNextWave();
            timer = waveDelay;
        }
    }

    bool firstPassOnEasy = true;

    private void SpawnNextWave() {
        // randomize spawn location if not on easy
        if (difficulty != 0 || (firstPassOnEasy && difficulty == 0)) {
            int rnd = Random.Range(0, 3);
            spawn.transform.position = spawnLocations[rnd].transform.position;

            if(difficulty == 0)
                firstPassOnEasy = false;
        }

        waveCount++;
        wavesCounter.text = waveCount.ToString();

        scoreCounter.text = "Wave: " + waveCount.ToString();

        for (int i=0; i<nrLandEnemies; i++) {
            GameObject enemy = Instantiate(landEnemy, spawn.position, Quaternion.identity, enemiesParent);

            spawnedEnemies.Add(enemy);
            enemy.GetComponent<Enemy>().SetScript(this);
            enemy.GetComponent<Enemy>().SetTarget(playerCore);

            compass.GetComponent<Compass>().AddMarker(enemy.GetComponent<CompassMarker>());
        }
        for(int i=0; i<nrAirEnemies; i++) {
            GameObject enemy = Instantiate(airEnemy, spawn.position, Quaternion.identity, enemiesParent);
            enemy.GetComponent<NavMeshAgent>().enabled = false;

            NavMeshHit closestHit;
            if (NavMesh.SamplePosition(enemy.transform.position, out closestHit, 1000f, NavMesh.AllAreas))
                enemy.transform.position = closestHit.position;
            else
                Debug.LogError("Air enemy couln't find NavMesh");

            enemy.GetComponent<NavMeshAgent>().enabled = true;

            spawnedEnemies.Add(enemy);
            enemy.GetComponent<Enemy>().SetScript(this);
            enemy.GetComponent<Enemy>().SetTarget(playerCore);

            compass.GetComponent<Compass>().AddMarker(enemy.GetComponent<CompassMarker>());
        }

        switch (difficulty) {
            case 0:
                if (waveCount % 2 == 0) nrLandEnemies++;
                if (waveCount % 5 == 0) nrAirEnemies++;
                break;
            case 1:
                if (waveCount % 2 == 0) nrLandEnemies++;
                else nrAirEnemies++;
                break;
            case 2:
                nrLandEnemies++;
                nrAirEnemies++;
                break;
        }
    }

    public void Remove(GameObject obj) {
        spawnedEnemies.Remove(obj);
        compass.GetComponent<Compass>().RemoveMarker(obj.GetComponent<CompassMarker>());
        if (spawnedEnemies.Count == 0) isWave = false;
    }

    private void UpdateTimer() {
        timer -= Time.deltaTime;

        int min = (int)(timer / 60);
        int sec = (int)(timer % 60);

        wavesTimer.text = min.ToString() + ":" + (sec / 10 > 0 ? sec.ToString() : "0" + sec.ToString());
    }
}
