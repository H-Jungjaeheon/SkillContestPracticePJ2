using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Vector3 moveVector;

    void Start()
    {
        moveVector.y = speed;
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        transform.Translate(moveVector * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
        }
    }
}
