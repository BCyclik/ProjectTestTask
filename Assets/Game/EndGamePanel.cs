using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Game;

namespace GameInterface
{
    public class EndGamePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text Title_Text;
        [SerializeField] private TMP_Text NickName_Text;
        [SerializeField] private TMP_Text CountCoins;
        [SerializeField] private Button BackLobby_Button;

        private void Awake()
        {
            BackLobby_Button.onClick.AddListener(BackLobby);
        }
        private void BackLobby()
        {
            SceneManager.LoadScene("Lobby");
        }
        public void ShowWinPanel()
        {
            Title_Text.text = "Победа!";
            Title_Text.color = Color.green;

            NickName_Text.text = PhotonNetwork.NickName;
            CountCoins.text = "Вы собрали: " + Player.LocalPlayer.Coins.ToString();

            gameObject.SetActive(true);
        }
        public void ShowLosePanel()
        {
            Title_Text.text = "Поражение!";
            Title_Text.color = Color.red;

            NickName_Text.text = PhotonNetwork.NickName;
            CountCoins.text = "Вы собрали: " + Player.LocalPlayer.Coins.ToString();

            gameObject.SetActive(true);
        }
    }
}