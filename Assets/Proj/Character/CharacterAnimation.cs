using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public Animator Animator;
    private Vector3 currentDirectAnim;

    void Start()
    {
        SetAnimationDirect(new Vector3(0.0f, -0.1f));
    }

    public void SetAnimationDirect(Vector3 direct)
	{
        Animator.SetFloat("X", direct.x);
        Animator.SetFloat("Y", direct.y);

        currentDirectAnim = direct;
    }

    public void SetToIdle()
	{
        SetAnimationDirect(currentDirectAnim / 10.0f);
    }
}
