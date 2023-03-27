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

    [TextArea]
    [SerializeField]
    private string[] stageOpStrings2;

    [SerializeField]
    private Image stageOpImg;

    [SerializeField]
    private Text[] stageOpTexts;

    [SerializeField]
    private GameObject warningObj;

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
        Color textColor2 = Color.white;

        stageOpTexts[0].text = stageOpStrings[curStage - 1];
        stageOpTexts[1].text = stageOpStrings2[curStage - 1];

        stageOpObj.SetActive(true);

        yield return new WaitForSeconds(4f);

        while (imgColor.a > 0f)
        {
            stageOpTexts[0].color = textColor;
            stageOpTexts[1].color = textColor2;
            stageOpImg.color = imgColor;

            imgColor.a -= Time.deltaTime;
            textColor.a -= Time.deltaTime;
            textColor2.a -= Time.deltaTime;

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
        WaitForSeconds warningDelay = new WaitForSeconds(0.75f);

        float curFillAmount = 0f;
        int warningCount = 0;

        while (warningCount < 4)
        {
            warningObj.SetActive(true);

            yield return warningDelay;

            warningObj.SetActive(false);

            yield return warningDelay;

            warningCount++;
        }

        bossNameText.text = bossName;

        bossHpUiObj.SetActive(true);

        es.SpawnBoss(curStage - 1);

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
