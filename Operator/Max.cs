using System;
using System.Diagnostics;

namespace LibNoise.Operator
{
    /// <summary>
    /// Provides a noise module that outputs the larger of the two output values from two
    /// source modules. [OPERATOR]
    /// </summary>
    public class Max : ModuleBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of Max.
        /// </summary>
        public Max()
            : base(2)
        {
        }

        /// <summary>
        /// Initializes a new instance of Max.
        /// </summary>
        /// <param name="lhs">The left hand input module.</param>
        /// <param name="rhs">The right hand input module.</param>
        public Max(ModuleBase lhs, ModuleBase rhs)
            : base(2)
        {
            Modules[0] = lhs;
            Modules[1] = rhs;
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
            Debug.Assert(Modules[0] != null);
            Debug.Assert(Modules[1] != null);
            var a = Modules[0].GetValue(x, y, z);
            var b = Modules[1].GetValue(x, y, z);
            return Math.Max(a, b);
        }

        #endregion
    }
}