using UnityEngine;

public class ParticleSystemAutoDestroy : MonoBehaviour
{
    private ParticleSystem _ps;

    public void Start()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (_ps && !_ps.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}