using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCheck : MonoBehaviour
{
    public bool HesHere;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Monster(Clone)") != null)
        {
            GameObject MonsterScript = GameObject.FindGameObjectWithTag("MonsterScript");
            MonsterScript.GetComponent<MonsterScript>().enabled = false;

            HesHere = true;
            StartCoroutine(TimeDespawn());
        }
    }

    IEnumerator TimeDespawn() 
    {
        yield return new WaitForSeconds(5f);

        GameObject MonsterScript = GameObject.FindGameObjectWithTag("MonsterScript");
        MonsterScript.GetComponent<MonsterScript>().enabled = true;
        HesHere = false;
        Destroy(GameObject.FindWithTag("Monster"));

    }
}
