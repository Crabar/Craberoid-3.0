using System.Collections;
using System.Collections.Generic;
using Signals;
using States;
using UnityEngine;
using Zenject;

public class FloorController : MonoBehaviour
{
    private FloorTouchedSignal _floorTouchedSignal;

    [Inject]
    public void Construct(FloorTouchedSignal floorTouchedSignal)
    {
        _floorTouchedSignal = floorTouchedSignal;
    }

    private void OnTriggerEnter(Collider other)
    {
        _floorTouchedSignal.Fire();
    }
}
