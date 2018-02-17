using System.Collections.Generic;
using System.Data;
using System.IO;

namespace CarCollection
{
    public static class ReadAndWriteFile
    {
    //    public static string PathCars = @"D:\Cars.txt";
    //    public static string PathBrands = @"D:\Brands.txt";
    //    public static string PathModels = @"D:\Models.txt";
    //    public static string PathSolds = @"D:\Solds.txt";
    //    public static string PathDeleteds = @"D:\Deleted.txt";

        public static void SaveCars(Car car)
        {
            StreamWriter sw = File.AppendText(Resources.PathCars);
           
            sw.WriteLine(car.ToString());
            sw.Close();
        }

        private static bool IsBrand(Brand brand)
        {
            StreamReader sr = File.OpenText(Resources.PathBrands);
            while (!sr.EndOfStream)
            {
                if (sr.ReadLine().ToUpper() != brand.Name.ToUpper())
                {
                    continue;
                }
                else
                {
                    sr.Close();
                    return true;
                }
            }

            sr.Close();
            return false;
        }

        public static void SaveBrands(Brand brand)
        {
            if (IsBrand(brand)== false)
            {
                StreamWriter sw = File.AppendText(Resources.PathBrands);

                sw.WriteLine(brand.ToString());
                sw.Close();
            }
        }

        public static void SaveModels(Model model,Brand brand)
        {
            string path = $"D:\\{brand.Name}.txt";

            StreamWriter sw = File.AppendText(@path);
            sw.WriteLine(model.ToString());
            sw.Close();            
        }

        public static DataTable Load( Brand brnd = null, Model mdl = null)
        {
            DataTable table = new DataTable();

            table.Columns.Add("Brand", typeof(string));
            table.Columns.Add("Model", typeof(string));
            table.Columns.Add("Color", typeof(string));
            table.Columns.Add("Year", typeof(int));
            table.Columns.Add("Price", typeof(decimal));

            if(brnd!=null)
            {
               if(mdl==null)
                {
                    string path = $"D:\\{brnd.Name}.txt";

                    StreamReader srModels = File.OpenText(@path);

                    Brand brand = StringToBrand(@path);

                    while (!srModels.EndOfStream)
                    {
                        Model model = StringToModel(srModels.ReadLine());

                        StreamReader srCars = File.OpenText(Resources.PathCars);
                        while (!srCars.EndOfStream)
                        {
                            Car car = StringLineToCar(srCars.ReadLine());

                            if (car.Model.Name == model.Name)
                            {
                                table.Rows.Add(brand.Name, car.Model, car.Color, car.Year, car.Price);

                            }
                        }
                        srCars.Close();
                    }
                    srModels.Close();
                    return table;

                }

                else
                {
                        StreamReader srCars = File.OpenText(Resources.PathCars);

                        while (!srCars.EndOfStream)
                        {
                            Car car = StringLineToCar(srCars.ReadLine());

                            if (car.Model.Name == mdl.Name)
                            {
                                table.Rows.Add(brnd.Name, car.Model, car.Color, car.Year, car.Price);

                            }
                        }
                        srCars.Close();
                   
                    return table;

                }
            }
            else
            {
                StreamReader srBrands = File.OpenText(Resources.PathBrands);

                while (!srBrands.EndOfStream)
                {
                    string path = $"D:\\{srBrands.ReadLine()}.txt";

                    StreamReader srModels = File.OpenText(@path);

                    Brand brand = StringToBrand(@path);

                    while (!srModels.EndOfStream)
                    {
                        Model model = StringToModel(srModels.ReadLine());

                        StreamReader srCars = File.OpenText(Resources.PathCars);
                        while (!srCars.EndOfStream)
                        {
                            Car car = StringLineToCar(srCars.ReadLine());

                            if (car.Model.Name == model.Name)
                            {
                                table.Rows.Add(brand.Name, car.Model, car.Color, car.Year, car.Price);

                            }
                        }
                        srCars.Close();
                    }
                    srModels.Close();
                }
                srBrands.Close();

                return table;
            }
        }

        private static Brand StringToBrand(this string path)
        {
            StreamReader sr = File.OpenText(path);
            List<Model> models = new List<Model>();

            while (!sr.EndOfStream)
            {
                models.Add(new Model(sr.ReadLine()));
            }
            sr.Close();

            int length = path.IndexOf('.') - path.IndexOf(':')-2;

            Brand brand = new Brand()
            {
                Name = path.Substring(path.IndexOf(':')+2,length),
                Models = models
            };

            return brand;
        }

        private static Model StringToModel(this string line)
        {
            Model model = new Model()
            {
                Name = line
            };

            return model;
        }

        private static Car StringLineToCar(this string line)
        {
            List<string> liststr = new List<string>();

            foreach (string item in line.Split('~'))
            {
                liststr.Add(item);
            }

            Car car = new Car()
            {
                Model = new Model(liststr[0]),
                Color = liststr[1],
                Year = System.Convert.ToInt32(liststr[2]),
                Price = System.Convert.ToDecimal(liststr[3]),
            };

            return car;
        }

        public static IEnumerable<Brand> GetBrands ()
        {
            StreamReader sr = File.OpenText(Resources.PathBrands);

            while (!sr.EndOfStream)
            {
                yield return new Brand(sr.ReadLine());
            }

            sr.Close();
        }

        public static IEnumerable<Model> GetModels(Brand brand)
        {
            string path = $"D:\\{brand.Name}.txt";

            StreamReader sr = File.OpenText(path);

            while (!sr.EndOfStream)
            {
                yield return new Model(sr.ReadLine());
            }
            sr.Close();
        }
    }
}
