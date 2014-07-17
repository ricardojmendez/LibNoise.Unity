using System;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace LibNoise.Unity.Operator
{
    /// <summary>
    /// Provides a noise module that rotates the input value around the origin before
    /// returning the output value from a source module. [OPERATOR]
    /// </summary>
    public class Rotate : ModuleBase
    {
        #region Fields

        private double m_x;
        private double m_x1Matrix;
        private double m_x2Matrix;
        private double m_x3Matrix;
        private double m_y;
        private double m_y1Matrix;
        private double m_y2Matrix;
        private double m_y3Matrix;
        private double m_z;
        private double m_z1Matrix;
        private double m_z2Matrix;
        private double m_z3Matrix;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Rotate.
        /// </summary>
        public Rotate()
            : base(1)
        {
            SetAngles(0.0, 0.0, 0.0);
        }

        /// <summary>
        /// Initializes a new instance of Rotate.
        /// </summary>
        /// <param name="input">The input module.</param>
        public Rotate(ModuleBase input)
            : base(1)
        {
            m_modules[0] = input;
        }

        /// <summary>
        /// Initializes a new instance of Rotate.
        /// </summary>
        /// <param name="x">The rotation around the x-axis.</param>
        /// <param name="y">The rotation around the y-axis.</param>
        /// <param name="z">The rotation around the z-axis.</param>
        /// <param name="input">The input module.</param>
        public Rotate(double x, double y, double z, ModuleBase input)
            : base(1)
        {
            m_modules[0] = input;
            SetAngles(x, y, z);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the rotation around the x-axis in degree.
        /// </summary>
        public double X
        {
            get { return m_x; }
            set { SetAngles(value, m_y, m_z); }
        }

        /// <summary>
        /// Gets or sets the rotation around the y-axis in degree.
        /// </summary>
        public double Y
        {
            get { return m_y; }
            set { SetAngles(m_x, value, m_z); }
        }

        /// <summary>
        /// Gets or sets the rotation around the z-axis in degree.
        /// </summary>
        public double Z
        {
            get { return m_x; }
            set { SetAngles(m_x, m_y, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the rotation angles.
        /// </summary>
        /// <param name="x">The rotation around the x-axis.</param>
        /// <param name="y">The rotation around the y-axis.</param>
        /// <param name="z">The rotation around the z-axis.</param>
        private void SetAngles(double x, double y, double z)
        {
            var xc = Math.Cos(x * Mathf.Deg2Rad);
            var yc = Math.Cos(y * Mathf.Deg2Rad);
            var zc = Math.Cos(z * Mathf.Deg2Rad);
            var xs = Math.Sin(x * Mathf.Deg2Rad);
            var ys = Math.Sin(y * Mathf.Deg2Rad);
            var zs = Math.Sin(z * Mathf.Deg2Rad);
            m_x1Matrix = ys * xs * zs + yc * zc;
            m_y1Matrix = xc * zs;
            m_z1Matrix = ys * zc - yc * xs * zs;
            m_x2Matrix = ys * xs * zc - yc * zs;
            m_y2Matrix = xc * zc;
            m_z2Matrix = -yc * xs * zc - ys * zs;
            m_x3Matrix = -ys * xc;
            m_y3Matrix = xs;
            m_z3Matrix = yc * xc;
            m_x = x;
            m_y = y;
            m_z = z;
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
            var nx = (m_x1Matrix * x) + (m_y1Matrix * y) + (m_z1Matrix * z);
            var ny = (m_x2Matrix * x) + (m_y2Matrix * y) + (m_z2Matrix * z);
            var nz = (m_x3Matrix * x) + (m_y3Matrix * y) + (m_z3Matrix * z);
            return m_modules[0].GetValue(nx, ny, nz);
        }

        #endregion
    }
}