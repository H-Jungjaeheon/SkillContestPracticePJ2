using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking
{
    public string name;
    public int score;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Ranking> ranking = new List<Ranking>();

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

    private void Start()
    {
        Ranking test = new Ranking();
        Ranking test2 = new Ranking();
        Ranking test3 = new Ranking();

        test.name = "a";
        test.score = 1;

        ranking.Add(test);

        test2.name = "b";
        test2.score = 2;

        ranking.Add(test2);

        test3.name = "c";
        test3.score = 3;

        ranking.Add(test3);

        ranking.Sort(SortRanking);

        for (int i = 0; i < ranking.Count; i++)
        {
            print(ranking[i].name);
        }
    }

    int SortRanking(Ranking a, Ranking b)
    {
        return (a.score >= b.score) ? -1 : 1;
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
