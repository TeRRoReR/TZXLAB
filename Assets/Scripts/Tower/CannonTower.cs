using UnityEngine;
using System.Collections;

public class CannonTower : TowerController 
{
    private RotationComponent m_rotation;

    protected virtual void Start()
    {
        base.Start();
        m_rotation = GetComponent<RotationComponent>();
        m_rotation.Init(m_shootPoint);
    }
    protected override void Rotation(GameObject target)
    {
        Vector3 aimDirection = m_rotation.CalculateLead(target, m_speedProjectile);
        m_rotation.RotationTower(aimDirection);
        m_rotation.RotationMuzzle(aimDirection);
    }
}
