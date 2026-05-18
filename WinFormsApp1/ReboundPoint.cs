using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class ReboundPoint : IImpactPoint
    {
        public int Radius = 50;

        public override void ImpactParticle(Particle particle)
        {
            float gX = X - particle.X;
            float gY = Y - particle.Y;
            double r = Math.Sqrt(gX * gX + gY * gY);

            if (r + particle.Radius < Radius)
            {
                float nx = (float)(gX / r);
                float ny = (float)(gY / r);

                float vx = particle.SpeedX;
                float vy = particle.SpeedY;

                float vn = vx * nx + vy * ny;

                particle.SpeedX = vx - 2 * vn * nx;
                particle.SpeedY = vy - 2 * vn * ny;
            }
        }

        public override void Render(Graphics g)
        {
            g.DrawEllipse(
                new Pen(Color.Red, 2),
                X - Radius,
                Y - Radius,
                Radius * 2,
                Radius * 2
            );
        }
    }
}