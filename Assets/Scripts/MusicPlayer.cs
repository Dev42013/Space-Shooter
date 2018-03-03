using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicPlayer : MonoBehaviour {


    private void Awake()
    {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;

        if (numMusicPlayers > 1)
        {
            Destroy(this.gameObject);
        } else
        {
            DontDestroyOnLoad(this.gameObject);
        }
        
    }

}
