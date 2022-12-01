using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator anim;

    public void ToogleBool(string boolname) 
    {
        anim.SetBool(boolname, !anim.GetBool(boolname));
    }
}
