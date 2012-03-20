using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace LibNoise.Unity.Operator
{
    /// <summary>
    /// Provides a noise module that maps the output value from a source module onto a
    /// terrace-forming curve. [OPERATOR]
    /// </summary>
    public class Terrace : ModuleBase
    {
        #region Fields

        private List<double> m_data = new List<double>();
        private bool m_inverted = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Terrace.
        /// </summary>
        public Terrace()
            : base(1)
        {
        }

        /// <summary>
        /// Initializes a new instance of Terrace.
        /// </summary>
        /// <param name="inverted">Indicates whether the terrace curve is inverted.</param>
        /// <param name="input">The input module.</param>
        public Terrace(bool inverted, ModuleBase input)
            : base(1)
        {
            this.m_modules[0] = input;
            this.IsInverted = inverted;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of control points.
        /// </summary>
        public int ControlPointCount
        {
            get { return this.m_data.Count; }
        }

        /// <summary>
        /// Gets the list of control points.
        /// </summary>
        public List<double> ControlPoints
        {
            get { return this.m_data; }
        }

        /// <summary>
        /// Gets or sets a value whether the terrace curve is inverted.
        /// </summary>
        public bool IsInverted
        {
            get { return this.m_inverted; }
            set { this.m_inverted = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a control point to the curve.
        /// </summary>
        /// <param name="input">The curves input value.</param>
        public void Add(double input)
        {
            if (!this.m_data.Contains(input))
            {
                this.m_data.Add(input);
            }
            this.m_data.Sort(delegate(double lhs, double rhs) { return lhs.CompareTo(rhs); });
        }

        /// <summary>
        /// Clears the control points.
        /// </summary>
        public void Clear()
        {
            this.m_data.Clear();
        }

        /// <summary>
        /// Auto-generates a terrace curve.
        /// </summary>
        /// <param name="steps">The number of steps.</param>
        public void Generate(int steps)
        {
            if (steps < 2)
            {
                throw new ArgumentException("Need at least two steps");
            }
            this.Clear();
            double ts = 2.0 / ((double)steps - 1.0);
            double cv = -1.0;
            for (int i = 0; i < (int)steps; i++)
            {
                this.Add(cv);
                cv += ts;
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
            System.Diagnostics.Debug.Assert(this.ControlPointCount >= 2);
            double smv = this.m_modules[0].GetValue(x, y, z);
            int ip;
            for (ip = 0; ip < this.m_data.Count; ip++)
            {
                if (smv < this.m_data[ip])
                {
                    break;
                }
            }
            int i0 = (int)Mathf.Clamp(ip - 1, 0, this.m_data.Count - 1);
            int i1 = (int)Mathf.Clamp(ip, 0, this.m_data.Count - 1);
            if (i0 == i1)
            {
                return this.m_data[i1];
            }
            double v0 = this.m_data[i0];
            double v1 = this.m_data[i1];
            double a = (smv - v0) / (v1 - v0);
            if (this.m_inverted)
            {
                a = 1.0 - a;
                double t = v0;
                v0 = v1;
                v1 = t;
            }
            a *= a;
            return Utils.InterpolateLinear(v0, v1, a);
        }

        #endregion
    }
}