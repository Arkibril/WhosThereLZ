using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderWithTime : MonoBehaviour
{
    public float Time;
    public string SceneName;
    void Start()
    {
        StartCoroutine(WaitSceneLoad());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitSceneLoad() 
    {
        yield return new WaitForSeconds(Time);
        SceneManager.LoadScene(SceneName);
    }
}
