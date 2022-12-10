using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorJ2 : MonoBehaviour
{
    public Door _door;
    public EnemyFollow _enemyFollow;
    public GameObject TheDoor;
    public GameObject TheChair;
    void Start()
    {
        _enemyFollow = this.GetComponent<EnemyFollow>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        _door = other.GetComponent<Door>();
        if(other.tag == "MainDoor") 
        {
            TheDoor = other.gameObject;
            if (_door.isLocked == false)
            {
                other.gameObject.GetComponent<Animator>().Play("DoorRoomWide");
            }
            else if(_door.isLocked == true) 
            {
                StartCoroutine(Blocked());
            }
        }
        else if( other.tag == "DoorNarrow") 
        {
            other.gameObject.GetComponent<Animator>().Play("DoorRoomNarrow");
        }
        else if(other.tag == "Chair")
        {
            TheChair = other.gameObject;
        }

    }

    IEnumerator Blocked()
    {
        _enemyFollow.Target = null;
        yield return new WaitForSeconds(3f);
        Destroy(TheDoor);
        Destroy(TheChair);
        _enemyFollow.Target = GameObject.FindGameObjectWithTag("Player");
    }
}
