using System;
using Signals;
using UnityEngine;
using Zenject;

public class InputController: IFixedTickable, IInitializable
{
    private readonly MovePlayerToPositionSignal _movePlayerToPositionSignal;
    
    public event Action OnSingleTap;
    public event Action OnDoubleTap;
    
    private Action _updateDelegate;
    private float _tapTimer;
    private bool _tap;
    private const float TapThreshold = 0.25f;
        
    public InputController(MovePlayerToPositionSignal movePlayerToPositionSignal)
    {
        _movePlayerToPositionSignal = movePlayerToPositionSignal;
    }

    public void ProcessPlayerMovement()
    {
        var targetPosition = GetWorldPositionOnPlane(Input.mousePosition, 0);
        _movePlayerToPositionSignal.Fire(targetPosition.x);
    }
        
    private Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z) 
    {
        var ray = Camera.main.ScreenPointToRay(screenPosition);
        var xy = new Plane(Vector3.up, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    public void FixedTick()
    {
        _updateDelegate?.Invoke();
    }

    private void UpdateEditor()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time < _tapTimer + TapThreshold)
            {
                OnDoubleTap?.Invoke();
                _tap = false;
                return;
            }
            _tap = true;
            _tapTimer = Time.time;
        }
        if (_tap && Time.time > _tapTimer + TapThreshold)
        {
            _tap = false;
            OnSingleTap?.Invoke();
        }
    }

    private void UpdateMobile ()
    {
        for(var i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                if(Input.GetTouch(i).tapCount == 2)
                {
                    OnDoubleTap?.Invoke();
                }
                if(Input.GetTouch(i).tapCount == 1)
                {
                    OnSingleTap?.Invoke();
                }
            }
        }
    }

    public void Initialize()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        _updateDelegate = UpdateEditor;
#elif UNITY_IOS || UNITY_ANDROID
        _updateDelegate = UpdateMobile;
#endif
    }
}