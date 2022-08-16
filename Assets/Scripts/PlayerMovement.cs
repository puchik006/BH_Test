using Mirror;
using System;
using System.Collections;
using UnityEngine;


public class PlayerMovement : NetworkBehaviour, IMovable, ISprintable
{
    [SerializeField] private PlayersSounds _sounds;

    [Header("Character controller")]
    [SerializeField] private CharacterController _controller;

    [Header("Attributes to control movement")]
    [SerializeField] [Range(0f, 100f)] [SyncVar(hook =(nameof(SpeedCallback)))] private float _speed;
    [SerializeField] [Range(0f, 100f)] private float _sprintDistance;
    [SerializeField] [Range(0f, 10f)] private float _speedMultipler;

    private const float FREEZE_SPRINT_TIME = 3f;

    [SyncVar(hook = (nameof(IsSprintingCallback)))] private bool _isSprinting = false;
    private bool _isSprintPossible = true;
    private float _defaultSpeed;

    public bool IsSprinting { get => _isSprinting; }
    public float Speed { get => _speed; }

    #region methods
    private void Start() => _defaultSpeed = _speed;

    public void Move(float x, float z)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move * _speed * Time.deltaTime);
    }

    [Command]
    public void Sprint(bool mouseDown)
    {
        if (mouseDown && _isSprintPossible)
        {
            float time = _sprintDistance / (_speed * _speedMultipler);

            _sounds.PlayAccelerationSound();

            StartCoroutine(SprintForTime(time));
            StartCoroutine(DisableSprintHandlerForTime(FREEZE_SPRINT_TIME));

            if (_isSprinting)
            {
                _speed = _speed * _speedMultipler;
            }
        }

        if (_isSprinting == false)
        {
            _speed = _defaultSpeed;
        }
    }

    [ClientRpc]
    private void PlayAccelerationSoundCallback()
    {
        _sounds.PlayAccelerationSound();
    }


    private void SpeedCallback(float oldSpeed,float newSpeed) => _speed = newSpeed;
    private void IsSprintingCallback(bool oldBool, bool newBool) => _isSprinting = newBool;


    private IEnumerator SprintForTime(float time)
    {
        _isSprinting = true;
        yield return new WaitForSeconds(time);
        _isSprinting = false;
    }

    private IEnumerator DisableSprintHandlerForTime(float time)
    {
        _isSprintPossible = false;
        yield return new WaitForSeconds(time);
        _isSprintPossible = true;
    }
    #endregion
}
