using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleInputNamespace
{
    public class AxisInputUIArrowsSol : MonoBehaviour, ISimpleInputDraggable
    {
        // Using xAxis for horizontal movement
        public SimpleInput.AxisInput xAxis = new SimpleInput.AxisInput("Horizontal");
        public float valueMultiplier = 1f;

#pragma warning disable 0649
        [Tooltip("Radius of the deadzone at the center of the arrows that will yield no input")]
        [SerializeField]
        private float deadzoneRadius;
        private float deadzoneRadiusSqr;
#pragma warning restore 0649

        public RectTransform rectTransform;

        private Vector2 m_value = Vector2.zero;
        public Vector2 Value { get { return m_value; } }

        private void Awake()
        {
            rectTransform = (RectTransform)transform;
            gameObject.AddComponent<SimpleInputDragListener>().Listener = this;

            deadzoneRadiusSqr = deadzoneRadius * deadzoneRadius;
        }

        private void OnEnable()
        {
            xAxis.StartTracking();
        }

        private void OnDisable()
        {
            xAxis.StopTracking();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            deadzoneRadiusSqr = deadzoneRadius * deadzoneRadius;
        }
#endif

        public void OnPointerDown(PointerEventData eventData)
        {
            CalculateInput(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pointerPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out pointerPos);

            // Check if pointer is within the collider
            if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, eventData.position, eventData.pressEventCamera))
            {
                // Calculate movement value based on collider bounds
                Vector2 min = rectTransform.rect.min;
                Vector2 max = rectTransform.rect.max;
                m_value.Set((pointerPos.x - min.x) / (max.x - min.x), (pointerPos.y - min.y) / (max.y - min.y));
            }
            else
            {
                m_value.Set(0f, 0f);
            }

            xAxis.value = m_value.x;

            CalculateInput(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_value = Vector2.zero;

            xAxis.value = 0f;
        }

        private void CalculateInput(PointerEventData eventData)
        {
            Vector2 pointerPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out pointerPos);

            // Check for deadzone
            if (pointerPos.x * pointerPos.x <= deadzoneRadiusSqr)
            {
                m_value.Set(0f, 0f);
            }
            else
            {
                // Set value for leftward movement
                m_value.Set(pointerPos.x <= 0f ? -valueMultiplier : 0f, 0f);
            }

            xAxis.value = m_value.x;
        }
    }
}
