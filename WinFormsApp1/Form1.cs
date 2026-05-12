using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new List<Emitter>();
        RadarPoint radarPoint;
        TopEmitter emitter;

        Color[] colors = new Color[]{Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet};

        public Form1()
        {
            InitializeComponent();

            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            this.emitter = new TopEmitter
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

            emitters.Add(this.emitter);

            radarPoint=new RadarPoint
            {
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2,
                Radius = 50,
                HighlightColor = Color.Lime
            };

            emitter.impactPoints.Add(radarPoint);

            picDisplay.MouseWheel += picDisplay_MouseWheel;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState();

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g);
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
