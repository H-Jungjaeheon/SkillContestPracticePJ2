using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public static CamShake instance;

    private Vector3 startPos;

    private Vector3 shakePos;

    private IEnumerator curShake;

    private float curShakeCount;

    private WaitForSeconds shakeDelay = new WaitForSeconds(0.02f);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        startPos = transform.position;
    }

    public void StartShake(int count, int amount)
    {
        if (curShake != null)
        {
            StopCoroutine(curShake);
        }

        curShake = CamShakeEffect(count, amount);
        StartCoroutine(curShake);
    }

    private IEnumerator CamShakeEffect(int count, int amount)
    {
        curShakeCount = 0;

        while (curShakeCount < count)
        {
            transform.position = startPos;

            shakePos.x = transform.position.x + Random.Range(-amount, amount);
            shakePos.y = transform.position.y + Random.Range(0, amount);
            shakePos.z = transform.position.z + Random.Range(-amount, amount);

            transform.position = shakePos;

            yield return shakeDelay;

            curShakeCount++;
        }

        transform.position = startPos;
    }
}
