using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sudoku_cs
{
       //declaro la clase Game 
    public class Game
    {
        //declaracion de eventos 
        public event ShowCluesEventHandler ShowClues;
        public delegate void ShowCluesEventHandler(int[][] grid);
        public event ShowSolutionEventHandler ShowSolution;
        public delegate void ShowSolutionEventHandler(int[][] grid);

        /* Declaro 3 array de tipo lista de Enteros de tamaño 9 , la "lista" de enteros permite metodos
        adicionales a los de los array basicos. 
        */
        private List<int>[] HRow = new List<int>[9];
        private List<int>[] VRow = new List<int>[9];
        private List<int>[] ThreeSquare = new List<int>[9];

        /* Declaro un jagged array de  de tipo Entero de tamaño 9, un jagged array es un "array de arrays"
        el primer bracket indica el tamaño del jagged array, 9 en este caso indicando que guarda hasta 9 "arrays" 
        el segundo bracket indica la dimensionalidad, en este caso son array de 1 sola dimension, si fueran de mas 
        se indicarian con comas    private int[2][ , ] guardiara 2 array de 2 dimensiones cada uno
        
             */
        private int[][] grid = new int[9][];

        //Declaro  generador de numeros aleatorios https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netframework-4.8 
        // 
        private Random r;
        public void NewGame(Random rn)
        {
            this.r = rn;
            createNewGame();
        }

        //Inicializo las  listas y el array creado anteriormente
        private void initializeLists()
        {
            for (int x = 0; x <= 8; x++)
            {
                HRow[x] = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                VRow[x] = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                ThreeSquare[x] = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                int[] row = new int[9];
                grid[x] = row;
            }
        }

        private void createNewGame()
        {
            while (true)
            {
            break1:
                initializeLists();
                for (int y = 0; y <= 8; y++)
                {
                    for (int x = 0; x <= 8; x++)
                    {
                        int si = (y / 3) * 3 + (x / 3);
                        int[] useful = HRow[y].Intersect(VRow[x]).Intersect(ThreeSquare[si]).ToArray();
                        if (useful.Count() == 0)
                        {
                            goto break1;
                        }
                        int randomNumber = useful[this.r.Next(0, useful.Count())];
                        HRow[y].Remove(randomNumber);
                        VRow[x].Remove(randomNumber);
                        ThreeSquare[si].Remove(randomNumber);
                        grid[y][x] = randomNumber;
                        if (y == 8 & x == 8)
                        {
                            goto break2;
                        }
                    }                    
                }                                
            };
        break2:
    
            if (ShowClues != null)
            {
                ShowClues(grid);
            }

        }

        //Muestro la solucion al sudoku
        public void showGridSolution()
        {
            if (ShowSolution != null)
            {
                ShowSolution(grid);
            }

        }

    }
}
