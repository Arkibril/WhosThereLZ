using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    class WorldSpaceUIEnabler
    {
        private void Update()
        {
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void LateUpdate()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
