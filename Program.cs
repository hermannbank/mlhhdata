using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Data
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (var reader = new StreamReader(@"SpermVision-motility-20201216-1678-2.txt"))
            {
                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                var data = new List<Data>();
                var dic = new Dictionary<int, List<double>>();
                var i = 0;
                while (!reader.EndOfStream)
                {
                    // Console.WriteLine(i);
                    i += 1;

                    // if(i > 10){
                    //     break;
                    // }

                    var line = reader.ReadLine();
                    var values = line.Split("\t");

                    if (int.TryParse(values[0], out int ids))
                    {
                        // listA.Add(values[0]);
                        // listB.Add(values[21]);
                        // data.Add(new Data(){Mod = int.Parse(values[21]), Modid = int.Parse(values[0])});
                        // Console.WriteLine($"values[0] {values[21]}");
                        var id = int.Parse(values[0]);
                        // Console.WriteLine($"key {id}");
                        if (!dic.ContainsKey(id))
                        {
                            dic.Add(int.Parse(values[0]), new List<double>());
                        }
                        else
                        {
                            dic[int.Parse(values[0])].Add(double.Parse(values[21]));
                        }

                    }
                    // Console.WriteLine(i);

                }
                Console.WriteLine($"Number of keys {dic.Keys.Count}");
                string path = @"Results.txt";
                // This text is added only once to the file.
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {

                        foreach (var key in dic.Keys)
                        {
                            var orderedList = dic[key].OrderBy(x => x).ToArray();
                            var numberOfItems = dic[key].Count;
                            var otofem = Convert.ToInt32(Math.Ceiling(0.25 * numberOfItems));
                            var t = orderedList.Skip(otofem).Take(otofem * 2).Average();
                            Console.WriteLine($"key: {key}, number of values {dic[key].Count} with average {dic[key].Average()}, trimmean: {t}");
                            sw.WriteLine($"key: {key}, number of values {dic[key].Count} with average {dic[key].Average()}, trimmean: {t}");
                        }
                    }
                }
            }
        }
    }

    public class Data
    {
        public int Modid { get; set; }
        public int Mod { get; set; }
    }
}
