using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaterialKind
{
    Basic,
    Hit
}

public enum UnitState
{
    Basic,
    Dead
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
    protected MeshRenderer[] mrs;

    [SerializeField]
    protected Material[] materials;

    protected UnitState curUnitState;

    WaitForSeconds zeroPerOne = new WaitForSeconds(0.1f);

    public virtual void Hit(float dmg)
    {
        if (hp <= 0)
        {
            StartCoroutine(Dead());
        }
        else
        {
            hp -= dmg;

            StartCoroutine(HitEffect());
        }
    } 

    public virtual IEnumerator HitEffect()
    {
        for (int i = 0; i < mrs.Length; i++)
        {
            mrs[i].material = materials[(int)MaterialKind.Hit];
        }

        yield return zeroPerOne;

        for (int i = 0; i < mrs.Length; i++)
        {
            mrs[i].material = materials[(int)MaterialKind.Basic];
        }
    }

    protected abstract IEnumerator Dead();

    protected abstract IEnumerator Move();

    protected abstract IEnumerator Shoot();
}
