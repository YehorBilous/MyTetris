
namespace MyTetris.Pentamino
{
    public class BlockPentaX : Block
    {
        private Position[][] states =
        [
            [new(0, 1), new(1, 0), new(1, 1), new(1, 2), new(2, 1)]
        ];
        public override int Id => 21;

        protected override Position StartOffset => new Position(0, 2);

        protected override Position[][] Tiles => states;
    }
}
