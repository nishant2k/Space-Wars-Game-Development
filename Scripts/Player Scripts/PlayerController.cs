using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f; //speed of the spaceship 
    public float speed_forward_acc = 3f; // forward motion of the spaceship
    public AudioClip laser_sound;
    public AudioClip explosion_sound;
    public AudioClip alert_sound;
    public AudioClip bonus_sound;
    public float min_Y, max_Y; // min ANd maximum value in the area
    public float min_X, max_X; //min and maximum value for forward and backward motion .

    [SerializeField]
    private GameObject player_Bullet; //Attaching bullet for the player

    [SerializeField]
    private Transform Attack_Point; // Attaching a firing point for the player

    public float attack_Timer = 0.4f; //time between two shoots
    private float current_Attack_Timer;
    private bool canAttack;
    private AudioSource audi;
    private Animator anim;
   // private AudioSource explosionSound;
    public int maxHealth = 4;
    public int currentHealth;
    public HealthBar healthBar;
    void Awake(){
        audi = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        //explosionSound = GetComponent<AudioSource>();
    }
    void Start()
    {
        current_Attack_Timer = attack_Timer;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer(); //moving the player in 2D space
        Attack(); // Attacking the object
        if(currentHealth < 5){
            if (CoinManager.coins >= 10)
            {
                audi.clip = bonus_sound;
               // audi.time = 1f;
                audi.Play();
                currentHealth += 1;
                healthBar.SetHealth(currentHealth);
                CoinManager.coins -= 10;
            }
        }
        
        
    }

    void MovePlayer(){
        if(Input.GetAxisRaw("Vertical") > 0f){ // Up arrow and W key
            Vector3 temp = transform.position; //taking position of the player
            temp.y += speed * Time.deltaTime; // incresing the y postion

            if(temp.y > max_Y){
                temp.y = max_Y; // condition so it remians in bounded region
            }
            transform.position = temp;
        }
        else if(Input.GetAxisRaw("Vertical") < 0f){ // Down Arrow or S key
            Vector3 temp = transform.position; // taking position of the player
            temp.y -= speed * Time.deltaTime; // decresing the y position
             
            if(temp.y < min_Y){
                temp.y = min_Y; // condition to remians in bounded region.
            }
            transform.position = temp;
        }
        else if(Input.GetAxisRaw("Horizontal") > 0f){ // forward key or D key
            Vector3 temp = transform.position; 
            temp.x += speed_forward_acc * Time.deltaTime; // increasing the x position

            if(temp.x > max_X){
                temp.x = max_X;// condition for the player to remians in bounded region
            }
          transform.position = temp;
        }
        else if(Input.GetAxisRaw("Horizontal") < 0f){
            Vector3 temp = transform.position;
            temp.x -= speed_forward_acc * Time.deltaTime;

            if(temp.x < min_X){
                temp.x = min_X;
            }
            transform.position = temp;
        }
    }

    void Attack(){
        
        attack_Timer += Time.deltaTime; 
        if(attack_Timer > current_Attack_Timer){ // condition for time between two shoots
            canAttack = true;
        }

        if(Input.GetKeyDown(KeyCode.G)){
            if(canAttack){
                
                canAttack = false;
                attack_Timer = 0f;

                Instantiate(player_Bullet, Attack_Point.position, Quaternion.identity);
                audi.clip = laser_sound;
                audi.Play();
            }
        }
     
    }
    void TurnOffGameObject(){
        gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D target){
        if(target.tag == "Bullet" || target.tag == "Enemy"){
           // canMove = false;
            
           // Application.Quit();
            
            currentHealth -= 1;
            audi.clip = alert_sound;
            audi.Play();
            healthBar.SetHealth(currentHealth);
            if(currentHealth == 0)
            {
                if (canAttack)
                {
                    canAttack = false;
                    CancelInvoke("StartShooting");
                }
                Invoke("TurnOffGameObject", 0.5f);
                //   explosionSound.Play();
                anim.Play("Destroy");
                audi.clip = explosion_sound;
                audi.Play();
                gameoverscript.isgameover = true;
                // Time.timeScale = 0f;
                var gameOver = FindObjectOfType<GameOverCode>();
                gameOver.ShowButtons();
                gameObject.SetActive(true);
                
            }
        }
    }
   /* void OnDestroy()
    {
        // Game Over.
        var gameOver = FindObjectOfType<GameOverCode>();
        gameOver.ShowButtons();
    }*/


}
