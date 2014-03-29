namespace LibNoise.Unity.Operator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Provides a noise module that outputs the smaller of the two output values from two
    /// source modules. [OPERATOR]
    /// </summary>
    public class Min : ModuleBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of Min.
        /// </summary>
        public Min()
            : base(2)
        {
        }

        /// <summary>
        /// Initializes a new instance of Min.
        /// </summary>
        /// <param name="lhs">The left hand input module.</param>
        /// <param name="rhs">The right hand input module.</param>
        public Min(ModuleBase lhs, ModuleBase rhs)
            : base(2)
        {
            this.m_modules[0] = lhs;
            this.m_modules[1] = rhs;
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
            System.Diagnostics.Debug.Assert(this.m_modules[1] != null);
            double a = this.m_modules[0].GetValue(x, y, z);
            double b = this.m_modules[1].GetValue(x, y, z);
            return Math.Min(a, b);
        }

        #endregion
    }
}