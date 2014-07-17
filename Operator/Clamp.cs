using System.Diagnostics;

namespace LibNoise.Unity.Operator
{
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
            m_modules[0] = input;
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
            Minimum = min;
            Maximum = max;
            m_modules[0] = input;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the maximum to clamp to.
        /// </summary>
        public double Maximum
        {
            get { return m_max; }
            set { m_max = value; }
        }

        /// <summary>
        /// Gets or sets the minimum to clamp to.
        /// </summary>
        public double Minimum
        {
            get { return m_min; }
            set { m_min = value; }
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
            Debug.Assert(min < max);
            m_min = min;
            m_max = max;
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
            if (m_min > m_max)
            {
                var t = m_min;
                m_min = m_max;
                m_max = t;
            }
            var v = m_modules[0].GetValue(x, y, z);
            if (v < m_min)
            {
                return m_min;
            }
            if (v > m_max)
            {
                return m_max;
            }
            return v;
        }

        #endregion
    }
}