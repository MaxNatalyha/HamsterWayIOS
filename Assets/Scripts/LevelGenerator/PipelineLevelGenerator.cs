using Pipeline;
using SaveSystem;
using Services;
using SkinSystem;
using UnityEngine;
using Utilities;
using Zenject;

namespace LevelGenerator
{
    public class PipelineLevelGenerator : AbstractLevelGenerator, IPipelineLevelGenerator
    {
        private RectTransform _upperViewParent;
        private IPipelineObjectsProvider _objectsProvider;

        private ISkinsService _skinsService;
        private ILevelObjectsPool _levelObjectsPool;
        
        private readonly ICustomLogger _logger;

        private Vector2Int _currentLevelGridSize;

        public PipelineLevelGenerator()
        {
            _logger = new DebugLogger();
        }

        [Inject]
        public void Construct(ILevelObjectsPool levelObjectsPool, ISkinsService skinsService, IPipelineObjectsProvider objectsProvider)
        {
            _levelObjectsPool = levelObjectsPool;
            _skinsService = skinsService;
            _objectsProvider = objectsProvider;
        }

        public void Initialize(RectTransform board, RectTransform upperViewParent)
        {           
            _board = board;
            _upperViewParent = upperViewParent;
            
            _objectsProvider.HamsterHouse = _levelObjectsPool.GetLevelObject<HamsterHouse>(GameObjectTypes.House);
            _objectsProvider.Bowl = _levelObjectsPool.GetLevelObject<Bowl>(GameObjectTypes.Bowl);
            
            _logger.PrintInfo("Pipeline Generator", "Initialized");
        }

        public void Generate(PipelineLevelData pipelineLevelData)
        {
            _currentLevelGridSize = new Vector2Int(pipelineLevelData.gridSizeX, pipelineLevelData.gridSizeY);

            foreach (var levelObjectData in pipelineLevelData.levelObjects)
            {
                GameObjectTypes objectType = (GameObjectTypes)levelObjectData.currentType;
                
                if (objectType == GameObjectTypes.House)
                {
                    SetUpObject(levelObjectData, _objectsProvider.HamsterHouse);
                    continue;
                }                  
                
                if (objectType == GameObjectTypes.Bowl)
                {
                    SetUpObject(levelObjectData, _objectsProvider.Bowl);
                    continue;
                }            
                
                Pipe levelObject = _levelObjectsPool.GetPipe(objectType);
                SetUpObject(levelObjectData, levelObject);
                _objectsProvider.Pipes.Add(levelObject);
            }
            
            ApplySkins();
            _objectsProvider.HamsterHouse.transform.SetAsLastSibling();
        }

        public void CleanUp()
        {
            _levelObjectsPool.ReturnObjectToPool(_objectsProvider.HamsterHouse.gameObject);
            _levelObjectsPool.ReturnObjectToPool(_objectsProvider.Bowl.gameObject);
            
            foreach (var pipe in _objectsProvider.Pipes)
            {
                _levelObjectsPool.ReturnPipeToPool(pipe);
            }
            
            _objectsProvider.Pipes.Clear();
        }

        private void SetUpObject(PipelineObjectSaveData objectSaveData, IConnectable connectableObject)
        {            
            float tileSize = CalculateBoardTileSize(_currentLevelGridSize);
            Vector2 tileOffset = CalculateTileOffset(_currentLevelGridSize, tileSize);
            
            float yPosGridSizeOffset = connectableObject.ObjectConfig.contentGridSize.y > 1
                ? tileSize / 2 * connectableObject.ObjectConfig.contentGridSize.y - tileSize / 2
                : 0;
            
            Vector2 objectPosition = new Vector2(objectSaveData.xPos * tileSize - tileOffset.x, objectSaveData.yPos * tileSize - tileOffset.y + yPosGridSizeOffset);
            Vector2 objectSize = new Vector2(connectableObject.ObjectConfig.contentGridSize.x * tileSize, connectableObject.ObjectConfig.contentGridSize.y * tileSize);
            
            connectableObject.SetUp(objectSize, objectPosition, _upperViewParent, _board);
        }

        private void ApplySkins()
        {
            var pipesSkin = _skinsService.GetSkin(SkinCategories.Pipes);

            foreach (var pipe in _objectsProvider.Pipes)
                pipe.ApplySkin(pipesSkin);
            
            _objectsProvider.Bowl.ApplySkin(_skinsService.GetSkin(SkinCategories.Bowl));
            _objectsProvider.Hamster.ApplySkin(_skinsService.GetSkin(SkinCategories.Pet));
            _objectsProvider.HamsterHouse.ApplySkin(_skinsService.GetSkin(SkinCategories.House), pipesSkin);
        }
    }
}