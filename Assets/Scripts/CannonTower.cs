using UnityEngine;
using System.Collections;

public class CannonTower : TowerController 
{
	[SerializeField] private GameObject m_muzzle;
	[SerializeField] private float m_speedRotation = 0.5f;
	// public override void Rotation(GameObject target)
    // {
    //     Vector3 targetPosition = target.transform.position;
    //     Vector3 shooterPosition = m_shootPoint.transform.position;
    //     //float distanceToTarget = Vector3.Distance(targetPosition, shooterPosition);
    //     Vector3 displacement = targetPosition - shooterPosition;
    //     float distanceToTarget = displacement.magnitude;
    //     //Debug.Log(m_shootPoint.transform.position);
    //     float timeToHit = distanceToTarget / m_speedProjectile;
    //     Debug.Log(timeToHit);
    //     Vector3 futureTargetPosition = targetPosition + target.GetComponent<Rigidbody>().velocity * timeToHit;
    //     Vector3 directionToTarget = futureTargetPosition - shooterPosition;
    //     Quaternion lookLocalRotation = Quaternion.LookRotation(directionToTarget);
    //     float targetX = lookLocalRotation.x;
    //     RotationMuzzle(targetX);
    //     directionToTarget = new Vector3(directionToTarget.x, 0f, directionToTarget.z);
    //     Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
    //     transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * m_speedRotation);
        
    // }

    // private void RotationMuzzle(float targetx)
    // {
    //     Quaternion targetRotation = Quaternion.Euler(targetx * 100f, 0f, 0f);
    //     Quaternion currentRotation = m_muzzle.transform.localRotation;
    //     Quaternion newRotation = Quaternion.Lerp(currentRotation, targetRotation, Time.deltaTime * m_speedRotation);
    //     m_muzzle.transform.localRotation = newRotation;
    // }
//     private float currentTime = 0f;
    public override void Rotation(GameObject target)
    {
        Vector3 targetDir = target.transform.position - m_shootPoint.transform.position;
        Vector3 targetVelocity = target.GetComponent<Rigidbody>().velocity;
        float angle = Vector3.Angle(targetDir, transform.forward);
        float timeToTarget = targetDir.magnitude / m_speedProjectile;
        float timeToIntersection = GetTimeToIntersection(targetDir, targetVelocity, m_speedProjectile);
        Vector3 aimPoint = timeToIntersection <= timeToTarget ? 
                        target.transform.position + targetVelocity * timeToIntersection :
                        target.transform.position + targetVelocity * timeToTarget;
        Vector3 aimDirection = aimPoint - transform.position;
        transform.rotation = Quaternion.LookRotation(aimDirection);
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

        float t1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
        float t2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
        Debug.Log($"{t1}......{t2}");
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
