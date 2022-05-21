using UnityEngine;

public class AlePickup : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField]
    private IntVariable _aleCount;

    #endregion


    #region Collisions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ale"))
        {
            _aleCount.Value++;
            Destroy(collision.gameObject);
        }
    }

    #endregion
}
