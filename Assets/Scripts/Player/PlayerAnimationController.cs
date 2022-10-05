using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private const string SPEED = "Speed";
    private const string JUMP = "Jump";
    
    private static readonly int Speed = Animator.StringToHash(SPEED);
    private static readonly int Jump = Animator.StringToHash(JUMP);
    
    private Animator _animator;
   

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void SetSpeed(int value)
    {
        _animator.SetInteger(Speed, value);
    }

    public void SetJump()
    {
        _animator.SetTrigger(Jump);
    }
}
