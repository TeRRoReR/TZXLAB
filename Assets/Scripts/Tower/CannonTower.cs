﻿using UnityEngine;
using System.Collections;

public class CannonTower : TowerController, ITower
{
    [SerializeField] private bool m_isBallistic;
    private RotationComponent m_rotation;

    protected override void Start()
    {
        base.Start();
        m_rotation = m_weapon.GetComponent<RotationComponent>();
        m_rotation.Init(m_attack.shootPoint, m_attack.speedProjectile);
    }
    public void Init(GameObject container)
    {
        m_container = container;
    }

    private void Rotation(GameObject target)
    {
        Vector3 aimDirection = m_rotation.CalculateLead(target, m_isBallistic);
        m_rotation.RotationTower(aimDirection);
        if(m_isBallistic)
        {
            m_attack.ChangeBullet(1);
            m_rotation.RotationBallisticMuzzle(aimDirection);
        }
        else
        {
            m_attack.ChangeBullet(0);
            m_rotation.RotationMuzzle(aimDirection);
        }
    }

    protected override void Update()
    {
        base.Update();
        if(m_target)
        {
            Rotation(m_target);
        }
    }

    
}
