using UnityEngine;

public class CharacterFlipper : MonoBehaviour
{
    [Tooltip("Optionally assign if object has a character controller")]
    [SerializeField] private CharacterController _characterController;
    private SpriteRenderer _renderer;

    private bool _haveCharacterController = false;

    private void Awake()
    {
        if(_characterController != null)
        {
            _haveCharacterController = true;
        }

        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_haveCharacterController) return;

        FlipX(_characterController.velocity.x < 0);
    }
    
    public void FlipX(bool flipX)
    {
        transform.localScale = new Vector3(flipX ? -1 : 1, transform.localScale.y, transform.localScale.z);
    }
}
