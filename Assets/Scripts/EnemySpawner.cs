using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemys;

    [SerializeField]
    private GameObject boss;

    [SerializeField]
    private Vector3[] spawnPoses;

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
        Instantiate(enemys[0], spawnPoses[3], Quaternion.identity);
        yield return seven;
        Instantiate(enemys[0], spawnPoses[1], Quaternion.identity);
        yield return seven;
        Instantiate(enemys[0], spawnPoses[4], Quaternion.identity);
        yield return ten;
        Instantiate(enemys[0], spawnPoses[6], Quaternion.identity);
        Instantiate(enemys[0], spawnPoses[0], Quaternion.identity);
        yield return ten;
        Instantiate(enemys[1], spawnPoses[2], Quaternion.identity);
        Instantiate(enemys[0], spawnPoses[4], Quaternion.identity);
        yield return ten;
        Instantiate(enemys[1], spawnPoses[1], Quaternion.identity);
        Instantiate(enemys[1], spawnPoses[5], Quaternion.identity);
        Instantiate(enemys[0], spawnPoses[3], Quaternion.identity);
        yield return ten;
        Instantiate(enemys[0], spawnPoses[0], Quaternion.identity);
        Instantiate(enemys[0], spawnPoses[3], Quaternion.identity);
        Instantiate(enemys[0], spawnPoses[6], Quaternion.identity);
        yield return ten;
        Instantiate(enemys[1], spawnPoses[2], Quaternion.identity);
        Instantiate(enemys[1], spawnPoses[4], Quaternion.identity);
        yield return ten;
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

    public void SpawnBoss()
    {
        Instantiate(boss, spawnPoses[3], Quaternion.identity);
    }
}
