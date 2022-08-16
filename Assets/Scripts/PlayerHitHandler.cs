using Mirror;
using System.Collections;
using System;
using UnityEngine;

public class PlayerHitHandler : NetworkBehaviour
{
    private bool _isPause = false;

    private const float PAUSE_TIME = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != gameObject && _isPause == false)
        {
            _isPause = true;
            StartCoroutine(RegisterHitToPlayerWithPause(PAUSE_TIME, other.gameObject));
        }
    }

    private void PlayerHitPlayer(GameObject player, GameObject hitedPlayer)
    {
        if (player.GetComponent<PlayerMovement>().IsSprinting && !hitedPlayer.GetComponent<PlayerMovement>().IsSprinting
            && player.GetComponent<PlayerActivityController>().IsPlayerActive && hitedPlayer.GetComponent<PlayerActivityController>().IsPlayerActive)
        {
            hitedPlayer.GetComponent<PlayerActivityController>().ChangeActivityOnServer();
            player.GetComponent<Player>().TakeScore();
        }
    }

    private IEnumerator RegisterHitToPlayerWithPause(float time, GameObject hitedPlayer)
    {
        PlayerHitPlayer(gameObject, hitedPlayer);
        yield return new WaitForSeconds(time);
        _isPause = false;
    }
}
