using UnityEngine;

public class ExitController : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private BoolVariable _isLevelWin;

    #endregion


    #region Collisions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _animator.SetTrigger(_openDoorTriggerId);
        }
    }

    #endregion


    #region Public methods

    public void OnDoorOpenAnimationComplete()
    {
        _isLevelWin.Value = true;
    }

    #endregion


    #region Private

    private int _openDoorTriggerId = Animator.StringToHash("OpenDoor");

    #endregion
}
