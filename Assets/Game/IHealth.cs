using UnityEngine;

public interface IHealth
{
    /* ������� �������� ���-�� ������ */
    public float GetHealth()
    {
        return -1;
    }
    /* ������� ���������� ������ */
    public void TakeDamage(float value)
    {
    }
    /* ������� ���������� ������ */
    public void AddHealth(float value)
    {
    }
}