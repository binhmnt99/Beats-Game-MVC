using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Beats
{
    public class GameplayController : Singleton<GameplayController>
    {
        [Header("Input")]
        [SerializeField] private KeyCode _up = KeyCode.UpArrow;
        [SerializeField] private KeyCode _down = KeyCode.DownArrow;
        [SerializeField] private KeyCode _left = KeyCode.LeftArrow;
        [SerializeField] private KeyCode _right = KeyCode.RightArrow;

        [Header("Track")]
        [Tooltip("Beats track to play")]
        [SerializeField] private Track _track;
        
        /// <summary>
        /// The Current Track.
        /// </summary>
        public Track Track { get { return _track; } }

        public float SecondPerBeat { get; private set; }
        public float BeatsPerSecond { get; private set; }

        private bool _played;
        private bool _completed;

        private TrackView _trackView;

        private WaitForSeconds _waitAndStop;

        #region MonoBehaviour Menthods

        protected override void Awake()
        {
            base.Awake();
            SecondPerBeat = Track.bpm / 60f;
            BeatsPerSecond = 60f / Track.bpm;
            _waitAndStop = new (BeatsPerSecond*2f);
            _trackView = FindObjectOfType<TrackView>();
            if (!_trackView)
            {
                Debug.Log("No TrackView found in current scene");
            }
        }
        private void Start()
        {
            InvokeRepeating("NextBeat", 0f, BeatsPerSecond);
        }
        private void Update()
        {
            if (_played || _completed)
            {
                return;
            }
            if (Input.GetKeyDown(_up))
            {
                PlayBeat(0);
            }
            if (Input.GetKeyDown(_down))
            {
                PlayBeat(1);
            }
            if (Input.GetKeyDown(_left))
            {
                PlayBeat(2);
            }
            if (Input.GetKeyDown(_right))
            {
                PlayBeat(3);
            }
        }
        #endregion

        #region Gameplay
        private int _current;
        public int Current
        {
            get { return _current; }
            set 
            { 
                _current = value;
                if (_current == Track.beats.Count)
                {
                    CancelInvoke("NextBeat");
                    _completed = true;
                    StartCoroutine(WaitAndStop());
                }
            }
        }
        private void PlayBeat(int input)
        {
            //Debug.Log(input);
            _played = true;
            if (Track.beats[Current] == -1)
            {
                //Debug.Log(string.Format("{0} played untimely", input));
            }
            else if (Track.beats[Current] == input)
            {
                //Debug.Log(string.Format("{0} played right", input));
                _trackView.TriggerBeatView(Current, TrackView.Trigger.Right);
            }
            else
            {
                //Debug.Log(string.Format("{0} played, {1} expected",input, Track.beats[Current]));
                _trackView.TriggerBeatView(Current, TrackView.Trigger.Wrong);
            }
        }
        private void NextBeat() 
        {
            //Debug.Log("Tick");
            if (!_played && Track.beats[Current] != -1)
            {
                //Debug.Log(string.Format("{0} missed", Track.beats[Current]));
                _trackView.TriggerBeatView(Current, TrackView.Trigger.Missed);
            }
            _played = false;
            Current++;
        }
        private IEnumerator WaitAndStop()
        {
            yield return _waitAndStop;
            enabled = false;
        }
        #endregion


    }
}