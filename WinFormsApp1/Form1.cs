using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new List<Emitter>();
        //Emitter emitter;
        //GravityPoint point1;
        //GravityPoint point2;
        //ReboundPoint point;
        RadarPoint radarPoint;
        TopEmitter emitter;
        List<ChangeColorPoint> colorPoints = new List<ChangeColorPoint>();

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

            //point = new ReboundPoint
            //{
            //    X = picDisplay.Width / 2 + 100,
            //    Y = picDisplay.Height / 2 + 100,
            //    Radius = 30
            //};

            //emitter.impactPoints.Add(new ReboundPoint
            //{
            //    X = 280,
            //    Y = 280,
            //    Radius = 40
            //});

            //emitter.impactPoints.Add(new ReboundPoint
            //{
            //    X = 300,
            //    Y = 130,
            //    Radius = 40
            //});
            //emitter.impactPoints.Add(point);

            //CreateRainbowPoints();
        }

        //private void CreateRainbowPoints()
        //{
        //    int space = 100;
        //    int y = picDisplay.Height/2-100;

        //    for (int i = 0; i < colors.Length; i++)
        //    {
        //        var point = new ChangeColorPoint
        //        {
        //            X = space*(i+1),
        //            Y = y,
        //            Radius=46,
        //            NewColor=colors[i]
        //        };

        //        colorPoints.Add(point);
        //        emitter.impactPoints.Add(point);
        //    }
        //}

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

        private int MousePositionX;
        private int MousePositionY;

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            radarPoint.X = e.X;
            radarPoint.Y = e.Y;

            //emitter.MousePositionX = e.X;
            //emitter.MousePositionY = e.Y;

            //point.X = e.X;
            //point.Y = e.Y;
        }

        //private void tbDirection_Scroll(object sender, EventArgs e)
        //{
        //    emitter.Direction = tbDirection.Value;
        //    lblDirection.Text = $"{tbDirection.Value}°";
        //}

        //private void tbGraviton_Scroll(object sender, EventArgs e)
        //{
        //    point1.Power = tbGraviton.Value;
        //}

        //private void tbGraviton2_Scroll(object sender, EventArgs e)
        //{
        //    point2.Power = tbGraviton2.Value;
        //}
    }
}
