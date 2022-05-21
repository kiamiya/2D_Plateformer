using UnityEngine;

public class GroundCheckerWithRaycast : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField]
    private Transform _checkOrigin;

    [SerializeField]
    private float _checkDistance;

    [SerializeField]
    private LayerMask _whatIsGround;

    [SerializeField]
    private Color _gizmosColor;

    #endregion


    #region Main methods

    public bool CheckGround()
    {
        return Physics2D.RaycastNonAlloc(_checkOrigin.position, Vector2.down, _hitBuffer, _checkDistance, _whatIsGround) > 0;
    }

    #endregion


    #region Debug

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;
        Gizmos.DrawRay(_checkOrigin.position, Vector2.down * _checkDistance);
    }

    #endregion


    #region Private

    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[1];

    #endregion
}
