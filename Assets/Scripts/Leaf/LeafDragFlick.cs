using UnityEngine;
using UnityEngine.EventSystems;

namespace ClearLeaves
{
    public class LeafDragFlick : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
    {
        private RectTransform leafRect;
        private RectTransform panelRect;
        private Vector2 pointerOffset;
        private LeafManager leafManager;

        void Awake()
        {
            leafRect = GetComponent<RectTransform>();
            panelRect = transform.parent.GetComponent<RectTransform>();
            leafManager = transform.parent.parent.GetComponent<LeafManager>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            leafRect.localScale = Vector3.one * 2.4f;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRect, eventData.position, eventData.pressEventCamera, out Vector2 localPointerPos);
            pointerOffset = leafRect.anchoredPosition - localPointerPos;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRect, eventData.position, eventData.pressEventCamera, out Vector2 localPointerPos))
            {
                leafRect.anchoredPosition = localPointerPos + pointerOffset;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            leafRect.localScale = Vector3.one * 2;

            if (!IsLeafInsidePanel())
            {
                Destroy(gameObject);
                leafManager.LeafCleared();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            leafRect.localScale = Vector3.one *2;
        }

        private bool IsLeafInsidePanel()
        {
            Vector3[] panelCorners = new Vector3[4];
            panelRect.GetWorldCorners(panelCorners);

            Vector3[] leafCorners = new Vector3[4];
            leafRect.GetWorldCorners(leafCorners);

            foreach (Vector3 corner in leafCorners)
            {
                if (corner.x >= panelCorners[0].x && corner.x <= panelCorners[2].x && corner.y >= panelCorners[0].y && corner.y <= panelCorners[2].y)
                {
                    return true;
                }
            }

            return false;
        }
    }
}