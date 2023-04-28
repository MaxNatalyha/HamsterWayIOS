using System;
using System.Collections.Generic;
using System.Linq;
using MatchingCards;
using SaveSystem;
using Services;
using SkinSystem;
using UnityEngine;
using Utilities;
using Zenject;
using Random = System.Random;

namespace LevelGenerator
{
    public class MatchingCardLevelGenerator : AbstractLevelGenerator, IMatchingCardLevelGenerator
    {
        private int PairsLimit => _cardMatchPairs.Count;

        private List<CardMatchPair> _cardMatchPairs;

        private Vector2Int _currentLevelGridSize;
        private ICardsProvider _cardsProvider;

        private ILevelObjectsPool _levelObjectsPool;
        private ISkinsService _skinsService;
        
        private readonly MatchingCardConfig _config;
        private readonly ICustomLogger _logger;
        private readonly Random _random;

        private Action<Card> _onCardOpen;
        
        public MatchingCardLevelGenerator(MatchingCardConfig config)
        {
            _config = config;
            _cardMatchPairs = new List<CardMatchPair>();
            _random = new Random();
            _logger = new DebugLogger();
        }

        [Inject]
        public void Construct(ILevelObjectsPool levelObjectsPool, ISkinsService skinsService, ICardsProvider cardsProvider)
        {
            _levelObjectsPool = levelObjectsPool;
            _skinsService = skinsService;
            _cardsProvider = cardsProvider;
        }

        public void Initialize(RectTransform board, Action<Card> onCardOpen)
        {           
            _board = board;
            _onCardOpen = onCardOpen;
            
            for (int i = 0; i < _config.cardFaceSprites.Count; i++)
                _cardMatchPairs.Add(new CardMatchPair(_config.cardFaceSprites[i], i));

            _logger.PrintInfo("Matching Card Generator", "Initialized");
        }

        public void Generate(MatchingCardsLevelData levelData)
        {
            _currentLevelGridSize = levelData.gridSize;
            var generatedCards = new List<Card>();
            
            var cardPairs = _currentLevelGridSize.x * _currentLevelGridSize.y / 2;
            cardPairs = cardPairs > PairsLimit ? PairsLimit : cardPairs;

            int totalCards = cardPairs * 2;
            
            for (int x = 0; x < _currentLevelGridSize.x; x++)
            {
                for (int y = 0; y < _currentLevelGridSize.y; y++)
                {
                    if(generatedCards.Count == totalCards) break;
                
                    var card = _levelObjectsPool.GetCard();
                    SetupCard(card, x, y);

                    generatedCards.Add(card);
                }
            }

            _cardsProvider.Cards = generatedCards;
            
            SetPairs(cardPairs);
            ApplySkins();
        }

        private void SetPairs(int cardsPairs)
        {
            _cardsProvider.Cards = _cardsProvider.Cards.OrderBy(a => _random.Next()).ToList();
            _cardMatchPairs = _cardMatchPairs.OrderBy(p => _random.Next()).ToList();

            for (int i = 0; i < cardsPairs; i++)
            {
                int cardPairIndex = i * 2;

                _cardsProvider.Cards[cardPairIndex].SetPair(_cardMatchPairs[i]);
                _cardsProvider.Cards[cardPairIndex + 1].SetPair(_cardMatchPairs[i]);
            }
        }

        public void CleanUp()
        {
            foreach (var card in _cardsProvider.Cards)
            {
                card.CleanUp();
                _levelObjectsPool.ReturnCardToPool(card);
            }
            
            _cardsProvider.Cards.Clear();
        }

        private void SetupCard(Card card, int x, int y)
        {
            var tileSize = CalculateBoardTileSize(_currentLevelGridSize, _config.cardAspectRatio);
            var tileOffset = CalculateTileOffset(_currentLevelGridSize, tileSize);
            
            Vector2 cardPos = new Vector2(x * tileSize.x - tileOffset.x, y * tileSize.y - tileOffset.y);
            Vector2 cardSize = new Vector2(tileSize.x, tileSize.y);
            
            card.Setup(_onCardOpen,cardPos, cardSize, _board);
        }

        private void ApplySkins()
        {
            var cardSkin = _skinsService.GetSkin(SkinCategories.Card);

            foreach (var card in _cardsProvider.Cards)
                card.ApplySkin(cardSkin);
        }
    }
}