namespace e_info_22maj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader fileReadStream = File.OpenText("utca.txt");

            Dictionary<Classification, int> PricesByClassification = new Dictionary<Classification, int>();
            string[] PricesOfClassifications = fileReadStream.ReadLine().Split(" ");
            PricesByClassification.Add(Classification.A, int.Parse(PricesOfClassifications[0]));
            PricesByClassification.Add(Classification.B, int.Parse(PricesOfClassifications[1]));
            PricesByClassification.Add(Classification.C, int.Parse(PricesOfClassifications[2]));

            int ado(Classification classification, int BuildingArea)
            {
                return BuildingArea * PricesByClassification[classification];
            }

            Dictionary<int, Owner> Owners = new();
            List<Plot> Plots = new();
            List<string> Streets = new();

            while (!fileReadStream.EndOfStream) {
                string[] entries = fileReadStream?.ReadLine().Split(" ");
                int taxID = int.Parse(entries[0]);
                string street = entries[1];
                string address = entries[2];
                Classification classification;
                switch(entries[3])
                {
                    case "A":
                        classification = Classification.A;
                        break;
                    case "B":
                        classification = Classification.B;
                        break;
                    case "C":
                        classification = Classification.C;
                        break;
                    default: 
                        classification = Classification.None; 
                        break;
                }
                int buildingArea = int.Parse(entries[4]);

                if(!Owners.ContainsKey(taxID)) {
                    Owners.Add(taxID, new Owner(taxID));
                }

                Plot plot = new(street, address, classification, buildingArea);
                Owners[taxID].Plots.Add(plot);
                Plots.Add(plot);

                if (!Streets.Contains(street))
                {
                    Streets.Add(street);
                }
            }
            fileReadStream.Close();

            Console.WriteLine($"2. feladat. A mintában {Plots.Count} telek szerepel.");

            try
            {

                int taxIDOfSelectedOwner = int.Parse(Console.ReadLine());
                Owner selectedOwner = Owners[taxIDOfSelectedOwner];

                Console.WriteLine($"3. feladat. Egy tulajdonos adószáma: {selectedOwner.TaxID}");
                foreach (Plot plot in selectedOwner.Plots)
                {
                    Console.WriteLine($"{plot.Street} utca {plot.Address}");
                }

            }
            catch (Exception ex) 
            {
                Console.WriteLine("Nem szerepel az adatállományban.");
            }

            Console.WriteLine("5.feladat");
            var PlotsWithClassificationA = Plots.Where((plot) => plot.Classification == Classification.A);
            var PlotsWithClassificationB = Plots.Where((plot) => plot.Classification == Classification.B);
            var PlotsWithClassificationC = Plots.Where((plot) => plot.Classification == Classification.C);
            Console.WriteLine($"A sávba {PlotsWithClassificationA.Count()} telek esik, az adó {PlotsWithClassificationA.Aggregate(0, (prev, current) => prev + ado(current.Classification, current.BuildingArea))} Ft.");
            Console.WriteLine($"B sávba {PlotsWithClassificationB.Count()} telek esik, az adó {PlotsWithClassificationB.Aggregate(0, (prev, current) => prev + ado(current.Classification, current.BuildingArea))} Ft.");
            Console.WriteLine($"C sávba {PlotsWithClassificationC.Count()} telek esik, az adó {PlotsWithClassificationC.Aggregate(0, (prev, current) => prev + ado(current.Classification, current.BuildingArea))} Ft.");

            foreach(string street in Streets)
            {
                int countOfPlotsInStreetWithClassificationA = Plots.Where((plot) => plot.Classification == Classification.A && plot.Street == street).Count();
                int countOfPlotsInStreetWithClassificationB = Plots.Where((plot) => plot.Classification == Classification.B && plot.Street == street).Count();
                int countOfPlotsInStreetWithClassificationC = Plots.Where((plot) => plot.Classification == Classification.C && plot.Street == street).Count();
            
                if (countOfPlotsInStreetWithClassificationA > 0) 
                { 
                    if (countOfPlotsInStreetWithClassificationB > 0 || countOfPlotsInStreetWithClassificationC > 0)
                    {
                        Console.WriteLine(street);
                    }
                } 
                else if (countOfPlotsInStreetWithClassificationB > 0)
                {
                    if (countOfPlotsInStreetWithClassificationA > 0 || countOfPlotsInStreetWithClassificationC > 0)
                    {
                        Console.WriteLine(street);
                    }
                }
                else if (countOfPlotsInStreetWithClassificationC > 0)
                {
                    if (countOfPlotsInStreetWithClassificationA > 0 || countOfPlotsInStreetWithClassificationB > 0)
                    {
                        Console.WriteLine(street);
                    }
                }
            }

            StreamWriter fileWriteStream = new StreamWriter("fizetendo.txt");
            foreach (Owner owner in Owners.Values)
            {
                fileWriteStream.WriteLine($"{owner.TaxID} {owner.Plots.Aggregate(0, (prev, curr) => prev + ado(curr.Classification, curr.BuildingArea))}");
            }
        }
    }
}