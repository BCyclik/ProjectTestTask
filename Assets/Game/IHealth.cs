using UnityEngine;

public interface IHealth
{
    /* Функция получить кол-во жизней */
    public float GetHealth()
    {
        return -1;
    }
    /* Функция добавления жизней */
    public void TakeDamage(float value)
    {
    }
    /* Функция добавления жизней */
    public void AddHealth(float value)
    {
    }
}