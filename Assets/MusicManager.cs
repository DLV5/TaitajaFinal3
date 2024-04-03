using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private string _musicName;
    private void Start()
    {
        try
        {
            AudioManager.Instance.PlayMusic(_musicName);
        }
        catch
        {
            Debug.LogWarning("Unable to play music with name " + _musicName);     
        }
    }
}
