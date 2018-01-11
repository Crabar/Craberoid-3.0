namespace LevelGenerators
{
    public class LevelGeneratorFactory
    {
        private readonly ClassicLevelGenerator.Factory _classicLevelGeneratorFactory;

        public LevelGeneratorFactory(ClassicLevelGenerator.Factory classicLevelGeneratorFactory)
        {
            _classicLevelGeneratorFactory = classicLevelGeneratorFactory;
        }

        public ILevelGenerator CreateLevelGenerator(LevelGenerator level)
        {
            switch (level)
            {
                case LevelGenerator.Classic:
                {
                    return _classicLevelGeneratorFactory.Create();
                }
                default:
                {
                    return _classicLevelGeneratorFactory.Create();
                }
            }
        }
    }
}
