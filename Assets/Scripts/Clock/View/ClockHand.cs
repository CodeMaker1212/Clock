using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Clock
{
    [RequireComponent(typeof(Image), typeof(Outline))]

    public class ClockHand : MonoBehaviour, IDragHandler
    {
        private Image _image;
        private Outline _highlight;

        public bool IsCanBeDrag
        {
            set
            {
                _image.raycastTarget = value;
                _highlight.enabled = value;
            }
        }

        public float Angle => transform.eulerAngles.z;

        public event Action OnDragged;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _highlight = GetComponent<Outline>();
        }

        public void Rotate(Quaternion rotation) => transform.rotation = rotation;
        

        public void OnDrag(PointerEventData eventData)
        {
            var direction = new Vector3(eventData.position.x, eventData.position.y) - transform.position;
            var angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
            Rotate(Quaternion.Euler(0, 0, angle));

            OnDragged?.Invoke();
        }
    }
}