using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;// speed of the enmy sspaceship
    public float rotate_speed = 50f; //rotating speed for asteriods 
    public bool canRotate; //for asteriods
    public bool canShoot; //for enemy spaceships
    private bool canMove = true;
    public float boound_X= -11f;
    public Transform attack_Point;
    public GameObject bulletprefab;
    private Animator anim;
    private AudioSource explosionSound;
    private int count;
    void Awake() {
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        if(canShoot){
            Invoke("StartShooting", Random.Range(1f, 3f));
        }
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RotateEnemy();
    }
    void Move(){
        if(canMove){
            Vector3 temp = transform.position; //current position of enemy
            temp.x -= speed*Time.deltaTime; //speeding the enemy
            transform.position = temp;

            if(temp.x<boound_X){ 
                gameObject.SetActive(false);
            }
        }
    }
    void RotateEnemy(){
        if(canRotate){
            transform.Rotate(new Vector3(0f, 0f, rotate_speed * Time.deltaTime), Space.World);
        }
    }
    void StartShooting(){
        GameObject bullet = Instantiate(bulletprefab, attack_Point.position, Quaternion.identity);
        bullet.GetComponent<BulletScript>().is_EnemyBullet = true;
        if(canShoot){
            Invoke("StartShooting", Random.Range(1f, 3f));
        }
    }
    
    void TurnOffGameObject(){
        gameObject.SetActive(false);
    }
    private int health = 2;
    void OnTriggerEnter2D(Collider2D target){
        if (target.tag == "Bullet")
        {
           // health -= 1;
           // if (health == 0)
           // {
            canMove = false;
            if (canShoot)
            {
                canShoot = false;
                CancelInvoke("StartShooting");
            }
            Invoke("TurnOffGameObject", 0.5f);
            explosionSound.Play(0);
            anim.Play("Destroy");
            ScoreManager.score += 1;
        }
        

        
    }
}
