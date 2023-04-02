using UnityEngine;
using Photon.Pun;

namespace Game
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(PhotonView))]
    public class CoinController : MonoBehaviourPunCallbacks
    {
        private void Awake()
        {
            transform.SetParent(SceneController_Game.Instance.parentCoins); // Назначить родителя для монеты
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.GetComponent<Player>()) return; // Проверить есть ли компонент
            Player player = collision.GetComponent<Player>(); // Получить компонент игрока
            player.Coins++; // Добавить монету игроку

            if (!photonView.IsMine) return; // Если не masterserver - не продолжать
            PhotonNetwork.Destroy(gameObject);
        }
    }
}