using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Ready,
    Play,
    End
}

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public GameState curState;

    public int curStage = 1;

    private int score;

    [SerializeField]
    private Text scoreText;

    public Player player;

    [SerializeField]
    private GameObject stageOpObj;

    [TextArea]
    [SerializeField]
    private string[] stageOpStrings;

    [SerializeField]
    private Image stageOpImg;

    [SerializeField]
    private Text stageOpText;

    [SerializeField]
    private GameObject bossHpUiObj;

    public Image bossHpBar;

    [SerializeField]
    private Text bossNameText;

    [SerializeField]
    private EnemySpawner es;

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
        StartCoroutine(StageOp());
    }

    public IEnumerator StageOp()
    {
        Color imgColor = Color.black;
        Color textColor = Color.yellow;

        stageOpText.text = stageOpStrings[curStage - 1];
        stageOpText.color = textColor;
        stageOpImg.color = imgColor;

        stageOpObj.SetActive(true);

        yield return new WaitForSeconds(4f);

        while (imgColor.a > 0f)
        {
            imgColor.a -= Time.deltaTime;
            textColor.a -= Time.deltaTime;

            stageOpText.color = textColor;
            stageOpImg.color = imgColor;

            yield return null;
        }

        stageOpObj.SetActive(false);

        es.StartSpawn(curStage);
    }

    public void ScoreUpdate(int plusScore)
    {
        score += plusScore;

        scoreText.text = $"{score} Á¡";
    }

    public IEnumerator BossStartUIAnim(string bossName)
    {
        float curFillAmount = 0f;

        bossNameText.text = bossName;

        bossHpUiObj.SetActive(true);

        while (curFillAmount < 1f)
        {
            bossHpBar.fillAmount = curFillAmount / 1f;
            curFillAmount += Time.deltaTime / 4f;

            yield return null;
        }

        bossHpBar.fillAmount = 1f;
    }

    public void BossUIOff() => bossHpUiObj.SetActive(false);
}
