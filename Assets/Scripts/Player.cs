using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float sprint = 100f;
    public float maxSprint = 100f;

    private FirstPersonController _firstPersonController;


    [HideInInspector]
    public float runLevel = 20.0f;
    [HideInInspector]
    public float walkLevel = 5.0f;

    public bool m_sprint;
    public bool m_run;
    public bool m_walk;

    // Start is called before the first frame update
    void Start()
    {
        _firstPersonController = GetComponent<FirstPersonController>();
    
    }

    // Update is called once per frame
    void Update()
    {
        

        SprintOrRun();
        Run();
        RegenSprintBar();

    }

    void Run()
    {
        if (sprint > 0 && !_firstPersonController.movementInputData.IsCrouching)
        {
            if (Input.GetButton("Vertical") && Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Horizontal") && Input.GetKey(KeyCode.LeftShift))
            {
                sprint -= 10 * Time.deltaTime;
            }

        }

    }

    void SprintOrRun()
    {
        if (sprint > runLevel)
        {
            m_sprint = true;
            m_run = false;
            m_walk = false;
        }
        else if (sprint < runLevel && sprint > walkLevel)
        {
            m_sprint = false;
            m_run = true;
            m_walk = false;
        }
        else if(sprint <= walkLevel)
        {
            m_sprint = false;
            m_run = false;
            m_walk = true;
        }
    }

    void RegenSprintBar()
    {
        if (m_walk)
        {
            StartCoroutine(Regeneration());
        }
        else if(sprint != maxSprint && _firstPersonController.m_currentSpeed <= _firstPersonController.walkSpeed || sprint != maxSprint && _firstPersonController.m_currentSpeed == 0)
        {
            StartCoroutine(Regeneration());
        }
        else if(sprint > runLevel)
        {
            StopAllCoroutines();
        }

    }

    IEnumerator Regeneration()
    {
        yield return new WaitForSeconds(2f);
        if(sprint <= maxSprint)
        {
            sprint += 20 * Time.deltaTime;
        }


    }
}

