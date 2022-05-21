using UnityEngine;

public class PlatformCollisions : MonoBehaviour
{
    #region Collisions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.SetParent(null);
    }

    #endregion
}
