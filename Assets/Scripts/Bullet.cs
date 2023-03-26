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

    private void Start()
    {
        moveVec.y = 1f;
        StartCoroutine(DestroyBullet());
    }

    private void Update()
    {
        Move();
    }

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
