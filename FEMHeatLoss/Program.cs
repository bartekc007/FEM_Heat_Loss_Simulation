using System;
using System.Diagnostics;
using System.Linq;
using FEMHeatLoss.Models;

namespace FEMHeatLoss
{
    class Program
    {
        static void Main(string[] args)
        {
            StartUp.FemInscription(); Console.WriteLine();

            // Fetch data from txt file
            GlobalData data = new GlobalData();
            data.DisplayData();
            data.DisplayMaterialData();
            string temperatureStringFormatInit = "iteration: {0,-4} Time: {1,-5}     Temperature inside building: {2,-4}";
            Console.WriteLine(string.Format(temperatureStringFormatInit, 0, 0, data.InitialTemperature));

            // Generate grid 
            Grid grid = new Grid(data);
            grid.GenerateElementsWithMineralCotton2();
            grid.GenerateNodes(data);

            //for (int i = 0; i < grid.Elements.Length; i++)
               // grid.DisplayElementWithNodes(i);

            // Create empty Global Structure
            Stopwatch t1 = Stopwatch.StartNew();
            GlobalStructure GloalStructure = new GlobalStructure(data);

            // For each simulation time step 
            for (int j = 0; j < data.Time / data.dTime; j++)
            {

                // Clear Global table before new time step
                GloalStructure.ClearTables();

                // For each element in grid
                for (int u = 0; u < grid.Elements.Length; u++)
                {
                    // Create abstract local element
                    LocalElement localElement = new LocalElement(grid, grid.Elements[u], data);

                    // for each integration point
                    for (int i = 0; i < Math.Pow(data.IntegrationSchemaWariant, 2); i++)
                    {
                        // Calculate Local H and Local C
                        localElement.CalculateJacobian(i);
                        localElement.ReverseJacobian();
                        localElement.CalculateNxNy(i);
                        localElement.CalculateH(i, grid.Elements[u].KFactor);
                        localElement.CalculateC(i, grid.Elements[u].SpecyficHeat, grid.Elements[u].Dencity);

                        // Push Local H and Local C to grid
                        grid.Elements[u].CalculateLocalH(u, grid, localElement);
                        grid.Elements[u].CalculateLocalC(u, grid, localElement);
                    }
                    // Calculate H Boudary Condition
                    localElement.CalculateHcb(data.Alpha, grid, u, data.AmbientTemperature, data.IntegrationSchemaWariant);
                    // Merging Elements With Boundary Condition matrix
                    grid.Elements[u].MergeHWithHbcAndP(localElement.Hbc, localElement.P);


                    // Calculate Global H, Global C, Global P
                    GloalStructure.CalculateGlobalH(u, grid);
                    GloalStructure.CalculateGlobalC(u, grid);
                    GloalStructure.CalculateGlobalP(u, grid);
                }

                // Calculate Final Equation 
                GloalStructure.CalculateFinalEquation(data, grid);

                // Calculate T[] From Final Equation
                double[] T0 = SimulationSolver.GaussElimination(GloalStructure.GlobalH, GloalStructure.GlobalP, data.NumberOfNodes);

                // Average Air Temperature inside building
                double avrSum = 0;
                double avrCounter = 0;
                for (int i = 0; i < data.NumberOfNodes; i++)
                {
                    if (grid.Nodes[i].X > 0.535)
                    {
                        avrSum += T0[i];
                        avrCounter++;
                    }  
                }
                avrSum = avrSum / avrCounter;
                for (int i = 0; i < data.NumberOfNodes; i++)
                {
                    if (grid.Nodes[i].X > 0.535)
                        T0[i] = avrSum;
                }

                // Swap old Temperatures with new ones from the equation
                for (int i = 0; i < grid.Nodes.Length; i++)
                {
                    grid.Nodes[i].Temperature = T0[i];
                }

                string temperatureStringFormat = "iteration: {0,-6} Time: {1,-8}     Temperature tynk: {2,-10:F4} Temperature pustak: {3,-10:F4} Temperature styropian: {4,-10:F4} Temperature szczelina: {5,-10:F4} Temperature cegla: {6,-10:F4} Temperature powietrze: {7,-10:F4}";
                Console.WriteLine(string.Format(temperatureStringFormat, j + 1,(j+1)*2.5,T0[0],T0[12],T0[212],T0[308],T0[314],T0[404], T0[T0.Length-1]));
            }

            Console.WriteLine("Execution Time: " + (t1.Elapsed.TotalMilliseconds / 1000) + " seconds");
            GloalStructure.CalculateNonZeroElementPercentage();
            Console.ReadKey();
        }
    }
}
