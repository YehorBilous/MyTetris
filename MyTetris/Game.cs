using System.IO;

namespace MyTetris
{
    public class Game
    {
        public Grid GameGrid { get; }

        private Block currentBlock;

        public bool IsTetramino { get; set; }

        public bool GameOver { get; private set; }

        public int Score { get; private set; }

        public int Lines { get; private set; }

        public int BlockDelay { get; private set; }

        public BlockQueue BlockQueue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private string filePath = "aboba.txt";


        public Game(bool isTetramino)
        {
            IsTetramino = isTetramino;
            BlockDelay = 500;
            GameGrid = new Grid(23, 10);
            BlockQueue = new BlockQueue(isTetramino);
            CurrentBlock = BlockQueue.GetAndUpdate(isTetramino);
            GameOver = false;
            Score = 0;
            Lines = 0;
        }

        public Block CurrentBlock
        {
            get { return currentBlock; }
            private set
            {
                currentBlock = value;
                currentBlock.Reset();
            }
        }





        private bool BlockFits()
        {
            foreach (var pos in CurrentBlock.TilePositions())
            {
                if (!GameGrid.IsEmpty(pos.Row, pos.Column))
                {
                    return false;
                }
            }
            return true;
        }

        public void PlaceBlock()
        {
            foreach (var pos in CurrentBlock.TilePositions())
            {
                GameGrid[pos.Row, pos.Column] = CurrentBlock.Id;
            }
            IncreaseScore();
            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                WriteFile();
                BlockUpdate();
            }
        }

        private void WriteFile() 
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < GameGrid.Rows; i++)
                {
                    for (int j = 0; j < GameGrid.Columns; j++)
                    {
                        writer.Write("dadsdaasd");
                    }
                    writer.WriteLine();
                }
            }
        }

        private void IncreaseScore()
        {
            int clearedRows = GameGrid.ClearFullRows();
            switch (clearedRows)
            {
                case 1:
                    Score += 100;
                    break;
                case 2:
                    Score += 300;
                    break;
                case 3:
                    Score += 700;
                    break;
                case 4:
                    Score += 1500;
                    break;
                case 5:
                    Score += 5000;
                    break;
                default:
                    return;
            }
            if (clearedRows > 0)
            {
                DecreaseBlockDelay();
                Lines += clearedRows;
            }
        }

        public void BlockUpdate()
        {
            CurrentBlock = BlockQueue.GetAndUpdate(IsTetramino);
        }

        public void MoveBlockDown()
        {
            CurrentBlock.Move(1, 0);
            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }

        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }

        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }

        public void RotateBlockCW()
        {
            CurrentBlock.RotateCW();
            if (!BlockFits())
            {
                CurrentBlock.RotateCCW();
            }
        }

        public void RotateBlockCCW()
        {
            CurrentBlock.RotateCCW();
            if (!BlockFits())
            {
                CurrentBlock.RotateCW();
            }
        }

        private bool IsGameOver()
        {
            return !GameGrid.IsRowEmpty(2);
        }

        private int TileDropDistance(Position p)
        {
            int drop = 0;
            while (GameGrid.IsEmpty(p.Row + drop + 1, p.Column))
            {
                drop++;
            }
            return drop;
        }

        public int BlockDropDistance()
        {
            int drop = GameGrid.Rows;
            foreach (Position p in CurrentBlock.TilePositions())
            {
                drop = Math.Min(drop, TileDropDistance(p));
            }
            return drop;
        }

        public void DropBlock()
        {
            CurrentBlock.Move(BlockDropDistance(), 0);
            PlaceBlock();
        }

        public void DecreaseBlockDelay()
        {
            BlockDelay = Math.Max(50, 500 - (Score / 10000) * 50);
        }
    }
}

