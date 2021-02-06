using System;
using System.Collections.Generic;
using System.Text;

namespace FEMHeatLoss.Models
{
    public class Grid
    {
        public GlobalData Data { get; set; }
        public Element[] Elements { get; set; }
        public Node[] Nodes { get; set; }

        public Grid(GlobalData globaldata)
        {
            this.Data = globaldata;
            this.Elements = new Element[globaldata.NumberOfElements];
            this.Nodes = new Node[globaldata.NumberOfNodes];
        }


        // Elements
        public void GenerateElementsTestCase()
        {
            int temp = 0;
            for (int i = 0; i < this.Data.WidthPointNumber - 1; i++)
            {
                for (int j = 1; j < this.Data.HeightPointNumber; j++)
                {
                    Elements[temp] = new Element(
                        temp + 1,
                        j + i * this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + i * this.Data.HeightPointNumber + 1,300,7800,700);
                    temp++;
                }

            }
        }

        public void GenerateElementsWithMineralCotton2()
        {
            int temp = 0;
            for (int i = 0; i < this.Data.WidthPointNumber-1; i++)
            {
                for (int j = 1; j < this.Data.HeightPointNumber; j++)
                {
                    if(i <=3)
                    {
                        Elements[temp] = new Element(
                        temp + 1,
                        j + i * this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + i * this.Data.HeightPointNumber + 1, 
                        this.Data.Tynk_wew.KFactor,
                        this.Data.Tynk_wew.Dencity,
                        this.Data.Tynk_wew.SpecyficHeat);
                        temp++;
                    }
                    else if(i <= 3)
                    {
                        Elements[temp] = new Element(
                        temp + 1,
                        j + i * this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + i * this.Data.HeightPointNumber + 1,
                        this.Data.Pustak_cer.KFactor,
                        this.Data.Pustak_cer.Dencity,
                        this.Data.Pustak_cer.SpecyficHeat);
                        temp++;
                    }
                    else if (i <= 77)
                    {
                        Elements[temp] = new Element(
                        temp + 1,
                        j + i * this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + i * this.Data.HeightPointNumber + 1,
                        this.Data.Welna_min.KFactor,
                        this.Data.Welna_min.Dencity,
                        this.Data.Welna_min.SpecyficHeat);
                        temp++;
                    }
                    else if (i <= 83)
                    {
                        Elements[temp] = new Element(
                        temp + 1,
                        j + i * this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + i * this.Data.HeightPointNumber + 1,
                        this.Data.Szczelina_wen.KFactor,
                        this.Data.Szczelina_wen.Dencity,
                        this.Data.Szczelina_wen.SpecyficHeat);
                        temp++;
                    }
                    else if (i <= 107)
                    {
                        Elements[temp] = new Element(
                        temp + 1,
                        j + i * this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + i * this.Data.HeightPointNumber + 1,
                        this.Data.Cegla_klin.KFactor,
                        this.Data.Cegla_klin.Dencity,
                        this.Data.Cegla_klin.SpecyficHeat);
                        temp++;
                    }
                    else if (i <= 117)
                    {
                        Elements[temp] = new Element(
                        temp + 1,
                        j + i * this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + i * this.Data.HeightPointNumber + 1,
                        this.Data.Powietrze.KFactor,
                        this.Data.Powietrze.Dencity,
                        this.Data.Powietrze.SpecyficHeat);
                        temp++;
                    }

                }
            }
        }
        public void GenerateElementsWithMineralCotton()
        {
            int temp = 0;
            int collumn = 0;

            for (int i = 0; i < this.Data.Tynk_wew.WidthPoints; i++)
            {
                for (int j = 1; j < this.Data.HeightPointNumber; j++)
                {
                    Elements[temp] = new Element(
                        temp + 1,
                        j+collumn* this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + collumn * this.Data.HeightPointNumber + +1,
                        this.Data.Tynk_wew.KFactor,
                        this.Data.Tynk_wew.Dencity,
                        this.Data.Tynk_wew.SpecyficHeat);
                    temp++;
                }
                collumn++;
            }

            for (int i = 0; i < this.Data.Pustak_cer.WidthPoints; i++)
            {
                for (int j = 1; j < this.Data.HeightPointNumber; j++)
                {
                    Elements[temp] = new Element(
                        temp + 1,
                        j + collumn * this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + collumn * this.Data.HeightPointNumber + +1,
                        this.Data.Pustak_cer.KFactor,
                        this.Data.Pustak_cer.Dencity,
                        this.Data.Pustak_cer.SpecyficHeat);
                    temp++;
                }
                collumn++;
            }

            for (int i = 0; i < this.Data.Welna_min.WidthPoints; i++)
            {
                for (int j = 1; j < this.Data.HeightPointNumber; j++)
                {
                    Elements[temp] = new Element(
                        temp + 1,
                        j + collumn * this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + collumn * this.Data.HeightPointNumber + +1,
                        this.Data.Welna_min.KFactor,
                        this.Data.Welna_min.Dencity,
                        this.Data.Welna_min.SpecyficHeat);
                    temp++;
                }
                collumn++;
            }

            for (int i = 0; i < this.Data.Szczelina_wen.WidthPoints; i++)
            {
                for (int j = 1; j < this.Data.HeightPointNumber; j++)
                {
                    Elements[temp] = new Element(
                        temp + 1,
                        j + collumn * this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + collumn * this.Data.HeightPointNumber + +1,
                        this.Data.Szczelina_wen.KFactor,
                        this.Data.Szczelina_wen.Dencity,
                        this.Data.Szczelina_wen.SpecyficHeat);
                    temp++;
                }
                collumn++;
            }

            for (int i = 0; i < this.Data.Cegla_klin.WidthPoints; i++)
            {
                for (int j = 1; j < this.Data.HeightPointNumber; j++)
                {
                    Elements[temp] = new Element(
                        temp + 1,
                        j + collumn * this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + collumn * this.Data.HeightPointNumber + +1,
                        this.Data.Cegla_klin.KFactor,
                        this.Data.Cegla_klin.Dencity,
                        this.Data.Cegla_klin.SpecyficHeat);
                    temp++;
                }
                collumn++;
            }

            for (int i = 0; i < this.Data.Powietrze.WidthPoints; i++)
            {
                for (int j = 1; j < this.Data.HeightPointNumber; j++)
                {
                    Elements[temp] = new Element(
                        temp + 1,
                        j + collumn * this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + collumn * this.Data.HeightPointNumber + +1,
                        this.Data.Powietrze.KFactor,
                        this.Data.Powietrze.Dencity,
                        this.Data.Powietrze.SpecyficHeat);
                    temp++;
                }
                collumn++;
            }

        }
        public void GenerateElementsWithStyropian()
        {
            int temp = 0;
            int collumn = 0;

            for (int i = 0; i < this.Data.Tynk_wew.WidthPoints; i++)
            {
                for (int j = 1; j < this.Data.HeightPointNumber; j++)
                {
                    Elements[temp] = new Element(
                        temp + 1,
                        j + collumn * this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + collumn * this.Data.HeightPointNumber + +1,
                        this.Data.Tynk_wew.KFactor,
                        this.Data.Tynk_wew.Dencity,
                        this.Data.Tynk_wew.SpecyficHeat);
                    temp++;
                }
                collumn++;
            }

            for (int i = 0; i < this.Data.Pustak_cer.WidthPoints; i++)
            {
                for (int j = 1; j < this.Data.HeightPointNumber; j++)
                {
                    Elements[temp] = new Element(
                        temp + 1,
                        j + collumn * this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + collumn * this.Data.HeightPointNumber + +1,
                        this.Data.Pustak_cer.KFactor,
                        this.Data.Pustak_cer.Dencity,
                        this.Data.Pustak_cer.SpecyficHeat);
                    temp++;
                }
                collumn++;
            }

            for (int i = 0; i < this.Data.Styropian.WidthPoints; i++)
            {
                for (int j = 1; j < this.Data.HeightPointNumber; j++)
                {
                    Elements[temp] = new Element(
                        temp + 1,
                        j + collumn * this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + collumn * this.Data.HeightPointNumber + +1,
                        this.Data.Styropian.KFactor,
                        this.Data.Styropian.Dencity,
                        this.Data.Styropian.SpecyficHeat);
                    temp++;
                }
                collumn++;
            }

            for (int i = 0; i < this.Data.Szczelina_wen.WidthPoints; i++)
            {
                for (int j = 1; j < this.Data.HeightPointNumber; j++)
                {
                    Elements[temp] = new Element(
                        temp + 1,
                        j + collumn * this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + collumn * this.Data.HeightPointNumber + +1,
                        this.Data.Szczelina_wen.KFactor,
                        this.Data.Szczelina_wen.Dencity,
                        this.Data.Szczelina_wen.SpecyficHeat);
                    temp++;
                }
                collumn++;
            }

            for (int i = 0; i < this.Data.Cegla_klin.WidthPoints; i++)
            {
                for (int j = 1; j < this.Data.HeightPointNumber; j++)
                {
                    Elements[temp] = new Element(
                        temp + 1,
                        j + collumn * this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + collumn * this.Data.HeightPointNumber + +1,
                        this.Data.Cegla_klin.KFactor,
                        this.Data.Cegla_klin.Dencity,
                        this.Data.Cegla_klin.SpecyficHeat);
                    temp++;
                }
                collumn++;
            }

            for (int i = 0; i < this.Data.Powietrze.WidthPoints; i++)
            {
                for (int j = 1; j < this.Data.HeightPointNumber; j++)
                {
                    Elements[temp] = new Element(
                        temp + 1,
                        j + collumn * this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + collumn * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + collumn * this.Data.HeightPointNumber + +1,
                        this.Data.Powietrze.KFactor,
                        this.Data.Powietrze.Dencity,
                        this.Data.Powietrze.SpecyficHeat);
                    temp++;
                }
                collumn++;
            }

        }
        public void GenerateElementsWithStyropian2()
        {
            int temp = 0;
            for (int i = 0; i < this.Data.WidthPointNumber - 1; i++)
            {
                for (int j = 1; j < this.Data.HeightPointNumber; j++)
                {
                    if (i <= 3)
                    {
                        Elements[temp] = new Element(
                        temp + 1,
                        j + i * this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + i * this.Data.HeightPointNumber + 1,
                        this.Data.Tynk_wew.KFactor,
                        this.Data.Tynk_wew.Dencity,
                        this.Data.Tynk_wew.SpecyficHeat);
                        temp++;
                    }
                    else if (i <= 3)
                    {
                        Elements[temp] = new Element(
                        temp + 1,
                        j + i * this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + i * this.Data.HeightPointNumber + 1,
                        this.Data.Pustak_cer.KFactor,
                        this.Data.Pustak_cer.Dencity,
                        this.Data.Pustak_cer.SpecyficHeat);
                        temp++;
                    }
                    else if (i <= 77)
                    {
                        Elements[temp] = new Element(
                        temp + 1,
                        j + i * this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + i * this.Data.HeightPointNumber + 1,
                        this.Data.Styropian.KFactor,
                        this.Data.Styropian.Dencity,
                        this.Data.Styropian.SpecyficHeat);
                        temp++;
                    }
                    else if (i <= 89)
                    {
                        Elements[temp] = new Element(
                        temp + 1,
                        j + i * this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + i * this.Data.HeightPointNumber + 1,
                        this.Data.Szczelina_wen.KFactor,
                        this.Data.Szczelina_wen.Dencity,
                        this.Data.Szczelina_wen.SpecyficHeat);
                        temp++;
                    }
                    else if (i <= 113)
                    {
                        Elements[temp] = new Element(
                        temp + 1,
                        j + i * this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + i * this.Data.HeightPointNumber + 1,
                        this.Data.Cegla_klin.KFactor,
                        this.Data.Cegla_klin.Dencity,
                        this.Data.Cegla_klin.SpecyficHeat);
                        temp++;
                    }
                    else if (i <= 123)
                    {
                        Elements[temp] = new Element(
                        temp + 1,
                        j + i * this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber,
                        j + i * this.Data.HeightPointNumber + this.Data.HeightPointNumber + 1,
                        j + i * this.Data.HeightPointNumber + 1,
                        this.Data.Powietrze.KFactor,
                        this.Data.Powietrze.Dencity,
                        this.Data.Powietrze.SpecyficHeat);
                        temp++;
                    }

                }
            }

        }
        public void DisplayElements()
        {
            string oneElemnt = "Element ID: {0,-4} ID1: {1,-4} ID2: {2,-4} ID3: {3,-4} ID4: {4,-4}";
            foreach (var item in this.Elements)
                Console.WriteLine(string.Format(oneElemnt, item.ElementID, item.ID[0], item.ID[1], item.ID[2], item.ID[3]));
            Console.WriteLine();
        }


