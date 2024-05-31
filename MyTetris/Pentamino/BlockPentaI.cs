
namespace MyTetris.Pentamino
{
    public class BlockPentaI : Block
    {
        private Position[][] states =
        [
            [new(0, 2), new(1, 2), new(2, 2), new(3, 2), new(4, 2)],
            [new(2, 0), new(2, 1), new(2, 2), new(2, 3), new(2, 4)]
        ];
        public override int Id => 8;

        protected override Position StartOffset => new Position(0, 2);

        protected override Position[][] Tiles => states;
    }
}
