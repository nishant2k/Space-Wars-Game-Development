using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource bgmusic;

    private void Start()
    {
        bgmusic = GetComponent<AudioSource>();
        Invoke("playmusic", 60f);
    }
    void playmusic()
    {
        bgmusic.Play();
    }
    //public void StartGame()
   // {
    //    Application.LoadLevel("GamePlay");
   // }
    public void BACK()
    {
        Application.LoadLevel("Menu");
    }
}
