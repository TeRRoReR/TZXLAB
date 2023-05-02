using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour 
{
	[SerializeField] private float m_speed = 0.1f;
	private GameObject m_moveTarget;
	private Rigidbody rb;

	private void Start() {
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate () 
	{
		Vector3 direction = (m_moveTarget.transform.position - transform.position).normalized;
    	rb.velocity = direction * m_speed;
	}

	public void Init(GameObject moveTarget)
	{
		m_moveTarget = moveTarget;
	}
}
