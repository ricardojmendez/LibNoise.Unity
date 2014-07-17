using System.Diagnostics;
using LibNoise.Unity.Generator;

namespace LibNoise.Unity.Operator
{
    /// <summary>
    /// Provides a noise module that that randomly displaces the input value before
    /// returning the output value from a source module. [OPERATOR]
    /// </summary>
    public class Turbulence : ModuleBase
    {
        #region Constants

        private const double X0 = (12414.0 / 65536.0);
        private const double Y0 = (65124.0 / 65536.0);
        private const double Z0 = (31337.0 / 65536.0);
        private const double X1 = (26519.0 / 65536.0);
        private const double Y1 = (18128.0 / 65536.0);
        private const double Z1 = (60493.0 / 65536.0);
        private const double X2 = (53820.0 / 65536.0);
        private const double Y2 = (11213.0 / 65536.0);
        private const double Z2 = (44845.0 / 65536.0);

        #endregion

        #region Fields

        private double m_power = 1.0;
        private readonly Perlin m_xDistort;
        private readonly Perlin m_yDistort;
        private readonly Perlin m_zDistort;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Turbulence.
        /// </summary>
        public Turbulence()
            : base(1)
        {
            m_xDistort = new Perlin();
            m_yDistort = new Perlin();
            m_zDistort = new Perlin();
        }

        /// <summary>
        /// Initializes a new instance of Turbulence.
        /// </summary>
        /// <param name="input">The input module.</param>
        public Turbulence(ModuleBase input)
            : base(1)
        {
            m_xDistort = new Perlin();
            m_yDistort = new Perlin();
            m_zDistort = new Perlin();
            m_modules[0] = input;
        }

        /// <summary>
        /// Initializes a new instance of Turbulence.
        /// </summary>
        public Turbulence(double power, ModuleBase input)
            : this(new Perlin(), new Perlin(), new Perlin(), power, input)
        {
        }

        /// <summary>
        /// Initializes a new instance of Turbulence.
        /// </summary>
        /// <param name="x">The perlin noise to apply on the x-axis.</param>
        /// <param name="y">The perlin noise to apply on the y-axis.</param>
        /// <param name="z">The perlin noise to apply on the z-axis.</param>
        /// <param name="power">The power of the turbulence.</param>
        /// <param name="input">The input module.</param>
        public Turbulence(Perlin x, Perlin y, Perlin z, double power, ModuleBase input)
            : base(1)
        {
            m_xDistort = x;
            m_yDistort = y;
            m_zDistort = z;
            m_modules[0] = input;
            Power = power;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the frequency of the turbulence.
        /// </summary>
        public double Frequency
        {
            get { return m_xDistort.Frequency; }
            set
            {
                m_xDistort.Frequency = value;
                m_yDistort.Frequency = value;
                m_zDistort.Frequency = value;
            }
        }

        /// <summary>
        /// Gets or sets the power of the turbulence.
        /// </summary>
        public double Power
        {
            get { return m_power; }
            set { m_power = value; }
        }

        /// <summary>
        /// Gets or sets the roughness of the turbulence.
        /// </summary>
        public int Roughness
        {
            get { return m_xDistort.OctaveCount; }
            set
            {
                m_xDistort.OctaveCount = value;
                m_yDistort.OctaveCount = value;
                m_zDistort.OctaveCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the seed of the turbulence.
        /// </summary>
        public int Seed
        {
            get { return m_xDistort.Seed; }
            set
            {
                m_xDistort.Seed = value;
                m_yDistort.Seed = value + 1;
                m_zDistort.Seed = value + 2;
            }
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
            Debug.Assert(m_modules[0] != null);
            var xd = x + (m_xDistort.GetValue(x + X0, y + Y0, z + Z0) * m_power);
            var yd = y + (m_yDistort.GetValue(x + X1, y + Y1, z + Z1) * m_power);
            var zd = z + (m_zDistort.GetValue(x + X2, y + Y2, z + Z2) * m_power);
            return m_modules[0].GetValue(xd, yd, zd);
        }

        #endregion
    }
}