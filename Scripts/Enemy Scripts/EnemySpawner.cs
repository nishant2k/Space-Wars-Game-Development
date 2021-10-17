using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float min_Y = -4.3f, max_Y = 4.4f;

    public GameObject[] asteroid_Prefabs;
    public GameObject[] enemy_Prefabs;
    public GameObject coins;

    public float timer = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemies", timer);
    }

    // Update is called once per frame
    void SpawnEnemies()
    {
        float pos_Y = Random.Range(min_Y, max_Y);
        Vector3 temp = transform.position;
        temp.y = pos_Y;
        int num = Random.Range(0, 3);

        if (gameoverscript.isgameover == false)
        {
            if (num == 0)
            {
                Instantiate(asteroid_Prefabs[Random.Range(0, asteroid_Prefabs.Length)],
                temp, Quaternion.identity);
            }
            else if(num == 1)
            {
                Instantiate(enemy_Prefabs[Random.Range(0, enemy_Prefabs.Length)], temp, Quaternion.Euler(0f, 0f, -90f));
            }
            else
            {
                Instantiate(coins, temp, Quaternion.identity);
            }
            Invoke("SpawnEnemies", timer);
        }
    }

}
