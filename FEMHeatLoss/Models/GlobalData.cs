using System;
using System.Collections.Generic;
using System.Text;

namespace FEMHeatLoss.Models
{
    public class GlobalData
    {
        public readonly double Height;
        public readonly double Width;
        public readonly int HeightPointNumber;
        public readonly int WidthPointNumber;
        public readonly int NumberOfElements;
        public readonly int NumberOfNodes;
        public readonly int IntegrationSchemaWariant;
        public readonly double Alpha;
        public readonly double InitialTemperature;
        public readonly double AmbientTemperature;
        public readonly double Time;
        public readonly double dTime;
        public readonly Material Tynk_wew;
        public readonly Material Pustak_cer;
        public readonly Material Welna_min;
        public readonly Material Szczelina_wen;
        public readonly Material Cegla_klin;
        public readonly Material Powietrze;
        public readonly Material Styropian;


        public GlobalData()
        {

            // GlobalData.txt
            string[] lines = System.IO.File.ReadAllLines(@"Data/GlobalData.txt");

            string[] oneLine = lines[0].Split(' '); this.Height = Double.Parse(oneLine[0]);
            oneLine = lines[1].Split(' '); this.Width = Double.Parse(oneLine[0]);
            oneLine = lines[2].Split(' '); this.HeightPointNumber = Int32.Parse(oneLine[0]);
            oneLine = lines[3].Split(' '); this.WidthPointNumber = Int32.Parse(oneLine[0]);
            this.NumberOfElements = (HeightPointNumber - 1) * (WidthPointNumber - 1);
            this.NumberOfNodes = HeightPointNumber * WidthPointNumber;
            oneLine = lines[4].Split(' ');
            if (Int32.Parse(oneLine[0]) == 2 || Int32.Parse(oneLine[0]) == 3 || Int32.Parse(oneLine[0]) == 4)
                this.IntegrationSchemaWariant = Int32.Parse(oneLine[0]);
            else
                throw new ArgumentException("Integration schema wariat has to be set as value 2,3 or 4");
            oneLine = lines[5].Split(' '); this.Alpha = double.Parse(oneLine[0]);
            oneLine = lines[6].Split(' '); this.InitialTemperature = double.Parse(oneLine[0]);
            oneLine = lines[7].Split(' '); this.AmbientTemperature = double.Parse(oneLine[0]);
            oneLine = lines[8].Split(' '); this.Time = double.Parse(oneLine[0]);
            oneLine = lines[9].Split(' '); this.dTime = double.Parse(oneLine[0]);




            // MaterialData.txt
            lines = System.IO.File.ReadAllLines(@"C:\\Users\\barte\\OneDrive\\Pulpit\\Studia\\Rok_III\\Finite_Elements_Method\\FEMHeatLoss\\FEMHeatLoss\\Data\\MaterialData.txt");

            oneLine = lines[0].Split("*"); string name = oneLine[0];
            oneLine = lines[1].Split(' ');  int widthPoints = Int32.Parse(oneLine[0]);
            oneLine = lines[2].Split(' ');  double dencity = Double.Parse(oneLine[0]);
            oneLine = lines[3].Split(' ');  double kFactor = Double.Parse(oneLine[0]);
            oneLine = lines[4].Split(' ');  double specyficHeat = Double.Parse(oneLine[0]);
            this.Tynk_wew = new Material(name,widthPoints, dencity, kFactor, specyficHeat);

            oneLine = lines[5].Split("*"); name = oneLine[0];
            oneLine = lines[6].Split(' '); widthPoints = Int32.Parse(oneLine[0]);
            oneLine = lines[7].Split(' '); dencity = Double.Parse(oneLine[0]);
            oneLine = lines[8].Split(' '); kFactor = Double.Parse(oneLine[0]);
            oneLine = lines[9].Split(' '); specyficHeat = Double.Parse(oneLine[0]);
            this.Pustak_cer = new Material(name,widthPoints, dencity, kFactor, specyficHeat);

            oneLine = lines[10].Split("*"); name = oneLine[0];
            oneLine = lines[11].Split(' '); widthPoints = Int32.Parse(oneLine[0]);
            oneLine = lines[12].Split(' '); dencity = Double.Parse(oneLine[0]);
            oneLine = lines[13].Split(' '); kFactor = Double.Parse(oneLine[0]);
            oneLine = lines[14].Split(' '); specyficHeat = Double.Parse(oneLine[0]);
            this.Welna_min = new Material(name,widthPoints, dencity, kFactor, specyficHeat);

            oneLine = lines[15].Split("*"); name = oneLine[0];
            oneLine = lines[16].Split(' '); widthPoints = Int32.Parse(oneLine[0]);
            oneLine = lines[17].Split(' '); dencity = Double.Parse(oneLine[0]);
            oneLine = lines[18].Split(' '); kFactor = Double.Parse(oneLine[0]);
            oneLine = lines[19].Split(' '); specyficHeat = Double.Parse(oneLine[0]);
            this.Szczelina_wen = new Material(name,widthPoints, dencity, kFactor, specyficHeat);
            this.Powietrze = new Material("Powietrze", 10, dencity, kFactor, specyficHeat);

            oneLine = lines[20].Split("*"); name = oneLine[0];
            oneLine = lines[21].Split(' '); widthPoints = Int32.Parse(oneLine[0]);
            oneLine = lines[22].Split(' '); dencity = Double.Parse(oneLine[0]);
            oneLine = lines[23].Split(' '); kFactor = Double.Parse(oneLine[0]);
            oneLine = lines[24].Split(' '); specyficHeat = Double.Parse(oneLine[0]);
            this.Cegla_klin = new Material(name,widthPoints, dencity, kFactor, specyficHeat);

            oneLine = lines[25].Split("*"); name = oneLine[0];
            oneLine = lines[26].Split(' '); widthPoints = Int32.Parse(oneLine[0]);
            oneLine = lines[27].Split(' '); dencity = Double.Parse(oneLine[0]);
            oneLine = lines[28].Split(' '); kFactor = Double.Parse(oneLine[0]);
            oneLine = lines[29].Split(' '); specyficHeat = Double.Parse(oneLine[0]);
            this.Styropian = new Material(name, widthPoints, dencity, kFactor, specyficHeat);
        }

