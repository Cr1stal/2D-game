using UnityEngine;
using System.Collections;

public class PlayerScoreManager : MonoBehaviour
{
    int score;

    public int GetScore()
    {
        return score; 
    }

    public void IncreaseScore(int value)
    {
        score += value; 
    }

    private void Start()
    {
        score = 0;
    }
}
