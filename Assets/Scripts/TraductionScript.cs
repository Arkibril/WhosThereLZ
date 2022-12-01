using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraductionScript : MonoBehaviour
{
    public string Francais;
    public string Anglais;

    public Text text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Language") == 1)
        {
            text.text = Francais;
        }
        else if (PlayerPrefs.GetInt("Language") == 2) 
        {
            text.text = Anglais;
        }
    }
}
