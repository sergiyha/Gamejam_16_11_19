using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static GameObject DraggedInstance;
        public static EventSystem CurrentSystem;
        public static Camera UiCamera;
        public static List<RaycastResult> raycastResults = new List<RaycastResult>();

        [SerializeField]
        protected Transform _contriner;

        public bool IsLocked;

        private Vector3 _startPosition;
        private Vector3 _offsetToMouse;
        private float _zDistanceToCamera;
        private int _initialSubIndex;
        private Transform _initialParent;

        private IItemHolder initialHolder;

        private void Start()
        {
            if (CurrentSystem == null)
            {
                CurrentSystem = EventSystem.current;
                UiCamera = GetComponentInParent<Canvas>().worldCamera;
            }

            initialHolder = GetComponentInParent<IItemHolder>();
            
        }

        #region Interface Implementations

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(IsLocked)
                return;

            DraggedInstance = gameObject;
            _initialParent = transform.parent;
            _startPosition = transform.position;
            _zDistanceToCamera = Mathf.Abs(_startPosition.z - UiCamera.transform.position.z);

            _offsetToMouse = _startPosition - UiCamera.ScreenToWorldPoint(
                                 new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
                             );

            _initialSubIndex = transform.GetSiblingIndex();
            transform.SetParent(_contriner);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (IsLocked)
                return;

            if (Input.touchCount > 1)
                return;

            transform.position = UiCamera.ScreenToWorldPoint(
                                     new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zDistanceToCamera)
                                 ) + _offsetToMouse;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (IsLocked)
                return;

            CurrentSystem.RaycastAll(eventData, raycastResults);

            foreach (RaycastResult result in raycastResults)
            {
                var slot = result.gameObject.GetComponent<IItemHolder>();
                if (slot != null && slot != initialHolder)
                {
                    if (slot.AddItem(this))
                    {
                        initialHolder.RemoveItem(this);
                        initialHolder = slot;
                        return;
                    }
                }
            }

            transform.SetParent(_initialParent);
            DraggedInstance = null;
            _offsetToMouse = Vector3.zero;
            transform.position = _startPosition;
            transform.SetSiblingIndex(_initialSubIndex);
        }

        #endregion
    }
}
