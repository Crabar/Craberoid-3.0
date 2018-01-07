using System.Collections;
using System.Collections.Generic;
using Signals;
using States;
using UnityEngine;
using Zenject;

public class FloorController : MonoBehaviour
{
    private GameEndedSignal _gameEndedSignal;

    [Inject]
    public void Construct(GameEndedSignal gameEndedSignal)
    {
        _gameEndedSignal = gameEndedSignal;
    }

    private void OnTriggerEnter(Collider other)
    {
        _gameEndedSignal.Fire();
    }
}
