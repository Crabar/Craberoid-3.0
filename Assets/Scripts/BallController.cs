using System;
using System.Collections;
using System.Collections.Generic;
using States;
using UnityEngine;
using Zenject;

public class BallController : MonoBehaviour
{
	private Settings _settings;

	[Inject]
	public void Construct(Settings settings, MoveBallSignal moveBallSignal, LaunchBallSignal launchBallSignal)
	{
		_settings = settings;
		moveBallSignal += SetVelocity;
		launchBallSignal += LaunchBall;
	}
	
	private void SetVelocity(Vector3 velocity)
	{
		GetComponent<Rigidbody>().velocity = velocity;
	}

	private void LaunchBall(Vector3 velocity)
	{
		var newBallSpeed = velocity.normalized * _settings.ballSpeed;
		GetComponent<Rigidbody>().velocity = newBallSpeed;
	}

	[Serializable]
	public class Settings
	{
		public int ballSpeed = 10;
	}
}
