using System.Diagnostics;

namespace LibNoise.Unity.Operator
{
    /// <summary>
    /// Provides a noise module that outputs a weighted blend of the output values from
    /// two source modules given the output value supplied by a control module. [OPERATOR]
    /// </summary>
    public class Blend : ModuleBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of Blend.
        /// </summary>
        public Blend()
            : base(3)
        {
        }

        /// <summary>
        /// Initializes a new instance of Blend.
        /// </summary>
        /// <param name="lhs">The left hand input module.</param>
        /// <param name="rhs">The right hand input module.</param>
        /// <param name="controller">The controller of the operator.</param>
        public Blend(ModuleBase lhs, ModuleBase rhs, ModuleBase controller)
            : base(3)
        {
            m_modules[0] = lhs;
            m_modules[1] = rhs;
            m_modules[2] = controller;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the controlling module.
        /// </summary>
        public ModuleBase Controller
        {
            get { return m_modules[2]; }
            set
            {
                Debug.Assert(value != null);
                m_modules[2] = value;
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
            Debug.Assert(m_modules[1] != null);
            Debug.Assert(m_modules[2] != null);
            var a = m_modules[0].GetValue(x, y, z);
            var b = m_modules[1].GetValue(x, y, z);
            var c = (m_modules[2].GetValue(x, y, z) + 1.0) / 2.0;
            return Utils.InterpolateLinear(a, b, c);
        }

        #endregion
    }
}