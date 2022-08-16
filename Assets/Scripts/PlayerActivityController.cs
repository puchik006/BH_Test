using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivityController : NetworkBehaviour
{
    [SerializeField] private Renderer _rend;

    private const float INACTIVE_TIME = 3f;

    private Material _inactiveMaterial;
    private Material _activeMaterial;
    //private Renderer _rend;
    public bool _isPlayerActive = true;

    public bool IsPlayerActive { get => _isPlayerActive;}

    private void Start()
    {
       // _rend = GetComponentInChildren<Renderer>();
        _inactiveMaterial = Resources.Load<Material>("PlayerInactiveMaterial");
        _activeMaterial = Resources.Load<Material>("PlayerActiveMaterial");
    }

    [Command]
    public void ChangeActivityOnServer()
    {
        StartCoroutine(ChangeActivityForSec(INACTIVE_TIME));
        ChangeActivityCallback();
    }

    [ClientRpc]
    private void ChangeActivityCallback()
    {
        StartCoroutine(ChangeActivityForSec(INACTIVE_TIME));
    }

    private IEnumerator ChangeActivityForSec(float time)
    {
        _rend.material = _inactiveMaterial;
        _isPlayerActive = false;
        yield return new WaitForSeconds(time);
        _rend.material = _activeMaterial;
        _isPlayerActive = true;
    }
}
