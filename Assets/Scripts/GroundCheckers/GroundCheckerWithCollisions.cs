using System.Collections.Generic;

using UnityEngine;

public class GroundCheckerWithCollisions : MonoBehaviour
{
    #region Show in inspector

    [SerializeField]
    private LayerMask _whatIsGround;

    #endregion


    #region Collisions

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == null)
        {
            return;
        }

        if (_whatIsGround != (_whatIsGround | (1 << collision.gameObject.layer)))
        {
            return;
        }

        if (!_inCollisionWith.Contains(collision.collider))
        {
            _inCollisionWith.Add(collision.collider);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider == null)
        {
            return;
        }

        if (_whatIsGround != (_whatIsGround | (1 << collision.gameObject.layer)))
        {
            return;
        }

        if (_inCollisionWith.Contains(collision.collider))
        {
            _inCollisionWith.Remove(collision.collider);
        }
    }

    #endregion


    #region Public methods

    public bool CheckGround()
    {
        return _inCollisionWith.Count > 0;
    }

    #endregion


    #region Private

    private List<Collider2D> _inCollisionWith = new List<Collider2D>();

    #endregion
}
