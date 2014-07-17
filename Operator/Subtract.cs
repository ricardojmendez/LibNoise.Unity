using System.Diagnostics;

namespace LibNoise.Unity.Operator
{
    /// <summary>
    /// Provides a noise module that outputs the difference of the two output values from two
    /// source modules. [OPERATOR]
    /// </summary>
    public class Subtract : ModuleBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of Subtract.
        /// </summary>
        public Subtract()
            : base(2)
        {
        }

        /// <summary>
        /// Initializes a new instance of Subtract.
        /// </summary>
        /// <param name="lhs">The left hand input module.</param>
        /// <param name="rhs">The right hand input module.</param>
        public Subtract(ModuleBase lhs, ModuleBase rhs)
            : base(2)
        {
            m_modules[0] = lhs;
            m_modules[1] = rhs;
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
            Debug.Assert(m_modules[1] != null);
            return m_modules[0].GetValue(x, y, z) - m_modules[1].GetValue(x, y, z);
        }

        #endregion
    }
}