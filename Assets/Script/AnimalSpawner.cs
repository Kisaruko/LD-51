using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public int yDirection;
    public Transform spawnPos;

    public GameObject[] animals;
    public int animalSpeed;


    public float spawnRate;
    public float timeUntilSpawnRateIncrease;
    public bool spawnMoreAnimals = false;

    private void Start()
    {
        StartCoroutine(SpawnAnimals(spawnRate));
    }

    IEnumerator SpawnAnimals(float firstDelay)
    {
        float spawnRateCountdown = timeUntilSpawnRateIncrease;
        float spawnCountdown = firstDelay;
        while (true)
        {
            yield return null;
            spawnRateCountdown -= Time.deltaTime;
            spawnCountdown -= Time.deltaTime;

            //New object needs to spawn ?
            if (spawnCountdown < 0)
            {
                spawnCountdown += spawnRate;
                GameObject clone = Instantiate(animals[Random.Range(0, animals.Length)], spawnPos.position, Quaternion.identity);
                if(spawnMoreAnimals)
                {
                    GameObject clone2 = Instantiate(animals[Random.Range(0, animals.Length)], spawnPos.position, Quaternion.identity);
                    clone2.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-30, 30) * Time.deltaTime * animalSpeed, (20 * yDirection) * Time.deltaTime * animalSpeed), ForceMode2D.Impulse);
                }
                clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-30, 30) * Time.deltaTime * animalSpeed, (20 * yDirection) * Time.deltaTime * animalSpeed), ForceMode2D.Impulse);
                //Debug.Log(clone.GetComponent<Rigidbody2D>().velocity);
            }

            //Increase spawnrate ?
            if (spawnRateCountdown < 0 && spawnRate > 1)
            {
                spawnRateCountdown += timeUntilSpawnRateIncrease;
                spawnRate -= .3f;
            }

        }
    }
}
