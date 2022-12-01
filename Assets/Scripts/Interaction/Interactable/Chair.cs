using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{

    public bool isTake = false;
    public bool onDoor;

    private PlayerInteraction _playerInteraction;
    private Rigidbody rb;

    private void Update()
    {

        _playerInteraction = GameObject.Find("Mark").GetComponentInChildren<PlayerInteraction>();
        rb = GetComponentInChildren<Rigidbody>();

        if (isTake)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                isTake = false;
                GetComponentInChildren<MeshCollider>().enabled = true;
                GetComponentInChildren<MeshCollider>().isTrigger = false;
                rb.useGravity = true;
                rb.AddForce(transform.forward * 5, ForceMode.Impulse);

                _playerInteraction.selectedObject = null;
            }
        }
    }
}
