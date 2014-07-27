using System;

namespace LibNoise.Generator
{
    /// <summary>
    /// Provides a noise module that outputs concentric spheres. [GENERATOR]
    /// </summary>
    public class Spheres : ModuleBase
    {
        #region Fields

        private double _frequency = 1.0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Spheres.
        /// </summary>
        public Spheres()
            : base(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of Spheres.
        /// </summary>
        /// <param name="frequency">The frequency of the concentric spheres.</param>
        public Spheres(double frequency)
            : base(0)
        {
            Frequency = frequency;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the frequency of the concentric spheres.
        /// </summary>
        public double Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
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
            x *= _frequency;
            y *= _frequency;
            z *= _frequency;
            var dfc = Math.Sqrt(x * x + y * y + z * z);
            var dfss = dfc - Math.Floor(dfc);
            var dfls = 1.0 - dfss;
            var nd = Math.Min(dfss, dfls);
            return 1.0 - (nd * 4.0);
        }

        #endregion
    }
}