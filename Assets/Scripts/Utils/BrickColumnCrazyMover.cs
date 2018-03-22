using UnityEngine;

namespace Utils
{
    public class BrickColumnCrazyMover : MonoBehaviour
    {
        private double _offset;
        private int _direction = 1;
        private double _speed = 0.1;

        private void Update()
        {
            if (_offset > 5)
            {
                _direction = -1;
            }
            else if (_offset < -5)
            {
                _direction = 1;
            }

            _offset += _speed * _direction;
            transform.position = new Vector3(transform.position.x, transform.position.y,
                (float) (transform.position.z + _speed * _direction));
        }

        public void SetSpeed(double speed)
        {
            _speed = speed;
        }
    }
}