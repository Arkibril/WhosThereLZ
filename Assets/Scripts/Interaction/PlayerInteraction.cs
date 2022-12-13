using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Object")]
    [SerializeField]
    private GameObject viewObject = null;
    [SerializeField]
    public GameObject selectedObject = null;
    public GameObject interactionCanvas;

    [Header("Crosshair")]
    public GameObject crosshair;
    public GameObject crosshairInteract;
    public GameObject crosshairLevel;

    [Header("References")]
    [SerializeField]
    public Chair _chair;
    [SerializeField]
    private ChairIndication _chairIndication;
    [SerializeField]
    private Door _door;
    [SerializeField]
    private Levier _levier;
    [SerializeField]
    private Torche _torche;
    public InteractionType _interactionType;

    //private TMPro.TextMeshProUGUI interacationText;
    private GameObject chairPoint;
    private GameObject torchePoint;
    private GameObject Mark;
    private GameObject cameraPivot;

    private Vector3 chairPos;
    private Quaternion chairRot;
    private Vector3 torchePos;
    private Quaternion torcheRot;

    [Header("Value")]
    public float interactionDistance;
    private RaycastHit hit;

    private void Update()
    {
        //interacationText = interactionCanvas.GetComponent<TMPro.TextMeshProUGUI>();
        chairPoint = GameObject.Find("Chaise Point");
        torchePoint = GameObject.Find("Torche Point");
        cameraPivot = GameObject.Find("Camera_Pivot");
        Mark = GameObject.Find("Mark");
        _interactionType = GetComponent<InteractionType>();

        chairRot = Mark.transform.rotation;
        chairPos = chairPoint.transform.position;

        torcheRot = Quaternion.Euler(cameraPivot.transform.rotation.eulerAngles.x - 13, Mark.transform.rotation.eulerAngles.y - 3, 0);
        torchePos = torchePoint.transform.position;        

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            if (viewObject != null)
            {
                if(viewObject.CompareTag("Chair")) _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if (viewObject.CompareTag("Door")) _interactionType.HandleInteraction(InteractionType.interactionType.Click);


                if (selectedObject != null && selectedObject.CompareTag("Chair") && viewObject.CompareTag("Door") && _door != null && _door.isIn && !_door.isOpen)
                {
                    _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                    if (_chairIndication != null) _chairIndication.ActivateRenderer();
                }
            }
        }

        if(_chairIndication != null)
        {
            _chairIndication.DesactivateRenderer();
        }

        crosshair.SetActive(true);
        crosshairInteract.SetActive(false);
        crosshairLevel.SetActive(false);

        crosshairLevel.GetComponent<Image>().fillAmount = _interactionType.holdTime;

        if(_chair != null && _chair.isTake)
        {
            _chair.transform.SetPositionAndRotation(chairPos, chairRot);
        }
        else if(_torche != null && _torche.isTake)
        {
            _torche.transform.SetPositionAndRotation(torchePos, torcheRot);
        }

        ScriptReferences();
    }


    void ScriptReferences()
    {
        WhatISee();

        if(viewObject != null)
        {
            _chair = viewObject.GetComponent<Chair>();
            _door = viewObject.GetComponentInParent<Door>();
            _chairIndication = viewObject.GetComponentInChildren<ChairIndication>();
            _levier = viewObject.GetComponent<Levier>();
            _torche = viewObject.GetComponent<Torche>();
        }
        else
        {
            _chair = null;
            _door = null;
            _chairIndication = null;
            _levier = null;
            _torche = null;
        }

        if (selectedObject != null && selectedObject.CompareTag("Chair") || selectedObject != null && selectedObject.CompareTag("Torche"))
        {
            _chair = selectedObject.GetComponent<Chair>();
            _torche = selectedObject.GetComponent<Torche>();
        }
    }

    void WhatISee()
    {
        if(hit.transform != null)
        {
            if (hit.transform.CompareTag("Chair") || hit.transform.CompareTag("Door"))
            {
                viewObject = hit.transform.gameObject;
                crosshair.SetActive(false);
                crosshairInteract.SetActive(true);
                crosshairLevel.SetActive(true);
            }
            else
            {
                viewObject = null;
            }
        }

    }

    public void Interacte()
    {

        if (viewObject != null && viewObject.CompareTag("Chair") && selectedObject == null)
        {
            if (!_chair.isTake)
            {
                _chair.isTake = true;
                viewObject.GetComponent<MeshCollider>().enabled = false;
                viewObject.GetComponent<Rigidbody>().useGravity = false;
                selectedObject = viewObject;

                if (_chair.onDoor)
                {
                    _chair.onDoor = false;
                }
            }
        }

        if (viewObject != null)
        {
            if(viewObject.CompareTag("Door"))
            {
                if(_door != null && !_door.isLocked)
                {
                    if (!_door.isOpen)
                    {
                        _door.isOpen = true;

                        _door.anim[_door.openingAnim].time = _door.openStartAt;
                        _door.anim[_door.openingAnim].speed = _door.openingSpeed;
                        _door.anim.Play();
                    }
                    else
                    {
                        _door.isOpen = false;

                        _door.anim[_door.openingAnim].time = _door.closeStartAt;
                        _door.anim[_door.openingAnim].speed = _door.closingSpeed;
                        _door.anim.Play();
                    }
                }
            }
            /*else if(selectedObject != null && selectedObject.CompareTag("Chair") && viewObject.CompareTag("Door") && _door.isIn && !_door.isLocked)
            {
                if (!_door.isOpen)
                {
                    _chair.isTake = false;
                    _chair.transform.SetPositionAndRotation(_chairIndication.transform.position, _chairIndication.transform.rotation);

                    _chair.onDoor = true;
                    _door.chairOnDoor = selectedObject;

                    selectedObject.GetComponent<MeshCollider>().enabled = true;
                    selectedObject.GetComponent<MeshCollider>().isTrigger = true;
                    selectedObject = null;
                    _chairIndication.DesactivateRenderer();
                }
            }*/
        }

    }



}
