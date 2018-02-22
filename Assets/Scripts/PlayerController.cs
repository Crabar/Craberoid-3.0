using System;
using Signals;
using States;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private Settings _settings;
    private MovePlayerSignal _movePlayerSignal;
    private MovePlayerToPositionSignal _movePlayerToPositionSignal;
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
        MovePlayerSignal movePlayerSignal,
        MovePlayerToPositionSignal movePlayerToPositionSignal,
        ResetPlayerStateSignal resetPlayerStateSignal)
    {
        _settings = settings;
        _movePlayerSignal = movePlayerSignal;
        _movePlayerToPositionSignal = movePlayerToPositionSignal;
        _resetPlayerStateSignal = resetPlayerStateSignal;
        _movePlayerSignal += MovePlayer;
        _movePlayerToPositionSignal += MovePlayerToPosition;
        _resetPlayerStateSignal += OnReset;
    }

    private void MovePlayerToPosition(float targetPosition)
    {
        var halfWidth = GetComponent<MeshCollider>().bounds.extents.x;
        transform.position = new Vector3(Mathf.Clamp(targetPosition, -19.5f + halfWidth, 19.5f - halfWidth),
            transform.position.y, transform.position.z);
    }

    private void OnDestroy()
    {
        _movePlayerSignal -= MovePlayer;
        _resetPlayerStateSignal -= OnReset;
    }

    private void OnReset()
    {
        Rb.velocity = Vector3.zero;
        transform.position = new Vector3(0, 2, -19);
    }

    private void MovePlayer(float horizontalOffset)
    {
        Rb.velocity = new Vector3(horizontalOffset * _settings.playerSpeed, 0, 0);
    }

    [Serializable]
    public class Settings
    {
        public int playerSpeed = 10;
    }
}