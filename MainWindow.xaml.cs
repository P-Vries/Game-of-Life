using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using GameOfLifeTwo;

namespace GameOfLifeTwoWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int defaultTickSpeed = 1000;
        public int currentTickSpeed = 1000;
        public make grid = new make();
        Timer tick = new Timer();
        public MainWindow()
        {
            InitializeComponent();
            
        }

        public void render()
        {
            const int heightGrid = make.gridLength;
            const int widthGrid = make.gridLength;

            for (int x = 0; x < heightGrid; x++)
            {
                for (int y = 0; y < widthGrid; y++)
                {
                    Rectangle cell = new Rectangle();
                    cell.Width = golCanvas.ActualWidth / widthGrid -1;
                    cell.Height = golCanvas.ActualHeight / heightGrid -1;
                    cell.Tag = x.ToString() + "," + y.ToString();
                    if (grid.grid[x,y] == 1)
                    {
                        cell.Fill = Brushes.Yellow;
                    }
                    else
                    {
                        cell.Fill = Brushes.LightGray;
                    }
                    cell.MouseLeftButtonDown += Cell_MouseLeftButtonDown;
                    golCanvas.Children.Add(cell);
                    Canvas.SetLeft(cell, y * golCanvas.ActualWidth / widthGrid);
                    Canvas.SetTop(cell, x * golCanvas.ActualHeight / heightGrid);
                }
            }
            grid.grid = (int[,])grid.tempgrid.Clone();
        }


        private void Cell_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int temX = int.Parse((((Rectangle)sender).Tag.ToString()).Split(',')[0]);
            int temY = int.Parse((((Rectangle)sender).Tag.ToString()).Split(',')[1]);
            if (grid.grid[temX, temY] == 0)
            {
                grid.tempgrid[temX, temY] = 1;
            }
            else grid.tempgrid[temX, temY] = 0;
            grid.grid = (int[,])grid.tempgrid.Clone();
            for (int i = 0; i < golCanvas.Children.Count; i++)
            {
                int tempX = int.Parse((((Rectangle)golCanvas.Children[i]).Tag.ToString()).Split(',')[0]);
                int tempY = int.Parse((((Rectangle)golCanvas.Children[i]).Tag.ToString()).Split(',')[1]);

                if (tempX == temX && tempY == temY)
                {
                    if(grid.grid[tempX, tempY] == 1) ((Rectangle)golCanvas.Children[i]).Fill = Brushes.Yellow;
                    else ((Rectangle)golCanvas.Children[i]).Fill = Brushes.LightGray;
                }
            }


        }

        public void update()
        {
            
            for (int i = 0; i < golCanvas.Children.Count; i++)
            {
                int tempX = int.Parse((((Rectangle)golCanvas.Children[i]).Tag.ToString()).Split(',')[0]);
                int tempY = int.Parse((((Rectangle)golCanvas.Children[i]).Tag.ToString()).Split(',')[1]);

                if (grid.grid[tempX,tempY] == 0)
                {
                    ((Rectangle)golCanvas.Children[i]).Fill = Brushes.LightGray;
                }
                else
                {
                    ((Rectangle)golCanvas.Children[i]).Fill = Brushes.Yellow;
                }

            }
            grid.grid = (int[,])grid.tempgrid.Clone();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {       
            grid.makeGrid();
            render();
            tick.Tick += Tick_Elapsed;
            tick.Interval = currentTickSpeed;
            tick.Enabled = false;
        }

        private void Tick_Elapsed(object sender, EventArgs e)
        {
            tick.Enabled = false;
            grid.checkPos();
            update();
            tick.Start();
            tick.Enabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            grid.checkPos();
            grid.grid = (int[,])grid.tempgrid.Clone();
            update();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnStart.Content == "Start")
            {
                tick.Enabled = true;
                btnStart.Content = "Stop";
            }
            else
            {
                tick.Enabled = false;
                btnStart.Content = "Start";
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            currentTickSpeed = defaultTickSpeed - (int)sldrSpeed.Value * 100 + 1;
            tick.Interval = currentTickSpeed;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            for (int x = 0; x < grid.grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.grid.GetLength(1); y++)
                {
                    grid.tempgrid[x, y] = 0;

                }
            }
            grid.grid = (int[,])grid.tempgrid.Clone();
            update();

        }
    }
}
