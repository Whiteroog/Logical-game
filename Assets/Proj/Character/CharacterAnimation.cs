using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterAnimation : MonoBehaviour
{
    public Animator animator;
    private Vector3 _currentDirectAnim;

    void Start()
    {
        SetDirectAnimation(new Vector3(0.0f, -0.1f));
    }

    public void SetDirectAnimation(Vector3 direct)
	{
        animator.SetFloat("X", direct.x);
        animator.SetFloat("Y", direct.y);

        _currentDirectAnim = direct;
    }

    public void SetIdleAnimation()
	{
        SetDirectAnimation(_currentDirectAnim / 10.0f);
    }
}
