using System.Diagnostics;

namespace LibNoise.Unity.Operator
{
    /// <summary>
    /// Provides a noise module that scales the coordinates of the input value before
    /// returning the output value from a source module. [OPERATOR]
    /// </summary>
    public class Scale : ModuleBase
    {
        #region Fields

        private double m_x = 1.0;
        private double m_y = 1.0;
        private double m_z = 1.0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Scale.
        /// </summary>
        public Scale()
            : base(1)
        {
        }

        /// <summary>
        /// Initializes a new instance of Scale.
        /// </summary>
        /// <param name="input">The input module.</param>
        public Scale(ModuleBase input)
            : base(1)
        {
            m_modules[0] = input;
        }

        /// <summary>
        /// Initializes a new instance of Scale.
        /// </summary>
        /// <param name="x">The scaling on the x-axis.</param>
        /// <param name="y">The scaling on the y-axis.</param>
        /// <param name="z">The scaling on the z-axis.</param>
        /// <param name="input">The input module.</param>
        public Scale(double x, double y, double z, ModuleBase input)
            : base(1)
        {
            m_modules[0] = input;
            X = x;
            Y = y;
            Z = z;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the scaling factor on the x-axis.
        /// </summary>
        public double X
        {
            get { return m_x; }
            set { m_x = value; }
        }

        /// <summary>
        /// Gets or sets the scaling factor on the y-axis.
        /// </summary>
        public double Y
        {
            get { return m_y; }
            set { m_y = value; }
        }

        /// <summary>
        /// Gets or sets the scaling factor on the z-axis.
        /// </summary>
        public double Z
        {
            get { return m_z; }
            set { m_z = value; }
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
            return m_modules[0].GetValue(x * m_x, y * m_y, z * m_z);
        }

        #endregion
    }
}