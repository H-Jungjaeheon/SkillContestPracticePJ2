using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void plusScore(int curPlusScore) 
    {
        score += curPlusScore;

        StageManager.instance.scoreText.text = $"{score} Á¡";
    }

    public void deleateScore()
    {
        score = 0;
    }
}
