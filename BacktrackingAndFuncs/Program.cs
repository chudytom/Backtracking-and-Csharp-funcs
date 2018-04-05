using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BacktrackingAndFuncs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var queens = new QueensBacktracking(rows: 8, columns: 8);
            queens.FindSolution();
        }
    }

    internal class QueensBacktracking
    {
        private int rows;
        private int columns;
        private bool[,] board;
        private bool[] usedRows;
        private List<bool[,]> solutions = new List<bool[,]>();

        public QueensBacktracking(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            board = new bool[rows, columns];
            usedRows = new bool[rows];
        }

        public void FindSolution()
        {
            bool solutionFound = false;
            for (int row = 0; row < rows; row++)
            {
                bool tempResult = PutQueen(row, column: 0, queenNumber: 1);
                if (tempResult)
                    solutionFound = tempResult;
            }

            foreach (var solution in solutions)
            {
                PrintBoard(solution);
            }
            Console.WriteLine($"Solutions found: {solutions.Count}");
        }

        private bool PutQueen(int row, int column, int queenNumber)
        {
            board[row, column] = true;
            usedRows[row] = true;
            if (queenNumber == Math.Min(rows, columns))
            {
                solutions.Add((bool[,])board.Clone());
                board[row, column] = false;
                usedRows[row] = false;
                return false;
            }

            for (int newRow = 0; newRow < rows; newRow++)
            {
                bool isPossible = IsPositionPossible(newRow, column + 1);
                if (!isPossible) continue;
                else
                {
                    bool result = PutQueen(newRow, column + 1, queenNumber + 1);
                    if (result) return result;
                }
            }
            board[row, column] = false;
            usedRows[row] = false;
            return false;

        }

        private bool IsPositionPossible(int queenRow, int queenColumn)
        {
            if (usedRows[queenRow] == true) return false;
            var operations = new List<(Func<int, int> rowOperation, Func<int, int> columnOperation)>();
            operations.Add((row => ++row, column => --column));
            operations.Add((row => ++row, column => ++column));
            operations.Add((row => --row, column => --column));
            operations.Add((row => --row, column => ++column));

            bool isPossible = true;
            foreach (var operation in operations)
            {
                if (TestIfPossible(board, queenRow, queenColumn, operation.rowOperation, operation.columnOperation) == false)
                    isPossible = false;
            }
            return isPossible;
        }

        private bool TestIfPossible(bool[,] board, int queenRow, int queenColumn, Func<int, int> rowOperation, Func<int, int> columnOperation)
        {
            for (int row = queenRow, column = queenColumn;
                row >= 0 && column >= 0 && row < board.GetLength(0) && column < board.GetLength(1);
                row = rowOperation.Invoke(row), column = columnOperation.Invoke(column))
            {
                if (board[row, column] == true)
                    return false;
            }
            return true;
        }

        private void PrintBoard(bool[,] board)
        {
            Console.WriteLine("---------------------------------------------------------------------");
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    if (board[row, column] == true)
                        Console.Write("1 ");
                    else
                        Console.Write("0 ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}

//int tempRow = row;
//int tempColumn = column;
//while (tempRow >= 0 && tempColumn >= 0)
//{
//    if (board[tempRow, tempColumn] == true) return false;
//    tempRow--;
//    tempColumn--;
//}
//tempRow = row;
//tempColumn = column;
//while (tempRow < rows && tempColumn < columns)
//{
//    if (board[tempRow, tempColumn] == true) return false;
//    tempRow++;
//    tempColumn++;
//}
//tempRow = row;
//tempColumn = column;
//while (tempRow >= 0 && tempColumn < columns)
//{
//    if (board[tempRow, tempColumn] == true) return false;
//    tempRow--;
//    tempColumn++;
//}
//tempRow = row;
//tempColumn = column;
//while (tempRow < rows && tempColumn >= 0)
//{
//    if (board[tempRow, tempColumn] == true) return false;
//    tempRow++;
//    tempColumn--;
//}
//return true;
