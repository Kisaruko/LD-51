using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    public float gameStateNewSpawnerInSeconds;
    public float gameStateMoreSpawnsInSeconds; //maybe not needed
    float timeToFinishTheGame = 400f;
    bool timeIsRunning = true;

    [Header("References")]
    public GameObject black_cage;
    public GameObject red_cage;
    public GameObject secondSpawner;
    public AnimalSpawner[] animalSpawners;

    [Header("Game is Finished")]
    public int nbOfAnimals;

    [Header("Game Over")]
    public bool fightStarted = false;

    private void Update()
    {
        GameCountdown();
        //Debug.Log(timeToFinishTheGame);


        if (timeToFinishTheGame < gameStateNewSpawnerInSeconds)
        {
            ActivateSecondSpawner();
        }

        if (fightStarted)
            GameOver();

        /*if (timeToFinishTheGame < gameStateMoreSpawnsInSeconds)
        {
            ActivateMoreSpawn();                COME BACK LATER
        }*/
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
        nbOfAnimals = black_cage.GetComponent<CageBehaviour>().numberOfAnimals + red_cage.GetComponent<CageBehaviour>().numberOfAnimals;
    }

    void GameOver()
    {
        Debug.Log("GameOver");
    }
}
