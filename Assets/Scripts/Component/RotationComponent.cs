using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RotationComponent : MonoBehaviour
{
    [SerializeField] private GameObject m_muzzle;
    [SerializeField] private float m_speedRotation = 20f;
    private Transform m_shootPoint;
    private float m_speedProjectile;
    private float m_gravity = 9.8f;
    private float m_timeToIntersection = 0f;
    private Vector3 direrc;
    private void Awake()
    {
        m_shootPoint = transform;
    }
    public void Init(Transform shootPoint, float speedProjectile)
    {
        m_shootPoint = shootPoint;
        m_speedProjectile = speedProjectile;
    }

    public Vector3 CalculateLead(GameObject target, bool mode)
    {
        Vector3 targetDir = target.transform.position - m_shootPoint.transform.position;
        Vector3 targetVelocity = target.GetComponent<Rigidbody>().velocity;
        if(mode)
        {
            m_timeToIntersection = GetTimeToIntersection(targetDir);
        }
        else
        {
            m_timeToIntersection = GetTimeToIntersection(target.transform.position, targetVelocity);
        }
        Vector3 aimPoint = target.transform.position + targetVelocity * m_timeToIntersection;
        direrc = aimPoint;
        Vector3 aimDirection = aimPoint - m_shootPoint.transform.position;
        return aimDirection;
    }
    public void RotationBallisticMuzzle(Vector3 dir)
    {
        float? angle = CalculateAngle(dir);
        if(angle != null)
        {
            Quaternion verticalRotation = Quaternion.Euler(360 - (float)angle, 0f, 0f);
            m_muzzle.transform.localRotation = Quaternion.RotateTowards(m_muzzle.transform.localRotation, verticalRotation, m_speedRotation * Time.deltaTime);
        }
    }

    public void RotationMuzzle(Vector3 dir)
    {
        Vector3 targetDirWithoutY = new Vector3(dir.x, 0f, dir.z).normalized;
        float verticalAngle = Vector3.Angle(targetDirWithoutY, dir);
        Quaternion verticalRotation = Quaternion.Euler(verticalAngle, 0f, 0f);
        m_muzzle.transform.localRotation = Quaternion.RotateTowards(m_muzzle.transform.localRotation, verticalRotation, m_speedRotation * Time.deltaTime);
    }

    public void RotationTower(Vector3 dir)
    {
        Vector3 targetDirWithoutX = new Vector3(dir.x, 0f, dir.z);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirWithoutX);
        Quaternion towerRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, m_speedRotation * Time.deltaTime);
        transform.rotation = towerRotation;
    }

    private float? CalculateAngle(Vector3 dir)
    {
        float y = dir.y;
        dir.y = 0f;
        float x = dir.magnitude;
        float sSqr = m_speedProjectile * m_speedProjectile;
        float underTheSqrRoot = (sSqr * sSqr) - m_gravity * (m_gravity * x * x + 2 * y * sSqr);
        if(underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float lowAngle = sSqr - root;
            return (Mathf.Atan2(lowAngle, m_gravity * x) * Mathf.Rad2Deg);
        }
        else return null;
    }

    private float GetTimeToIntersection(Vector3 dir)
    {
        float y = dir.y;
        dir.y = 0f;
        float x = dir.magnitude;
        float sSqr = m_speedProjectile * m_speedProjectile;
        float underTheSqrRoot = (sSqr * sSqr) - m_gravity * (m_gravity * x * x + 2 * y * sSqr);
        if (underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float lowAngle = sSqr - root;
            float timeToTarget = x / (m_speedProjectile * Mathf.Cos(Mathf.Atan2(lowAngle, m_gravity * x)));
            return timeToTarget;
        }
        else
        {
            return 0f;
        }
    }
    private float GetTimeToIntersection(Vector3 targetPos, Vector3 targetVelocity)
    {
        Vector3 projectileDirection = (targetPos - m_shootPoint.position).normalized;
        Vector3 projectileVelocity = projectileDirection * m_speedProjectile;
        Vector3 relativePosition = targetPos - m_shootPoint.position;
        float timeToIntercept = relativePosition.magnitude / (projectileVelocity - targetVelocity).magnitude;
        return timeToIntercept;
    }

    private void OnDrawGizmos()
    {
        if(direrc != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(direrc, 2f);
        } 
    }
}
