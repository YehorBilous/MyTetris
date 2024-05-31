
namespace MyTetris.Pentamino
{
    public class BlockPentaL : Block
    {
        private Position[][] states =
        [
            [new(0, 1), new(1, 1), new(2, 1), new(3, 1), new(3, 2)],
            [new(2, 0), new(2, 1), new(2, 2), new(2, 3), new(3, 0)],
            [new(0, 1), new(0, 2), new(1, 2), new(2, 2), new(3, 2)],
            [new(0, 3), new(1, 0), new(1, 1), new(1, 2), new(1, 3)]
        ];

        public override int Id => 12;

        protected override Position StartOffset => new Position(0, 3);

        protected override Position[][] Tiles => states;
    }
}
