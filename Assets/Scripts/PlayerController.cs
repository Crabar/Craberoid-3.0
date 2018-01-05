using System;
using States;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private Settings _settings;
    private MovePlayerSignal _movePlayerSignal;

    [Inject]
    public void Construct(Settings settings, MovePlayerSignal movePlayerSignal)
    {
        _settings = settings;
        movePlayerSignal += MovePlayer;
    }

    private void MovePlayer(float horizontalOffset)
    {
        GetComponent<Rigidbody>().velocity = new Vector3(horizontalOffset * _settings.playerSpeed, 0, 0);
    }

    [Serializable]
    public class Settings
    {
        public int playerSpeed = 10;
    }
}
