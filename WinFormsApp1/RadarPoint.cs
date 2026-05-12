using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class RadarPoint : IImpactPoint
    {
        public int Radius = 50;
        public Color HighlightColor = Color.Lime;
        public int ParticlesCount = 0;

        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;
            double r = Math.Sqrt(gX * gX + gY * gY);

            bool isInside = (r + particle.Radius < Radius);

            if (particle is ParticleColorful colorful)
            {
                if (isInside)
                {
                    colorful.FromColor = HighlightColor;
                    colorful.ToColor = Color.FromArgb(0, HighlightColor);
                }
                else
                {
                    colorful.FromColor = Color.White;
                    colorful.ToColor = Color.FromArgb(0, Color.White);
                }
            }
        }

        public override void Render(Graphics g)
        {
            var pen = new Pen(HighlightColor, 2);
            g.DrawEllipse(pen, X - Radius, Y - Radius, Radius * 2, Radius * 2);

            var brush = new SolidBrush(Color.FromArgb(30, HighlightColor));
            g.FillEllipse(brush, X - Radius, Y - Radius, Radius * 2, Radius * 2);

            var stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            var font = new Font("Verdana", 12);
            var textBrush = new SolidBrush(HighlightColor);

            g.DrawString(
                $"{ParticlesCount}",
                font,
                textBrush,
                X,
                Y,
                stringFormat
            );

            font.Dispose();
            textBrush.Dispose();
        }

        public void CountParticles(List<Particle> particles)
        {
            ParticlesCount = 0;

            foreach (var particle in particles)
            {
                float gX = X - particle.X;
                float gY = Y - particle.Y;
                double r = Math.Sqrt(gX * gX + gY * gY);

                if (r + particle.Radius < Radius)
                {
                    ParticlesCount++;
                }
            }
        }
    }
}