using System;
using CodeHelpers.Diagnostics;
using CodeHelpers.Mathematics;
using CodeHelpers.Unity;
using UnityEngine;

namespace GameJam.Players
{
	public class PlayerController : MonoBehaviour
	{
		void Awake()
		{
			rigidbody = GetComponent<Rigidbody2D>();
		}

		[SerializeField] float movementSpeed = 3f;
		[SerializeField] float jumpHeight = 1.25f;

		new Rigidbody2D rigidbody;
		float velocityY;

		float inputX;
		bool jumping;

		public bool IsGrounded { get; private set; }
		static float Gravity => Physics2D.gravity.y;

		void Update()
		{
			inputX = InputHelper.GetWASDMovement().x;
			jumping |= Input.GetKeyDown(KeyCode.Space);
		}

		void FixedUpdate()
		{
			//Check grounded
			IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);

			//Set velocity
			if (IsGrounded)
			{
				if (jumping)
				{
					velocityY = Mathf.Sqrt(-2f * Gravity * jumpHeight);
					jumping = false;
				}
				else velocityY = Gravity;
			}
			else velocityY += Gravity * Time.fixedDeltaTime;

			rigidbody.velocity = new Vector2(inputX * movementSpeed, velocityY);
		}
	}
}