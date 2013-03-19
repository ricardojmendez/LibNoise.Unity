using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibNoise.Unity.Operator
{
    /// <summary>
    /// Provides a noise module that uses three source modules to displace each
    /// coordinate of the input value before returning the output value from
    /// a source module. [OPERATOR]
    /// </summary>
    public class Displace : ModuleBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of Displace.
        /// </summary>
        public Displace()
            : base(4)
        {
        }

        /// <summary>
        /// Initializes a new instance of Displace.
        /// </summary>
        /// <param name="input">The input module.</param>
        /// <param name="x">The displacement module of the x-axis.</param>
        /// <param name="y">The displacement module of the y-axis.</param>
        /// <param name="z">The displacement module of the z-axis.</param>
        public Displace(ModuleBase input, ModuleBase x, ModuleBase y, ModuleBase z)
            : base(4)
        {
            this.m_modules[0] = input;
            this.m_modules[1] = x;
            this.m_modules[2] = y;
            this.m_modules[3] = z;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the controlling module on the x-axis.
        /// </summary>
        public ModuleBase X
        {
            get { return this.m_modules[1]; }
            set
            {
                System.Diagnostics.Debug.Assert(value != null);
                this.m_modules[1] = value;
            }
        }

        /// <summary>
        /// Gets or sets the controlling module on the z-axis.
        /// </summary>
        public ModuleBase Y
        {
            get { return this.m_modules[2]; }
            set
            {
                System.Diagnostics.Debug.Assert(value != null);
                this.m_modules[2] = value;
            }
        }

        /// <summary>
        /// Gets or sets the controlling module on the z-axis.
        /// </summary>
        public ModuleBase Z
        {
            get { return this.m_modules[3]; }
            set
            {
                System.Diagnostics.Debug.Assert(value != null);
                this.m_modules[3] = value;
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
            System.Diagnostics.Debug.Assert(this.m_modules[0] != null);
            System.Diagnostics.Debug.Assert(this.m_modules[1] != null);
            System.Diagnostics.Debug.Assert(this.m_modules[2] != null);
            System.Diagnostics.Debug.Assert(this.m_modules[3] != null);
            double dx = x + this.m_modules[1].GetValue(x, y, z);
            double dy = y + this.m_modules[2].GetValue(x, y, z);
            double dz = z + this.m_modules[3].GetValue(x, y, z);
            return this.m_modules[0].GetValue(dx, dy, dz);
        }

        #endregion
    }
}
