using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField]
    private AnimationControllerWithStateMachine _animator;

    #endregion


    #region Public properties

    #endregion


    #region Unity Lifecycle

    private void Update()
    {
        if (Input.GetButtonDown("Fire2") && !_isWeaponThrown)
        {
            ThrowWeapon();
        }
    }

    #endregion


    #region Public methods

    public void ThrowWeapon()
    {
        _animator.ThrowWeaponAnimation();
        _isWeaponThrown = true;
    }

    public void CatchWeapon()
    {
        _animator.CatchWeaponAnimation();
        _isWeaponThrown = false;
    }

    #endregion


    #region Private

    private bool _isWeaponThrown;

    #endregion
}
