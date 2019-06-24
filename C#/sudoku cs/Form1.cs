using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sudoku_cs
{
    public partial class Form1 : Form
    {

        private Game game = new Game();
        private Random r = new Random();
        int fila;
        int columna;
        bool MostrarSolucionFlag;
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
            btnNew.Click += btnNew_Click;
            DataGridView1.CellEndEdit += validacion;
            DataGridView1.CellLeave += ValidarMouse;
            btnSolution.Click += btnSolution_Click;
            DataGridView1.Paint += DataGridView1_Paint;
            ComboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            game.ShowClues += game_ShowClues;
            game.ShowSolution += game_ShowSolution;

        }
  
          // fix celledit end cuando cambia de celda el mouse
        private void ValidarMouse(object sender, DataGridViewCellEventArgs e)
        {      
            DataGridView1.EndEdit();
        }
        private void validacion(System.Object sender, System.EventArgs e)
        {
       
            if (DataGridView1.SelectedCells[0].Value == null) return;
            string numeroIngresado = DataGridView1.SelectedCells[0].Value.ToString();
            string[] posiblesValores = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            if (!(posiblesValores.Contains(numeroIngresado)))
            {
                DataGridView1.SelectedCells[0].Value = null;
                return;
            }

            fila = DataGridView1.SelectedCells[0].RowIndex;
            columna = DataGridView1.SelectedCells[0].ColumnIndex;

            for (int r = 0; r < 9; r++)
            {
                if (DataGridView1.Rows[r].Cells[columna].Value == null) continue;
                if (r == fila)
                {
                    continue;
                }
                if (DataGridView1.Rows[r].Cells[columna].Value.ToString() == numeroIngresado)
                {
                    DataGridView1.SelectedCells[0].Value = null;
                    return;
                }

            }
            for (int c = 0; c < 9; c++)
            {
                if (DataGridView1.Rows[fila].Cells[c].Value == null) continue;
                if (c == columna)
                {
                    continue;
                }

                if (DataGridView1.Rows[fila].Cells[c].Value.ToString() == numeroIngresado)
                {
                    DataGridView1.SelectedCells[0].Value = null;
                    return;
                }

            }

            if (Estacompleto() && !MostrarSolucionFlag)
            {
                string message = "Felicidades completó el sudoku";
                MessageBox.Show(message);
                return;
            }

            if (fila <= 2 && columna <=2 )
            {
                ValidarCuadrado(0,0,2,2,numeroIngresado);
                return;
            }
            if ( fila >= 3 && fila <= 5  && columna <= 2)
            {
                ValidarCuadrado(3, 0, 5, 2, numeroIngresado);
                return;
            }
            if (fila >= 6 && columna <= 2)
            {
                ValidarCuadrado(6, 0, 8, 2, numeroIngresado);
                return;
            }



            if (fila <= 2 && columna <= 5 && columna >= 3)
            {
                ValidarCuadrado(0, 3, 2, 5, numeroIngresado);
                return;
            }
            if (fila >= 3 && fila <= 5 && columna >= 3 && columna <= 5)
            {
                ValidarCuadrado(3, 3, 5, 5, numeroIngresado);
                return;
            }
            if (fila >= 6 && columna >= 3 && columna <= 5)
            {
                ValidarCuadrado(6, 3, 8, 5, numeroIngresado);
                return;
            }



            if (fila <= 2 && columna >= 6)
            {
                ValidarCuadrado(0, 6, 2, 8, numeroIngresado);
                return;
            }
            if (fila >= 3 && fila <= 5 && columna >= 6)
            {
                ValidarCuadrado(3, 6, 5, 8, numeroIngresado);
                return;
            }
            if (fila >= 6 && columna >= 6 )
            {
                ValidarCuadrado(6, 6, 8, 8, numeroIngresado);
                return;
            }
      

        }

      
        private void ValidarCuadrado(int inicioR , int inicioC, int finR, int finC, string numeroIngresado)
        {
            for (int r = inicioR; r <= finR; r++)
            {
                for (int c = inicioC; c <= finC; c++)
                {
                    if (DataGridView1.Rows[r].Cells[c].Value == null) continue;
                    if ((r == fila) && (c == columna))
                    {
                        continue;
                    }
                    if (DataGridView1.Rows[r].Cells[c].Value.ToString() == numeroIngresado)
                    {
                        DataGridView1.SelectedCells[0].Value = null;
                        return;
                    }
                }

            }
        }
        private bool Estacompleto()
        {
            for (int i = 0; i < DataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < DataGridView1.Rows[i].Cells.Count; j++)
                {

                    if (DataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        string Value = DataGridView1.Rows[i].Cells[j].Value.ToString();

                        if (StringExtensions.IsNullOrWhiteSpace(Value))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;

        }
        private void Form1_Load(System.Object sender, System.EventArgs e)
        {
            DataGridView1.Rows.Add(9);
            ComboBox1.SelectedIndex = 0;
            btnNew.PerformClick();
        }

        private void btnNew_Click(System.Object sender, System.EventArgs e)
        {
           
            game.NewGame(r);
            MostrarSolucionFlag = false;
        }

        private void DataGridView1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 75, 0, 75, 228);
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 150, 0, 150, 228);
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 0, 66, 228, 66);
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 0, 132, 228, 132);
        }

        private void btnSolution_Click(System.Object sender, System.EventArgs e)
        {
            game.showGridSolution();
        }

        private void ComboBox1_SelectedIndexChanged(System.Object sender, System.EventArgs e)
        {
            btnNew.PerformClick();
        }

        public void game_ShowClues(int[][] grid)
        {
        
            for (int y = 0; y <= 8; y++)
            {
                
                List<int> cells = new List<int>(new int[] {1,2,3,4,5,6,7,8,9});
                for (int c = 1; c <= 9 - (5 - ComboBox1.SelectedIndex); c++)
                {
                    int randomNumber = cells[r.Next(0, cells.Count())];
                    cells.Remove(randomNumber);
                }
                for (int x = 0; x <= 8; x++)
                {
                    if (cells.Contains(x + 1))
                    {
                        DataGridView1.Rows[y].Cells[x].Value = grid[y][x];
                        DataGridView1.Rows[y].Cells[x].Style.ForeColor = Color.Red;
                        DataGridView1.Rows[y].Cells[x].ReadOnly = true;
                    }
                    else
                    {
                        DataGridView1.Rows[y].Cells[x].Value = "";
                        DataGridView1.Rows[y].Cells[x].Style.ForeColor = Color.Black;
                        DataGridView1.Rows[y].Cells[x].ReadOnly = false;
                    }
                }
            }
        }

        public void game_ShowSolution(int[][] grid)
        {
           
            MostrarSolucionFlag = true; //evita las validaciones cuando se esta mostrando la solucion
          
            for (int y = 0; y <= 8; y++)
            {
                for (int x = 0; x <= 8; x++)
                {
                    if (DataGridView1.Rows[y].Cells[x].Style.ForeColor == Color.Black)
                    {
                        if (DataGridView1.Rows[y].Cells[x].Value == null)
                        {
                            DataGridView1.Rows[y].Cells[x].Style.ForeColor = Color.Blue;
                            DataGridView1.Rows[y].Cells[x].Value = grid[y][x];
                        }
                        else
                        {
                            if (grid[y][x].ToString() != DataGridView1.Rows[y].Cells[x].Value.ToString())
                            {
                                DataGridView1.Rows[y].Cells[x].Style.ForeColor = Color.Blue;
                                DataGridView1.Rows[y].Cells[x].Value = grid[y][x];
                            }
                        }
                    }
                }
            }
        }

        private void btnNew_Click_1(object sender, EventArgs e)
        {

        }

       
    }
    //agrego la clase stringextensions ya que .net 3.5 no tiene el metodo "IsNullOrWhiteSpace"
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (value != null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (!char.IsWhiteSpace(value[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
