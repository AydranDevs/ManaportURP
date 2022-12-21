using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manapotion.Actions.Targets;

public class Enemy : MonoBehaviour, ITargetable
{
    public void GetPosition(out Vector2 pos)
    {
        pos = new Vector2(transform.position.x, transform.position.y);
    }

    public Transform GetTransform()
    {
        return this.transform;
    }
}
