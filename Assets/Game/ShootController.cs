using UnityEngine.EventSystems;
using UnityEngine;

namespace GameInterface
{
    public class ShootController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [Tooltip("Нажат ли сейчас?")]
        public bool OnShoot = false;

        private static ShootController instance;
        public static ShootController Instance
        {
            get
            {
                if (instance == null) instance = FindObjectOfType<ShootController>();
                return instance;
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) OnShoot = true;
            if (Input.GetKeyUp(KeyCode.Space)) OnShoot = false;
        }
        public virtual void OnPointerDown(PointerEventData ped)
        {
            OnShoot = true; // Начать стрелять
        }

        public virtual void OnPointerUp(PointerEventData ped)
        {
            OnShoot = false; // Перестать стрелять
        }
    }
}