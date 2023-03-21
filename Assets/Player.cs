using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float speed;

    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        //horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");

        //transform.Translate(moveVector * Time.deltaTime);

        //transform.position = speed * Time.deltaTime * horizontal;
    }

    IEnumerator Move()
    {
        Vector3 moveVector = Vector3.zero;

        string horizontal = "Horizontal";
        string vertical = "Vertical";

        while (true)
        {
            if (transform.position.x + (Input.GetAxisRaw(horizontal) * speed * Time.deltaTime) < 5f && transform.position.x + (Input.GetAxisRaw(horizontal) * speed * Time.deltaTime) > -5f)
            {
                moveVector.x = Input.GetAxisRaw(horizontal);
            }
            else
            {
                moveVector.x = 0f;
            }

            moveVector.z = Input.GetAxisRaw(vertical);

            transform.Translate(moveVector * Time.deltaTime * speed);

            yield return null;
        }
    }
}
