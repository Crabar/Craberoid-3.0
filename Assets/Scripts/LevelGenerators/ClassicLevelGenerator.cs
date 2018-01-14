using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LevelGenerators
{
    public class ClassicLevelGenerator : ILevelGenerator
    {
        private readonly BrickController.Factory _brickFactory;

        public ClassicLevelGenerator(BrickController.Factory brickFactory)
        {
            _brickFactory = brickFactory;
        }

        public IList<BrickController> GenerateLevel()
        {
            var bricks = new List<BrickController>();

            const int rows = 5;
            const int columns = 8;

            var currentZ = 0;

            const int zStep = 1;
            const int xStep = 2;

            const float gap = 0.2f;

            for (var i = 0; i < rows; i++)
            {
                var currentX = -(columns - 1) * (xStep + gap) / 2;

                for (var j = 0; j < columns; j++)
                {
                    var brick = _brickFactory.Create();
                    bricks.Add(brick);
                    brick.transform.position = new Vector3(currentX + gap * j, 0, currentZ + gap * i);
                    currentX += xStep;
                }

                currentZ += zStep;
            }

            return bricks;
        }

        public class Factory : Factory<ClassicLevelGenerator>
        {
        }
    }
}
