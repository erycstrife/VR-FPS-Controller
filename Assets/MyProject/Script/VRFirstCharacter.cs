using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFirstCharacter : MonoBehaviour {

	CharacterController m_CharacterController;
	CollisionFlags m_CollisionFlags;
	Vector3 m_MoveDir = Vector3.zero;

	[SerializeField] private float m_StickToGroundForce = 10;
	[SerializeField] private float m_GravityMultiplier = 2;


	void Start () {
		m_CharacterController = GetComponent<CharacterController>();
	}


	void FixedUpdate () {


		if (m_CharacterController.isGrounded)
		{
			m_MoveDir.y = -m_StickToGroundForce;

		}
		else
		{
			m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
		}

		m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

	}
}
