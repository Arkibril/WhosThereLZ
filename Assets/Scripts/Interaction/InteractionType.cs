using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionType : MonoBehaviour
{
    public enum interactionType
    {
        Click,
        Hold
    }

    public float holdTime;

    private PlayerInteraction _playerInteraction;

    private void Start()
    {
        _playerInteraction = GameObject.Find("Mark").GetComponentInChildren<PlayerInteraction>();
    }

    public void HandleInteraction(interactionType currentType)
    {
        KeyCode key = KeyCode.E;

        if (currentType == interactionType.Click)
        {
            if (Input.GetKeyDown(key))
            {
                _playerInteraction.Interacte();
            }
        }

        if (currentType == interactionType.Hold)
        {
            if (Input.GetKey(key))
            {
                IncreasedHoldTime();

                if(GetHoldTime() > 1f)
                {
                    _playerInteraction.Interacte();
                    ResetHoldTime();
                }
            }
            else
            {
                ResetHoldTime();
            }
        }
    }

    private void IncreasedHoldTime()
    {
        holdTime += Time.deltaTime;
    }

    private void ResetHoldTime()
    {
        holdTime = 0;

    }

    private float GetHoldTime()
    {
        return holdTime;
    }

}
