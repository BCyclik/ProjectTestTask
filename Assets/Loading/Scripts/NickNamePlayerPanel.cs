using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using TMPro;
namespace Loading
{
    public class NickNamePlayerPanel : MonoBehaviour
    {
        [SerializeField] private string NickName = "Player";
        [Space]
        [Header("Кнопка принять nickname")]
        [SerializeField] private Button Accept_Button;
        [Header("Поле для ввода nickname")]
        [SerializeField] private TMP_InputField NickName_InputField;

        public void Init()
        {
            Debug.Log("[FindRoomController] Init");
            NickName_InputField.onValueChanged.AddListener(ChangeNickName);
            Accept_Button.onClick.AddListener(AcceptNickName);
            gameObject.SetActive(true);
        }
        /* Функция изменения текста в поле ввода nickname */
        private void ChangeNickName(string nickName)
        {
            NickName = nickName;
        }
        private void AcceptNickName()
        {
            PlayerPrefs.SetString("NickName", NickName); // Сохрнить nickname игрока
            PhotonNetwork.NickName = NickName; // Записать nickname игрока для сети
            SceneManager.LoadScene("Lobby"); // Перейти на сцену лобби
        }
    }
}