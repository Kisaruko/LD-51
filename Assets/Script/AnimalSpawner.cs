using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public int yDirection;
    public Transform spawnPos;

    public GameObject[] animals;
    public int animalSpeed;

    private AudioSource audioSource;

    public float spawnRate;
    public float timeUntilSpawnRateIncrease;
    public bool spawnMoreAnimals = false;

    //private Camera camera;
    private CameraShake shake;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        //camera = Camera.main;
        shake = Camera.main.GetComponent<CameraShake>();
    }

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
                StartCoroutine(shake.Shake(0.15f, 0.1f, -0.25f, 0.25f, -0.25f, 0.25f));
                audioSource.Play();
                if(spawnMoreAnimals)
                {
                    GameObject clone2 = Instantiate(animals[Random.Range(0, animals.Length)], spawnPos.position, Quaternion.identity);
                    clone2.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-30, 30) * Time.deltaTime * animalSpeed, (20 * yDirection) * Time.deltaTime * animalSpeed), ForceMode2D.Impulse);
                } //Not used at the time - collision problem
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
