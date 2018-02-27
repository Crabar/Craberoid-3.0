using Signals;
using UnityEngine;

namespace DefaultNamespace
{
    public class InputController
    {
        private readonly MovePlayerToPositionSignal _movePlayerToPositionSignal;
        
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
    }
}