using UnityEngine;
using Photon.Pun;
using TMPro;

namespace Game
{
    [RequireComponent(typeof(PlayerNetworkController))]

    public class Player : MonoBehaviour, IHealth
    {
        [Tooltip("Жизни игрока")]
        [SerializeField] public float health = 100;
        [Tooltip("Скорость передвижения")]
        [SerializeField] private float SpeedMove = 5f;
        [Space]
        [Tooltip("Смещение камеры относительно игрока")]
        [SerializeField] private Vector3 OffSetPosCamera = new(0, 1f, -10f);
        [Space]
        [Tooltip("Место куда крепиться оружие")]
        [SerializeField] private Transform GunLocation;
        [Tooltip("Текст над игроком о перезарядке")]
        [SerializeField] public TextMeshPro ReloadGun_Text;

        public static Player LocalPlayer; // Статическая переменная локального игрока

        [HideInInspector] public PlayerNetworkController playerNetworkController;
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
            playerNetworkController = GetComponent<PlayerNetworkController>(); // Назначить компонент в переменную
            ReloadGun_Text.gameObject.SetActive(false); // Выключить текст перезарядки

            if (!playerNetworkController.photonView.IsMine) return; // Если не мы - не продолжать
        }
        private void Update()
        {
            if (!playerNetworkController.photonView.IsMine) return; // Если не мы - не продолжать
            Camera.main.transform.position = transform.position + OffSetPosCamera; // Назначить позицию камере относительно игрока + смещение
        }
        /* Функция получить кол-во жизней */
        public float GetHealth()
        {
            return Health;
        }
        /* Функция отнятия жизней */
        public void TakeDamage(float value)
        {
            Health -= value;
        }
        /* Функция добавления жизней */
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