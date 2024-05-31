
namespace MyTetris.Pentamino
{
    public class BlockPentaS : Block
    {
        private Position[][] states =
        [
            [new(0, 1), new(0, 2), new(1, 1), new(2, 0), new(2, 1)],
            [new(0, 0), new(1, 0), new(1, 1), new(1, 2), new(2, 2)]
        ];

        public override int Id => 24;

        protected override Position StartOffset => new Position(0, 2);

        protected override Position[][] Tiles => states;
    }
}
