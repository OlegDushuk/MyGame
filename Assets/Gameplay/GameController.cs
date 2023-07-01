using UnityEngine;

namespace Gameplay
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerControllable player;
        [SerializeField] private GameObject inventory;

        [SerializeField] private Vector3 cameraPosition;
        [SerializeField] private LayerMask terrainLayer;

        private Camera _gameCamera;
        private InputHandler _inputHandler;

        private bool _handlesMouseClick;

        private void Start()
        {
            Screen.SetResolution(1280, 720, true);

            _handlesMouseClick = true;
            
            _gameCamera = GetComponent<Camera>();
            _inputHandler = GetComponent<InputHandler>();
        }
        
        private void Update()
        {
            HandleMouseClick();

            if (_inputHandler.GetInventoryKeyDown())
            {
                inventory.SetActive(!inventory.activeSelf);
                _handlesMouseClick = !inventory.activeSelf;
            }
            
            transform.position = player.transform.position + cameraPosition;
        }
        
        private void HandleMouseClick()
        {
            if (!Input.GetMouseButtonDown(0) || !_handlesMouseClick) return;
        
            if(Physics.Raycast(_gameCamera.ScreenPointToRay(Input.mousePosition), out var hit, 22, terrainLayer))
            {
                player.MoveToPoint(hit.point);
            }
        }
    }
}
