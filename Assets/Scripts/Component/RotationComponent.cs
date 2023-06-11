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
    private Vector3 m_point;
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
        Vector3 targetPos = target.transform.position;
        Vector3 targetDir = targetPos - m_shootPoint.position;
        Vector3 targetVelocity = target.GetComponent<Rigidbody>().velocity;
        m_timeToIntersection = GetTimeToIntersection(targetDir, targetVelocity);
        Vector3 aimPoint = targetPos + targetVelocity * m_timeToIntersection;
        m_point = aimPoint;
        Vector3 aimDirection = aimPoint - m_shootPoint.position;
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

    private float GetTimeToIntersection(Vector3 targetDir, Vector3 targetVelocity)
    {
        float a = targetVelocity.magnitude * targetVelocity.magnitude - m_speedProjectile * m_speedProjectile;
        float b = 2f * Vector3.Dot(targetVelocity, targetDir);
        float c = targetDir.magnitude * targetDir.magnitude;
        float discriminant = b * b - 4 * a * c;
        if (discriminant < 0)
        {
            return -1;
        }

        float twoA = 2f * a;
        float sqrtDiscriminant = Mathf.Sqrt(discriminant);
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
        // Vector3 projectileDirection = (targetPos - m_shootPoint.position).normalized;
        // Vector3 projectileVelocity = projectileDirection * m_speedProjectile;
        // Vector3 relativePosition = targetPos - m_shootPoint.position;
        // float timeToIntercept = relativePosition.magnitude / (projectileVelocity - targetVelocity).magnitude;
        // return timeToIntercept;
    }

    private void OnDrawGizmos()
    {
        if(m_point != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(m_point, 2f);
        } 
    }
}
