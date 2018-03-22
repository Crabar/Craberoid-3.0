using System.Collections.Generic;
using UnityEngine;
using Utils;
using Zenject;

namespace LevelGenerators
{
    public class CrazyLevelGenerator : ILevelGenerator
    {
        private readonly BrickController.Factory _brickFactory;

        public CrazyLevelGenerator(BrickController.Factory brickFactory)
        {
            _brickFactory = brickFactory;
        }
        
        public IList<BrickController> GenerateLevel()
        {
            var bricks = new List<BrickController>();

            const int rows = 5;
            const int columns = 8;

            var columnsObjects = new GameObject[8];
            for (var i = 0; i < columnsObjects.Length; i++)
            {
                columnsObjects[i] = new GameObject($"col{i}");
                columnsObjects[i].AddComponent<BrickColumnCrazyMover>();
                columnsObjects[i].GetComponent<BrickColumnCrazyMover>().SetSpeed(0.1 + 0.01 * i);
            }

            var currentZ = 0;

            const int zStep = 1;
            const int xStep = 2;

            const float gap = 0.4f;

            for (var i = 0; i < rows; i++)
            {
                var currentX = -(columns) * (xStep + gap) / 2;

                for (var j = 0; j < columns; j++)
                {
                    var brick = _brickFactory.Create();
                    if (i % 2 == 1)
                    {
                        brick.DecreaseHp();
                    }
                    bricks.Add(brick);
                    brick.transform.position = new Vector3(currentX + gap * j * 2, 0, currentZ + gap * i);
                    brick.transform.SetParent(columnsObjects[j].transform);
                    currentX += xStep;
                }

                currentZ += zStep;
            }

            return bricks;
        }

        public class Factory : Factory<CrazyLevelGenerator>
        {
        }
    }
}