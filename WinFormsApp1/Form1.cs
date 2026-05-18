using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new List<Emitter>();
        RadarPoint radarPoint;
        Emitter emitter;
        ReboundPoint point;
        Emitter ReboundEmitter;
        TopEmitter topEmitter;
        Color[] colors = new Color[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet };

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
                GravitationY = 0.35f
            };

            emitters.Add(topEmitter);

            this.emitter = new Emitter
            {
                Direction = 90,
                Spreading = 200,
                SpeedMin = 5,
                SpeedMax = 10,
                ColorFrom = Color.Gold,
                ColorTo = Color.FromArgb(0, Color.Red),
                ParticlesPerTick = 40,
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2,
                GravitationY = 0.2f,
                GravitationX = 0
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

                emitter.impactPoints.Add(point);
            }

            radarPoint =new RadarPoint
            {
                X = picDisplay.Width / 2-60,
                Y = picDisplay.Height / 2,
                Radius = 50,
                HighlightColor = Color.Lime
            };

            emitter.impactPoints.Add(radarPoint);

            picDisplay.MouseWheel += picDisplay_MouseWheel;

            ReboundEmitter = new Emitter
            {
                Direction = -36,
                Spreading = 30,
                SpeedMin = 10,
                SpeedMax = 10,
                ColorFrom = Color.Gold,
                ColorTo = Color.FromArgb(0, Color.Red),
                ParticlesPerTick = 10,
                X = 40,
                Y = 40,
            };

            emitters.Add(ReboundEmitter);
            emitters.Add(this.emitter);

            point = new ReboundPoint
            {
                X = picDisplay.Width / 2 + 100,
                Y = picDisplay.Height / 2 + 100,
                Radius = 30
            };

            emitter.impactPoints.Add(new ReboundPoint
            {
                X = 280,
                Y = 280,
                Radius = 40
            });

            emitter.impactPoints.Add(new ReboundPoint
            {
                X = 300,
                Y = 130,
                Radius = 40
            });
            emitter.impactPoints.Add(point);

            topEmitter.impactPoints.Add(radarPoint);
            ReboundEmitter.impactPoints.Add(radarPoint);
            emitter.impactPoints.Add(radarPoint);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach(var emitter in emitters)
            {
                emitter.UpdateState();
            }

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                foreach(var emitter in emitters)
                {
                    emitter.Render(g);
                }
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

    }
}
