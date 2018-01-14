using System;
using Signals;
using States;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private Settings _settings;
    private MovePlayerSignal _movePlayerSignal;
    private ResetPlayerStateSignal _resetPlayerStateSignal;

    [Inject]
    public void Construct(Settings settings, MovePlayerSignal movePlayerSignal, ResetPlayerStateSignal resetPlayerStateSignal)
    {
        _settings = settings;
        _movePlayerSignal = movePlayerSignal;
        _resetPlayerStateSignal = resetPlayerStateSignal;
        _movePlayerSignal += MovePlayer;
        _resetPlayerStateSignal += OnReset;
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
