
using MyTetris.Tetramino;
using MyTetris.Pentamino;

namespace MyTetris
{
    public class BlockQueue
    {
        public readonly Block[] tetraminoBlocks =
        [
            new BlockI(),
            new BlockJ(),
            new BlockL(),
            new BlockO(),
            new BlockS(),
            new BlockT(),
            new BlockZ()
        ];

        public readonly Block[] pentaminoBlocks =
        [
            new BlockPentaI(),
            new BlockPentaF(),
            new BlockPentaG(),
            new BlockPentaJ(),
            new BlockPentaL(),
            new BlockPentaP(),
            new BlockPentaRF(),
            new BlockPentaRG(),
            new BlockPentaRL(),
            new BlockPentaRP(),
            new BlockPentaRS(),
            new BlockPentaRY(),
            new BlockPentaS(),
            new BlockPentaT(),
            new BlockPentaU(),
            new BlockPentaW(),
            new BlockPentaX(),
            new BlockPentaY()
        ];

        private readonly Random random = new Random();

        public Block NextBlock { get; private set; }

        public BlockQueue(bool isTetramino)
        {
            NextBlock = RandomBlock(isTetramino);
        }





        private Block RandomBlock(bool isTetramino)
        {
            if (isTetramino)
            {
                return tetraminoBlocks[random.Next(tetraminoBlocks.Length)];
            }
            else
            {
                return pentaminoBlocks[random.Next(pentaminoBlocks.Length)];
            } 
        }

        public Block GetAndUpdate(bool isTetramino)
        {
            var block = NextBlock;
            do
            {
                NextBlock = RandomBlock(isTetramino);
            }
            while (block.Id == NextBlock.Id);
            return block;
        }
    }
}
