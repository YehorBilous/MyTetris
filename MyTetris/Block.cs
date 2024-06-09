
namespace MyTetris
{
    public abstract class Block
    {
        protected abstract Position[][] Tiles { get; }

        protected abstract Position StartOffset { get; }

        private Position offset;

        private int rotationState = 0;

        public abstract int Id { get; }

        public Block()
        {
            offset = new Position(StartOffset.Row, StartOffset.Column);
        }





        public void Move(int rows, int columns)
        {
            offset.Row += rows;
            offset.Column += columns;
        }

        public void RotateCW()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }

        public void RotateCCW()
        {
            if (rotationState == 0) 
            { 
                rotationState = Tiles.Length - 1; 
            }
            else 
            { 
                rotationState--; 
            }
        }

        public IEnumerable<Position> TilePositions()
        {
            foreach (var pos in Tiles[rotationState])
            {
                yield return new Position(pos.Row + offset.Row, pos.Column + offset.Column);
            }
        }

        public Position[] TilePositionsList()
        {
            var list = new Position[Tiles[rotationState].Length];
            int k = 0;
            foreach (var pos in Tiles[rotationState])
            {
                list[k] = new Position(pos.Row + offset.Row, pos.Column + offset.Column);
                k++;
            }
            return list;
        }

        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }
    }
}
