using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Beats
{
    [CustomEditor(typeof(Track))]
    public class TrackEditor : Editor
    {
        private Track track;
        private Vector2 position;
        private bool _displayBeatsData;

        private void OnEnable()
        {
            track = (Track)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (track.beats != null && track.beats.Count == 0)
            {
                EditorGUILayout.HelpBox("Empty Track", MessageType.Info);
                if (GUILayout.Button("Generate Random Track", EditorStyles.miniButton)) track.Randomize();
            }
            else
            {
                if (GUILayout.Button("Update Random Track", EditorStyles.miniButton)) track.Randomize();
                _displayBeatsData = EditorGUILayout.Foldout(_displayBeatsData, "Display Beats");
                if (_displayBeatsData)
                {
                    position = EditorGUILayout.BeginScrollView(position);

                    for (int i = 0; i < track.beats.Count; i++)
                    {
                        track.beats[i] = EditorGUILayout.IntSlider(track.beats[i], -1, Track.inputs - 1);
                    }

                    EditorGUILayout.EndScrollView();
                }
            }

            EditorUtility.SetDirty(track);
        }
    }
}
