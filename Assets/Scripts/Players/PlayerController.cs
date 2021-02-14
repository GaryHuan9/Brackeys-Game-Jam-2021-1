using System;
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

		Int2 input;
		bool jumping;

		public bool IsGrounded { get; private set; }
		static float Gravity => Physics2D.gravity.y;

		void Update()
		{
			input = InputHelper.GetWASDMovement();
			jumping = Input.GetKeyDown(KeyCode.Space);
		}

		void FixedUpdate()
		{
			//Check grounded
			IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);

			//Set velocity
			float y = Gravity;

			if (jumping && IsGrounded)
			{
				y += Mathf.Sqrt(-2f * Gravity * jumpHeight);
				jumping = false;
			}

			rigidbody.velocity = input * movementSpeed;
			rigidbody.velocity += new Vector2(0f, y);
		}
	}
}