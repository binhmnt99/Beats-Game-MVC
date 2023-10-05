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
        [SerializeField] private Track _track;

        [SerializeField] private RectTransform _up;
        [SerializeField] private RectTransform _down;
        [SerializeField] private RectTransform _left;
        [SerializeField] private RectTransform _right;

        [SerializeField] private RectTransform _empty;

        private RectTransform _rTransform;

        private void Start()
        {
            Init(_track);
        }

        public void Init(Track track)
        {
            _rTransform = (RectTransform)transform;

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
                Transform view = Instantiate(gameObject, transform).transform;
                view.SetAsFirstSibling();
            }
        }
    }

}
