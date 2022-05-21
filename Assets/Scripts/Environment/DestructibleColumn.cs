using UnityEngine;

public class DestructibleColumn : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Sprite[] _sprites;

    #endregion


    #region Collisions

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.CompareTag("Player"))
    //    {
    //        ColumnHit();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("PlayerWeapon"))
        {
            ColumnHit();
        }
    }

    #endregion


    #region Unity lifecycle

    private void Awake()
    {
        if (_sprites.Length == 0)
        {
            Debug.LogError("T'as oublié de mettre des sprites dans le tableau !");
        }
        else
        {
            _spriteRenderer.sprite = _sprites[0];
        }
    }

    #endregion


    #region Private methods

    private void ColumnHit()
    {
        _hitCount++;
        if (_hitCount >= _sprites.Length)
        {
            Destroy(gameObject);
        }
        else
        {
            _spriteRenderer.sprite = _sprites[_hitCount];
        }
    }

    #endregion


    #region Private

    private int _hitCount;

    #endregion
}
