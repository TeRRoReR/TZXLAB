using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour 
{
	[SerializeField] private float m_speed = 0.1f;
	const float m_reachDistance = 0.3f;
	private GameObject m_moveTarget;
	private Rigidbody rb;

	private void Start() {
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate () 
	{
		// if (m_moveTarget == null)
		// 	return;
		
		// if (Vector3.Distance (transform.position, m_moveTarget.transform.position) <= m_reachDistance) {
		// 	Destroy (gameObject);
		// 	return;
		// }
		// var translation = m_moveTarget.transform.position - transform.position;
		// if (translation.magnitude > m_speed) {
		// 	translation = translation.normalized * m_speed;
		// }
		// transform.Translate(translation * Time.deltaTime);
		Vector3 direction = (m_moveTarget.transform.position - transform.position).normalized;
    	rb.velocity = direction * m_speed;
	}

	public void Init(GameObject moveTarget)
	{
		m_moveTarget = moveTarget;
	}
}
