﻿using UnityEngine;
using System.Collections;

public class CannonProjectile : Projectile , IProjectile
{
	private Rigidbody m_rb;
	private Vector3 m_direction;

	private void OnEnable()
	{
		m_rb = GetComponent<Rigidbody>();
	}
	
	protected override void Update(){}
	
	protected override void Move(Vector3 dir)
    {
		m_rb.velocity = dir * m_speed;
	}

	public void Init(GameObject target, float speed, Vector3 dir)
	{
		m_target = target;
		m_speed = speed;
		m_direction = dir;
		Move(m_direction);
	}
}
