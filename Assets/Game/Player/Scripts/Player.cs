using GameInterface;
using UnityEngine;
using Photon.Pun;
using TMPro;

namespace Game
{
    [RequireComponent(typeof(PlayerNetworkController))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerGun))]
    public class Player : MonoBehaviour, IHealth
    {
        [Header("Здоровье")]
        [SerializeField] private float health = 10;
        [Header("Монеты")]
        [SerializeField] public int coins = 0;
        [Header("Смещение камеры от позиции игрока")]
        [SerializeField] private Vector3 OffSetPosCamera = new(0, 0, -10);
        [Header("SpriteRenderer для изменения цвета")]
        [SerializeField] private SpriteRenderer Body;
        //[Header("Текст перезарядки оружия")]
        //[SerializeField] public TextMeshPro ReloadGun_Text;

        public static Player LocalPlayer; // Локальный игрок

        private PlayerNetworkController playerNetworkController; // Контроллер сетевого игрока
        public PlayerNetworkController PlayerNetworkController
        {
            get
            {
                if (!playerNetworkController) playerNetworkController = GetComponent<PlayerNetworkController>(); // Получить компонент
                return playerNetworkController;
            }
        }
        private PlayerMovement playerMovement; // Котроллер движения игрока
        public PlayerMovement PlayerMovement
        {
            get
            {
                if (!playerMovement) playerMovement = GetComponent<PlayerMovement>(); // Получить компонент
                return playerMovement;
            }
        }

        private Animator animator;

        public float Health
        {
            set 
            { 
                health = value;
                if (PlayerNetworkController.photonView.IsMine) GUIController.Instance.SetHealth(health); // Изменить значения в графическом интерфейсе игрока
                if (health > 0) return;
                if (PlayerNetworkController.photonView.IsMine) SceneController_Game.Instance.EndGame(false); // Показать игроку конец игры
                else gameObject.SetActive(false); // Выключить игрока у других игроков
            }
            get { return health; }
        }
        public int Coins
        {
            set
            {
                coins = value;
                if (PlayerNetworkController.photonView.IsMine) GUIController.Instance.SetCoins(coins); // Изменить значения в графическом интерфейсе игрока
            }
            get { return coins; }
        }
        private void Awake()
        {
            //ReloadGun_Text.gameObject.SetActive(false); // Выключить

            if (!PlayerNetworkController.photonView.IsMine)
            {
                Body.color = new Color(Random.value, Random.value, Random.value); // Получить случайный цвет
                return; // Если не я - не продолжать
            }
            GUIController.Instance.SetState(Health); // Обновить статусы GUI
        }
        private void Update()
        {
            if (!PlayerNetworkController.photonView.IsMine) return; // Если не я - не продолжать
            //Camera.main.transform.position = transform.position + OffSetPosCamera; // Позиция камеры на игроке + offset
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