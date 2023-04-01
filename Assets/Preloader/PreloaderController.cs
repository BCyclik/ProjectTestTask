using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PreloaderController : MonoBehaviour
{
    private Animator animator; // Переменная компонента
    public Animator Animator // Переопределение для получения компонента animator
    {
        get
        {
            if (!animator) animator = GetComponent<Animator>(); // Если переменная пустая, назначить animator
            return animator; // Вернуть компонент animator
        }
    }
}