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

    private IEnumerator Stage1Spawn()
    {
        yield return null;
    }

    private IEnumerator Stage2Spawn()
    {
        yield return null;
    }

    private IEnumerator Stage3Spawn()
    {
        yield return null;
    }

    private void SpawnBoss()
    {
        Instantiate(bosses[StageManager.instance.curStage - 1], spawnPoses[3], Quaternion.identity);
    }
}
