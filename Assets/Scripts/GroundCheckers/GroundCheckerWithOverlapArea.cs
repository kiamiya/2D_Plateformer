using UnityEngine;

public class GroundCheckerWithOverlapArea : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField]
    private Transform _topLeft;

    [SerializeField]
    private Transform _bottomRight;

    [SerializeField]
    private LayerMask _whatIsGround;

    [SerializeField]
    private Color _gizmosColor;

    #endregion


    #region Main methods

    public bool CheckGround()
    {
        return Physics2D.OverlapAreaNonAlloc(_topLeft.position, _bottomRight.position, _buffer, _whatIsGround) > 0;

        // Equivalent à
        //int hitCount = Physics2D.OverlapAreaNonAlloc(_topLeft.position, _bottomRight.position, _buffer, _whatIsGround);
        //bool isGroundHit = hitCount > 0;
        //return isGroundHit;

        // Equivalent à (mais avec une allocation inutile)
        //return Physics2D.OverlapArea(_topLeft.position, _bottomRight.position, _whatIsGround) != null;
    }

    #endregion


    #region Debug

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;

        Vector2 topLeft = _topLeft.position;
        Vector2 topRight = new Vector2(_bottomRight.position.x, _topLeft.position.y);
        Vector2 bottomRight = _bottomRight.position;
        Vector2 bottomLeft = new Vector2(_topLeft.position.x, _bottomRight.position.y);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }

    #endregion


    #region Private

    private Collider2D[] _buffer = new Collider2D[1];

    #endregion
}
