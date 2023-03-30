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

    public Text scoreText;

    public Player player;

    [SerializeField]
    private GameObject stageOpObj;

    [SerializeField]
    private Image stageOpImg;

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
    }

    private void Start()
    {
        StartCoroutine(StageOp());
    }

    public IEnumerator StageOp()
    {
        Color imgColor = Color.black;

        stageOpObj.SetActive(true);

        yield return new WaitForSeconds(3f);

        while (imgColor.a > 0f)
        {
            stageOpImg.color = imgColor;

            imgColor.a -= Time.deltaTime;

            yield return null;
        }

        stageOpObj.SetActive(false);

        es.StartSpawn(curStage);
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
