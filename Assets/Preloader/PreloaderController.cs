using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PreloaderController : MonoBehaviour
{
    private Animator animator; // Переменная компонента animator
    public Animator Animator
    {
        get
        {
            if (!animator) animator = GetComponent<Animator>(); // Получить компонент animator
            return animator;
        }
    }
}