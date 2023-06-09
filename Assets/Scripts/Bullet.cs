using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected float speed;

    [SerializeField]
    private string targetTag;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float destroyTime;

    private Vector3 moveVec;

    private BasicUnit bu;

    public bool isTest;

    private float a = 0;

    private float b = 0;

    private void Start()
    {
        moveVec.z = 1f;

        StartCoroutine(DestroyBullet());
    }

    private void Update()
    {
        Move();
    }

    public void speedSetting(float settingSpeed) => speed = settingSpeed;

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject);
    }

    private void Move()
    {
        transform.Translate(moveVec * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            bu = other.gameObject.GetComponent<BasicUnit>();
            bu.StartCoroutine(bu.Hit(damage));
            Destroy(gameObject);
        }
    }
}
