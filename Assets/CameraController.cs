using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   [SerializeField] private List<Transform> _points = new List<Transform>();  //Points on which the camera will stop
    private Queue<Transform> _pointsQueue = new Queue<Transform>();
    private void Awake()
    {
        foreach(Transform point in _points)
        {
            _pointsQueue.Enqueue(point);    
        }
    }
    private void MoveCameraForward()
    {

    }
}
