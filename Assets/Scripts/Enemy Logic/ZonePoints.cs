using System;
using System.Collections.Generic;
using UnityEngine;

public partial class OpponentController
{
    [Serializable]
    public class ZonePoints
    {
        public List<Transform> spawnPoints = new List<Transform>();
    }
}

/// Comments 
        // On enemy died Check if spawn available and if yes spawn. If all enemies are ended -- invoke OnBatle Ended
