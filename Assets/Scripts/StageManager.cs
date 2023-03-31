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

    public int curStage;

    public Text scoreText;

    public Player player;

    [SerializeField]
    private GameObject warningObj;

    [SerializeField]
    private Image warningBgImg;

    [SerializeField]
    private GameObject bossHpUiObj;

    public Image bossHpBar;

    [SerializeField]
    private GameObject stageOpObj;

    [SerializeField]
    private Image stageOpImg;

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

        es.StartSpawn();
    }

    public IEnumerator WarningUIAnim()
    {
        Color color = Color.red;

        color.a = 0f;

        warningObj.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            while (color.a < 1f)
            {
                warningBgImg.color = color;

                color.a += Time.deltaTime;

                yield return null;
            }

            while (color.a > 0f)
            {
                warningBgImg.color = color;

                color.a -= Time.deltaTime;

                yield return null;
            }
        }

        warningObj.SetActive(false);

        es.SpawnBoss();
    }

    public IEnumerator BossStartUIAnim()
    {
        float curFillAmount = 0f;

        bossHpUiObj.SetActive(true);

        while (curFillAmount < 1f)
        {
            bossHpBar.fillAmount = curFillAmount / 1f;
            curFillAmount += Time.deltaTime / 3f;

            yield return null;
        }

        bossHpBar.fillAmount = 1f;
    }

    public void BossUIOff() => bossHpUiObj.SetActive(false);
}
