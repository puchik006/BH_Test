using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersSounds : NetworkBehaviour
{
    [SerializeField] AudioSource _audioSource;

    public void PlayAccelerationSound()
    {
        _audioSource.Play();
    }
}
