using GameInterface;
using UnityEngine;
namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Скорость движения")]
        [SerializeField] private float SpeedMove = 2f;
        private Player playerController;
        private Player PlayerController
        {
            get
            {
                if (!playerController) playerController = GetComponent<Player>(); // Получить компонент
                return playerController;
            }
        }
        private Rigidbody2D body;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            if (!PlayerController.PlayerNetworkController.photonView.IsMine) return; // Если не я - не продолжать

            float x = JoystickController.Instance.Horizontal();
            float y = JoystickController.Instance.Vertical();

            body.velocity = new(x * SpeedMove, y * SpeedMove); // Двигать игрока в направлении

            if (x == 0f && y == 0f) return;
            //float rotateZ = Mathf.Atan2(diference.y, Mathf.Abs(diference.x)) * Mathf.Rad2Deg;
            float angle = Mathf.Atan2(y - transform.forward.y, x - transform.forward.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            //Debug.Log(angle);
        }
    }
}