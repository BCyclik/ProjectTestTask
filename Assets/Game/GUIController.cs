using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace GameInterface
{
    public class GUIController : MonoBehaviour
    {
        [SerializeField] private Slider Health_Slider;
        [SerializeField] private TMP_Text CountCoins_Text;
        [SerializeField] public EndGamePanel endGamePanel;

        private static GUIController instance;
        public static GUIController Instance
        {
            get
            {
                if (instance == null) instance = FindObjectOfType<GUIController>();
                return instance;
            }
        }
        private void Awake()
        {
            endGamePanel.gameObject.SetActive(false); // Выключить
        }
        public void SetCoins(int value)
        {
            CountCoins_Text.text = value.ToString();
        }
        public void SetHealth(float value)
        {
            Health_Slider.value = value;
        }
        public void SetState(float maxHealth)
        {
            Health_Slider.maxValue = maxHealth; // Получить максимальное кол-во здоровья
            Health_Slider.value = maxHealth; // Назначить максимальное кол-во здоровья
            CountCoins_Text.text = "0"; // Установить текст кол-ва монет
        }
    }
}