using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemys;

    [SerializeField]
    private GameObject[] bosses;

    [SerializeField]
    private Vector3[] spawnPoses;

    Quaternion basicRotation = Quaternion.Euler(90f, 0f, 0f);

    WaitForSeconds seven = new WaitForSeconds(7f);

    WaitForSeconds ten = new WaitForSeconds(10f);

    public void StartSpawn(int curStageIndex)
    {
        switch (curStageIndex)
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
        Instantiate(enemys[0], spawnPoses[3], basicRotation);
        yield return seven;
        Instantiate(enemys[0], spawnPoses[1], basicRotation);
        yield return seven;
        Instantiate(enemys[0], spawnPoses[4], basicRotation);
        yield return ten;
        Instantiate(enemys[0], spawnPoses[6], basicRotation);
        Instantiate(enemys[0], spawnPoses[0], basicRotation);
        yield return ten;
        Instantiate(enemys[1], spawnPoses[2], basicRotation);
        Instantiate(enemys[0], spawnPoses[4], basicRotation);
        yield return ten;
        Instantiate(enemys[1], spawnPoses[1], basicRotation);
        Instantiate(enemys[1], spawnPoses[5], basicRotation);
        Instantiate(enemys[0], spawnPoses[3], basicRotation);
        yield return ten;
        Instantiate(enemys[0], spawnPoses[0], basicRotation);
        Instantiate(enemys[0], spawnPoses[3], basicRotation);
        Instantiate(enemys[0], spawnPoses[6], basicRotation);
        yield return ten;
        Instantiate(enemys[1], spawnPoses[2], basicRotation);
        Instantiate(enemys[1], spawnPoses[4], basicRotation);
        yield return ten;
        StageManager.instance.StartCoroutine(StageManager.instance.BossStartUIAnim("낙원으로 가는 길목의 수호자"));
    }

    private IEnumerator Stage2Spawn()
    {
        yield return null;
    }

    private IEnumerator Stage3Spawn()
    {
        yield return null;
    }

    public void SpawnBoss(int curBossIndex)
    {
        Instantiate(bosses[curBossIndex], spawnPoses[3], Quaternion.identity);
    }
}
