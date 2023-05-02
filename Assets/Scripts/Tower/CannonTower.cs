using UnityEngine;
using System.Collections;

public class CannonTower : TowerController, ITower
{
    public void Init(GameObject container)
    {
        m_container = container;
    }

    protected override void Rotation(GameObject target)
    {
        Vector3 aimDirection = m_rotation.CalculateLead(target);
        m_rotation.RotationTower(aimDirection);
        m_rotation.RotationMuzzle(aimDirection);
    }
}
