using UnityEngine;
using System.Collections;

public class CannonTower : TowerController 
{
	[SerializeField] private GameObject m_muzzle;
	[SerializeField] private float m_speedRotation = 0.5f;
	public override void Rotation(GameObject target)
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 shooterPosition = m_shootPoint.position;
        //float distanceToTarget = Vector3.SqrMagnitude(shooterPosition - targetPosition);
        //float dist = Vector3.Distance(targetPosition, shooterPosition);
        Vector3 displacement = targetPosition - shooterPosition;
        float distanceToTarget = displacement.magnitude;
        float timeToHit = distanceToTarget / m_speed;
        Vector3 futureTargetPosition = targetPosition + target.GetComponent<Rigidbody>().velocity * timeToHit;
        Vector3 directionToTarget = futureTargetPosition - shooterPosition;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * m_speedRotation);
    }
}