        // Nodes
        public void GenerateNodesTestCase(GlobalData data)
        {

            double dH = this.Data.Height / (this.Data.HeightPointNumber - 1);
            double dW = this.Data.Width / (this.Data.WidthPointNumber - 1);
            int temp = 0;
            bool edgeCondition = false;

            for (int i = 0; i < this.Data.WidthPointNumber; i++)
            {
                for (int j = 0; j < this.Data.HeightPointNumber; j++)
                {
                    if (i == 0 || i == this.Data.WidthPointNumber - 1)
                    {
                        edgeCondition = true;
                    }
                    else if (j == 0 || j == this.Data.HeightPointNumber - 1)
                    {
                        edgeCondition = true;
                    }
                    else
                    {
                        edgeCondition = false;
                    }
                    this.Nodes[temp] = new Node(data.InitialTemperature, i * dW, j * dH, edgeCondition);
                    temp++;
                }
            }
        }
        public void GenerateNodes(GlobalData data)
        {

            double dH = 0.005;
            double dW = 0.005;
            int temp = 0;
            bool edgeCondition = false;

            for (int i = 0; i < this.Data.WidthPointNumber; i++)
            {
                for (int j = 0; j < this.Data.HeightPointNumber; j++)
                {
                    if (i == 0)
                    {
                        edgeCondition = true;
                    }
                    else
                    {
                        edgeCondition = false;
                    }
                    this.Nodes[temp] = new Node(data.InitialTemperature, i * dW, j * dH, edgeCondition);
                    temp++;
                }
            }
        }
        public void DisplayNodes()
        {
            string oneElemnt = "Node ID:{0,-4}  Node temperature: {1,-4} X: {2,-4:N4} Y: {3,-4:N4} BC: {4,-4}";
            int i = 1;
            foreach (var item in this.Nodes)
                Console.WriteLine(string.Format(oneElemnt, i++, item.Temperature, item.X, item.Y, item.EdgeCondition));
            Console.WriteLine();
        }
        


