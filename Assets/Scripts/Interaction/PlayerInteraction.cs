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
    [SerializeField]
    private RecordPlayer _recordPlayer;
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
                if (viewObject.CompareTag("Door") && selectedObject == null || viewObject.CompareTag("Door") && selectedObject != null && !selectedObject.CompareTag("Chair")) _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if (viewObject.name == "Flashlight") _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if (viewObject.name == "FuseBox") _interactionType.HandleInteraction(InteractionType.interactionType.Hold);
                if (viewObject.name == "RecordPlayer_LOD0" || viewObject.name == "Turntable_LOD0") _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if (viewObject.CompareTag("Window")) _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if (viewObject.name == "Drawer" || viewObject.name == "Door_Bottom") _interactionType.HandleInteraction(InteractionType.interactionType.Click);

                if(viewObject.CompareTag("Door") && selectedObject != null && selectedObject.CompareTag("Chair"))
                {
                    if(_door != null && _door.isIn && !_door.isOpen && _door.anim[_door.openingAnim].time <= 0)
                    {
                        _interactionType.HandleInteraction(InteractionType.interactionType.Hold);
                        if (_chairIndication != null) _chairIndication.ActivateRenderer();
                    }
                }
            }
        }

        //print(hit.transform.name);

        if (viewObject == null || selectedObject == null|| _door != null && !_door.isIn)
        {
            if (_chairIndication != null) _chairIndication.DesactivateRenderer();
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
            _recordPlayer = viewObject.GetComponentInParent<RecordPlayer>();
        }
        else
        {
            _chair = null;
            _door = null;
            //_chairIndication = null;
            _levier = null;
            _torche = null;
            _recordPlayer = null;
        }

        if (selectedObject != null && selectedObject.CompareTag("Chair") || selectedObject != null && selectedObject.name == "Flashlight")
        {
            _chair = selectedObject.GetComponent<Chair>();
            _torche = selectedObject.GetComponent<Torche>();
        }
    }

    void WhatISee()
    {
        if(hit.transform != null)
        {
            if (hit.transform.CompareTag("Chair") || hit.transform.CompareTag("Door") || hit.transform.name == "Flashlight" || hit.transform.name == "FuseBox" || hit.transform.name == "RecordPlayer_LOD0" || hit.transform.name == "Turntable_LOD0" || hit.transform.CompareTag("Window") || hit.transform.name == "Drawer" || hit.transform.name == "Door_Bottom")
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
                if(_door != null && !_door.isLocked && selectedObject == null || _door != null && !_door.isLocked && selectedObject != null && !selectedObject.CompareTag("Chair"))
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

                if (_door.isLocked) 
                {
                    _door.audio.clip = _door.Locked;
                    _door.audio.Play();
                } 
            }

            if(viewObject.CompareTag("Door") && selectedObject != null && selectedObject.CompareTag("Chair"))
            {
                if(_door != null && _door.isIn && !_door.isOpen && _door.anim[_door.openingAnim].time <= 0)
                {
                    _chair.isTake = false;
                    _chair.transform.SetPositionAndRotation(_chairIndication.transform.position, _chairIndication.transform.rotation);

                    _chair.onDoor = true;
                    _door.chairOnDoor = selectedObject;

                    selectedObject.GetComponentInChildren<MeshCollider>().enabled = true;
                    selectedObject.GetComponentInChildren<MeshCollider>().isTrigger = true;
                    selectedObject = null;
                    _chairIndication.DesactivateRenderer();
                }
            }

            if(viewObject.name == "Flashlight" && selectedObject == null)
            {
                if (!_torche.isTake)
                {
                    _torche.isTake = true;
                    viewObject.GetComponent<BoxCollider>().enabled = false;
                    viewObject.GetComponent<Rigidbody>().useGravity = false;
                    selectedObject = viewObject;
                }
            }

            if(viewObject.name == "FuseBox")
            {
                if (!_levier.isOn)
                {
                    _levier.isOn = true;
                    _levier.anim.SetBool("isOn", true);

                    _levier.ActivateLight();
                }
                else
                {
                    _levier.isOn = false;
                    _levier.anim.SetBool("isOn", false);

                    _levier.DesactivateLight();

                }
            }

            if(viewObject.name == "Turntable_LOD0" || viewObject.name == "RecordPlayer_LOD0")
            {
                if (_recordPlayer.isOn)
                {
                    _recordPlayer.isOn = false;
                    _recordPlayer.audio.Stop();
                    _recordPlayer.anim.SetBool("isOn", false);
                }
                else
                {
                    _recordPlayer.isOn = true;
                    _recordPlayer.audio.Play();
                    _recordPlayer.anim.SetBool("isOn", true);
                }
            }

            if (viewObject.CompareTag("Window") || viewObject.name == ("Drawer") || viewObject.name == ("Door_Bottom"))
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

    }



}
