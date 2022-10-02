using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombIA : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public Vector2 currentVelocity;

    public float timeToLive = 10;
    public bool timeIsRunning = true;

    public Animator anim;
    public Animator timerAnim;
    public GameObject dangerTimer;

    private AudioSource source;
    public AudioClip release;

    private Dragger dragger;
    public bool isDragged;

    public ParticleSystem fx_smoke;

    public GameManager gameManager;

    private void Awake()
    {
        dragger = GetComponent<Dragger>();

        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        source = GetComponent<AudioSource>();
        //anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        //rb.AddForce(new Vector2(Random.Range(-15, 15) * Time.deltaTime * speed, 20 * Time.deltaTime * speed)); //Les valeurs du random sont à changés : le spawner doit indiqué si c'est vers le haut ou vers le bas
    }

    private void Update()
    {
        TimeToLive();
        Animate();
        currentVelocity = rb.velocity;
        //BombMovement();
    }

    private void TimeToLive()
    {
        if (timeIsRunning)
        {
            if (timeToLive > 0) //Un if peut être rajouté pour indiqué quand il y aura moins de X seconds
            {
                timeToLive -= Time.deltaTime;
            }
            if(timeToLive <= 3)
            {
                timerAnim.SetBool("isCountingDown", true);
            }
            if(timeToLive <= 0f)
            {
                timerAnim.SetBool("isDead", true);
                fx_smoke.Play();
                fx_smoke.GetComponent<Transform>().SetParent(gameManager.transform);
                gameManager.fightStarted = true;
                timeToLive = 0f;
                timeIsRunning = false;
            }
        }
    }

    /*private void BombMovement()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (dragger.IsMouseOverBomb()) //Check if the mouse is over a bomb or not
            {
                rb.velocity = Vector2.zero; //y'a ca a changé pour le addforce
                rb.AddForce(new Vector2(Random.Range(-30, 30) * Time.deltaTime * speed, 20 * Time.deltaTime * speed));
            }
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CageBlack") && this.gameObject.CompareTag("Black"))
        {
            dragger.cantBeDragAgain = true;
            collision.GetComponent<CageBehaviour>().numberOfAnimals++;
            this.gameObject.tag = "Untagged";
            timeIsRunning = false;

            source.clip = release;
            source.Play();

            dangerTimer.SetActive(false);
        }
        if (collision.CompareTag("CageRed") && this.gameObject.CompareTag("Red"))
        {
            dragger.cantBeDragAgain = true;
            collision.GetComponent<CageBehaviour>().numberOfAnimals++;
            this.gameObject.tag = "Untagged";
            timeIsRunning = false;

            source.clip = release;
            source.Play();

            dangerTimer.SetActive(false);
        }
        else if(collision.CompareTag("CageRed") && this.gameObject.CompareTag("Black") || collision.CompareTag("CageBlack") && this.gameObject.CompareTag("Red"))
        {
            gameManager.fightStarted = true;
        }
    }

    void Animate()
    {
        anim.SetFloat("movementX", currentVelocity.x);
        anim.SetFloat("movementY", currentVelocity.y);

        if (!dragger.cantBeDragAgain && dragger.IsMouseOverBomb() && Input.GetMouseButton(0))
        {
            anim.SetBool("isGrabbed", true);
        }
        else if(dragger.cantBeDragAgain|| !dragger.IsMouseOverBomb())
            anim.SetBool("isGrabbed", false);

    }
}
