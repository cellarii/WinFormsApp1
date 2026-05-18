using WinFormsApp1;

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
                if (colorful.DefaultFromColor == default(Color))
                {
                    colorful.DefaultFromColor = colorful.FromColor;
                    colorful.DefaultToColor = colorful.ToColor;
                }
                colorful.FromColor = HighlightColor;
                colorful.ToColor = Color.FromArgb(0, HighlightColor);
            }
            else
            {
                if (colorful.DefaultFromColor != default(Color))
                {
                    colorful.FromColor = colorful.DefaultFromColor;
                    colorful.ToColor = colorful.DefaultToColor;

                    colorful.DefaultFromColor = default(Color);
                    colorful.DefaultToColor = default(Color);
                }
            }
        }
    }

    public override void Render(Graphics g)
    {
        var pen = new Pen(HighlightColor, 2);
        g.DrawEllipse(pen, X - Radius, Y - Radius, Radius * 2, Radius * 2);

        var stringFormat = new StringFormat();
        stringFormat.Alignment = StringAlignment.Center;
        stringFormat.LineAlignment = StringAlignment.Center;

        g.DrawString(
            $"{ParticlesCount}",
            new Font("Verdana", 10),
            new SolidBrush(Color.White),
            X,
            Y,
            stringFormat
        );
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