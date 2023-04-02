using UnityEngine;
using Photon.Pun;

namespace Game
{
    [RequireComponent(typeof(PhotonRigidbody2DView))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PhotonView))]
    public class Bullet : MonoBehaviourPunCallbacks
    {
        [Header("Наносимый урон")]
        [SerializeField] private float Damage = 2f;
        [Header("SpriteRenderer для изменения цвета")]
        [SerializeField] private SpriteRenderer body;

        private void Awake()
        {
            if (photonView.IsMine) return; // Если это моя пуля - не продолжать
            body.color = Color.red; // Назначить красный цвет патрону
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PhotonView>() && collision.GetComponent<PhotonView>().Owner == photonView.Owner) return; // Если владелец объекта - не продолжать
            ApplyDamage(collision.gameObject); // Попытаться нанести урон объекту
            if (!photonView.IsMine) return; // Если не я - не продолжать
            PhotonNetwork.Destroy(gameObject); // Уничтожить объект
        }
        /* Функция нанесения урона */
        private void ApplyDamage(GameObject other)
        {
            if (other.GetComponent<IHealth>() == null) return;
            IHealth health = other.GetComponent<IHealth>(); // Получить интерфейс жизней объекта
            health.TakeDamage(Damage); // Нанести урон объекту
        }
    }
}