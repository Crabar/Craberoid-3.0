using System;
using System.Collections;
using System.Collections.Generic;
using Signals;
using UnityEngine;
using Zenject;

public class BrickController : MonoBehaviour
{
    private Dictionary<int, Material> _hpToMaterials = new Dictionary<int, Material>();
    private int _currentHp = 2;

    private Settings _settings;
    private GiveScorepointsSignal _giveScorepointsSignal;

    [Inject]
    public void Construct(Settings settings, GiveScorepointsSignal giveScorepointsSignal)
    {
        _settings = settings;
        _giveScorepointsSignal = giveScorepointsSignal;

        _hpToMaterials.Add(2, settings.Materials[1]);
        _hpToMaterials.Add(1, settings.Materials[0]);

        GetComponent<Renderer>().material = _hpToMaterials[_currentHp];
    }

    private void OnCollisionEnter(Collision collision)
    {
        _currentHp--;

        if (_currentHp == 0)
        {
            Destroy(gameObject);
            _giveScorepointsSignal.Fire(_settings.GainedScore);
        }
        else
        {
            GetComponent<Renderer>().material = _hpToMaterials[_currentHp];
        }
    }

    public class Factory : Factory<BrickController>
    {
    }

    [Serializable]
    public class Settings
    {
        public GameObject BrickPrefab;
        public Material[] Materials;
        public int GainedScore = 10;
    }
}
