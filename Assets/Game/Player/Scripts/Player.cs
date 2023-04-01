using UnityEngine;
using Photon.Pun;
using TMPro;

namespace Game
{
    [RequireComponent(typeof(PlayerNetworkController))]

    public class Player : MonoBehaviour, IHealth
    {
        [Tooltip("����� ������")]
        [SerializeField] public float health = 100;
        [Tooltip("�������� ������������")]
        [SerializeField] private float SpeedMove = 5f;
        [Space]
        [Tooltip("�������� ������ ������������ ������")]
        [SerializeField] private Vector3 OffSetPosCamera = new(0, 1f, -10f);
        [Space]
        [Tooltip("����� ���� ��������� ������")]
        [SerializeField] private Transform GunLocation;
        [Tooltip("����� ��� ������� � �����������")]
        [SerializeField] public TextMeshPro ReloadGun_Text;

        public static Player LocalPlayer; // ����������� ���������� ���������� ������

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
            playerNetworkController = GetComponent<PlayerNetworkController>(); // ��������� ��������� � ����������
            ReloadGun_Text.gameObject.SetActive(false); // ��������� ����� �����������

            if (!playerNetworkController.photonView.IsMine) return; // ���� �� �� - �� ����������
        }
        private void Update()
        {
            if (!playerNetworkController.photonView.IsMine) return; // ���� �� �� - �� ����������
            Camera.main.transform.position = transform.position + OffSetPosCamera; // ��������� ������� ������ ������������ ������ + ��������
        }
        /* ������� �������� ���-�� ������ */
        public float GetHealth()
        {
            return Health;
        }
        /* ������� ������� ������ */
        public void TakeDamage(float value)
        {
            Health -= value;
        }
        /* ������� ���������� ������ */
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