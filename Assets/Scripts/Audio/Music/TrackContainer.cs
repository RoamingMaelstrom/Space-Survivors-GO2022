using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackContainer : MonoBehaviour
{
    public List<AudioClip> tracks = new List<AudioClip>();
    public List<int> trackPath = new List<int>();
    
    public void GenerateTrackPath()
    {
        List<int> trackIndexesRemaining = new List<int>();
        for (int i = 0; i < tracks.Count; i++)
        {
            trackIndexesRemaining.Add(i);
        }

        for (int i = 0; i < tracks.Count; i++)
        {
            int randomTrackIndex = Random.Range(0, trackIndexesRemaining.Count);
            trackPath.Add(trackIndexesRemaining[randomTrackIndex]);
            trackIndexesRemaining.RemoveAt(randomTrackIndex);
        }
    }
}
