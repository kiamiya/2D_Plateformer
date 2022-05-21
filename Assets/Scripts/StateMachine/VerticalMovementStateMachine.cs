
using UnityEngine;

public enum VerticalMovementState
{
    GROUNDED,
    JUMPING,
    EXTRA_JUMPING,
    FALLING
}

public class VerticalMovementStateMachine : MonoBehaviour
{
    #region Show in inspector

    [SerializeField]
    private PlayerMoveControllerWithStateMachine _playerMoveController;

    [SerializeField]
    private AnimationControllerWithStateMachine _animationController;

    [SerializeField]
    private GroundCheckerWithOverlapArea _groundChecker;


    #endregion


    #region Public properties

    public VerticalMovementState CurrentState
    {
        get
        {
            return _currentState;
        }
    }

    #endregion


    #region Unity Lifecycle

    private void Update()
    {
        OnStateUpdate(_currentState);
    }

    #endregion


    #region State Machine

    private void OnStateEnter(VerticalMovementState state)
    {
        switch (state)
        {
            case VerticalMovementState.GROUNDED:
                DoGroundedEnter();
                break;

            case VerticalMovementState.JUMPING:
                DoJumpingEnter();
                break;

            case VerticalMovementState.EXTRA_JUMPING:
                DoExtraJumpingEnter();
                break;

            case VerticalMovementState.FALLING:
                DoFallingEnter();
                break;

            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }

    private void OnStateExit(VerticalMovementState state)
    {
        switch (state)
        {
            case VerticalMovementState.GROUNDED:
                DoGroundedExit();
                break;

            case VerticalMovementState.JUMPING:
                DoJumpingExit();
                break;

            case VerticalMovementState.EXTRA_JUMPING:
                DoExtraJumpingExit();
                break;

            case VerticalMovementState.FALLING:
                DoFallingExit();
                break;

            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }

    private void OnStateUpdate(VerticalMovementState state)
    {
        switch (state)
        {
            case VerticalMovementState.GROUNDED:
                DoGroundedUpdate();
                break;

            case VerticalMovementState.JUMPING:
                DoJumpingUpdate();
                break;

            case VerticalMovementState.EXTRA_JUMPING:
                DoExtraJumpingUpdate();
                break;

            case VerticalMovementState.FALLING:
                DoFallingUpdate();
                break;

            default:
                Debug.LogError("OnStateUpdate: Invalid state " + state.ToString());
                break;
        }
    }

    private void TransitionToState(VerticalMovementState fromState, VerticalMovementState toState)
    {
        OnStateExit(fromState);
        _currentState = toState;
        OnStateEnter(toState);
    }

    #endregion


    #region State Grounded

    private void DoGroundedEnter()
    {
        _playerMoveController.ResetExtraJumps();
        _animationController.EnterStateGrounded();
    }

    private void DoGroundedExit()
    {
        _animationController.ExitStateGrounded();
    }

    private void DoGroundedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            TransitionToState(_currentState, VerticalMovementState.JUMPING);
        }
        else if (!_groundChecker.CheckGround())
        {
            TransitionToState(_currentState, VerticalMovementState.FALLING);
        }
    }

    #endregion


    #region State Jumping

    private void DoJumpingEnter()
    {
        _playerMoveController.DoJump();
        _animationController.EnterStateJumping();
    }

    private void DoJumpingExit()
    {
        _animationController.ExitStateJumping();
    }

    private void DoJumpingUpdate()
    {
        if (Input.GetButtonDown("Fire1") && _playerMoveController.ExtraJumpsCount > 0)
        {
            TransitionToState(_currentState, VerticalMovementState.EXTRA_JUMPING);
        }
        else if (_playerMoveController.SpeedY < -0.1f)
        {
            TransitionToState(_currentState, VerticalMovementState.FALLING);
        }
    }

    #endregion


    #region State Extra Jumping

    private void DoExtraJumpingEnter()
    {
        _playerMoveController.DoExtraJump();
        _animationController.EnterStateJumping();
    }

    private void DoExtraJumpingExit()
    {
        _animationController.ExitStateJumping();
    }

    private void DoExtraJumpingUpdate()
    {
        if (Input.GetButtonDown("Fire1") && _playerMoveController.ExtraJumpsCount > 0)
        {
            TransitionToState(_currentState, VerticalMovementState.EXTRA_JUMPING);
        }
        else if (_playerMoveController.SpeedY < -0.1f)
        {
            TransitionToState(_currentState, VerticalMovementState.FALLING);
        }
    }

    #endregion


    #region State Falling

    private void DoFallingEnter()
    {
        _animationController.EnterStateFalling();
    }

    private void DoFallingExit()
    {
        _animationController.ExitStateFalling();
    }

    private void DoFallingUpdate()
    {
        if (Input.GetButtonDown("Fire1") && _playerMoveController.ExtraJumpsCount > 0)
        {
            TransitionToState(_currentState, VerticalMovementState.EXTRA_JUMPING);
        }
        else if (_groundChecker.CheckGround())
        {
            TransitionToState(_currentState, VerticalMovementState.GROUNDED);
        }
    }

    #endregion


    #region Debug

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle("button");
        style.fontSize = 28;
        style.alignment = TextAnchor.MiddleLeft;

        using (new GUILayout.AreaScope(new Rect(Screen.width - 400, 50, 350, 100)))
        {
            using (new GUILayout.VerticalScope())
            {
                GUILayout.Button($"State: {_currentState}", style, GUILayout.ExpandHeight(true));
                GUILayout.Button($"Extra jumps: {_playerMoveController.ExtraJumpsCount}", style, GUILayout.ExpandHeight(true));
            }
        }
    }

    #endregion


    #region Private

    private VerticalMovementState _currentState;

    #endregion
}
