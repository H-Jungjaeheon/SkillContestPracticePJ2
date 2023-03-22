using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaterialKind
{
    Basic,
    Hit
}

public abstract class BasicUnit : MonoBehaviour
{
    [SerializeField]
    protected float hp;

    [SerializeField]
    protected float damage;

    [SerializeField]
    protected float speed;

    [SerializeField]
    protected MeshRenderer mr;

    [SerializeField]
    protected Material[] materials;

    public virtual IEnumerator Hit(float damage)
    {
        hp -= damage;

        mr.materials[0] = materials[(int)MaterialKind.Hit];

        yield return null;

        mr.materials[0] = materials[(int)MaterialKind.Basic];
    }

    protected abstract IEnumerator Move();

    protected abstract IEnumerator Shoot();
}
