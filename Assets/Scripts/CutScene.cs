using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    [SerializeField] private List<GameObject> slides = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(PlayCutScene());
    }

    private IEnumerator PlayCutScene()
    {
        foreach (GameObject slide in slides)
        {
            yield return new WaitForSeconds(2f);
            slide.SetActive(true);
        }
    }
}
