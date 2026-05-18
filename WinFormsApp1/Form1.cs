using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        RadarPoint radarPoint;
        ReboundPoint point;
        TopEmitter topEmitter;
        Color[] colors = new Color[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet };
        List<TrackBar> trackBars = new List<TrackBar>();
        List<ChangeColorPoint> colorPoints = new List<ChangeColorPoint>();

        public Form1()
        {
            InitializeComponent();

            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            topEmitter = new TopEmitter
            {
                Width = picDisplay.Width,
                Direction = 270,
                Spreading = 30,
                SpeedMin = 1,
                SpeedMax = 3,
                ColorFrom = Color.White,
                ColorTo = Color.FromArgb(0, Color.White),
                ParticlesPerTick = 30,
                X = picDisplay.Width / 2,
                Y = 0,
                GravitationY = 0.1f
            };

            for (int i = 0; i < colors.Length; i++)
            {
                var point = new ChangeColorPoint
                {
                    X = 100 * (i + 1),
                    Y = 120,
                    Radius = 46,
                    NewColor = colors[i]
                };

                topEmitter.impactPoints.Add(point);
                colorPoints.Add(point);
            }

            radarPoint = new RadarPoint
            {
                X = picDisplay.Width / 2 - 60,
                Y = picDisplay.Height / 2,
                Radius = 50,
                HighlightColor = Color.Lime
            };

            topEmitter.impactPoints.Add(radarPoint);

            picDisplay.MouseWheel += picDisplay_MouseWheel;

            topEmitter.impactPoints.Add(new ReboundPoint
            {
                X = 280,
                Y = 350,
                Radius = 30
            });

            topEmitter.impactPoints.Add(new ReboundPoint
            {
                X = 800,
                Y = 200,
                Radius = 40
            });

            topEmitter.impactPoints.Add(radarPoint);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            topEmitter.UpdateState();

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                topEmitter.Render(g);
            }

            picDisplay.Invalidate();
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            radarPoint.X = e.X;
            radarPoint.Y = e.Y;
        }

        private void picDisplay_MouseWheel(object sender, MouseEventArgs e)
        {
            radarPoint.Radius += e.Delta > 0 ? 5 : -5;

            if (radarPoint.Radius < 20) radarPoint.Radius = 20;
            if (radarPoint.Radius > 150) radarPoint.Radius = 150;

            this.Text = $"Радар: {radarPoint.Radius}px";
        }

        private void tbChangePlace_Scroll(object sender, EventArgs e)
        {
            var count = 1;
            foreach(var color in colorPoints)
            {
                color.X = tbChangePlace.Value * count;
                count++;
            }
        }
    }
}
