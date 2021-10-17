using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 7f; //speed of the bullet
    public float deactivate_timer = 3f;

    [HideInInspector]
    public bool is_EnemyBullet = false;
    void Start()
    {
        if(is_EnemyBullet){
            speed *= -1f;
        }
        Invoke("DeactivateGameObject", deactivate_timer); // call the deactivate function after every deactivate_timer seconds
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move(){
        Vector3 temp = transform.position; //current position of the bullet
        temp.x += speed * Time.deltaTime; //forward movement
        transform.position = temp;
    }
    /*Function for deactivaing the bullets after they pass the play region for memory usage*/
    void DeactivateGameObject(){
        gameObject.SetActive(false); 
        
    }

    void OnTriggerEnter2D(Collider2D target){
        if(target.tag == "Bullet" || target.tag == "Enemy"){
            gameObject.SetActive(false);
        }
    }
}
