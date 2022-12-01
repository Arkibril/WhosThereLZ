using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent onInteract;
    public int ID;
    public Sprite interactIcon;
    public Vector2 IconSize;
    void Start()
    {
        ID = Random.Range(0, 99999);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
