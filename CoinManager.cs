using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static int coins;
    Text text;
    void Awake()
    {
        text = GetComponent<Text>();
        coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Coins: " + coins;
        /*if(CoinManager.coins == 10)
        {
            currentHealth += 1;
            CoinManager.coins = 0;
        }*/
    }
}