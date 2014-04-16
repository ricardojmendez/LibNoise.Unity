namespace LibNoise.Unity.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Provides a noise module that outputs concentric cylinders. [GENERATOR]
    /// </summary>
    public class Cylinders : ModuleBase
    {
        #region Fields

        private double m_frequency = 1.0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Cylinders.
        /// </summary>
        public Cylinders()
            : base(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of Cylinders.
        /// </summary>
        /// <param name="frequency">The frequency of the concentric cylinders.</param>
        public Cylinders(double frequency)
            : base(0)
        {
            this.Frequency = frequency;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the frequency of the concentric cylinders.
        /// </summary>
        public double Frequency
        {
            get { return this.m_frequency; }
            set { this.m_frequency = value; }
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
            x *= this.m_frequency;
            z *= this.m_frequency;
            double dfc = Math.Sqrt(x * x + z * z);
            double dfss = dfc - Math.Floor(dfc);
            double dfls = 1.0 - dfss;
            double nd = Math.Min(dfss, dfls);
            return 1.0 - (nd * 4.0);
        }

        #endregion
    }
}