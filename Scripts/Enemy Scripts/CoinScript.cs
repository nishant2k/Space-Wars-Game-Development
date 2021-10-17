using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 4f;
    //public float rotate_Speed = 50f;
    //public bool canRotate;
    public bool canMove = true;
    public float bound_X = -11f;
    private Animator anim;
    private AudioSource explosionSound;
 //   private int count;
    void Awake()
    {
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();
    }
   //anim.Play("idle");
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x -= speed * Time.deltaTime;
            transform.position = temp;
            if (temp.x < bound_X)
            {
                gameObject.SetActive(false);
            }
        }
    }
    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "MainPlayer")
        {
            
            canMove = false;
            
            Invoke("TurnOffGameObject", 0.5f);
            explosionSound.Play(0);
            anim.Play("Destroy");
            // ScoreManager.score += 1;
            CoinManager.coins += 1;
            }
        }
    }
