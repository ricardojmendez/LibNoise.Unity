namespace LibNoise.Unity.Operator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using LibNoise.Unity.Generator;

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
        private Perlin m_xDistort = null;
        private Perlin m_yDistort = null;
        private Perlin m_zDistort = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Turbulence.
        /// </summary>
        public Turbulence()
            : base(1)
        {
            this.m_xDistort = new Perlin();
            this.m_yDistort = new Perlin();
            this.m_zDistort = new Perlin();
        }

        /// <summary>
        /// Initializes a new instance of Turbulence.
        /// </summary>
        /// <param name="input">The input module.</param>
        public Turbulence(ModuleBase input)
            : base(1)
        {
            this.m_xDistort = new Perlin();
            this.m_yDistort = new Perlin();
            this.m_zDistort = new Perlin();
            this.m_modules[0] = input;
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
            this.m_xDistort = x;
            this.m_yDistort = y;
            this.m_zDistort = z;
            this.m_modules[0] = input;
            this.Power = power;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the frequency of the turbulence.
        /// </summary>
        public double Frequency
        {
            get { return this.m_xDistort.Frequency; }
            set
            {
                this.m_xDistort.Frequency = value;
                this.m_yDistort.Frequency = value;
                this.m_zDistort.Frequency = value;
            }
        }

        /// <summary>
        /// Gets or sets the power of the turbulence.
        /// </summary>
        public double Power
        {
            get { return this.m_power; }
            set { this.m_power = value; }
        }

        /// <summary>
        /// Gets or sets the roughness of the turbulence.
        /// </summary>
        public int Roughness
        {
            get { return this.m_xDistort.OctaveCount; }
            set
            {
                this.m_xDistort.OctaveCount = value;
                this.m_yDistort.OctaveCount = value;
                this.m_zDistort.OctaveCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the seed of the turbulence.
        /// </summary>
        public int Seed
        {
            get { return this.m_xDistort.Seed; }
            set
            {
                this.m_xDistort.Seed = value;
                this.m_yDistort.Seed = value + 1;
                this.m_zDistort.Seed = value + 2;
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
            System.Diagnostics.Debug.Assert(this.m_modules[0] != null);
            double xd = x + (this.m_xDistort.GetValue(x + Turbulence.X0, y + Turbulence.Y0, z + Turbulence.Z0) * this.m_power);
            double yd = y + (this.m_yDistort.GetValue(x + Turbulence.X1, y + Turbulence.Y1, z + Turbulence.Z1) * this.m_power);
            double zd = z + (this.m_zDistort.GetValue(x + Turbulence.X2, y + Turbulence.Y2, z + Turbulence.Z2) * this.m_power);
            return this.m_modules[0].GetValue(xd, yd, zd);
        }

        #endregion
    }
}