using System.Collections.Generic;
using Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Shop
{
    public class ShopSectionText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _sectionText;

        private ShopSectionsEnum _currentSection;
        private ILocalizationService _localizationService;
        private IShopController _shopController;

        private readonly Dictionary<ShopSectionsEnum, string> _localizationSectionsDictionary = new Dictionary<ShopSectionsEnum, string>()
        {
            { ShopSectionsEnum.Pets, "Pet"},
            { ShopSectionsEnum.Houses, "House"},
            { ShopSectionsEnum.Pipes, "Pipes"},
            { ShopSectionsEnum.Bowls, "Bowl"},
            { ShopSectionsEnum.Cards, "Card"},
            { ShopSectionsEnum.Food, "Food"},
            { ShopSectionsEnum.Finance, "Finance"}
        };

        [Inject]
        public void Construct(ILocalizationService localizationService, IShopController shopController)
        {
            _localizationService = localizationService;
            _shopController = shopController;
        }

        private void OnEnable()
        {
            OnSectionChange(_currentSection);
            _shopController.OnSectionChangeEvent += OnSectionChange;
        }

        private void OnDisable() => _shopController.OnSectionChangeEvent -= OnSectionChange;

        private void OnSectionChange(ShopSectionsEnum section)
        {
            _sectionText.text = _localizationService.GetLocalizedText(_localizationSectionsDictionary[section]);
            _currentSection = section;
        }
    }
}
