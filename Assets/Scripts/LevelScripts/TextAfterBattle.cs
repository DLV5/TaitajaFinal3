using TMPro;
using UnityEngine;

public class TextAfterBattle : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        LevelManager.Instance.MoveToTheNextBattle += ShowText;
        CameraConfinerController.OnPlayerReachedNewBattle += HideText;
    }
    
    private void OnDisable()
    {
        LevelManager.Instance.MoveToTheNextBattle -= ShowText;
        CameraConfinerController.OnPlayerReachedNewBattle -= HideText;
    }

    private void ShowText() => _text.enabled = true;
    private void HideText() => _text.enabled = false;
}
