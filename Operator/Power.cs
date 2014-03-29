namespace LibNoise.Unity.Operator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Provides a noise module that outputs value from a first source module
    /// to the power of the output value from a second source module. [OPERATOR]
    /// </summary>
    public class Power : ModuleBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of Power.
        /// </summary>
        public Power()
            : base(2)
        {
        }

        /// <summary>
        /// Initializes a new instance of Power.
        /// </summary>
        /// <param name="lhs">The left hand input module.</param>
        /// <param name="rhs">The right hand input module.</param>
        public Power(ModuleBase lhs, ModuleBase rhs)
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
            return Math.Pow(this.m_modules[0].GetValue(x, y, z), this.m_modules[1].GetValue(x, y, z));
        }

        #endregion
    }
}