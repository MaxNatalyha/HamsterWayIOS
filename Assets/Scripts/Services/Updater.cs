using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Services
{
    public class Updater : MonoBehaviour, IUpdater
    {
        private List<IUpdatable> _updatables = new List<IUpdatable>();

        private IPlayerProgressService _progressService;
        
        [Inject]
        public void Construct(IPlayerProgressService progressService)
        {
            _progressService = progressService;
        }

        public void Add(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }

        public void Remove(IUpdatable updatable)
        {
            _updatables.Remove(updatable);
        }

        private void Update()
        {
            if (_updatables.Count == 0) return;
            
            foreach (var updatable in _updatables)
                updatable.Update();

            CheatCurrency();
        }

        private void CheatCurrency()
        {
            if (Input.GetKeyDown(KeyCode.C))
                _progressService.AddCurrency(1000, CurrencyType.Coins);

            if (Input.GetKeyDown(KeyCode.M))
                _progressService.AddCurrency(1000, CurrencyType.Money);
        }
    }
}