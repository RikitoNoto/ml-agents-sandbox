using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class ScoreArea : MonoBehaviour
{
    public GameManager gameManager;
    public int agentId;

    private void OnTriggerEnter(Collider other)
    {
        gameManager.EndEpisode(agentId);
    }
}
