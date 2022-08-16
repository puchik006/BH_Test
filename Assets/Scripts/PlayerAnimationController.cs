using Mirror;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : NetworkBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private List<RuntimeAnimatorController> _animationControllers;
    
    private enum AnimationsName
    {
        Idle, 
        Run,
        Sprint,
        Fall
    };
  
    public void PlayAnimation(float horizontal, float vertical, ISprintable mover)
    {
        if (horizontal != 0 || vertical != 0)
        {
            if (mover.IsSprinting)
            {
                PlayAnimationByName(AnimationsName.Sprint);
            }
            else
            {
                PlayAnimationByName(AnimationsName.Run);
            }
        }
        else
        {
            PlayAnimationByName(AnimationsName.Idle);
        }
    }

    [Command]
    private void PlayAnimationByName(AnimationsName name)
    {
        _animator.runtimeAnimatorController = _animationControllers[(int)name];
        PlayAnimationByNameCallback(name);
    }

    [ClientRpc]
    private void PlayAnimationByNameCallback(AnimationsName name)
    {
        _animator.runtimeAnimatorController = _animationControllers[(int)name];
    }
}
