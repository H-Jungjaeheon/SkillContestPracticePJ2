using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Vector3[] spawnPoses;

    [SerializeField]
    private GameObject[] enemys;

    WaitForSeconds fourSec = new WaitForSeconds(4f);

    WaitForSeconds fiveSec = new WaitForSeconds(5f);

    public void EnemySpawnStart(int curStage)
    {
        switch (curStage)
        {
            case 1:
                StartCoroutine(Stage1Spawn());
                break;
            case 2:

                break;
            case 3:

                break;
        }
    }

    private IEnumerator Stage1Spawn() // 0 1 2 3 4 5 6
    {
        Instantiate(enemys[0], spawnPoses[3], Quaternion.identity);
        
        yield return fiveSec;

        for (int i = 1; i < 5; i += 4)
        {
            Instantiate(enemys[0], spawnPoses[i], Quaternion.identity);
        }

        yield return fiveSec;

        Instantiate(enemys[0], spawnPoses[4], Quaternion.identity);

        yield return fourSec;

        Instantiate(enemys[0], spawnPoses[5], Quaternion.identity);
    }
}
