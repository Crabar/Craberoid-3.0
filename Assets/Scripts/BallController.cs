using System;
using System.Collections;
using System.Collections.Generic;
using Signals;
using States;
using UnityEngine;
using Zenject;

public class BallController : MonoBehaviour
{
    private Settings _settings;

    [Inject(Id = "PlayerTransform")] public Transform PlayerTransform;

    [Inject]
    public void Construct(
        Settings settings,
        LaunchBallSignal launchBallSignal,
        AttachToPlayerSignal attachToPlayerSignal,
        ResetPlayerStateSignal resetPlayerStateSignal)
    {
        _settings = settings;
        launchBallSignal += LaunchBall;
        attachToPlayerSignal += AttachToPlayer;
        resetPlayerStateSignal += OnReset;
    }

    private void OnReset()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = new Vector3(0, 0.5f, -18);
    }

    private void OnDestroy()
    {
        // unsubscribe
    }

    private void AttachToPlayer(bool needToAttach)
    {
        transform.SetParent(needToAttach ? PlayerTransform : null);
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
