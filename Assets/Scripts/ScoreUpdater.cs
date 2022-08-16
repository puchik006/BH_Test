using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUpdater : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    [Command]
    public void ShowScore(int score)
    {
        _scoreText.text = score.ToString();
        ShowScoreCallback(score);
    }

    [ClientRpc]
    private void ShowScoreCallback(int score)
    {
        _scoreText.text = score.ToString();
    }
}
