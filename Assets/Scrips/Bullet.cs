using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float destoryTime;

    [SerializeField]
    private float damage;

    [SerializeField]
    private string targetTag;

    private Vector3 moveVector;

    void Start()
    {
        moveVector.z = speed;
        Destroy(gameObject, destoryTime);
    }

    void Update()
    {
        transform.Translate(moveVector * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            collision.gameObject.GetComponent<BasicUnit>().Hit(damage);

            Destroy(gameObject);
        }
    }
}
