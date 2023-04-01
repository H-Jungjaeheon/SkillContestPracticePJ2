using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [SerializeField]
    private int stage;

    [SerializeField]
    private int wave;

    [SerializeField]
    private Text waveText;

    [SerializeField]
    private Text waveStartText;

    [SerializeField]
    private RectTransform waveStartTextRt;

    [SerializeField]
    private Text enemyDeadCountText;

    private int enemyDeadCount;

    public int EnemyDeadCount
    {
        get
        {
            return enemyDeadCount;
        }
        set
        {
            enemyDeadCount = value;
            enemyDeadCountText.text = $"{value}/{maxEnemyDeadCount}";
        }
    }

    private int maxEnemyDeadCount;

    [SerializeField]
    private GameObject[] enemys;

    [SerializeField]
    private GameObject boss;

    [SerializeField]
    private Vector3[] spawnPoses;

    WaitForSeconds three = new WaitForSeconds(3f);

    WaitForSeconds two = new WaitForSeconds(2f);

    WaitForSeconds seven = new WaitForSeconds(7f);

    WaitForSeconds five = new WaitForSeconds(5f);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartSpawn()
    {
        switch (stage)
        {
            case 1:
                StartCoroutine(Stage1Spawn());
                break;
            case 2:
                StartCoroutine(Stage2Spawn());
                break;
            case 3:
                StartCoroutine(Stage3Spawn());
                break;
        }
    }

    private IEnumerator Stage1Spawn() //0 1 2 3 4 5 6
    {
        WaveStartSetting(3);

        Instantiate(enemys[0], spawnPoses[3], Quaternion.identity);
        yield return five;
        Instantiate(enemys[0], spawnPoses[1], Quaternion.identity);
        yield return five;
        Instantiate(enemys[0], spawnPoses[4], Quaternion.identity);

        while (enemyDeadCount < maxEnemyDeadCount)
        {
            yield return null;
        }

        WaveStartSetting(5);

        Instantiate(enemys[0], spawnPoses[3], Quaternion.identity);
        yield return two;
        Instantiate(enemys[0], spawnPoses[2], Quaternion.identity);
        Instantiate(enemys[0], spawnPoses[4], Quaternion.identity);
        yield return five;
        Instantiate(enemys[0], spawnPoses[0], Quaternion.identity);
        Instantiate(enemys[0], spawnPoses[6], Quaternion.identity);

        while (enemyDeadCount < maxEnemyDeadCount)
        {
            yield return null;
        }

        WaveStartSetting(8);

        Instantiate(enemys[0], spawnPoses[0], Quaternion.identity);
        Instantiate(enemys[0], spawnPoses[2], Quaternion.identity);
        yield return two;
        Instantiate(enemys[0], spawnPoses[4], Quaternion.identity);
        Instantiate(enemys[0], spawnPoses[6], Quaternion.identity);
        yield return seven;
        Instantiate(enemys[0], spawnPoses[1], Quaternion.identity);
        Instantiate(enemys[0], spawnPoses[3], Quaternion.identity);
        Instantiate(enemys[0], spawnPoses[5], Quaternion.identity);
        yield return three;
        Instantiate(enemys[1], spawnPoses[3], Quaternion.identity);

        while (enemyDeadCount < maxEnemyDeadCount)
        {
            yield return null;
        }

        WaveStartSetting(10);

        Instantiate(enemys[0], spawnPoses[1], Quaternion.identity);
        yield return three;
        Instantiate(enemys[1], spawnPoses[1], Quaternion.identity);

        yield return two;

        Instantiate(enemys[0], spawnPoses[3], Quaternion.identity);
        yield return three;
        Instantiate(enemys[1], spawnPoses[3], Quaternion.identity);

        yield return two;

        Instantiate(enemys[0], spawnPoses[5], Quaternion.identity);
        yield return three;
        Instantiate(enemys[1], spawnPoses[5], Quaternion.identity);
        
        yield return five;

        Instantiate(enemys[1], spawnPoses[0], Quaternion.identity);
        Instantiate(enemys[1], spawnPoses[6], Quaternion.identity);
        yield return three;
        Instantiate(enemys[1], spawnPoses[1], Quaternion.identity);
        Instantiate(enemys[1], spawnPoses[5], Quaternion.identity);

        while (enemyDeadCount < maxEnemyDeadCount)
        {
            yield return null;
        }

        WaveStartSetting(1);

        StageManager.instance.StartCoroutine(StageManager.instance.WarningUIAnim()); //"낙원으로 가는 길목의 수호자"
    }

    private IEnumerator Stage2Spawn()
    {
        yield return null;
    }

    private IEnumerator Stage3Spawn()
    {
        yield return null;
    }

    private void WaveStartSetting(int maxEnemyCount)
    {
        enemyDeadCount = 0;
        maxEnemyDeadCount = maxEnemyCount;

        wave++;
        waveText.text = $"Wave {wave}";
        waveStartText.text = $"Wave {wave} Start!";
        enemyDeadCountText.text = $"{0}/{maxEnemyCount}";

        StartCoroutine(WaveStartTextAnim());
    }

    private IEnumerator WaveStartTextAnim()
    {
        float curTime = 0;

        while (curTime < 3f)
        {
            waveStartTextRt.anchoredPosition = Vector3.Lerp(waveStartTextRt.anchoredPosition, new Vector3(-20f, -150f, 0f), 0.025f);
            curTime += Time.deltaTime;
            yield return null;
        }

        curTime = 0f;

        yield return three;

        while (curTime < 3f)
        {
            waveStartTextRt.anchoredPosition = Vector3.Lerp(waveStartTextRt.anchoredPosition, new Vector3(450f, -150f, 0f), 0.025f);
            curTime += Time.deltaTime;
            yield return null;
        }
    }

    public void SpawnBoss()
    {
        Instantiate(boss, spawnPoses[3], Quaternion.identity);
    }
}
