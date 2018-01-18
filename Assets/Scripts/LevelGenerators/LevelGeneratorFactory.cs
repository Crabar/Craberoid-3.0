namespace LevelGenerators
{
    public class LevelGeneratorFactory
    {
        private readonly ClassicLevelGenerator.Factory _classicLevelGeneratorFactory;
        private readonly StarLevelGenerator.Factory _starLevelGeneratorFactory;

        public LevelGeneratorFactory(ClassicLevelGenerator.Factory classicLevelGeneratorFactory, StarLevelGenerator.Factory starLevelGeneratorFactory)
        {
            _classicLevelGeneratorFactory = classicLevelGeneratorFactory;
            _starLevelGeneratorFactory = starLevelGeneratorFactory;
        }

        public ILevelGenerator CreateLevelGenerator(LevelGenerator level)
        {
            switch (level)
            {
                case LevelGenerator.Classic:
                {
                    return _classicLevelGeneratorFactory.Create();
                }
                case LevelGenerator.Star:
                {
                    return _starLevelGeneratorFactory.Create();
                }
                default:
                {
                    return _classicLevelGeneratorFactory.Create();
                }
            }
        }
    }
}
