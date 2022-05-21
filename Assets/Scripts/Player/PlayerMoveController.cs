using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField]
    private float _movementSpeed;

    [SerializeField]
    private float _jumpForce;

    [SerializeField]
    private int _maxExtraJumpsCount;

    [SerializeField]
    private GroundCheckerWithOverlapArea _groundChecker;

    //[SerializeField]
    //private GroundCheckerCollisions _groundChecker;

    [SerializeField]
    private float _fallGravityScale;

    [SerializeField]
    private float _shortJumpGravityScale;

    [SerializeField]
    private float _longJumpGravityScale;

    [SerializeField]
    private PlayerAnimationController _animationController;

    #endregion


    #region Public properties

    public float SpeedXNormalized
    {
        get
        {
            return _rigidbody.velocity.x / _movementSpeed;
        }
    }

    public float SpeedY
    {
        get
        {
            return _rigidbody.velocity.y;
        }
    }

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
    }

    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _extraJumpsCount = _maxExtraJumpsCount;
    }

    private void Update()
    {
        // Déplacement horizontal
        Vector2 inputDirection = new Vector2(Input.GetAxis("Horizontal"), 0);
        _horizontalVelocity = inputDirection * _movementSpeed;

        if (!Mathf.Approximately(inputDirection.x, 0))
        {
            _transform.right = inputDirection;
        }

        // Saut

        // On teste si on repose sur le sol.
        _isGrounded = false;

        // Si on est en train de sauter, on ne va rechecker le sol que lorsque 
        // la vitesse verticale deviendra négative.
        if (_isJumping)
        {
            if (SpeedY < -0.1f)
            {
                _isGrounded = _groundChecker.CheckGround();

                // Dans ce cas, si on touche le sol, alors on considère que le saut est terminé
                if (_isGrounded)
                {
                    _isJumping = false;
                }
            }
        }
        // Si on n'est pas en train de sauter, on teste le sol dans tous les cas.
        else
        {
            _isGrounded = _groundChecker.CheckGround();
        }

        // On récupère tous nos extra jumps dès qu'on retouche le sol
        if (!_wasGrounded && _isGrounded)
        {
            _extraJumpsCount = _maxExtraJumpsCount;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (_isGrounded)
            {
                _doJump = true;
                _isJumping = true;
            }
            else if (_extraJumpsCount > 0)
            {
                _extraJumpsCount--;
                _doJump = true;
                _isJumping = true;
            }
        }

        if (Input.GetButton("Fire1"))
        {
            _isLongJump = true;
        }

        _wasGrounded = _isGrounded;
    }

    private void FixedUpdate()
    {
        Vector2 verticalVelocity = new Vector2(0, _rigidbody.velocity.y);

        Vector2 velocity = _horizontalVelocity + verticalVelocity;

        _rigidbody.velocity = velocity;

        if (_doJump)
        {
            velocity.y = 0;
            _rigidbody.velocity = velocity;

            Vector2 jumpForce = Vector2.up * _jumpForce;
            _rigidbody.AddForce(jumpForce, ForceMode2D.Impulse);
            _doJump = false;
        }

        // Better jump in 4 lines of code https://youtu.be/7KiK0Aqtmzc
        if (_rigidbody.velocity.y < -0.1f)
        {
            //_rigidbody.velocity += Physics2D.gravity * (_fallGravityScale - 1) * Time.fixedDeltaTime;
            _rigidbody.gravityScale = _fallGravityScale;
        }
        else if (_rigidbody.velocity.y > 0.1f && !_isLongJump)
        {
            //_rigidbody.velocity += Physics2D.gravity * (_longJumpGravityScale - 1) * Time.fixedDeltaTime;
            _rigidbody.gravityScale = _shortJumpGravityScale;
        }
        else
        {
            _rigidbody.gravityScale = _longJumpGravityScale;
        }

        _isLongJump = false;
    }

    #endregion


    #region Private

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private Vector2 _horizontalVelocity;
    private bool _doJump;
    private bool _isLongJump;
    private bool _isJumping;
    private int _extraJumpsCount;
    private bool _isGrounded;
    private bool _wasGrounded;

    #endregion
}
