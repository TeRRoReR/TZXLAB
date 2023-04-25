using UnityEngine;
using System.Collections;

public class CannonTower : TowerController 
{
	[SerializeField] private GameObject m_muzzle;
	[SerializeField] private float m_speedRotation = 0.5f;

    public override void Rotation(GameObject target)
    {
        Vector3 targetDir = target.transform.position - m_shootPoint.transform.position;
        Vector3 targetVelocity = target.GetComponent<Rigidbody>().velocity;
        float timeToIntersection = GetTimeToIntersection(targetDir, targetVelocity, m_speedProjectile);
        Vector3 aimPoint = target.transform.position + targetVelocity * timeToIntersection;
        Vector3 aimDirection = aimPoint - m_shootPoint.transform.position;
        Quaternion lookLocalRotation = Quaternion.LookRotation(aimDirection);
        float angleX = lookLocalRotation.x;
        RotationMuzzle(angleX);
        aimDirection = new Vector3(aimDirection.x, 0f, aimDirection.z);
        Quaternion targetRotation = Quaternion.LookRotation(aimDirection);
        Quaternion towerRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, m_speedRotation * Time.deltaTime);
        transform.rotation = towerRotation;
    }

    private void RotationMuzzle(float angleX)
    {
        Quaternion targetRotation = Quaternion.Euler(angleX * 100f, 0f, 0f);
        Quaternion currentRotation = m_muzzle.transform.localRotation;
        Quaternion newRotation = Quaternion.Lerp(m_muzzle.transform.localRotation, targetRotation, Time.deltaTime * m_speedRotation);
        m_muzzle.transform.localRotation = newRotation;
    }

    private float GetTimeToIntersection(Vector3 targetDir, Vector3 targetVelocity, float bulletSpeed)
    {
        float a = Vector3.Dot(targetVelocity, targetVelocity) - bulletSpeed * bulletSpeed;
        float b = 2 * Vector3.Dot(targetVelocity, targetDir);
        float c = Vector3.Dot(targetDir, targetDir);
        float discriminant = b * b - 4 * a * c;
        if (discriminant < 0)
        {
            return -1;
        }

        float twoA = 2 * a;
        float t1 = (-b + Mathf.Pow(discriminant, 0.5f)) / twoA;
        float t2 = (-b - Mathf.Pow(discriminant, 0.5f)) / twoA;

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
