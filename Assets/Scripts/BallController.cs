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

    private LaunchBallSignal _launchBallSignal;
    private AttachToPlayerSignal _attachToPlayerSignal;
    private ResetPlayerStateSignal _resetPlayerStateSignal;
    
    private Rigidbody _rb;

    private Rigidbody Rb
    {
        get
        {
            if (_rb == null)
            {
                _rb = GetComponent<Rigidbody>();
            }

            return _rb;
        }
    } 

    [Inject]
    public void Construct(
        Settings settings,
        LaunchBallSignal launchBallSignal,
        AttachToPlayerSignal attachToPlayerSignal,
        ResetPlayerStateSignal resetPlayerStateSignal)
    {
        _settings = settings;
        _launchBallSignal = launchBallSignal;
        _attachToPlayerSignal = attachToPlayerSignal;
        _resetPlayerStateSignal = resetPlayerStateSignal;
        
        _launchBallSignal += LaunchBall;
        _attachToPlayerSignal += AttachToPlayer;
        _resetPlayerStateSignal += OnResetState;
    }

    private void OnResetState()
    {
        Rb.velocity = Vector3.zero;
        transform.position = new Vector3(0, 0.5f, -18);
    }

    private void OnDestroy()
    {
        _launchBallSignal -= LaunchBall;
        _attachToPlayerSignal -= AttachToPlayer;
        _resetPlayerStateSignal -= OnResetState;
    }
    

    private void OnCollisionEnter(Collision other)
    {
        GetComponent<AudioSource>().Play();
        Rb.velocity = Rb.velocity.normalized * _settings.ballSpeed;
    }

    private void OnCollisionExit(Collision other)
    {
        var ballAngle = Math.Abs(Vector3.Angle(Rb.velocity, Vector3.right));
        if (ballAngle < 2 || 180 - ballAngle < 2)
        {
            Rb.velocity = Quaternion.Euler(0, 5, 0) * Rb.velocity;
        }
    }

    private void AttachToPlayer(bool needToAttach)
    {
        transform.SetParent(needToAttach ? PlayerTransform : null);
    }

    private void LaunchBall(Vector3 velocity)
    {
        var newBallSpeed = velocity.normalized * _settings.ballSpeed;
        Rb.velocity = newBallSpeed;
    }

    [Serializable]
    public class Settings
    {
        public int ballSpeed = 10;
    }


}
