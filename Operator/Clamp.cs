namespace LibNoise.Unity.Operator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Provides a noise module that clamps the output value from a source module to a
    /// range of values. [OPERATOR]
    /// </summary>
    public class Clamp : ModuleBase
    {
        #region Fields

        private double m_min = -1.0;
        private double m_max = 1.0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Clamp.
        /// </summary>
        public Clamp()
            : base(1)
        {
        }

        /// <summary>
        /// Initializes a new instance of Clamp.
        /// </summary>
        /// <param name="input">The input module.</param>
        public Clamp(ModuleBase input)
            : base(1)
        {
            this.m_modules[0] = input;
        }

        /// <summary>
        /// Initializes a new instance of Clamp.
        /// </summary>
        /// <param name="input">The input module.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        public Clamp(double min, double max, ModuleBase input)
            : base(1)
        {
            this.Minimum = min;
            this.Maximum = max;
            this.m_modules[0] = input;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the maximum to clamp to.
        /// </summary>
        public double Maximum
        {
            get { return this.m_max; }
            set { this.m_max = value; }
        }

        /// <summary>
        /// Gets or sets the minimum to clamp to.
        /// </summary>
        public double Minimum
        {
            get { return this.m_min; }
            set { this.m_min = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the bounds.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        public void SetBounds(double min, double max)
        {
            System.Diagnostics.Debug.Assert(min < max);
            this.m_min = min;
            this.m_max = max;
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
            if (this.m_min > this.m_max)
            {
                double t = this.m_min;
                this.m_min = this.m_max;
                this.m_max = t;
            }
            double v = this.m_modules[0].GetValue(x, y, z);
            if (v < this.m_min)
            {
                return this.m_min;
            }
            else if (v > this.m_max)
            {
                return this.m_max;
            }
            return v;
        }

        #endregion
    }
}