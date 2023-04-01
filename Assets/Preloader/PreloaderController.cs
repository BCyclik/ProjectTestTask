using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PreloaderController : MonoBehaviour
{
    private Animator animator; // ���������� ����������
    public Animator Animator // ��������������� ��� ��������� ���������� animator
    {
        get
        {
            if (!animator) animator = GetComponent<Animator>(); // ���� ���������� ������, ��������� animator
            return animator; // ������� ��������� animator
        }
    }
}