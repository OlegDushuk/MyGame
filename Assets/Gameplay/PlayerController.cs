using Gameplay.InventorySystem;
using Gameplay.InventorySystem.UI;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(InputHandler), typeof(CharacterController), typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Gameplay Objects")] 
        [SerializeField] private Camera gameCamera;
        [SerializeField] private Canvas inventoryCanvas;
        [SerializeField] private ItemStorageUI playerInventoryUI;
        
        [Header("Options")]
        [SerializeField] private Vector3 cameraPosition;
        [SerializeField] private LayerMask terrainLayer;
        
        [Header("Movement")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        
        [Header("Inventory")]
        [SerializeField] private int amountSlots;
        
        private CharacterController _characterController;
        private InputHandler _inputHandler;
        private Animator _playerAnimator;
        private ItemStorage _inventory;
        
        private bool _isMoving;
        private Vector3 _targetPosition;
        private bool _handlesMouseClick;
        
        private void Start()
        {
            _inputHandler = GetComponent<InputHandler>();
            _characterController = GetComponent<CharacterController>();
            _playerAnimator = GetComponent<Animator>();
            
            _handlesMouseClick = true;
            _inventory = new ItemStorage(amountSlots);
            _inventory.ConnectUI(playerInventoryUI);
        }
        
        private void Update()
        {
            HandleMouseClick();
            HandleMovement();
            
            if (_inputHandler.GetInventoryKeyDown())
            {
                ShowInventory();
            }

            gameCamera.transform.position = transform.position + cameraPosition;
        }
        
        private void MoveToPoint(Vector3 point)
        {
            _isMoving = true;
            _targetPosition = point;
        }
        
        private void ShowInventory()
        {
            GameObject canvas;
            (canvas = inventoryCanvas.gameObject).SetActive(!inventoryCanvas.gameObject.activeSelf);
            
            (playerInventoryUI.gameObject).SetActive(!playerInventoryUI.gameObject.activeSelf);
            
            _handlesMouseClick = !canvas.activeSelf;
        }
        
        private void HandleMouseClick()
        {
            if (!Input.GetMouseButtonDown(0) || !_handlesMouseClick) return;
            
            if(Physics.Raycast(gameCamera.ScreenPointToRay(Input.mousePosition), out var hit, 22, terrainLayer))
            {
                MoveToPoint(hit.point);
            }
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
            
            _playerAnimator.SetBool($"IsMoving", _isMoving);
        }
    }
}
