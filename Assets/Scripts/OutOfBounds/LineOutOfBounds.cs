using UnityEngine;

public class LineOutOfBounds : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField]
    private Transform _pointA;

    [SerializeField]
    private Transform _pointB;

    [SerializeField]
    private Transform _playerTransform;

    [SerializeField]
    private Transform _respawnPoint;

    [SerializeField]
    private Color _gizmosColor;

    #endregion


    #region Unity Lifecycle

    private void Update()
    {
        if (IsBelow(_playerTransform.position))
        {
            _playerTransform.position = _respawnPoint.position;
            if (_playerTransform.TryGetComponent(out Rigidbody2D rigidbody))
            {
                rigidbody.velocity = Vector2.zero;
            }
        }
    }

    #endregion


    #region Private methods

    private bool IsBelow(Vector2 point)
    {
        return ((_pointB.position.x - _pointA.position.x) * (point.y - _pointA.position.y) -
            (_pointB.position.y - _pointA.position.y) * (point.x - _pointA.position.x)) > 0;
    }

    #endregion


    #region Debug

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;
        Gizmos.DrawRay(_pointA.position, (_pointB.position - _pointA.position) * 1000);
        Gizmos.DrawRay(_pointB.position, (_pointA.position - _pointB.position) * 1000);
    }

    #endregion
}
