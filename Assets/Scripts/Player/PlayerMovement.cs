using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.0f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _sr;

    private float _horizontal;
    private float _vertical;

    private bool _facingLeft = false;

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        // change sprite direction

        // if we are moving to the left but facing right or moving right but facing left...
        if(_horizontal < 0 && !_facingLeft || _horizontal > 0 && _facingLeft)
        {
            Flip();
        }
   
    }

    private void FixedUpdate()
    {
        if(_horizontal != 0)
        {
            _rb.linearVelocity = new Vector2(_horizontal * _moveSpeed, 0);
        }
        else if(_vertical != 0)
        {
            _rb.linearVelocity = new Vector2(0, _vertical * _moveSpeed);
        }
     
    }

    private void Flip()
    {
        _facingLeft = !_facingLeft;
        _sr.flipY = _facingLeft;
    }
}
