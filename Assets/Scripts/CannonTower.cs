using UnityEngine;
using System.Collections;

public class CannonTower : TowerController 
{
	[SerializeField] private GameObject m_muzzle;
	[SerializeField] private float m_speedRotation = 0.5f;
	public override void Rotation(GameObject target)
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 shooterPosition = m_shootPoint.transform.position;
        //float distanceToTarget = Vector3.Distance(targetPosition, shooterPosition);
        Vector3 displacement = targetPosition - shooterPosition;
        float distanceToTarget = displacement.magnitude;
        float timeToHit = distanceToTarget / m_speedProjectile;
        Vector3 futureTargetPosition = targetPosition + target.GetComponent<Rigidbody>().velocity * timeToHit;
        Vector3 directionToTarget = futureTargetPosition - shooterPosition;
        Quaternion lookLocalRotation = Quaternion.LookRotation(directionToTarget);
        float targetX = lookLocalRotation.x;
        RotationMuzzle(targetX);
        directionToTarget = new Vector3(directionToTarget.x, 0f, directionToTarget.z);
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * m_speedRotation);
        
    }

    private void RotationMuzzle(float targetx)
    {
        Quaternion targetRotation = Quaternion.Euler(targetx * 100f, 0f, 0f);
        Quaternion currentRotation = m_muzzle.transform.localRotation;
        Quaternion newRotation = Quaternion.Lerp(currentRotation, targetRotation, Time.deltaTime * m_speedRotation);
        m_muzzle.transform.localRotation = newRotation;
    }
}
