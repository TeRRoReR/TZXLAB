using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    event System.Action<GameObject> onDestroy;
    void Init(GameObject target, float speed);
}
