using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        PlayerPrefs.SetInt("Language", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLanguage(int language) 
    {
        PlayerPrefs.SetInt("Language", language);
    }
}
