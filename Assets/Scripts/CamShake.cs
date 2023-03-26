using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public static CamShake instance;

    [SerializeField]
    private GameObject playerObj;

    private Vector3 plusVec;

    private Vector3 shakePos;

    private IEnumerator curShake;

    private float curShakeCount;

    private WaitForSeconds shakeDelay = new WaitForSeconds(0.03f);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        plusVec = new Vector3(0f, 17f, -2f);
    }

    public void StartShake(int count, float amount)
    {
        if (curShake != null)
        {
            StopCoroutine(curShake);
        }

        curShake = CamShakeEffect(count, amount);
        StartCoroutine(curShake);
    }

    private IEnumerator CamShakeEffect(int count, float amount)
    {
        curShakeCount = 0;

        while (curShakeCount < count)
        {
            transform.position = playerObj.transform.position + plusVec;

            shakePos.x = transform.position.x + Random.Range(-amount, amount);
            shakePos.y = transform.position.y + Random.Range(0, amount);
            shakePos.z = transform.position.z + Random.Range(-amount, amount);

            transform.position = shakePos;

            yield return shakeDelay;

            curShakeCount++;
        }

        transform.position = playerObj.transform.position + plusVec;
    }
}
