using UnityEngine;
using Photon.Pun;
using TMPro;

namespace Game
{
    [RequireComponent(typeof(PlayerNetworkController))]

    public class Player : MonoBehaviour, IHealth
    {
        [Tooltip("Здоровье")]
        [SerializeField] public float health = 100;
        [Tooltip("Скорость движения")]
        [SerializeField] private float SpeedMove = 5f;
        [Space]
        [Tooltip("Смещение камеры от позиции игрока")]
        [SerializeField] private Vector3 OffSetPosCamera = new(0, 0, 0);
        [Space]
        [Tooltip("Позиция выстрела")]
        [SerializeField] private Transform ShootLocation;
        [Tooltip("Текст перезарядки оружия")]
        [SerializeField] public TextMeshPro ReloadGun_Text;

        public static Player LocalPlayer; // Локальный игрок

        private PlayerNetworkController playerNetworkController; // Переменная сетевого контроллера игрока
        public PlayerNetworkController PlayerNetworkController
        {
            get
            {
                if (!playerNetworkController) playerNetworkController = GetComponent<PlayerNetworkController>(); // Получить компонент
                return playerNetworkController;
            }
        }

        private Animator animator;

        public float Health
        {
            set 
            { 
                health = value;
                if (health > 0) return;
            }
            get { return health; }
        }
        private void Awake()
        {
            ReloadGun_Text.gameObject.SetActive(false); // Выключить

            if (!playerNetworkController.photonView.IsMine) return; // Если не я - не продолжать
        }
        private void Update()
        {
            if (!playerNetworkController.photonView.IsMine) return; // Если не я - не продолжать
            Camera.main.transform.position = transform.position + OffSetPosCamera; // Позиция камеры на игроке + offset
        }
        /* Получить здоровье */
        public float GetHealth()
        {
            return Health;
        }
        /* Получить урон */
        public void TakeDamage(float value)
        {
            Health -= value;
        }
        /* Добавить здоровья */
        public void AddHealth(float value)
        {
            Health += value;
        }
        private void OnDestroy()
        {
            playerNetworkController = null;
        }
    }
}