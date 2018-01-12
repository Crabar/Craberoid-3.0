using System;
using System.Collections.Generic;
using System.Linq;
using Signals;

namespace LevelGenerators
{
    public class LevelManager
    {
        private readonly Queue<ILevelGenerator> _levelGenerators;
        private ILevelGenerator _currentLevel;
        private IList<BrickController> _bricksOnLevel;
        private LevelCompletedSignal _levelCompletedSignal;

        public LevelManager(Settings settings, LevelGeneratorFactory levelGeneratorFactory, LevelCompletedSignal levelCompletedSignal)
        {
            _levelCompletedSignal = levelCompletedSignal;
            _levelGenerators = new Queue<ILevelGenerator>();
            foreach (var level in settings.Levels)
            {
                _levelGenerators.Enqueue(levelGeneratorFactory.CreateLevelGenerator(level));
            }
        }

        public bool TryGenerateNextLevel()
        {
            if (_levelGenerators.Count > 0)
            {
                _currentLevel = _levelGenerators.Dequeue();
                _bricksOnLevel = _currentLevel.GenerateLevel();
                foreach (var brick in _bricksOnLevel)
                {
                    brick.BrickDestoyed += OnBrickDestroy;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OnBrickDestroy(BrickController brick)
        {
            _bricksOnLevel.Remove(brick);
            if (_bricksOnLevel.Count == 0)
            {
                _levelCompletedSignal.Fire();
            }
        }

        [Serializable]
        public class Settings
        {
            public LevelGenerator[] Levels;
        }
    }
}