        public void DisplayData()
        {
            string oneLineOfData = "{0,-30} {1,10}";
            Console.WriteLine(string.Format(oneLineOfData, "Height:", this.Height));
            Console.WriteLine(string.Format(oneLineOfData, "Width:", this.Width));
            Console.WriteLine(string.Format(oneLineOfData, "Number of height points:", this.HeightPointNumber));
            Console.WriteLine(string.Format(oneLineOfData, "Number of width points:", this.WidthPointNumber));
            Console.WriteLine(string.Format(oneLineOfData, "Number of Elements:", this.NumberOfElements));
            Console.WriteLine(string.Format(oneLineOfData, "Number of Nodes:", this.NumberOfNodes));
            Console.WriteLine(string.Format(oneLineOfData, "Integration schema wariant:", this.IntegrationSchemaWariant));
            Console.WriteLine(string.Format(oneLineOfData, "Alpha:", this.Alpha));
            Console.WriteLine(string.Format(oneLineOfData, "Initial Temperature:", this.InitialTemperature));
            Console.WriteLine(string.Format(oneLineOfData, "Initial Ambient:", this.AmbientTemperature));
            Console.WriteLine(string.Format(oneLineOfData, "Time: ", this.Time));
            Console.WriteLine(string.Format(oneLineOfData, "dTime: ", this.dTime));
            Console.WriteLine();
        }
        public void DisplayMaterialData()
        {
            string oneLineOfData = @"Material: {0,-30}
Length: {1,-20}
Dencity: {2,-30}
K Factor: {3,-20}
Specyfic Heat: {4,-20}";
            Console.WriteLine(string.Format(oneLineOfData, this.Tynk_wew.Name, (this.Tynk_wew.WidthPoints * 0.5) + "cm", this.Tynk_wew.Dencity, this.Tynk_wew.KFactor, this.Tynk_wew.SpecyficHeat)); Console.WriteLine();
            Console.WriteLine(string.Format(oneLineOfData, this.Pustak_cer.Name, (this.Pustak_cer.WidthPoints * 0.5) + "cm", this.Pustak_cer.Dencity, this.Pustak_cer.KFactor, this.Pustak_cer.SpecyficHeat)); Console.WriteLine();
            Console.WriteLine(string.Format(oneLineOfData, this.Welna_min.Name, (this.Welna_min.WidthPoints * 0.5) + "cm", this.Welna_min.Dencity, this.Welna_min.KFactor, this.Welna_min.SpecyficHeat)); Console.WriteLine();
            Console.WriteLine(string.Format(oneLineOfData, this.Szczelina_wen.Name, (this.Szczelina_wen.WidthPoints * 0.5) + "cm", this.Szczelina_wen.Dencity, this.Szczelina_wen.KFactor, this.Szczelina_wen.SpecyficHeat)); Console.WriteLine();
            Console.WriteLine(string.Format(oneLineOfData, this.Cegla_klin.Name, (this.Cegla_klin.WidthPoints * 0.5) + "cm", this.Cegla_klin.Dencity, this.Cegla_klin.KFactor, this.Cegla_klin.SpecyficHeat)); Console.WriteLine();
            Console.WriteLine(string.Format(oneLineOfData, this.Powietrze.Name, (this.Powietrze.WidthPoints * 0.5) + "cm", this.Powietrze.Dencity, this.Powietrze.KFactor, this.Powietrze.SpecyficHeat)); Console.WriteLine();

        }

    }

    public class Material
    {
        public string Name { get; }
        public int WidthPoints { get;}
        public double Dencity { get; }
        public double KFactor { get; }
        public double SpecyficHeat { get; }

        public Material() { }
        public Material(string name,int width, double dencity, double kFactor, double specyficHeat)
        {
            this.Name = name;
            this.WidthPoints = width;
            this.Dencity = dencity;
            this.KFactor = kFactor;
            this.SpecyficHeat = specyficHeat;
        }

    }
}
