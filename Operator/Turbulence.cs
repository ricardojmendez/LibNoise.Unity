using System.Diagnostics;
using LibNoise.Generator;

namespace LibNoise.Operator
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

        private double _power = 1.0;
        private readonly Perlin _xDistort;
        private readonly Perlin _yDistort;
        private readonly Perlin _zDistort;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Turbulence.
        /// </summary>
        public Turbulence()
            : base(1)
        {
            _xDistort = new Perlin();
            _yDistort = new Perlin();
            _zDistort = new Perlin();
        }

        /// <summary>
        /// Initializes a new instance of Turbulence.
        /// </summary>
        /// <param name="input">The input module.</param>
        public Turbulence(ModuleBase input)
            : base(1)
        {
            _xDistort = new Perlin();
            _yDistort = new Perlin();
            _zDistort = new Perlin();
            Modules[0] = input;
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
            _xDistort = x;
            _yDistort = y;
            _zDistort = z;
            Modules[0] = input;
            Power = power;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the frequency of the turbulence.
        /// </summary>
        public double Frequency
        {
            get { return _xDistort.Frequency; }
            set
            {
                _xDistort.Frequency = value;
                _yDistort.Frequency = value;
                _zDistort.Frequency = value;
            }
        }

        /// <summary>
        /// Gets or sets the power of the turbulence.
        /// </summary>
        public double Power
        {
            get { return _power; }
            set { _power = value; }
        }

        /// <summary>
        /// Gets or sets the roughness of the turbulence.
        /// </summary>
        public int Roughness
        {
            get { return _xDistort.OctaveCount; }
            set
            {
                _xDistort.OctaveCount = value;
                _yDistort.OctaveCount = value;
                _zDistort.OctaveCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the seed of the turbulence.
        /// </summary>
        public int Seed
        {
            get { return _xDistort.Seed; }
            set
            {
                _xDistort.Seed = value;
                _yDistort.Seed = value + 1;
                _zDistort.Seed = value + 2;
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
            Debug.Assert(Modules[0] != null);
            var xd = x + (_xDistort.GetValue(x + X0, y + Y0, z + Z0) * _power);
            var yd = y + (_yDistort.GetValue(x + X1, y + Y1, z + Z1) * _power);
            var zd = z + (_zDistort.GetValue(x + X2, y + Y2, z + Z2) * _power);
            return Modules[0].GetValue(xd, yd, zd);
        }

        #endregion
    }
}