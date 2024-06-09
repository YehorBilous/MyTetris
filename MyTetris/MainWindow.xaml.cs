using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MyTetris
{
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImages =
        [
            new BitmapImage(new Uri("AssetsTiles/EmptyBlock3.png", UriKind.Relative)),

            new BitmapImage(new Uri("AssetsTiles/BlueBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsTiles/RedBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsTiles/GreenBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsTiles/CyanBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsTiles/PurpleBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsTiles/OrangeBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsTiles/YellowBlock.png", UriKind.Relative)),

            new BitmapImage(new Uri("AssetsTiles/WhiteBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsTiles/BrownBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsTiles/DarkGreenBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsTiles/DarkPinkBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsTiles/GoldBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsTiles/GrayBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsTiles/PinkBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsTiles/SkinBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsTiles/SkyBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsTiles/RepBlock.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsTiles/DarkBlueBlock.png", UriKind.Relative))
        ];

        private readonly ImageSource[] blockImages =
        [
            new BitmapImage(new Uri("AssetsBlocks/BlockI.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockJ.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockL.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockO.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockS.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockT.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockZ.png", UriKind.Relative)),

            new BitmapImage(new Uri("AssetsBlocks/BlockPentoI.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockPentoF.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockPentoRF.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockPentoRL.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockPentoL.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockPentoRP.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockPentoP.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockPentoG.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockPentoRG.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockPentoT.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockPentoU.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockPentoJ.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockPentoW.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockPentoX.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockPentoY.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockPentoRY.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockPentoS.png", UriKind.Relative)),
            new BitmapImage(new Uri("AssetsBlocks/BlockPentoZ.png", UriKind.Relative))

        ];

        private Game game = new Game(true);

        private readonly Image[,] gameField;

        private readonly Random random = new Random();



        public MainWindow()
        {
            InitializeComponent();
            gameField = SetupGameField(game.GameGrid);
        }

        private async Task GameLoop()
        {
            DisplayGame(game);
            while (!game.GameOver)
            {
                await Task.Delay(game.BlockDelay);
                game.MoveBlockDown();
                DisplayGame(game);
            }
            GameOverGrid.Visibility = Visibility.Visible;
        }

        private Image[,] SetupGameField(Grid grid)
        {
            Image[,] gameField = new Image[grid.Rows, grid.Columns];
            int cellsize = (int)(GameFieldCanvas.Width / grid.Columns);
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    Image fieldCell = new()
                    {
                        Width = cellsize,
                        Height = cellsize
                    };
                    Canvas.SetTop(fieldCell, (r - 3) * cellsize + 10);
                    Canvas.SetLeft(fieldCell, c * cellsize);
                    GameFieldCanvas.Children.Add(fieldCell);
                    gameField[r, c] = fieldCell;
                }
            }
            return gameField;
        }

        private void DrawGrid(Grid grid)
        {
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    if (id > 18)
                    {
                        id -= 18;
                    }
                    gameField[r, c].Opacity = 1;
                    gameField[r, c].Source = tileImages[id];
                }
            }
        }

        private void DrawBlock(Block block)
        {
            foreach (var pos in block.TilePositions())
            {
                gameField[pos.Row, pos.Column].Opacity = 1;
                gameField[pos.Row, pos.Column].Source = block.Id <= 18? tileImages[block.Id] : tileImages[block.Id - 18];
            }
        }
        
        private void DisplayText()
        {
            ScoreText.Text = $"{game.Score}";
            LinesText.Text = $"{game.Lines}";
            LevelText.Text = $"{Math.Min(game.Score / 100, 9)}";
        }

        private void DrawGhostBlock(Block block)
        {
            int dropDistance = game.BlockDropDistance();
            foreach (var pos in block.TilePositions())
            {
                gameField[pos.Row + dropDistance, pos.Column].Opacity = 0.4;
                gameField[pos.Row + dropDistance, pos.Column].Source = block.Id <= 18? tileImages[block.Id] : tileImages[block.Id - 18];
            }
        }

        private void DrawNextBlock(BlockQueue blockQueue)
        {
            NextImage.Source = blockImages[blockQueue.NextBlock.Id - 1];
        }

        private void DisplayGame(Game game)
        {
            DrawGrid(game.GameGrid);
            DrawGhostBlock(game.CurrentBlock);
            DrawBlock(game.CurrentBlock);
            DisplayText();
            DrawNextBlock(game.BlockQueue);
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (game.GameOver)
            {
                return;
            }
            switch (e.Key)
            {
                case Key.A:
                    game.MoveBlockLeft();
                    break;
                case Key.D:
                    game.MoveBlockRight();
                    break;
                case Key.S:
                    game.MoveBlockDown();
                    break;
                case Key.Left:
                    game.RotateBlockCCW();
                    break;
                case Key.Right:
                    game.RotateBlockCW();
                    break;
                case Key.Space:
                    game.DropBlock();
                    break;
                case Key.L:
                    game.WriteFile();
                    break;
                default:
                    return;
            }
            DisplayGame(game);
        }

        private async void PlayTetraminoClick(object sender, RoutedEventArgs e)
        {
            MenuGrid.Visibility = Visibility.Hidden;
            GameGrid.Visibility = Visibility.Visible;
            game = new Game(true);
            ModeText.Text = "Tetromino";
            game.IsTetramino = true;
            await GameLoop();
        }

        private async void PlayAgainClick(object sender, RoutedEventArgs e)
        {
            GameOverGrid.Visibility = Visibility.Hidden;
            bool currentMode = game.IsTetramino;
            game = new Game(currentMode);
            await GameLoop();
        }

        private void MainMenuClick(object sender, RoutedEventArgs e)
        {
            GameOverGrid.Visibility = Visibility.Hidden;
            GameGrid.Visibility = Visibility.Hidden;
            MenuGrid.Visibility = Visibility.Visible;
        }

        private async void PlayPentominoClick(object sender, RoutedEventArgs e)
        {
            MenuGrid.Visibility = Visibility.Hidden;
            GameGrid.Visibility = Visibility.Visible;
            game = new Game(false);
            ModeText.Text = "Pentomino";
            game.IsTetramino = false;
            await GameLoop();
        }
    }
}