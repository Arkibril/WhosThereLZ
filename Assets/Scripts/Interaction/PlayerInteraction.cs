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
    private Closets _closets;
    [SerializeField]
    private Levier _levier;
    [SerializeField]
    private Torche _torche;
    [SerializeField]
    private Armoire _armoire;
    public InteractionType _interactionType;

    private TMPro.TextMeshProUGUI interacationText;
    private GameObject chairPoint;
    private GameObject torchePoint;
    private GameObject Mark;
    private GameObject cameraPivot;

    private Vector3 chairPos;
    private Quaternion chairRot;
    private Vector3 torchePos;
    private Quaternion torcheRot;

    private Animation slideDoorAnim = null;
    private Animation windowAnim = null;
    private Animation doorAnim = null;

    [Header("Value")]
    public float interactionDistance;
    private RaycastHit hit;

    private void Update()
    {
        interacationText = interactionCanvas.GetComponent<TMPro.TextMeshProUGUI>();
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
                if(viewObject.CompareTag("MainDoor")) _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if(viewObject.CompareTag("SlideDoor")) _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if(viewObject.CompareTag("RoomDoor")) _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if(viewObject.CompareTag("Window")) _interactionType.HandleInteraction(InteractionType.interactionType.Hold);
                if(viewObject.CompareTag("Chair")) _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if(viewObject.CompareTag("GarageDoor")) _interactionType.HandleInteraction(InteractionType.interactionType.Hold);
                if(viewObject.CompareTag("Electrical")) _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if(viewObject.CompareTag("Torche")) _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if(viewObject.CompareTag("Armoire1")) _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if(viewObject.CompareTag("Drawer")) _interactionType.HandleInteraction(InteractionType.interactionType.Click);

                if (viewObject != null && viewObject.CompareTag("MainDoor") || viewObject != null && viewObject.CompareTag("RoomDoor"))
                {
                    if (selectedObject != null && selectedObject.CompareTag("Chair"))
                    {
                        _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                        if (_chairIndication != null)
                        {
                            _chairIndication.ActivateRenderer();
                        }
                    }
                }
            }
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
            _door = viewObject.GetComponent<Door>();
            _chairIndication = viewObject.GetComponentInChildren<ChairIndication>();
            _closets = viewObject.GetComponent<Closets>();
            _levier = viewObject.GetComponent<Levier>();
            _torche = viewObject.GetComponent<Torche>();
            _armoire = viewObject.GetComponent<Armoire>();

            doorAnim = viewObject.GetComponent<Animation>();
            slideDoorAnim = viewObject.GetComponentInChildren<Animation>();
            windowAnim = viewObject.GetComponentInChildren<Animation>();
        }
        else
        {
            _chair = null;
            _door = null;
            _chairIndication = null;
            _closets = null;
            _levier = null;
            _torche = null;
            _armoire = null;

            doorAnim = null;
            slideDoorAnim = null;
            windowAnim = null;
        }

        if (selectedObject != null && selectedObject.CompareTag("Chair") || selectedObject != null && selectedObject.CompareTag("Torche"))
        {
            _chair = selectedObject.GetComponent<Chair>();
            _torche = selectedObject.GetComponent<Torche>();
        }
    }

    void WhatISee()
    {
        if (hit.transform != null && hit.transform.CompareTag("Chair") || hit.transform != null && hit.transform.CompareTag("GarageDoor") || hit.transform != null && hit.transform.CompareTag("SlideDoor") || hit.transform != null && hit.transform.CompareTag("MainDoor") || hit.transform != null && hit.transform.CompareTag("RoomDoor") || hit.transform != null && hit.transform.CompareTag("Window") || hit.transform != null && hit.transform.CompareTag("Electrical") || hit.transform != null && hit.transform.CompareTag("Torche") || hit.transform != null && hit.transform.CompareTag("Armoire1") || hit.transform != null && hit.transform.CompareTag("Drawer"))
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

    public void Interacte()
    {
        if (viewObject != null && viewObject.CompareTag("Chair") && selectedObject == null)
        {
            if (!_chair.isTake)
            {
                _chair.isTake = true;
                viewObject.GetComponentInChildren<MeshCollider>().enabled = false;
                viewObject.gameObject.GetComponentInParent<Rigidbody>().useGravity = false;
                selectedObject = viewObject;

                if (_chair.onDoor)
                {
                    _chair.onDoor = false;
                }
            }
        }

        if (viewObject != null && viewObject.CompareTag("Torche") && selectedObject == null)
        {
            if (!_torche.isTake)
            {
                _torche.isTake = true;
                viewObject.GetComponent<BoxCollider>().enabled = false;
                viewObject.GetComponent<Rigidbody>().useGravity = false;
                selectedObject = viewObject;
            }
        }

        if (viewObject != null)
        {
            if (viewObject.CompareTag("GarageDoor"))
            {

                if (_door != null && !_door.isOpen)
                {
                    _door.isOpen = true;
                    doorAnim["DoorGarage_open"].speed = 0.1f;
                    doorAnim.Play("DoorGarage_open");
                }
                else if (doorAnim.IsPlaying("DoorGarage_open"))
                {
                    _door.isOpen = false;
                    doorAnim["DoorGarage_open"].speed = -0.1f;
                    doorAnim.Play("DoorGarage_open");
                }
                else
                {
                    _door.isOpen = false;
                    doorAnim["DoorGarage_open"].speed = -0.1f;
                    doorAnim["DoorGarage_open"].time = 0.30f;
                    doorAnim.Play("DoorGarage_open");
                }
            }

            if (viewObject.CompareTag("SlideDoor"))
            {
                if (_door != null && !_door.isOpen)
                {
                    _door.isOpen = true;
                    slideDoorAnim["DoorSlide02_open"].speed = 1f;
                    slideDoorAnim.Play("DoorSlide02_open");
                }
                else if (slideDoorAnim.IsPlaying("DoorSlide02_open"))
                {
                    _door.isOpen = false;
                    slideDoorAnim["DoorSlide02_open"].speed = -1f;
                    slideDoorAnim.Play("DoorSlide02_open");
                }
                else
                {
                    _door.isOpen = false;
                    slideDoorAnim["DoorSlide02_open"].speed = -1f;
                    slideDoorAnim["DoorSlide02_open"].time = 1f;
                    slideDoorAnim.Play("DoorSlide02_open");
                }
            }

            if (viewObject.CompareTag("Window"))
            {
                if (_door != null && !_door.isOpen)
                {
                    _door.isOpen = true;
                    windowAnim["Window_open"].speed = 1f;
                    windowAnim.Play("Window_open");
                }
                else if (windowAnim.IsPlaying("Window_open"))
                {
                    _door.isOpen = false;
                    windowAnim["Window_open"].speed = -1f;
                    windowAnim.Play("Window_open");
                }
                else
                {
                    _door.isOpen = false;
                    slideDoorAnim["Window_open"].speed = -1f;
                    slideDoorAnim["Window_open"].time = 1f;
                    slideDoorAnim.Play("Window_open");
                }
            }

            if (_door != null && _door.isLocked == false && selectedObject == null || _door != null && _door.isLocked == false && selectedObject != null && !selectedObject.CompareTag("Chair"))
            {

                if (viewObject.CompareTag("MainDoor"))
                {
                    if (!_door.isOpen)
                    {
                        _door.isOpen = true;
                        doorAnim["DoorMain_open"].speed = 1f;
                        doorAnim.Play("DoorMain_open");

                    }
                    else if (doorAnim.IsPlaying("DoorMain_open"))
                    {
                        _door.isOpen = false;
                        doorAnim["DoorMain_open"].speed = -1f;
                        doorAnim.Play("DoorMain_open");
                    }
                    else
                    {
                        _door.isOpen = false;
                        doorAnim["DoorMain_open"].speed = -1f;
                        doorAnim["DoorMain_open"].time = 1f;
                        doorAnim.Play("DoorMain_open");
                    }
                }

                if (viewObject.CompareTag("RoomDoor"))
                {
                    if (!_door.isOpen)
                    {
                        _door.isOpen = true;
                        doorAnim["DoorRoomsWide_open"].speed = 1f;
                        doorAnim.Play("DoorRoomsWide_open");
                    }
                    else if (doorAnim.IsPlaying("DoorRoomsWide_open"))
                    {
                        _door.isOpen = false;
                        doorAnim["DoorRoomsWide_open"].speed = -1f;
                        doorAnim.Play("DoorRoomsWide_open");
                    }
                    else
                    {
                        _door.isOpen = false;
                        doorAnim["DoorRoomsWide_open"].speed = -1f;
                        doorAnim["DoorRoomsWide_open"].time = 1f;
                        doorAnim.Play("DoorRoomsWide_open");
                    }
                }
            }
            else
            {
                if(_door != null)
                {
                    _door.audioSource.clip = _door.doorSounds[0];
                    _door.audioSource.Play();
                }
            }


            if (viewObject.CompareTag("MainDoor") || viewObject.CompareTag("RoomDoor"))
            {
                if (selectedObject != null && selectedObject.CompareTag("Chair"))
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



            if (viewObject.CompareTag("Electrical"))
            {
                if (_levier != null && !_levier.isOn)
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

            if (viewObject.CompareTag("Armoire1"))
            {
                if(_armoire != null && !_armoire.isOpen)
                {
                    _armoire.isOpen = true;

                    if(doorAnim.clip.name == "KitchenSetDoorOne_anim")
                    {
                        doorAnim["KitchenSetDoorOne_anim"].speed = 1f;
                        doorAnim.Play("KitchenSetDoorOne_anim");
                    }
                    else if(doorAnim.clip.name == "KitchenSetDoorTwo_anim")
                    {
                        doorAnim["KitchenSetDoorTwo_anim"].speed = 1f;
                        doorAnim.Play("KitchenSetDoorTwo_anim");
                    }
                    else if(doorAnim.clip.name == "BathroomSink_open")
                    {
                        doorAnim["BathroomSink_open"].speed = 1f;
                        doorAnim.Play("BathroomSink_open");
                    }
                    else if (doorAnim.clip.name == "Down_anim")
                    {
                        doorAnim["Down_anim"].speed = 1f;
                        doorAnim.Play("Down_anim");
                    }
                    else if (doorAnim.clip.name == "Top_anim")
                    {
                        doorAnim["Top_anim"].speed = 1f;
                        doorAnim.Play("Top_anim");
                    }
                }
                else
                {
                    _armoire.isOpen = false;

                    if (doorAnim.clip.name == "KitchenSetDoorOne_anim")
                    {
                        doorAnim["KitchenSetDoorOne_anim"].speed = -1f;
                        doorAnim["KitchenSetDoorOne_anim"].time = 1f;
                        doorAnim.Play("KitchenSetDoorOne_anim");
                    }
                    else if (doorAnim.clip.name == "KitchenSetDoorTwo_anim")
                    {
                        doorAnim["KitchenSetDoorTwo_anim"].speed = -1f;
                        doorAnim["KitchenSetDoorTwo_anim"].time = 1f;
                        doorAnim.Play("KitchenSetDoorTwo_anim");
                    }
                    else if (doorAnim.clip.name == "BathroomSink_open")
                    {
                        doorAnim["BathroomSink_open"].speed = -1f;
                        doorAnim["BathroomSink_open"].time = 1f;
                        doorAnim.Play("BathroomSink_open");
                    }
                    else if (doorAnim.clip.name == "Down_anim")
                    {
                        doorAnim["Down_anim"].speed = -1f;
                        doorAnim["Down_anim"].time = 1f;
                        doorAnim.Play("Down_anim");
                    }
                    else if (doorAnim.clip.name == "Top_anim")
                    {
                        doorAnim["Top_anim"].speed = -1f;
                        doorAnim["Top_anim"].time = 1f;
                        doorAnim.Play("Top_anim");
                    }
                }
            }


            if (viewObject.CompareTag("Drawer"))
            {
                if(_armoire != null && !_armoire.isOpen)
                {
                    _armoire.isOpen = true;

                    if(doorAnim.clip.name == "Drawer_anim")
                    {
                        doorAnim["Drawer_anim"].speed = 1f;
                        doorAnim.Play("Drawer_anim");
                    }
                    else if(doorAnim.clip.name == "Drawer_A_anim")
                    {
                        doorAnim["Drawer_A_anim"].speed = 1f;
                        doorAnim.Play("Drawer_A_anim");
                    }
                    else if(doorAnim.clip.name == "Drawer_B_anim")
                    {
                        doorAnim["Drawer_B_anim"].speed = 1f;
                        doorAnim.Play("Drawer_B_anim");
                    }
                    else if(doorAnim.clip.name == "Drawer_C_anim")
                    {
                        doorAnim["Drawer_C_anim"].speed = 1f;
                        doorAnim.Play("Drawer_C_anim");
                    }
                    else if(doorAnim.clip.name == "DrawerA")
                    {
                        doorAnim["DrawerA"].speed = 1f;
                        doorAnim.Play("DrawerA");
                    }
                    else if(doorAnim.clip.name == "DrawerB")
                    {
                        doorAnim["DrawerB"].speed = 1f;
                        doorAnim.Play("DrawerB");
                    }
                    else if(doorAnim.clip.name == "DrawerC")
                    {
                        doorAnim["DrawerC"].speed = 1f;
                        doorAnim.Play("DrawerC");
                    }
                    else if(doorAnim.clip.name == "ToiletTable_ainm")
                    {
                        doorAnim["ToiletTable_ainm"].speed = 1f;
                        doorAnim.Play("ToiletTable_ainm");
                    }
                    else if(doorAnim.clip.name == "BedsideTable_open")
                    {
                        doorAnim["BedsideTable_open"].speed = 1f;
                        doorAnim.Play("BedsideTable_open");
                    }
                }
                else
                {
                    _armoire.isOpen = false;

                    if (doorAnim.clip.name == "Drawer_anim")
                    {
                        doorAnim["Drawer_anim"].speed = -1f;
                        doorAnim["Drawer_anim"].time = 1f;
                        doorAnim.Play("Drawer_anim");
                    }
                    else if (doorAnim.clip.name == "Drawer_A_anim")
                    {
                        doorAnim["Drawer_A_anim"].speed = -1f;
                        doorAnim["Drawer_A_anim"].time = 1f;
                        doorAnim.Play("Drawer_A_anim");
                    }
                    else if (doorAnim.clip.name == "Drawer_B_anim")
                    {
                        doorAnim["Drawer_B_anim"].speed = -1f;
                        doorAnim["Drawer_B_anim"].time = 1f;
                        doorAnim.Play("Drawer_B_anim");
                    }
                    else if (doorAnim.clip.name == "Drawer_C_anim")
                    {
                        doorAnim["Drawer_C_anim"].speed = -1f;
                        doorAnim["Drawer_C_anim"].time = 1f;
                        doorAnim.Play("Drawer_C_anim");
                    }
                    else if (doorAnim.clip.name == "DrawerA")
                    {
                        doorAnim["DrawerA"].speed = -1f;
                        doorAnim["DrawerA"].time = 1f;
                        doorAnim.Play("DrawerA");
                    }
                    else if (doorAnim.clip.name == "DrawerB")
                    {
                        doorAnim["DrawerB"].speed = -1f;
                        doorAnim["DrawerB"].time = 1f;
                        doorAnim.Play("DrawerB");
                    }
                    else if (doorAnim.clip.name == "DrawerC")
                    {
                        doorAnim["DrawerC"].speed = -1f;
                        doorAnim["DrawerC"].time = 1f;
                        doorAnim.Play("DrawerC");
                    }
                    else if (doorAnim.clip.name == "ToiletTable_ainm")
                    {
                        doorAnim["ToiletTable_ainm"].speed = -1f;
                        doorAnim["ToiletTable_ainm"].time = 1f;
                        doorAnim.Play("ToiletTable_ainm");
                    }
                    else if(doorAnim.clip.name == "BedsideTable_open")
                    {
                        doorAnim["BedsideTable_open"].speed = -1f;
                        doorAnim["BedsideTable_open"].time = 1f;
                        doorAnim.Play("BedsideTable_open");
                    }
                }
            }


        }

    }



}
