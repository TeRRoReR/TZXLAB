﻿using UnityEngine;
using System.Collections;

public class CannonTower : TowerController 
{
    [SerializeField] private float m_speedRotation = 0.5f;
	[SerializeField] private GameObject m_muzzle;

    public override void Rotation(GameObject target)
    {
        Vector3 targetDir = target.transform.position - m_shootPoint.transform.position;
        Vector3 targetVelocity = target.GetComponent<Rigidbody>().velocity;
        float timeToIntersection = GetTimeToIntersection(targetDir, targetVelocity, m_speedProjectile);
        Vector3 aimPoint = target.transform.position + targetVelocity * timeToIntersection;
        Vector3 aimDirection = aimPoint - m_shootPoint.transform.position;
        RotationTower(aimDirection);
        RotationMuzzle(aimDirection);
    }

    private void RotationMuzzle(Vector3 dir)
    {
        Vector3 targetDirWithoutY = new Vector3(dir.x, 0f, dir.z).normalized;
        float verticalAngle = Vector3.Angle(targetDirWithoutY, dir);
        Quaternion verticalRotation = Quaternion.Euler(verticalAngle, 0f, 0f);
        m_muzzle.transform.localRotation = Quaternion.RotateTowards(m_muzzle.transform.localRotation, verticalRotation, Time.deltaTime * m_speedRotation);
    }

    private void RotationTower(Vector3 dir)
    {
        Vector3 targetDirWithoutX = new Vector3(dir.x, 0f, dir.z);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirWithoutX);
        Quaternion towerRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, m_speedRotation * Time.deltaTime);
        transform.rotation = towerRotation;
    }

    private float GetTimeToIntersection(Vector3 targetDir, Vector3 targetVelocity, float bulletSpeed)
    {
        float a = (targetVelocity.x * targetVelocity.x + targetVelocity.y * targetVelocity.y + targetVelocity.z * targetVelocity.z) - bulletSpeed * bulletSpeed;
        float b = 2 * (targetVelocity.x * targetDir.x + targetVelocity.y * targetDir.y + targetVelocity.z * targetDir.z);
        float c = targetDir.x * targetDir.x + targetDir.y * targetDir.y + targetDir.z * targetDir.z;
        float discriminant = b * b - 4 * a * c;
        if (discriminant < 0)
        {
            return -1;
        }

        float twoA = 2 * a;
        float sqrtDiscriminant = Mathf.Pow(discriminant, 0.5f);
        float t1 = (-b + sqrtDiscriminant) / twoA;
        float t2 = (-b - sqrtDiscriminant) / twoA;

        if (t1 < 0 && t2 < 0)
        {
            return -1;
        }

        if (t1 < 0)
        {
            return t2;
        }

        if (t2 < 0)
        {
            return t1;
        }

        return Mathf.Min(t1, t2);
    }

}
