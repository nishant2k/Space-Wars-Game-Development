using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameoverscript : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isgameover;
    Text text;
    void Start()
    {
        isgameover = false;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isgameover)
        {
            text.text = "Game Over";
        }
    }
}
