using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : NetworkBehaviour
{
    [SerializeField] private ScoreUpdater _scoreUpdater;

    private const float TIME_TO_RESTART_GAME = 5f;

    private int _score;
    private int _scoreToEndGame = 2;

    public int Score { get => _score; }

    [Command]
    public void TakeScore()
    {
        _score++;
        _scoreUpdater.ShowScore(_score);
        TakeScoreCallBack();

        if (_score > _scoreToEndGame)
        {
            StartCoroutine(RestartGameAfter(TIME_TO_RESTART_GAME));
            return;
        }
    }

    private IEnumerator RestartGameAfter(float time)
    {
       yield return new WaitForSeconds(time);
       ResetScore();
       NetworkManager.singleton.ServerChangeScene(SceneManager.GetActiveScene().name);
    }

    [ClientRpc]
    private void TakeScoreCallBack()
    {
        _score++;
        _scoreUpdater.ShowScore(_score);
    }

    [Command]
    private void ResetScore()
    {
        _score = 0;
        ResetScoreCallback();
    }

    [ClientRpc]
    private void ResetScoreCallback()
    {
        _score = 0;
    }
}
