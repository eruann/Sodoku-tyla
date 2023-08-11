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
        adicionales a los de los array basicos, en especial me interesa el metodo "intersect" que viene (del namespace linq
        el cual permite agregarle a c# la funcionalidad de hacer "queries" orientadas al lengaje de programacion)
        trae un subset de elementos que estan en ambos arrays, por lo que permite chequear si un elemento esta repetido
        */
        /*
        Declaraciones necesarias para usar el generador de sudokus
        private List<int>[] HRow = new List<int>[9];
        private List<int>[] VRow = new List<int>[9];
        private List<int>[] ThreeSquare = new List<int>[9];
        */

        /* Declaro un jagged array de  de tipo Entero de tamaño 9, un jagged array es un "array de arrays"
        el primer bracket indica el tamaño del jagged array, 9 en este caso indicando que guarda hasta 9 "arrays" 
        el segundo bracket indica la dimensionalidad, en este caso son array de 1 sola dimension, si fueran de mas 
        se indicarian con comas    private int[2][ , ] guardiara 2 array de 2 dimensiones cada uno
        En este caso el Jagged array contendra cada fila del sudoku y cada elemento de los array de fila seran una celda
             */
        private int[][] grid = new int[9][];
 
        /*
        Declaro  generador de numeros aleatorios https://docs.microsoft.com/en-us/dotnet/api/system.random?view=netframework-4.8 
        originalmente se usa para generar numeros para el armado de sudokus, pero se cambia su proposito para seleccionar
        aleatoriamente uno de los sudokus predefenidos.
        */
        private Random r;
        public void NewGame(Random rn)
        {
            this.r = rn;
            createNewGame();
        }

        /*Inicializo las  listas y el array creado anteriormente.Este paso se mantiene  por buena practica, 7
        ya que no siendo necesaria la generacion automatica, se podria inicializar y cargar datos en un mismo paso
        */
        private void initializeLists()
        {
            
            for (int x = 0; x <= 8; x++)
            {
                //Inicilizo los array previamente creados, 
                /*
                HRow[x] = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                VRow[x] = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                ThreeSquare[x] = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                */
                //inicializo el jagged array grid, poniendo en cada posicion un array de 9 posiciones vacio

                int[] row = new int[9];
                grid[x] = row;
            }
            
  
        }

        private void createNewGame()
        {
            //reutilizo el random declardo previamente para generar un numero entre 0 y 9 que seleccionara el sudoku a usar
            int Randomsudoku = r.Next(0, 9);
            switch (Randomsudoku)
            {
                case 0:
                    grid[0] = new int[] {3,7,6,5,9,4,2,8,1}; //Fila:0
                    grid[1] = new int[] {5,9,1,7,2,8,3,4,6}; //Fila:1
                    grid[2] = new int[] {8,4,2,1,6,3,5,7,9}; //Fila:2
                    grid[3] = new int[] {2,3,9,4,8,7,6,1,5}; //Fila:3
                    grid[4] = new int[] {7,5,8,6,1,9,4,2,3}; //Fila:4
                    grid[5] = new int[] {1,6,4,3,5,2,7,9,8}; //Fila:5
                    grid[6] = new int[] {4,8,7,9,3,5,1,6,2}; //Fila:6
                    grid[7] = new int[] {6,2,5,8,7,1,9,3,4}; //Fila:7
                    grid[8] = new int[] {9,1,3,2,4,6,8,5,7}; //Fila:8
                    break;
                case 1:
                    grid[0] = new int[] {8,9,6,7,4,5,1,2,3}; //Fila:0
                    grid[1] = new int[] {2,7,3,9,6,1,4,8,5}; //Fila:1
                    grid[2] = new int[] {4,5,1,2,3,8,6,7,9}; //Fila:2
                    grid[3] = new int[] {7,2,4,8,5,3,9,6,1}; //Fila:3
                    grid[4] = new int[] {1,3,5,6,2,9,8,4,7}; //Fila:4
                    grid[5] = new int[] {6,8,9,1,7,4,5,3,2}; //Fila:5
                    grid[6] = new int[] {3,4,8,5,9,2,7,1,6}; //Fila:6
                    grid[7] = new int[] {9,1,7,3,8,6,2,5,4}; //Fila:7
                    grid[8] = new int[] {5,6,2,4,1,7,3,9,8}; //Fila:8
                    break;
                case 2:
                    grid[0] = new int[] {9,1,2,4,5,7,6,3,8}; //Fila:0
                    grid[1] = new int[] {8,5,7,6,3,9,1,4,2}; //Fila:1
                    grid[2] = new int[] {3,4,6,8,2,1,7,5,9}; //Fila:2
                    grid[3] = new int[] {6,2,5,7,9,3,4,8,1}; //Fila:3
                    grid[4] = new int[] {7,8,4,1,6,2,5,9,3}; //Fila:4
                    grid[5] = new int[] {1,3,9,5,4,8,2,7,6}; //Fila:5
                    grid[6] = new int[] {5,6,3,2,8,4,9,1,7}; //Fila:6
                    grid[7] = new int[] {2,7,8,9,1,5,3,6,4}; //Fila:7
                    grid[8] = new int[] {4,9,1,3,7,6,8,2,5}; //Fila:8
                    break;
                case 3:
                    grid[0] = new int[] {6,8,9,4,3,2,1,5,7}; //Fila:0
                    grid[1] = new int[] {5,7,3,8,1,6,2,4,9}; //Fila:1
                    grid[2] = new int[] {4,2,1,5,9,7,8,3,6}; //Fila:2
                    grid[3] = new int[] {2,6,4,1,7,5,9,8,3}; //Fila:3
                    grid[4] = new int[] {3,9,7,6,8,4,5,1,2}; //Fila:4
                    grid[5] = new int[] {1,5,8,3,2,9,7,6,4}; //Fila:5
                    grid[6] = new int[] {9,3,5,7,6,1,4,2,8}; //Fila:6
                    grid[7] = new int[] {8,4,2,9,5,3,6,7,1}; //Fila:7
                    grid[8] = new int[] {7,1,6,2,4,8,3,9,5}; //Fila:8
                    break;
                case 4:
                    grid[0] = new int[] {7,1,8,2,6,4,5,3,9}; //Fila:0
                    grid[1] = new int[] {6,3,9,8,5,1,4,2,7}; //Fila:1
                    grid[2] = new int[] {4,2,5,3,7,9,6,8,1}; //Fila:2
                    grid[3] = new int[] {5,4,3,1,2,6,9,7,8}; //Fila:3
                    grid[4] = new int[] {9,6,1,7,8,3,2,5,4}; //Fila:4
                    grid[5] = new int[] {8,7,2,4,9,5,3,1,6}; //Fila:5
                    grid[6] = new int[] {3,8,7,6,4,2,1,9,5}; //Fila:6
                    grid[7] = new int[] {1,9,4,5,3,8,7,6,2}; //Fila:7
                    grid[8] = new int[] {2,5,6,9,1,7,8,4,3}; //Fila:8
                    break;
                case 5:
                    grid[0] = new int[] {3,1,6,5,8,7,4,2,9}; //Fila:0
                    grid[1] = new int[] {7,2,4,9,1,6,3,8,5}; //Fila:1
                    grid[2] = new int[] {9,5,8,2,3,4,7,6,1}; //Fila:2
                    grid[3] = new int[] {6,8,1,7,5,9,2,4,3}; //Fila:3
                    grid[4] = new int[] {4,9,2,8,6,3,1,5,7}; //Fila:4
                    grid[5] = new int[] {5,3,7,1,4,2,8,9,6}; //Fila:5
                    grid[6] = new int[] {8,4,9,6,7,1,5,3,2}; //Fila:6
                    grid[7] = new int[] {2,7,3,4,9,5,6,1,8}; //Fila:7
                    grid[8] = new int[] {1,6,5,3,2,8,9,7,4}; //Fila:8
                    break;
                case 6:
                    grid[0] = new int[] {9,7,6,4,3,8,5,2,1}; //Fila:0
                    grid[1] = new int[] {5,1,3,9,7,2,8,6,4}; //Fila:1
                    grid[2] = new int[] {4,8,2,6,5,1,7,9,3}; //Fila:2
                    grid[3] = new int[] {6,5,7,2,8,3,4,1,9}; //Fila:3
                    grid[4] = new int[] {1,2,4,5,9,7,3,8,6}; //Fila:4
                    grid[5] = new int[] {8,3,9,1,6,4,2,7,5}; //Fila:5
                    grid[6] = new int[] {7,4,8,3,1,6,9,5,2}; //Fila:6
                    grid[7] = new int[] {3,6,5,7,2,9,1,4,8}; //Fila:7
                    grid[8] = new int[] {2,9,1,8,4,5,6,3,7}; //Fila:8
                    break;
                case 7:
                    grid[0] = new int[] {4,6,9,3,5,8,1,2,7}; //Fila:0
                    grid[1] = new int[] {2,7,1,6,9,4,8,3,5}; //Fila:1
                    grid[2] = new int[] {5,3,8,1,7,2,9,4,6}; //Fila:2
                    grid[3] = new int[] {3,2,7,4,1,9,6,5,8}; //Fila:3
                    grid[4] = new int[] {1,5,4,7,8,6,2,9,3}; //Fila:4
                    grid[5] = new int[] {9,8,6,2,3,5,4,7,1}; //Fila:5
                    grid[6] = new int[] {6,1,5,9,4,7,3,8,2}; //Fila:6
                    grid[7] = new int[] {8,4,2,5,6,3,7,1,9}; //Fila:7
                    grid[8] = new int[] {7,9,3,8,2,1,5,6,4}; //Fila:8
                    break;
                case 8:
                    grid[0] = new int[] {5,4,9,8,2,1,3,7,6}; //Fila:0
                    grid[1] = new int[] {3,1,7,6,9,5,4,2,8}; //Fila:1
                    grid[2] = new int[] {8,2,6,7,4,3,5,9,1}; //Fila:2
                    grid[3] = new int[] {6,9,5,4,3,8,2,1,7}; //Fila:3
                    grid[4] = new int[] {1,3,2,5,7,6,8,4,9}; //Fila:4
                    grid[5] = new int[] {7,8,4,2,1,9,6,5,3}; //Fila:5
                    grid[6] = new int[] {9,6,8,1,5,4,7,3,2}; //Fila:6
                    grid[7] = new int[] {4,7,3,9,6,2,1,8,5}; //Fila:7
                    grid[8] = new int[] {2,5,1,3,8,7,9,6,4}; //Fila:8
                    break;
                case 9:
                    grid[0] = new int[] {2,5,9,4,7,1,8,3,6}; //Fila:0
                    grid[1] = new int[] {8,3,4,5,6,2,7,9,1}; //Fila:1
                    grid[2] = new int[] {1,6,7,3,9,8,2,4,5}; //Fila:2
                    grid[3] = new int[] {9,1,6,7,8,3,5,2,4}; //Fila:3
                    grid[4] = new int[] {5,4,2,6,1,9,3,8,7}; //Fila:4
                    grid[5] = new int[] {7,8,3,2,5,4,1,6,9}; //Fila:5
                    grid[6] = new int[] {4,9,8,1,2,5,6,7,3}; //Fila:6
                    grid[7] = new int[] {3,7,1,8,4,6,9,5,2}; //Fila:7
                    grid[8] = new int[] {6,2,5,9,3,7,4,1,8}; //Fila:8
                    break;
            }

            /*
            En resumidas cuentas el generador de sudokus trabaja por fuerza bruta  
             "int si = (y / 3) * 3 + (x / 3);" ubica la posicion dentro de cada uno de los elementos de los cuadrados
             de 3x3 , mientras que y recorre las filas y x las columnas. Inicialmente array "grid" se encuentra lleno
             con los numeros del 1 al 9 de la inicilizacion, por lo que la formula va recorriendo e intersectando cada 
             elemento de los 3 arrays (Filas, columnas, cuadrados 3x3) y removiendo los repetidos. 
             luego reemplaza el numero por otro aleatorio  y vuelve a comparar todos los elementos hasta que cada casilla
             esta completa por un numero unico y "util" es decir de acuerdo a las reglas del sudoku
             esta estrategia si bien es utilizable utiliza sentencias "goto" para resolver el problema, por una cuestion
             de buena programacion en general se desaconseja el uso de este tipo de sentencias. por lo cual se 
             reemplaza con 10 sudokus estaticos.
             */
/*
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
  */       

            if (ShowClues != null)
            {
                ShowClues(grid);
            }

        }

        public void showGridSolution()
        {
            if (ShowSolution != null)
            {
                ShowSolution(grid);
            }

        }

    }
}
