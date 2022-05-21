using TMPro;

using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Show In Inspector

    [Header("Ale counter")]

    [SerializeField]
    private IntVariable _aleCount;

    [SerializeField]
    private IntVariable _totalAleCount;

    [SerializeField]
    private TMP_Text _aleCountText;

    [SerializeField]
    private TMP_Text _aleCollectedText;

    [SerializeField]
    private TMP_Text _totalAleCountText;

    [Header("Level win")]

    [SerializeField]
    private BoolVariable _isLevelWin;

    [SerializeField]
    private Canvas _winCanvas;

    #endregion


    #region Unity Lifecycle

    private void Start()
    {
        _totalAleCountText.text = _totalAleCount.Value.ToString("D3");
    }

    private void Update()
    {
        _aleCountText.text = _aleCount.Value.ToString("D3");
        _aleCollectedText.text = _aleCount.Value.ToString("D3");

        _winCanvas.gameObject.SetActive(_isLevelWin.Value);
    }

    #endregion
}
