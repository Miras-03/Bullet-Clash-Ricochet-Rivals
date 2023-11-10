using System.Collections.Generic;

namespace HealthSpace
{
    public sealed class Health
    {
        private HashSet<IDieable> dieableObservers = new HashSet<IDieable>();
        private HashSet<IHealthObserver> healthObservers = new HashSet<IHealthObserver>();

        private int health;
        private const int healthOverValue = 0;

        public int TakeDamage
        {
            get => health;
            set
            {
                if (value >= healthOverValue)
                {
                    health = value;
                    NotifyObserversAboutChange();
                }
                else
                    NotifyObserversAboutDeath();
            }
        }

        public void AddDieableObserver(IDieable observer) => dieableObservers.Add(observer);
        public void AddHealthObserver(IHealthObserver observer) => healthObservers.Add(observer);

        public void RemoveDieableObservers() => dieableObservers.Clear();
        public void RemoveHealthObservers() => healthObservers.Clear();

        public void NotifyObserversAboutDeath()
        {
            foreach (IDieable observer in dieableObservers)
                observer.PerformMurder();
        }

        public void NotifyObserversAboutChange()
        {
            foreach (IHealthObserver observer in healthObservers)
                observer.OnHealthChanged(health);
        }
    }
}