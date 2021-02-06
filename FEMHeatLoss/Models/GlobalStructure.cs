using System;
using System.Collections.Generic;
using System.Text;

namespace FEMHeatLoss.Models
{
    public class GlobalStructure
    {

        // Properties
        public double[,] GlobalH { get; }
        public double[,] GlobalC { get; }
        public double[] GlobalP { get; }


        // Constructors
        public GlobalStructure(GlobalData data)
        {
            this.GlobalH = new double[data.NumberOfNodes, data.NumberOfNodes];
            this.GlobalC = new double[data.NumberOfNodes, data.NumberOfNodes];
            this.GlobalP = new double[data.NumberOfNodes];
        }


        // Calculate
        public void CalculateGlobalH(int u, Grid grid)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.GlobalH[grid.Elements[u].ID[i] - 1, grid.Elements[u].ID[j] - 1] += grid.Elements[u].LocalH[i, j];
                }
            }
        }
        public void CalculateGlobalC(int u, Grid grid)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.GlobalC[grid.Elements[u].ID[i] - 1, grid.Elements[u].ID[j] - 1] += grid.Elements[u].LocalC[i, j];
                }
            }
        }
        public void CalculateGlobalP(int u, Grid grid)
        {
            for (int j = 0; j < 4; j++)
            {
                this.GlobalP[grid.Elements[u].ID[j] - 1] += grid.Elements[u].LocalP[j];
            }
        }
        public void CalculateNonZeroElementPercentage()
        {
            double NON = 0;
            for (int i = 0; i < GlobalH.GetLength(0); i++)
            {
                for (int j = 0; j < GlobalH.GetLength(1); j++)
                {
                    if (this.GlobalH[i, j] != 0)
                    { NON++; }
                }
            }
            double percentage = NON / (GlobalH.GetLength(0) * GlobalH.GetLength(1));
            Console.WriteLine("Non Zero Elements in Global Matrix: {0:P}", percentage);
        }
        public void CalculateFinalEquation(GlobalData data, Grid grid)
        {
            for (int i = 0; i < data.NumberOfNodes; i++)
            {
                for (int j = 0; j < data.NumberOfNodes; j++)
                {
                    this.GlobalH[i, j] = this.GlobalH[i, j] + (this.GlobalC[i, j] / data.dTime);
                }
            }

            for (int i = 0; i < data.NumberOfNodes; i++)
            {
                double row = 0.0;
                for (int j = 0; j < data.NumberOfNodes; j++)
                {
                    row += (GlobalC[i, j] / data.dTime) * grid.Nodes[j].Temperature;
                }
                this.GlobalP[i] = -1.0 * this.GlobalP[i] + row;
            }
        }

        // Display
        public void DisplayGlobalH()
        {
            Console.WriteLine("Global H:");
            for (int j = 0; j < GlobalH.GetLength(0); j++)
            {
                Console.Write("[");
                for (int k = 0; k < GlobalH.GetLength(1); k++)
                {
                    string element = "{0,-10:F3}";
                    if (GlobalH[j, k] != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(string.Format(element, GlobalH[j, k]));
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else
                    {
                        Console.Write(string.Format(element, GlobalH[j, k]));
                    }

                }
                Console.WriteLine("]");
            }
        }
        public void DisplayGlobalC()
        {
            Console.WriteLine("Global C:");
            for (int j = 0; j < GlobalC.GetLength(0); j++)
            {
                Console.Write("[");
                for (int k = 0; k < GlobalC.GetLength(1); k++)
                {
                    string element = "{0,-10:F3}";
                    if (GlobalC[j, k] != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(string.Format(element, GlobalC[j, k]));
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else
                    {
                        Console.Write(string.Format(element, GlobalC[j, k]));
                    }

                }
                Console.WriteLine("]");
            }
        }
        public void DisplayGlobalP()
        {
            Console.WriteLine("Global P:");

            Console.Write("[");
            for (int k = 0; k < GlobalC.GetLength(1); k++)
            {
                string element = "{0,-10:F3} ";
                Console.Write(string.Format(element, GlobalP[k]));
            }
            Console.WriteLine("]");
        }


        // Clear
        public void ClearTables()
        {
            for (int i = 0; i < GlobalP.Length; i++)
            {
                for (int j = 0; j < GlobalP.Length; j++)
                {
                    this.GlobalH[i, j] = 0;
                    this.GlobalC[i, j] = 0;
                }
                this.GlobalP[i] = 0;
            }
        }
    }
}
