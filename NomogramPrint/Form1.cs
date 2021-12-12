using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

using ComponentFactory.Krypton.Toolkit;

using NomogramPrint.Drawing;
using NomogramPrint.Models;
using NomogramPrint.Servicies;


using NomogramPrint.Settings;

namespace NomogramPrint
{
    public partial class MainWindow : KryptonForm
    {

        private DrawSettings _drawSettings;
        private Drawer _drawer;
        private ExcelParser _excelParser;
        private DbContext _context;
        private Image _image;
        private bool _isDataReady;
        private bool _isPreparingData;

        public MainWindow()
        {
            InitializeComponent();
            _isDataReady = false;
            _isPreparingData = false;
        }

        private Point GetTopPoint(Point point) =>
            new Point(point.X + point.Y, 0);

        // Печать
        private void kryptonButton1_Click(object sender, EventArgs e)
        {

            
        }

        private async void kryptonButton4_Click(object sender, EventArgs e)
        {
            if (_isPreparingData)
            {
                MessageBox.Show("Данные уже считываются", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var thread = new Thread(new ThreadStart(PrepareData));
            _isDataReady = false;

            _isPreparingData = true;
            thread.Start();

        }

        private async void PrepareData()
        {
            try
            {
                MessageBox.Show("Происходит считывание данных", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var kilometerSettings = new KilometerSettings(30, 200, 400, Color.Black);
                kilometerSettings.Cells = new List<Cell>
                {
                new Cell(0.15f, CellContentType.None),
                new Cell(0.2f, CellContentType.None),
                new Cell(0.2f, CellContentType.Number),
                new Cell(0.45f, CellContentType.Objects)
                };

                var lightSettings = new LightSettings(30, 200, 400, Color.Blue);
                var boundariesSettings = new StationBoundariesSettings(30, 240, 400, Color.Red, new float[] { 10, 4 });
                var brakeSettings = new BrakeSettings(30, 200, 400, Color.Brown);


                _drawSettings = new DrawSettings(kilometerSettings, lightSettings, boundariesSettings, brakeSettings);
                _drawer = new Drawer(_drawSettings);

                _excelParser = new ExcelParser();
                _context = _excelParser.GetData();
                _isDataReady = true;
                _isPreparingData = false;
                MessageBox.Show("Данные успешно считаны", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка", "Ошибка");
            }

        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            if (!_isDataReady)
            {
                MessageBox.Show("Данные ещё не загружены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var width = (int)(_context.Kilometers.Count * _drawSettings.KilometerSettings.Width +
                _drawSettings.KilometerSettings.Width * 100);
            var test = new Bitmap(width, 1000);

            using (Graphics graphics = Graphics.FromImage(test))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                _drawer.DrawKilometers(graphics, _context.Kilometers);
                _drawer.DrawLights(graphics, _context.Kilometers, _context.Lights);
                _drawer.DrawBoundaries(graphics, _context.Kilometers, _context.StationsBoundaries);
                _drawer.DrawBrakes(graphics, _context.Kilometers, _context.BrakeTests);

            }

            _image = (Image)test;

            

            PictureBox picture = new PictureBox();
            picture.Image = _image;
            picture.SizeMode = PictureBoxSizeMode.AutoSize;

            panel1.AutoScroll = true;
            panel1.Controls.Add(picture);

            //pictureBox1.CreateGraphics().DrawImage(img, new Point(0, 0));

        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if(_image == null)
            {
                MessageBox.Show("Номограмма не построена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                _image.Save(AppDomain.CurrentDomain.BaseDirectory + @"\files\Nomogram.png");
                MessageBox.Show("Номограмма успешно сохранена", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
