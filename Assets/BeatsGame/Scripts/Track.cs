using System;
using System.Collections.Generic;
using UnityEngine;

namespace Beats
{
    [Serializable]
    public class TrackData
    {
        [Header("Playback Settings")]
        [Tooltip ("# of beat per minute")]
        [Range (30, 360)] public int bpm = 120;
        public List<int> beats;
    }

    public class Track : MonoBehaviour
    {
        public TrackData trackData;
    }
}

