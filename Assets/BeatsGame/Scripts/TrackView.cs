using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Beats
{
    [RequireComponent (typeof(VerticalLayoutGroup))]
    [RequireComponent (typeof(ContentSizeFitter))]
    [RequireComponent (typeof(RectTransform))]
    public class TrackView : MonoBehaviour
    {
        public enum Trigger
        {
            Missed,
            Right,
            Wrong
        }
        //[SerializeField] private Track _track;

        [SerializeField] private RectTransform _up;
        [SerializeField] private RectTransform _down;
        [SerializeField] private RectTransform _left;
        [SerializeField] private RectTransform _right;

        [SerializeField] private RectTransform _empty;

        private float _trackViewSpeed;
        private RectTransform _rTransform;
        private Vector2 _position;
        private float _beatViewSize;
        private float _spacing;
        private List<Image> _beatsView;

        public float Position
        {
            get { return _position.y; }
            set 
            {
                _position.y = value;
                _rTransform.anchoredPosition = _position;
            }
        }

        private void Start()
        {
            Init(GameplayController.Instance.Track);
        }

        private void Update()
        {
            _trackViewSpeed = GameplayController.Instance.SecondPerBeat;
            Position -= (_beatViewSize + _spacing) * Time.deltaTime * _trackViewSpeed;
        }

        public void Init(Track track)
        {
            _rTransform = (RectTransform)transform;

            _position = _rTransform.anchoredPosition;

            _beatViewSize = _empty.rect.height;
            _spacing = GetComponent<VerticalLayoutGroup>().spacing;

            _beatsView = new ();

            foreach (int b in track.beats)
            {
                GameObject gameObject;
                switch (b)
                {
                    case 0:
                        gameObject = _up.gameObject;
                        break;
                    case 1:
                        gameObject = _down.gameObject;
                        break;
                    case 2:
                        gameObject = _left.gameObject;
                        break;
                    case 3:
                        gameObject = _right.gameObject;
                        break;
                    default:
                        gameObject = _empty.gameObject;
                        break;
                }
                Image view = Instantiate(gameObject, transform).GetComponent<Image>();
                view.transform.SetAsFirstSibling();

                _beatsView.Add(view);
            }
        }

        public void TriggerBeatView(int index, Trigger trigger)
        {
            switch(trigger)
            {
                case Trigger.Missed:
                    _beatsView[index].color = Color.gray;
                    break;
                case Trigger.Right:
                    _beatsView[index].color = Color.green;
                    break;
                case Trigger.Wrong:
                    _beatsView[index].color = Color.cyan;
                    break;
            }
        }
    }

}
