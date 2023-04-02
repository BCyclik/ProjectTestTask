using GameInterface;
using UnityEngine;
using Photon.Pun;

namespace Game
{
    public class PlayerGun : MonoBehaviour
    {
        [Header("Сила выстрела")]
        [SerializeField] private float power = 10;
        [Header("Задержка выстрела")]
        [SerializeField] private float delayShoot = 1.5f;
        [Header("Позиция выстрела")]
        [SerializeField] private Transform ShootLocation;
        [Header("Префаб пули")]
        [SerializeField] private Bullet bulletPrefab;

        private float delayTimer = 0;

        private Player playerController;
        private Player PlayerController
        {
            get
            {
                if (!playerController) playerController = GetComponent<Player>(); // Получить компонент
                return playerController;
            }
        }
        private void Update()
        {
            if (!PlayerController.PlayerNetworkController.photonView.IsMine) return; // Если не я - не продолжать

            if (delayTimer > 0) delayTimer -= Time.deltaTime;
            else
            {
                if (!ShootController.Instance.OnShoot) return; // Если сейчас не стреляем - не продолжать
                Shoot(); // Выстрелить
                delayTimer = delayShoot; // Назначить задержку перед следующим выстрелом
            }
        }
        /* Функция выстрела */
        public virtual void Shoot()
        {
            Bullet _bullet = PhotonNetwork.Instantiate(bulletPrefab.name, ShootLocation.position, ShootLocation.rotation).GetComponent<Bullet>(); // Создать пулю
            _bullet.GetComponent<Rigidbody2D>().AddForce(_bullet.transform.right * power, ForceMode2D.Impulse); // Задать направление пули
        }
    }
}