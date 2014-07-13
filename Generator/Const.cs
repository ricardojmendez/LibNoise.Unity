namespace LibNoise.Unity.Generator
{
    using System;
    
    /// <summary>
    /// Provides a noise module that outputs a constant value. [GENERATOR]
    /// </summary>
    public class Const : ModuleBase
    {
        #region Fields

        private double m_value = 0.0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Const.
        /// </summary>
        public Const()
            : base(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of Const.
        /// </summary>
        /// <param name="value">The constant value.</param>
        public Const(double value)
            : base(0)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the constant value.
        /// </summary>
        public double Value
        {
            get { return this.m_value; }
            set { this.m_value = value; }
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
            return this.m_value;
        }

        #endregion
    }
}