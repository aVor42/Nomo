using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NomogramPrint.Models;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;



namespace NomogramPrint.Servicies
{
    public class ExcelParser
    {
        //Test Git
        private Application _app;

        public DbContext GetData()
        {
            
            DbContext context = new DbContext();
            var currentDir = AppDomain.CurrentDomain.BaseDirectory;

            var km = GetKm(AppDomain.CurrentDomain.BaseDirectory + @"\files\Километры.xlsx");
            var objectsCollections = new List<IObject>[]{
                GetKilometersObjects<KTSM>(),
                GetKilometersObjects<UKSPS>(),
                GetKilometersObjects<Cliffs>(),
                GetKilometersObjects<LEP>(),
                GetKilometersObjects<Crossing>(),
                GetKilometersObjects<CrossingWithControll>()
            };

            context.Lights = GetLights(AppDomain.CurrentDomain.BaseDirectory + @"\files\Светофоры.xlsx");
            context.StationsBoundaries = GetStationsBoundaries(AppDomain.CurrentDomain.BaseDirectory + @"\files\Границы станций.xlsx");
            context.BrakeTests = GetBrakes(AppDomain.CurrentDomain.BaseDirectory + @"\files\Проверки торомозов.xlsx");

            for (int i = km.Item2; i > km.Item1; i--)
            {
                var kilometer = new Kilometer { Number = i };
                foreach(var objects in objectsCollections)
                {
                    var obj = objects.FirstOrDefault(x => kilometer.Number == x.Kilometers + Math.Ceiling(x.Meters / 1000d));
                    if (obj != null)
                        kilometer.Objects.Add(obj);
                }
                context.Kilometers.Add(kilometer);
            }
                
            return context;
        }

        private List<IObject> GetKilometersObjects<T>() where T:IObject, new ()
        {
            var fileName = "";
            switch (new T())
            {
                case KTSM ktsm:
                    fileName = AppDomain.CurrentDomain.BaseDirectory + @"\files\КТСМ.xlsx";
                    break;
                case UKSPS uksps:
                    fileName = AppDomain.CurrentDomain.BaseDirectory + @"\files\УКСПС.xlsx";
                    break;
                case Cliffs cliffs:
                    fileName = AppDomain.CurrentDomain.BaseDirectory + @"\files\Обрывы.xlsx";
                    break;
                case LEP lep:
                    fileName = AppDomain.CurrentDomain.BaseDirectory + @"\files\ЛЭП.xlsx";
                    break;
                case Crossing cross:
                    fileName = AppDomain.CurrentDomain.BaseDirectory + @"\files\Переезды без дежурных.xlsx";
                    break;
                case CrossingWithControll cross:
                    fileName = AppDomain.CurrentDomain.BaseDirectory + @"\files\Переезды с дежурными.xlsx";
                    break;
                default:
                    return new List<IObject>();
            }
            return GetKilometersObjects(fileName,
                        (kilometers, meters) => new T { Kilometers = kilometers, Meters = meters });
        }

        private List<IObject> GetKilometersObjects(string fileName, Func<int, int, IObject> factory)
        {
            var kilometerObjects = new List<IObject>();
            var range = ReadFile(fileName);

            int rows = range.Rows.Count;
            int cols = range.Columns.Count;
            for (int i = 1; i <= rows; i++)
            {

                if (range.Cells[i, 1] == null ||
                    range.Cells[i, 1].Value2 == null ||
                    range.Cells[i, 2] == null ||
                    range.Cells[i, 2].Value2 == null)
                    break;

                var kilometers = (int)range.Cells[i, 1].Value2;
                var meters = (int)range.Cells[i, 2].Value2;

                kilometerObjects.Add(factory(kilometers, meters));

            }

            _app.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(_app);
            return kilometerObjects;
        }



        private Range ReadFile(string path)
        {
            _app = new Application();
            var excelBook = _app.Workbooks.Open(path);
            var excelSheet = excelBook.Sheets[1];
            var excelRange = excelSheet.UsedRange;
            return excelRange;
        }

        private (int, int) GetKm(string fileName)
        {
            var range = ReadFile(fileName);

            if (range.Cells[1, 1] == null ||
                range.Cells[1, 1].Value2 == null ||
                range.Cells[1, 2] == null ||
                range.Cells[1, 2].Value2 == null)
                throw new Exception("Неправильная структура файла километража");


            var startKm = (int)range.Cells[1, 1].Value2;
            var endKm = (int)range.Cells[1, 2].Value2;

            _app.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(_app);
            return (startKm, endKm);
        }


        private List<Light> GetLights(string fileName)
        {
            var lights = new List<Light>();
            var range = ReadFile(fileName);

            int rows = range.Rows.Count;
            int cols = range.Columns.Count;
            for (int i = 1; i <= rows; i++)
            {
                if (range.Cells[i, 1] == null ||
                    range.Cells[i, 1].Value2 == null ||
                    range.Cells[i, 2] == null ||
                    range.Cells[i, 2].Value2 == null)
                    break;

                var kilometers = (int)range.Cells[i, 1].Value2;
                var meters = (int)range.Cells[i, 2].Value2;
                lights.Add(new Light(kilometers, meters));
            }

            _app.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(_app);
            return lights;
        }

        // Границы станций
        private List<StationBoundaries> GetStationsBoundaries(string fileName)
        {
            var boundaries = new List<StationBoundaries>();
            var range = ReadFile(fileName);

            int rows = range.Rows.Count;
            for (int i = 1; i <= rows; i++)
            {
                if (range.Cells[i, 1] == null ||
                    range.Cells[i, 1].Value2 == null)
                    break;

                var name = (string)range.Cells[i, 1].Value2;
                var inKilometers = range.Cells[i, 2]?.Value2 == null? 0: (int)range.Cells[i, 2]?.Value2;
                var inMeters = range.Cells[i, 3]?.Value2 == null ? 0 : (int)range.Cells[i, 3]?.Value2;
                var outKilometers = range.Cells[i, 4]?.Value2 == null ? 0 : (int)range.Cells[i, 4]?.Value2;
                var outMeters = range.Cells[i, 5]?.Value2 == null ? 0 : (int)range.Cells[i, 5]?.Value2;
                boundaries.Add(new StationBoundaries
                {
                    Name = name,
                    InKilometers = inKilometers,
                    InMeters = inMeters,
                    OutKilometers = outKilometers,
                    OutMeters = outMeters
                });
            }

            _app.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(_app);
            return boundaries;
        }

        // Тормоза
        private List<BrakeTest> GetBrakes(string fileName)
        {
            var brekes = new List<BrakeTest>();
            var range = ReadFile(fileName);

            int rows = range.Rows.Count;
            for (int i = 1; i <= rows; i++)
            {
                if (range.Cells[i, 1] == null ||
                    range.Cells[i, 1].Value2 == null)
                    break;

                var kilometers = (int)range.Cells[i, 1].Value2;
                var meters = (int)range.Cells[i, 2].Value2;
                var velocity = (int)range.Cells[i, 3].Value2;
                var distance = (int)range.Cells[i, 4].Value2;

                brekes.Add(new BrakeTest
                {
                    Kilometers = kilometers,
                    Meters = meters,
                    MaxVelocity = velocity,
                    MaxDistance = distance
                });
            }

            _app.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(_app);
            return brekes;
        }

    }
}