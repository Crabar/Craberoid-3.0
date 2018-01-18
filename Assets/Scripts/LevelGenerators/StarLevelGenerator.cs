using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace LevelGenerators
{
    public class StarLevelGenerator : ILevelGenerator
    {
        private readonly BrickController.Factory _brickFactory;

        public StarLevelGenerator(BrickController.Factory brickFactory)
        {
            _brickFactory = brickFactory;
        }

        public IList<BrickController> GenerateLevel()
        {
            var bricks = new List<BrickController>();

            for (var i = 0; i < 10; i++)
            {
                var brick = _brickFactory.Create();
                bricks.Add(brick);
                brick.transform.position = new Vector3(-8 + 0.75f * i, 0, -4 + 1.2f * i);
            }

            for (var i = 0; i < 10; i++)
            {
                var brick = _brickFactory.Create();
                bricks.Add(brick);
                brick.transform.position = new Vector3(8 - 0.75f * i, 0, -4 + 1.2f * i);
            }

            var topBrick = _brickFactory.Create();
            bricks.Add(topBrick);
            topBrick.transform.position = new Vector3(0, 0, 8);

            var rowBrick = _brickFactory.Create();
            bricks.Add(rowBrick);
            rowBrick.transform.position = new Vector3(-10.1f, 0, 3.2f);

            rowBrick = _brickFactory.Create();
            bricks.Add(rowBrick);
            rowBrick.transform.position = new Vector3(-7.9f, 0, 3.2f);

            rowBrick = _brickFactory.Create();
            bricks.Add(rowBrick);
            rowBrick.transform.position = new Vector3(-5.7f, 0, 3.2f);

            rowBrick = _brickFactory.Create();
            bricks.Add(rowBrick);
            rowBrick.transform.position = new Vector3(-1.3f, 0, 3.2f);

            rowBrick = _brickFactory.Create();
            bricks.Add(rowBrick);
            rowBrick.transform.position = new Vector3(1.3f, 0, 3.2f);

            rowBrick = _brickFactory.Create();
            bricks.Add(rowBrick);
            rowBrick.transform.position = new Vector3(5.7f, 0, 3.2f);

            rowBrick = _brickFactory.Create();
            bricks.Add(rowBrick);
            rowBrick.transform.position = new Vector3(7.9f, 0, 3.2f);

            rowBrick = _brickFactory.Create();
            bricks.Add(rowBrick);
            rowBrick.transform.position = new Vector3(10.1f, 0, 3.2f);

            return bricks;
        }

        public class Factory : Factory<StarLevelGenerator>
        {
        }
    }
}
