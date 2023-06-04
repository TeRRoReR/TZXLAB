using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    event System.Action<GameObject,int> onDestroy;
    void Init(GameObject target, float speed, int ID);
}

public interface IMove
{
    void Move();
}

public interface ITower
{
    void Init(GameObject container);
}
