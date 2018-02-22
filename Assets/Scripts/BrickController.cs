using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Signals;
using UnityEngine;
using Zenject;

public class BrickController : MonoBehaviour
{
    private readonly Dictionary<int, Material> _hpToMaterials = new Dictionary<int, Material>();
    private int _currentHp = 2;

    private Settings _settings;
    private GiveScorepointsSignal _giveScorepointsSignal;

    public event Action<BrickController> BrickDestoyed;

    [Inject]
    public void Construct(Settings settings, GiveScorepointsSignal giveScorepointsSignal)
    {
        _settings = settings;
        _giveScorepointsSignal = giveScorepointsSignal;

        _hpToMaterials.Add(2, settings.Materials[1]);
        _hpToMaterials.Add(1, settings.Materials[0]);

        GetComponent<Renderer>().material = _hpToMaterials[_currentHp];
    }

    private void OnDestroy()
    {
        BrickDestoyed?.Invoke(this);
    }

    private void OnCollisionEnter(Collision collision)
    {

        _currentHp--;

        if (_currentHp == 0)
        {
            AudioSource.PlayClipAtPoint(_settings.BreakSound, new Vector3(0, 10, 0));
            Instantiate(_settings.Explosion, transform.position, transform.rotation);
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
        public AudioClip BreakSound;
        public GameObject Explosion;
    }
}
