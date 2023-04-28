using System;
using Cysharp.Threading.Tasks;
using MatchingCards;
using SaveSystem;
using UnityEngine;

namespace LevelGenerator
{
    public interface IMatchingCardLevelGenerator
    { 
        void Initialize(RectTransform board, Action<Card> onCardOpen);
        void Generate(MatchingCardsLevelData levelData);
        void CleanUp();
    }
}