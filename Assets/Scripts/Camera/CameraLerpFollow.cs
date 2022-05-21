using UnityEngine;

public class CameraLerpFollow : MonoBehaviour
{
    #region Show In Inspector

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _delta;

    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        Vector3 origin = _transform.position;

        Vector3 target = _target.position;
        target.z = origin.z;

        float t = _delta;

        _transform.position = Vector3.Lerp(origin, target, t);
    }

    #endregion


    #region Private

    private Transform _transform;

    #endregion
}
