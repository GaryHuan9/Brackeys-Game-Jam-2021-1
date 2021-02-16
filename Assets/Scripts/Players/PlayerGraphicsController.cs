﻿using System;
using CodeHelpers.Mathematics;
using UnityEngine;

namespace GameJam.Players
{
	public class PlayerGraphicsController : MonoBehaviour
	{
		void Awake()
		{
			rigidbody = GetComponent<Rigidbody2D>();
		}

		[SerializeField] SpriteRenderer image;
		[SerializeField] float animationThreshold = 0.05f;

		[Space]
		//
		[SerializeField] float tiltAngle = 15f;
		[SerializeField] float tiltTime = 0.6f;

		new Rigidbody2D rigidbody;

		int tiltDirection;
		float tiltVelocity;

		void Update()
		{
			float angle = image.transform.eulerAngles.z;

			angle = Scalars.Damp(angle, tiltDirection * tiltAngle, ref tiltVelocity, tiltTime, Time.deltaTime);
			image.transform.eulerAngles = Float3.forward * angle;
		}

		void FixedUpdate()
		{
			Float2 velocity = rigidbody.velocity.normalized;
			tiltDirection = 0;

			if (Math.Abs(velocity.x) < Math.Abs(velocity.y)) return;
			if (Math.Abs(velocity.x) < animationThreshold) return;

			image.flipX = velocity.x > 0f;
			tiltDirection = velocity.x.Sign();
		}
	}
}