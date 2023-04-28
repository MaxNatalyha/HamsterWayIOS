using UnityEngine;

namespace LevelGenerator
{
    public abstract class AbstractLevelGenerator
    {
        protected RectTransform _board;
        
        protected float CalculateBoardTileSize(Vector2Int gridSize)
        {
            var rect = _board.rect;
            
            float horizontalSize = rect.width / gridSize.x;
            float verticalSize = rect.height / gridSize.y;

            return horizontalSize > verticalSize ? verticalSize : horizontalSize;
        }
        
        protected Vector2 CalculateBoardTileSize(Vector2Int gridSize, float sideRatio)
        {
            var rect = _board.rect;

            float horizontalSize = rect.width / gridSize.x;
            float verticalSize = rect.height / gridSize.y;

            return gridSize.x > gridSize.y ? new Vector2(horizontalSize, horizontalSize * sideRatio) : new Vector2(verticalSize / sideRatio, verticalSize);
        }

        protected Vector2 CalculateTileOffset(Vector2Int gridSize, float tileSize)
        {
            float xOffset = gridSize.x * tileSize / 2 - tileSize / 2;
            float yOffset = gridSize.y * tileSize / 2 - tileSize / 2;

            return new Vector2(xOffset, yOffset);
        }
        
        protected Vector2 CalculateTileOffset(Vector2Int gridSize, Vector2 tileSize)
        {
            float xOffset = gridSize.x * tileSize.x / 2 - tileSize.x / 2;
            float yOffset = gridSize.y * tileSize.y / 2 - tileSize.y / 2;

            return new Vector2(xOffset, yOffset);
        }
    }
}