using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
namespace GameInterface
{
    public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [Header("Фон для джостика")]
        [SerializeField] private Image joystickBG;
        [Header("Стик джостика")]
        [SerializeField] private Image stick;

        [Tooltip("Нажат ли джостик сейчас?")]
        public static bool OnJoystick = false;
        //[Tooltip("Нажатие на джостик")]
        //public static Touch JoysticTouch;
        [Tooltip("Векток направления джостика")]
        private Vector2 inputVector;

        private static JoystickController instance;
        public static JoystickController Instance
        {
            get
            {
                if (instance == null) instance = FindObjectOfType<JoystickController>();
                return instance;
            }
        }
        public virtual void OnPointerDown(PointerEventData ped)
        {
            //if (Input.touchCount > 0) JoysticTouch = Input.GetTouch(0);
            OnJoystick = true;
            OnDrag(ped);
        }

        public virtual void OnPointerUp(PointerEventData ped)
        {
            stick.rectTransform.anchoredPosition = Vector2.zero;
            inputVector = Vector2.zero;
            OnJoystick = false;
        }

        public virtual void OnDrag(PointerEventData ped)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform, ped.position, ped.pressEventCamera, out Vector2 pos))
            {
                pos.x /= joystickBG.rectTransform.sizeDelta.x;
                pos.y /= joystickBG.rectTransform.sizeDelta.x;

                inputVector = new Vector2(pos.x * 2, pos.y * 2);
                inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
                stick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBG.rectTransform.sizeDelta.x / 2), inputVector.y * (joystickBG.rectTransform.sizeDelta.y / 2));
            }
        }

        public float Horizontal()
        {
            if (inputVector.x != 0)
                return inputVector.x;
            else
                return Input.GetAxis("Horizontal");
        }

        public float Vertical()
        {
            if (inputVector.y != 0)
                return inputVector.y;
            else
                return Input.GetAxis("Vertical");
        }
    }
}