using System;
using System.Collections;
using System.Collections.Generic;

namespace LevelGenerators
{
    public class LevelManager
    {
        private Queue<ILevelGenerator> _levelGenerators;

        public LevelManager(Settings settings, LevelGeneratorFactory _levelGeneratorFactory)
        {
            _levelGenerators = new Queue<ILevelGenerator>();
            foreach (var level in settings.Levels)
            {
                _levelGenerators.Enqueue(_levelGeneratorFactory.CreateLevelGenerator(level));
            }
        }

        public ILevelGenerator GetNextLevelGenerator()
        {
            return _levelGenerators.Dequeue();
        }

        [Serializable]
        public class Settings
        {
            public LevelGenerator[] Levels;
        }
    }
}
