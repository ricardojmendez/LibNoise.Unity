using System.Diagnostics;

namespace LibNoise.Unity.Operator
{
    /// <summary>
    /// Provides a noise module that applies a scaling factor and a bias to the output
    /// value from a source module. [OPERATOR]
    /// </summary>
    public class ScaleBias : ModuleBase
    {
        #region Fields

        private double m_scale = 1.0;
        private double m_bias;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of ScaleBias.
        /// </summary>
        public ScaleBias()
            : base(1)
        {
        }

        /// <summary>
        /// Initializes a new instance of ScaleBias.
        /// </summary>
        /// <param name="input">The input module.</param>
        public ScaleBias(ModuleBase input)
            : base(1)
        {
            m_modules[0] = input;
        }

        /// <summary>
        /// Initializes a new instance of ScaleBias.
        /// </summary>
        /// <param name="scale">The scaling factor to apply to the output value from the source module.</param>
        /// <param name="bias">The bias to apply to the scaled output value from the source module.</param>
        /// <param name="input">The input module.</param>
        public ScaleBias(double scale, double bias, ModuleBase input)
            : base(1)
        {
            m_modules[0] = input;
            Bias = bias;
            Scale = scale;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the bias to apply to the scaled output value from the source module.
        /// </summary>
        public double Bias
        {
            get { return m_bias; }
            set { m_bias = value; }
        }

        /// <summary>
        /// Gets or sets the scaling factor to apply to the output value from the source module.
        /// </summary>
        public double Scale
        {
            get { return m_scale; }
            set { m_scale = value; }
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
            return m_modules[0].GetValue(x, y, z) * m_scale + m_bias;
        }

        #endregion
    }
}