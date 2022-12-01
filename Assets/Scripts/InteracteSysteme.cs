using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class InteracteSysteme : MonoBehaviour
{
    public LayerMask InterectableLayerMask;
    UnityEvent onInteract;
    public Interactable interactable;
    public Image interactImage;
    public Sprite defaultIcon;
    public Vector2 defaultIconSize;
    public Sprite defaultIconInteractionIcon;
    public Vector2 defaultInteractIconSize;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2, InterectableLayerMask)) 
        {
            if(hit.collider.GetComponent<Interactable>() != false) 
            {

                if(interactable == null || interactable.ID != hit.collider.GetComponent<Interactable>().ID) 
                {
                    interactable = hit.collider.GetComponent<Interactable>();
                }

                if(interactable.interactIcon != null) 
                {
                    interactImage.sprite = interactable.interactIcon;
                    if(interactable.IconSize == Vector2.zero) 
                    {
                        interactImage.rectTransform.sizeDelta = defaultInteractIconSize;
                    }
                    else 
                    {
                        interactImage.rectTransform.sizeDelta = interactable.IconSize;
                    }
                }
                else 
                {
                    interactImage.sprite = defaultIcon;
                    interactImage.rectTransform.sizeDelta = defaultInteractIconSize;
                }
                //onInteract = hit.collider.GetComponent<Interactable>().onInteract;
                if (Input.GetKeyDown(KeyCode.E)) 
                {
                    interactable.onInteract.Invoke();
                }
            }
        }
        else 
        {
            if(interactImage.sprite != defaultIcon) 
            {
                interactImage.sprite = defaultIcon;
                interactImage.rectTransform.sizeDelta = defaultIconSize;
            }
        }

    }
}
