

using UnityEngine;
using UnityEngine.InputSystem;

public abstract class NPC : MonoBehaviour, Interactable
{

    [SerializeField] private SpriteRenderer interactSprite;

    private Transform _playerTransform;
    private bool _isInteractable = true;
    private const float INTERACT_DISTANCE = 15f;
    private InputAction _action;

    public abstract void Interact();

    private void Update()
    {   
        if (IsWhithinInteractDistance() && _action.triggered) { 
            Interact(); 

        }

        if(interactSprite.gameObject.activeSelf && !IsWhithinInteractDistance())
        {
            interactSprite.gameObject.SetActive(false);
        }
        else if(!interactSprite.gameObject.activeSelf && IsWhithinInteractDistance())
        {
            interactSprite.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _action = new InputAction(binding: "<Keyboard>/e", type: InputActionType.Button);
        _action.Enable();
    }

    private bool IsWhithinInteractDistance()
    {
        if(Vector2.Distance(_playerTransform.position, transform.position) < INTERACT_DISTANCE)
        {
            return true;
        }

        return false;
        //return _isInteractable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isInteractable = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isInteractable = false;
    }
}