using System;
using System.Collections.Generic;
using System.Text;

namespace FEMHeatLoss.Models
{
    public class LocalElement
    {
        #region Properties

        // Local Element Properties
        readonly double[] Ksi;
        readonly double[] Eta;
        readonly double[] IntegrationPointWeightKsi;
        readonly double[] IntegrationPointWeightEta;
        readonly int integrationPointsNumber;


        // Real Element Coordinates
        readonly double[] X;
        readonly double[] Y;


        // Jacoby Properties
        public double[,] Jacoby { get; private set; }
        public double[,] ReverseJacoby { get; private set; }
        public double DetJacoby { get; private set; }

        // Derivatives
        public double[] Nx { get; private set; }
        public double[] Ny { get; private set; }
        public double[] N { get; private set; }
        public double[,,] DerivativesOfShapeFunctions { get; }


        // Shape Function
        public double[,] ShapeFunctions { get; private set; }


        // Helpeing variables
        public double[,] FinalNx { get; private set; }
        public double[,] FinalNy { get; private set; }


        // Local Matrixes and Vectors
        public double[,] H { get; private set; }
        public double[,] C { get; private set; }
        public double[,] Hbc { get; private set; }
        public double[] P { get; set; }

        #endregion

        public LocalElement(Grid grid, Element element, GlobalData globalData)
        {
            if (globalData.IntegrationSchemaWariant == 3)
            {
                this.Ksi = new double[9] { -1 * Math.Sqrt(3.0 / 5.0), 0, Math.Sqrt(3.0 / 5.0), -1 * Math.Sqrt(3.0 / 5.0), 0, Math.Sqrt(3.0 / 5.0), -1 * Math.Sqrt(3.0 / 5.0), 0, Math.Sqrt(3.0 / 5.0) };
                this.Eta = new double[9] { -1 * Math.Sqrt(3.0 / 5.0), -1 * Math.Sqrt(3.0 / 5.0), -1 * Math.Sqrt(3.0 / 5.0), 0, 0, 0, Math.Sqrt(3.0 / 5.0), Math.Sqrt(3.0 / 5.0), Math.Sqrt(3.0 / 5.0) };
                this.IntegrationPointWeightKsi = new double[9] { 5.0 / 9.0, 8.0 / 9.0, 5.0 / 9.0, 5.0 / 9.0, 8.0 / 9.0, 5.0 / 9.0, 5.0 / 9.0, 8.0 / 9.0, 5.0 / 9.0 };
                this.IntegrationPointWeightEta = new double[9] { 5.0 / 9.0, 5.0 / 9.0, 5.0 / 9.0, 8.0 / 9.0, 8.0 / 9.0, 8.0 / 9.0, 5.0 / 9.0, 5.0 / 9.0, 5.0 / 9.0 };
                this.DerivativesOfShapeFunctions = new double[2, 9, 4];
                this.ShapeFunctions = new double[9, 4];
                this.integrationPointsNumber = 9;
            }
            else if (globalData.IntegrationSchemaWariant == 2)
            {
                this.Ksi = new double[4] { -1 * 1 / Math.Sqrt(3), 1 / Math.Sqrt(3), 1 / Math.Sqrt(3), -1 * 1 / Math.Sqrt(3) };
                this.Eta = new double[4] { -1 * 1 / Math.Sqrt(3), -1 * 1 / Math.Sqrt(3), 1 / Math.Sqrt(3), 1 / Math.Sqrt(3) };
                this.IntegrationPointWeightKsi = new double[4] { 1, 1, 1, 1 };
                this.IntegrationPointWeightEta = new double[4] { 1, 1, 1, 1 };
                this.DerivativesOfShapeFunctions = new double[2, 4, 4];
                this.ShapeFunctions = new double[4, 4];
                this.integrationPointsNumber = 4;
            }
            else if (globalData.IntegrationSchemaWariant == 4)
            {
                this.Ksi = new double[16] { -1 * Math.Sqrt((3.0/7.0) + (2.0/7.0) * Math.Sqrt(6.0/5.0)), -1 * Math.Sqrt((3.0 / 7.0) - (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)), Math.Sqrt((3.0 / 7.0) - (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)), Math.Sqrt((3.0 / 7.0) + (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)),
                 -1 * Math.Sqrt((3.0/7.0) + (2.0/7.0) * Math.Sqrt(6.0/5.0)), -1 * Math.Sqrt((3.0 / 7.0) - (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)), Math.Sqrt((3.0 / 7.0) - (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)), Math.Sqrt((3.0 / 7.0) + (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)),
                 -1 * Math.Sqrt((3.0/7.0) + (2.0/7.0) * Math.Sqrt(6.0/5.0)), -1 * Math.Sqrt((3.0 / 7.0) - (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)), Math.Sqrt((3.0 / 7.0) - (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)), Math.Sqrt((3.0 / 7.0) + (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)),
                 -1 * Math.Sqrt((3.0/7.0) + (2.0/7.0) * Math.Sqrt(6.0/5.0)), -1 * Math.Sqrt((3.0 / 7.0) - (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)), Math.Sqrt((3.0 / 7.0) - (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)), Math.Sqrt((3.0 / 7.0) + (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)) };

                this.Eta = new double[16] { -1 * Math.Sqrt((3.0/7.0) + (2.0/7.0) * Math.Sqrt(6.0/5.0)), -1 * Math.Sqrt((3.0/7.0) + (2.0/7.0) * Math.Sqrt(6.0/5.0)), -1 * Math.Sqrt((3.0/7.0) + (2.0/7.0) * Math.Sqrt(6.0/5.0)), -1 * Math.Sqrt((3.0/7.0) + (2.0/7.0) * Math.Sqrt(6.0/5.0)),
                -1 * Math.Sqrt((3.0 / 7.0) - (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)), -1 * Math.Sqrt((3.0 / 7.0) - (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)), -1 * Math.Sqrt((3.0 / 7.0) - (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)), -1 * Math.Sqrt((3.0 / 7.0) - (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)),
                Math.Sqrt((3.0 / 7.0) - (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)), Math.Sqrt((3.0 / 7.0) - (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)), Math.Sqrt((3.0 / 7.0) - (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)), Math.Sqrt((3.0 / 7.0) - (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)),
                Math.Sqrt((3.0 / 7.0) + (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)), Math.Sqrt((3.0 / 7.0) + (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)), Math.Sqrt((3.0 / 7.0) + (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)), Math.Sqrt((3.0 / 7.0) + (2.0 / 7.0) * Math.Sqrt(6.0 / 5.0)) };

                this.IntegrationPointWeightKsi = new double[16] { (18.0 - Math.Sqrt(30.0))/36, (18.0 + Math.Sqrt(30.0)) / 36, (18.0 + Math.Sqrt(30.0)) / 36, (18.0 - Math.Sqrt(30.0)) / 36,
                    (18.0 - Math.Sqrt(30.0))/36, (18.0 + Math.Sqrt(30.0)) / 36, (18.0 + Math.Sqrt(30.0)) / 36, (18.0 - Math.Sqrt(30.0)) / 36,
                    (18.0 - Math.Sqrt(30.0))/36, (18.0 + Math.Sqrt(30.0)) / 36, (18.0 + Math.Sqrt(30.0)) / 36, (18.0 - Math.Sqrt(30.0)) / 36,
                    (18.0 - Math.Sqrt(30.0))/36, (18.0 + Math.Sqrt(30.0)) / 36, (18.0 + Math.Sqrt(30.0)) / 36, (18.0 - Math.Sqrt(30.0)) / 36};

                this.IntegrationPointWeightEta = new double[16] { (18.0 - Math.Sqrt(30.0)) / 36, (18.0 - Math.Sqrt(30.0)) / 36, (18.0 - Math.Sqrt(30.0)) / 36, (18.0 - Math.Sqrt(30.0)) / 36,
                (18.0 + Math.Sqrt(30.0)) / 36, (18.0 + Math.Sqrt(30.0)) / 36, (18.0 + Math.Sqrt(30.0)) / 36, (18.0 + Math.Sqrt(30.0)) / 36,
                (18.0 + Math.Sqrt(30.0)) / 36, (18.0 + Math.Sqrt(30.0)) / 36, (18.0 + Math.Sqrt(30.0)) / 36, (18.0 + Math.Sqrt(30.0)) / 36,
                (18.0 - Math.Sqrt(30.0)) / 36, (18.0 - Math.Sqrt(30.0)) / 36, (18.0 - Math.Sqrt(30.0)) / 36, (18.0 - Math.Sqrt(30.0)) / 36};
                this.DerivativesOfShapeFunctions = new double[2, 16, 4];
                this.ShapeFunctions = new double[16, 4];
                this.integrationPointsNumber = 16;
            }
            else
            {
                throw new InvalidOperationException("Wrong Integration Schema wariant in GlobalData.txt file has been declaired.");
            }


            this.X = new double[4];
            this.Y = new double[4];

            this.X[0] = grid.Nodes[grid.Elements[element.ElementID - 1].ID[0] - 1].X;
            this.X[1] = grid.Nodes[grid.Elements[element.ElementID - 1].ID[1] - 1].X;
            this.X[2] = grid.Nodes[grid.Elements[element.ElementID - 1].ID[2] - 1].X;
            this.X[3] = grid.Nodes[grid.Elements[element.ElementID - 1].ID[3] - 1].X;

            this.Y[0] = grid.Nodes[grid.Elements[element.ElementID - 1].ID[0] - 1].Y;
            this.Y[1] = grid.Nodes[grid.Elements[element.ElementID - 1].ID[1] - 1].Y;
            this.Y[2] = grid.Nodes[grid.Elements[element.ElementID - 1].ID[2] - 1].Y;
            this.Y[3] = grid.Nodes[grid.Elements[element.ElementID - 1].ID[3] - 1].Y;

            Jacoby = new double[2, 2];
            ReverseJacoby = new double[2, 2];

            Nx = new double[4];
            Ny = new double[4];
            N = new double[4];

            FinalNx = new double[4, 4];
            FinalNy = new double[4, 4];

            H = new double[4, 4];
            C = new double[4, 4];
            Hbc = new double[4, 4];
            P = new double[4];
        }

