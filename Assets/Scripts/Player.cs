using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicUnit
{
    protected override IEnumerator Move()
    {
        yield return null;
    }

    protected override IEnumerator Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator Dead()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator Hit()
    {
        throw new System.NotImplementedException();
    }
}
