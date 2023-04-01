
namespace Game
{
    public interface IHealth
    {
        /* Функция получения кол-ва здоровья */
        public float GetHealth()
        {
            return -1;
        }
        /* Функция получения урона */
        public void TakeDamage(float value)
        {
        }
        /* Функция отнятия здоровья */
        public void AddHealth(float value)
        {
        }
    }
}