        private void CalculateShapeFunctions()
        {
            for (int i = 0; i < this.integrationPointsNumber; i++)
            {
                this.ShapeFunctions[i, 0] = 0.25 * (1 - this.Eta[i]) * (1 - this.Ksi[i]);
                this.ShapeFunctions[i, 1] = 0.25 * (1 - this.Eta[i]) * (1 + this.Ksi[i]);
                this.ShapeFunctions[i, 2] = 0.25 * (1 + this.Eta[i]) * (1 + this.Ksi[i]);
                this.ShapeFunctions[i, 3] = 0.25 * (1 + this.Eta[i]) * (1 - this.Ksi[i]);
            }
        }
        private void CalculateDerivativesForShapeFunctions()
        {
            for (int i = 0; i < this.integrationPointsNumber; i++)
            {
                this.DerivativesOfShapeFunctions[0, i, 0] = -0.25 * (1 - this.Eta[i]);
                this.DerivativesOfShapeFunctions[0, i, 1] = 0.25 * (1 - this.Eta[i]);
                this.DerivativesOfShapeFunctions[0, i, 2] = 0.25 * (1 + this.Eta[i]);
                this.DerivativesOfShapeFunctions[0, i, 3] = -0.25 * (1 + this.Eta[i]);
            }
            for (int i = 0; i < this.integrationPointsNumber; i++)
            {
                this.DerivativesOfShapeFunctions[1, i, 0] = -0.25 * (1 - this.Ksi[i]);
                this.DerivativesOfShapeFunctions[1, i, 1] = -0.25 * (1 + this.Ksi[i]);
                this.DerivativesOfShapeFunctions[1, i, 2] = 0.25 * (1 + this.Ksi[i]);
                this.DerivativesOfShapeFunctions[1, i, 3] = 0.25 * (1 - this.Ksi[i]);
            }
        }
        private void CalculateDeterminant()
        {
            this.DetJacoby = (this.Jacoby[0, 0] * this.Jacoby[1, 1]) - (this.Jacoby[0, 1] * this.Jacoby[1, 0]);
        }
        public void CalculateH(int pc, double kFactor)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.H[i, j] = (this.FinalNx[i, j] + this.FinalNy[i, j]) * kFactor * this.DetJacoby * this.IntegrationPointWeightKsi[pc] * this.IntegrationPointWeightEta[pc];
                }
            }
        }
        public void CalculateJacobian(int j)
        {
            CalculateDerivativesForShapeFunctions();
            CalculateShapeFunctions();

            this.Jacoby[0, 0] = DerivativesOfShapeFunctions[0, j, 0] * this.X[0]
                + DerivativesOfShapeFunctions[0, j, 1] * this.X[1]
                + DerivativesOfShapeFunctions[0, j, 2] * this.X[2]
                + DerivativesOfShapeFunctions[0, j, 3] * this.X[3];
            this.Jacoby[0, 1] = DerivativesOfShapeFunctions[1, j, 0] * this.X[0]
                + DerivativesOfShapeFunctions[1, j, 1] * this.X[1]
                + DerivativesOfShapeFunctions[1, j, 2] * this.X[2]
                + DerivativesOfShapeFunctions[1, j, 3] * this.X[3];
            this.Jacoby[1, 0] = DerivativesOfShapeFunctions[0, j, 0] * this.Y[0]
                + DerivativesOfShapeFunctions[0, j, 1] * this.Y[1]
                + DerivativesOfShapeFunctions[0, j, 2] * this.Y[2]
                + DerivativesOfShapeFunctions[0, j, 3] * this.Y[3];
            this.Jacoby[1, 1] = DerivativesOfShapeFunctions[1, j, 0] * this.Y[0]
                + DerivativesOfShapeFunctions[1, j, 1] * this.Y[1]
                + DerivativesOfShapeFunctions[1, j, 2] * this.Y[2]
                + DerivativesOfShapeFunctions[1, j, 3] * this.Y[3];
        }
        public void CalculateNxNy(int pc)
        {
            for (int i = 0; i < 4; i++)
            {
                this.Nx[i] = (this.ReverseJacoby[0, 0] * this.DerivativesOfShapeFunctions[0, pc, i]) + (this.ReverseJacoby[0, 1] * this.DerivativesOfShapeFunctions[1, pc, i]);
                this.Ny[i] = (this.ReverseJacoby[1, 0] * this.DerivativesOfShapeFunctions[0, pc, i]) + (this.ReverseJacoby[1, 1] * this.DerivativesOfShapeFunctions[1, pc, i]);
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.FinalNx[i, j] = this.Nx[i] * this.Nx[j];
                    this.FinalNy[i, j] = this.Ny[i] * this.Ny[j];
                }
            }
        }
        public void DisplayLocalH(double[,] Hl, int u)
        {
            Console.WriteLine("Element:" + (u + 1));
            for (int j = 0; j < 4; j++)
            {
                Console.Write("[");
                for (int k = 0; k < 4; k++)
                {
                    Console.Write(Hl[j, k] + " , ");
                }
                Console.WriteLine("]");
            }
        }
        public void PrintDfSF()
        {
            for (int n = 0; n < 2; n++)
            {
                for (int j = 0; j < this.integrationPointsNumber; j++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Console.Write(this.DerivativesOfShapeFunctions[n, j, i] + "  ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }
        public void ReverseJacobian()
        {
            CalculateDeterminant();

            this.ReverseJacoby[0, 0] = this.Jacoby[1, 1] / this.DetJacoby;
            this.ReverseJacoby[0, 1] = -1 * this.Jacoby[0, 1] / this.DetJacoby;
            this.ReverseJacoby[1, 0] = -1 * this.Jacoby[1, 0] / this.DetJacoby;
            this.ReverseJacoby[1, 1] = this.Jacoby[0, 0] / this.DetJacoby;
        }
        public void CalculateC(int pc, double cp, double ro)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.C[i, j] = (this.ShapeFunctions[pc, i] * this.ShapeFunctions[pc, j]) * this.DetJacoby * cp * ro * this.IntegrationPointWeightEta[pc] * this.IntegrationPointWeightKsi[pc];
                }
            }
        }
        public void DisplayGausseQuadrature(int u)
        {
            if (u == 0)
            {
                Console.WriteLine("Ksi");
                for (int i = 0; i < this.integrationPointsNumber; i++)
                {
                    Console.WriteLine(Ksi[i]);
                }
                Console.WriteLine();
                Console.WriteLine("Eta");
                for (int i = 0; i < this.integrationPointsNumber; i++)
                {
                    Console.WriteLine(Eta[i]);
                }
            }

        }
        public void CalculateHcb(double alpha, Grid grid, int elementNumber, double InitialTemperature, int integrationSchema)
        {
            // for each element edge
            for (int i = 0; i < grid.Elements[elementNumber].ID.Length; i++)
            {
                Node nodeA = grid.Nodes[grid.Elements[elementNumber].ID[i] - 1];
                Node nodeB = grid.Nodes[grid.Elements[elementNumber].ID[(i + 1) % 4] - 1];

                if (!(nodeA.EdgeCondition == true && nodeB.EdgeCondition == true))
                    continue;
                // for each integration point
                for (int j = 0; j < integrationSchema; j++)
                {
                    double ksi = 0, eta = 0;
                    if (i == 0)
                    {
                        ksi = Ksi[j];
                        eta = -1.0;
                    }
                    else if (i == 1)
                    {
                        ksi = 1;
                        eta = Ksi[j];
                    }
                    else if (i == 2)
                    {
                        ksi = Ksi[j];
                        eta = 1.0;
                    }
                    else if (i == 3)
                    {
                        ksi = -1.0;
                        eta = Ksi[j];
                    }

                    double[] Nvector = new double[4];

                    // for each shape function
                    Nvector[0] = 0.25 * (1.0 - ksi) * (1.0 - eta);
                    Nvector[1] = 0.25 * (1.0 + ksi) * (1.0 - eta);
                    Nvector[2] = 0.25 * (1.0 + ksi) * (1.0 + eta);
                    Nvector[3] = 0.25 * (1.0 - ksi) * (1.0 + eta);

                    double detJ = Math.Sqrt(Math.Pow(nodeA.X - nodeB.X, 2) + Math.Pow(nodeA.Y - nodeB.Y, 2)) / 2;

                    for (int a = 0; a < 4; a++)
                    {
                        for (int b = 0; b < 4; b++)
                        {
                            this.Hbc[a, b] += Nvector[a] * Nvector[b] * IntegrationPointWeightKsi[j] * detJ * alpha;
                        }
                    }

                    for (int c = 0; c < 4; c++)
                    {
                        this.P[c] += Nvector[c] * IntegrationPointWeightKsi[j] * InitialTemperature * detJ * alpha * -1;
                    }
                }
            }
        }
    }
}
