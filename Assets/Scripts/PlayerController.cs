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
        transform.position = new Vector3(Mathf.Clamp(targetPosition, -19.5f + halfWidth, 19.5f - halfWidth), transform.position.y, transform.position.z);
//        var moveVector = Vector3.MoveTowards(new Vector3(transform.position.x, 0f, 0f), new Vector3(targetPosition, 0f, 0f), _settings.playerSpeed);
//        System.Diagnostics.Debug.WriteLine(moveVector);
//        var rb = GetComponent<Rigidbody>();
//        rb.velocity = moveVector;
    }

    private void OnDestroy()
    {
        _movePlayerSignal -= MovePlayer;
        _resetPlayerStateSignal -= OnReset;
    }

    private void OnReset()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = new Vector3(0, 2, -19);
    }

    private void MovePlayer(float horizontalOffset)
    {
        var rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(horizontalOffset * _settings.playerSpeed, 0, 0);
    }

    [Serializable]
    public class Settings
    {
        public int playerSpeed = 10;
    }
}
