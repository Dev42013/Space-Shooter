using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        Invoke("LoadFirstScene", 3f); 
    }

    void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }
}
