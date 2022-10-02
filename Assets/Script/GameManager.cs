using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    public float gameStateNewSpawnerInSeconds;
    public float gameStateMoreSpawnsInSeconds; //maybe not needed
    float timeToFinishTheGame = 400f;
    bool timeIsRunning = true;
    bool spawnerIsEnable = false;

    [Header("References")]
    public GameObject black_cage;
    public GameObject red_cage;
    public GameObject secondSpawner;
    public AnimalSpawner[] animalSpawners;
    public TextMeshProUGUI text;
    public GameObject panel;

    [Header("Game is Finished")]
    public int nbOfAnimals;

    [Header("Game Over")]
    public bool fightStarted = false;

    private void Update()
    {
        GameCountdown();
        //Debug.Log(timeToFinishTheGame);


        if(!spawnerIsEnable)
        {
            if (timeToFinishTheGame < gameStateNewSpawnerInSeconds)
            {

                ActivateSecondSpawner();
            }
        }
        

        if (fightStarted)
            GameOver();

        /*if (timeToFinishTheGame < gameStateMoreSpawnsInSeconds)
        {
            ActivateMoreSpawn();                COME BACK LATER
        }*/

        nbOfAnimals = black_cage.GetComponent<CageBehaviour>().numberOfAnimals + red_cage.GetComponent<CageBehaviour>().numberOfAnimals;
    }

    void GameCountdown()
    {
        if (timeIsRunning)
        {
            if (timeToFinishTheGame > 0)
            {
                timeToFinishTheGame -= Time.deltaTime;
            }
            else
            {
                timeToFinishTheGame = 0f;
                timeIsRunning = false;
                GameIsFinish();
            }
        }
    }

    void ActivateSecondSpawner()
    {
        secondSpawner.GetComponent<AnimalSpawner>().enabled = true;
        spawnerIsEnable = true;
    }

    void ActivateMoreSpawn()
    {
        foreach (AnimalSpawner spawner in animalSpawners)
        {
            spawner.spawnMoreAnimals = true;
        }
    }

    void GameIsFinish()
    {
        SceneManager.LoadScene("End");
    }

    void GameOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }

        KillEverything();
    }

    void KillEverything()
    {
        GameObject[] objectToKill = GameObject.FindGameObjectsWithTag("Red");
        GameObject[] objectToKill2 = GameObject.FindGameObjectsWithTag("Black");

        foreach (GameObject animal in objectToKill)
        {
            Destroy(animal);
        }

        foreach (GameObject animal in objectToKill2)
        {
            Destroy(animal);
        }

        foreach (AnimalSpawner spawner in animalSpawners)
        {
            Destroy(spawner);
        }

        panel.SetActive(true);
        text.enabled = true;

    }
}
