using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1.0f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _sr;

    private Animator _animator;

    private float _horizontal;
    private float _vertical;

    private bool _facingLeft = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        // set animator parameters
        _animator.SetFloat("horizontal", _horizontal);
        _animator.SetFloat("vertical", _vertical);

        if (_horizontal != 0)
        {
            _animator.SetFloat("moveMagnitude", Mathf.Abs(_horizontal));
        }
        else if (_vertical != 0)
        {
            _animator.SetFloat("moveMagnitude", Mathf.Abs(_vertical));
        }
        else
        {
            _animator.SetFloat("moveMagnitude", 0);
        }

        // change sprite direction
        // if we are moving to the left but facing right or moving right but facing left...
        if (_horizontal < 0 && !_facingLeft || _horizontal > 0 && _facingLeft)
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
        else
        {
            _rb.linearVelocity = Vector2.zero;
        }
     
    }

    private void Flip()
    {
        // debugging
        Debug.Log("flipped player sprite");

        _facingLeft = !_facingLeft;
        _sr.flipX = _facingLeft;
    }
}