        // Elments + Nodes
        public void DisplayElementWithNodes(int elementId)
        {
            string oneElement = $@"Element ID: {Elements[elementId].ElementID,-4}
First Node: {Elements[elementId].ID[0],-5}  Node temperature: {Nodes[Elements[elementId].ID[0]].Temperature,-4} X: {Nodes[Elements[elementId].ID[0]].X,-4:N4} Y: {Nodes[Elements[elementId].ID[0]].Y,-4:N4}
Second Node: {Elements[elementId].ID[1],-4}  Node temperature: {Nodes[Elements[elementId].ID[1]].Temperature,-4} X: {Nodes[Elements[elementId].ID[1]].X,-4:N4} Y: {Nodes[Elements[elementId].ID[1]].Y,-4:N4}
Third Node: {Elements[elementId].ID[2],-5}  Node temperature: {Nodes[Elements[elementId].ID[2]].Temperature,-4} X: {Nodes[Elements[elementId].ID[2]].X,-4:N4} Y: {Nodes[Elements[elementId].ID[2]].Y,-4:N4}
Fourth Node: {Elements[elementId].ID[3],-4}  Node temperature: {Nodes[Elements[elementId].ID[3]].Temperature,-4} X: {Nodes[Elements[elementId].ID[3]].X,-4:N4} Y: {Nodes[Elements[elementId].ID[3]].Y,-4:N4}";
            Console.WriteLine(oneElement);
        }
    }
}
