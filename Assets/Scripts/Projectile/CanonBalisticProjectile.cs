using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBalisticProjectile : CannonProjectile, IProjectile
{
    protected override void OnEnable() 
    {
        base.OnEnable();
        m_rb.useGravity = true;
    }
}
