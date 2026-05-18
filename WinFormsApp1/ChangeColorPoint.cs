using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class ChangeColorPoint : IImpactPoint
    {
        public int Radius;
        public Color NewColor;

        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;
            double r = Math.Sqrt(gX * gX + gY * gY);

            if (r < Radius) // + particle.Radius
            {
                if (particle is ParticleColorful colorful)
                {
                    colorful.FromColor = NewColor;
                    colorful.ToColor = Color.FromArgb(0, NewColor);
                }
            }
        }

        public override void Render(Graphics g)
        {
            var pen = new Pen(NewColor, 2);
            g.DrawEllipse(pen, X - Radius, Y - Radius, Radius * 2, Radius * 2);
            pen.Dispose();
        }
    }
}