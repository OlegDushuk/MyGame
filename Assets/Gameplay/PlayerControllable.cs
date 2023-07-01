using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class PlayerControllable : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        
        private CharacterController _characterController;
        private Animator _playerAnimator;

        private bool _isMoving;
        private Vector3 _targetPosition;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _playerAnimator = GetComponent<Animator>();
        }
        
        private void Update()
        {
            HandleMovement();
            
            _playerAnimator.SetBool($"IsMoving", _isMoving);
        }

        public void MoveToPoint(Vector3 point)
        {
            _isMoving = true;
            _targetPosition = point;
        }
        
        private void HandleMovement()
        {
            if (!_isMoving) return;
        
            var targetDirection = _targetPosition - transform.position;
            targetDirection.y = 0f;
            var targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (targetDirection.magnitude >= 0.1f)
            {
                _characterController.Move(transform.forward * (moveSpeed * Time.deltaTime));
            }
            else
            {
                _isMoving = false;
                _targetPosition = Vector3.zero;
            }
        }
    }
}
