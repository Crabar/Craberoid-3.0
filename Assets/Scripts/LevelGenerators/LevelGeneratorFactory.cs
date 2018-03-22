namespace LevelGenerators
{
    public class LevelGeneratorFactory
    {
        private readonly ClassicLevelGenerator.Factory _classicLevelGeneratorFactory;
        private readonly StarLevelGenerator.Factory _starLevelGeneratorFactory;
        private readonly CrazyLevelGenerator.Factory _crazyLevelGeneratorFactory;

        public LevelGeneratorFactory(ClassicLevelGenerator.Factory classicLevelGeneratorFactory, StarLevelGenerator.Factory starLevelGeneratorFactory, CrazyLevelGenerator.Factory crazyLevelGeneratorFactory)
        {
            _classicLevelGeneratorFactory = classicLevelGeneratorFactory;
            _starLevelGeneratorFactory = starLevelGeneratorFactory;
            _crazyLevelGeneratorFactory = crazyLevelGeneratorFactory;
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
                case LevelGenerator.Crazy:
                {
                    return _crazyLevelGeneratorFactory.Create();
                }
                default:
                {
                    return _classicLevelGeneratorFactory.Create();
                }
            }
        }
    }
}
