using System.Collections;
using UnityEngine;

public class PlayerBoundaries : MonoBehaviour
{
    public Camera MainCamera; //be sure to assign this in the inspector to your main camera
    private Vector2 screenBounds;

    private bool _shouldBeEnabled = true;

    private void OnEnable()
    {
        LevelManager.Instance.MoveToTheNextBattle += OnBattleChange;
        CameraConfinerController.OnPlayerReachedNewBattle += () => { StartCoroutine(SetNewScreenBoundaries()); };
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SetNewScreenBoundaries());
    }

    private void OnDisable()
    {
        LevelManager.Instance.MoveToTheNextBattle -= OnBattleChange;
        CameraConfinerController.OnPlayerReachedNewBattle -= () => { StartCoroutine(SetNewScreenBoundaries()); };
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!_shouldBeEnabled) return;

        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x, screenBounds.x > 0 ? screenBounds.x * 2: screenBounds.x * -1);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y, screenBounds.y * -1);
        transform.position = viewPos;
    }

    private void OnBattleChange() => _shouldBeEnabled = false;

    private IEnumerator SetNewScreenBoundaries()
    {
        yield return new WaitForSeconds(.1f);
        _shouldBeEnabled = true;
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
    }
}
