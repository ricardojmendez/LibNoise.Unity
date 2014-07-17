using System;

namespace LibNoise.Unity.Generator
{
    /// <summary>
    /// Provides a noise module that outputs Voronoi cells. [GENERATOR]
    /// </summary>
    public class Voronoi : ModuleBase
    {
        #region Fields

        private double m_displacement = 1.0;
        private double m_frequency = 1.0;
        private int m_seed;
        private bool m_distance;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Voronoi.
        /// </summary>
        public Voronoi()
            : base(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of Voronoi.
        /// </summary>
        /// <param name="frequency">The frequency of the first octave.</param>
        /// <param name="displacement">The displacement of the ridged-multifractal noise.</param>
        /// <param name="persistence">The persistence of the ridged-multifractal noise.</param>
        /// <param name="seed">The seed of the ridged-multifractal noise.</param>
        /// <param name="distance">Indicates whether the distance from the nearest seed point is applied to the output value.</param>
        public Voronoi(double frequency, double displacement, int seed, bool distance)
            : base(0)
        {
            Frequency = frequency;
            Displacement = displacement;
            Seed = seed;
            UseDistance = distance;
            Seed = seed;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the displacement value of the Voronoi cells.
        /// </summary>
        public double Displacement
        {
            get { return m_displacement; }
            set { m_displacement = value; }
        }

        /// <summary>
        /// Gets or sets the frequency of the seed points.
        /// </summary>
        public double Frequency
        {
            get { return m_frequency; }
            set { m_frequency = value; }
        }

        /// <summary>
        /// Gets or sets the seed value used by the Voronoi cells.
        /// </summary>
        public int Seed
        {
            get { return m_seed; }
            set { m_seed = value; }
        }

        /// <summary>
        /// Gets or sets a value whether the distance from the nearest seed point is applied to the output value.
        /// </summary>
        public bool UseDistance
        {
            get { return m_distance; }
            set { m_distance = value; }
        }

        #endregion

        #region ModuleBase Members

        /// <summary>
        /// Returns the output value for the given input coordinates.
        /// </summary>
        /// <param name="x">The input coordinate on the x-axis.</param>
        /// <param name="y">The input coordinate on the y-axis.</param>
        /// <param name="z">The input coordinate on the z-axis.</param>
        /// <returns>The resulting output value.</returns>
        public override double GetValue(double x, double y, double z)
        {
            x *= m_frequency;
            y *= m_frequency;
            z *= m_frequency;
            var xi = (x > 0.0 ? (int) x : (int) x - 1);
            var iy = (y > 0.0 ? (int) y : (int) y - 1);
            var iz = (z > 0.0 ? (int) z : (int) z - 1);
            var md = 2147483647.0;
            double xc = 0;
            double yc = 0;
            double zc = 0;
            for (var zcu = iz - 2; zcu <= iz + 2; zcu++)
            {
                for (var ycu = iy - 2; ycu <= iy + 2; ycu++)
                {
                    for (var xcu = xi - 2; xcu <= xi + 2; xcu++)
                    {
                        var xp = xcu + Utils.ValueNoise3D(xcu, ycu, zcu, m_seed);
                        var yp = ycu + Utils.ValueNoise3D(xcu, ycu, zcu, m_seed + 1);
                        var zp = zcu + Utils.ValueNoise3D(xcu, ycu, zcu, m_seed + 2);
                        var xd = xp - x;
                        var yd = yp - y;
                        var zd = zp - z;
                        var d = xd * xd + yd * yd + zd * zd;
                        if (d < md)
                        {
                            md = d;
                            xc = xp;
                            yc = yp;
                            zc = zp;
                        }
                    }
                }
            }
            double v;
            if (m_distance)
            {
                var xd = xc - x;
                var yd = yc - y;
                var zd = zc - z;
                v = (Math.Sqrt(xd * xd + yd * yd + zd * zd)) * Utils.Sqrt3 - 1.0;
            }
            else
            {
                v = 0.0;
            }
            return v + (m_displacement * Utils.ValueNoise3D((int) (Math.Floor(xc)), (int) (Math.Floor(yc)),
                (int) (Math.Floor(zc)), 0));
        }

        #endregion
    }
}