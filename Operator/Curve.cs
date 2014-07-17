using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace LibNoise.Unity.Operator
{
    /// <summary>
    /// Provides a noise module that maps the output value from a source module onto an
    /// arbitrary function curve. [OPERATOR]
    /// </summary>
    public class Curve : ModuleBase
    {
        #region Fields

        private readonly List<KeyValuePair<double, double>> m_data = new List<KeyValuePair<double, double>>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Curve.
        /// </summary>
        public Curve()
            : base(1)
        {
        }

        /// <summary>
        /// Initializes a new instance of Curve.
        /// </summary>
        /// <param name="input">The input module.</param>
        public Curve(ModuleBase input)
            : base(1)
        {
            m_modules[0] = input;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of control points.
        /// </summary>
        public int ControlPointCount
        {
            get { return m_data.Count; }
        }

        /// <summary>
        /// Gets the list of control points.
        /// </summary>
        public List<KeyValuePair<double, double>> ControlPoints
        {
            get { return m_data; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a control point to the curve.
        /// </summary>
        /// <param name="input">The curves input value.</param>
        /// <param name="output">The curves output value.</param>
        public void Add(double input, double output)
        {
            var kvp = new KeyValuePair<double, double>(input, output);
            if (!m_data.Contains(kvp))
            {
                m_data.Add(kvp);
            }
            m_data.Sort(
                delegate(KeyValuePair<double, double> lhs, KeyValuePair<double, double> rhs)
                {
                    return lhs.Key.CompareTo(rhs.Key);
                });
        }

        /// <summary>
        /// Clears the control points.
        /// </summary>
        public void Clear()
        {
            m_data.Clear();
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
            Debug.Assert(ControlPointCount >= 4);
            var smv = m_modules[0].GetValue(x, y, z);
            int ip;
            for (ip = 0; ip < m_data.Count; ip++)
            {
                if (smv < m_data[ip].Key)
                {
                    break;
                }
            }
            var i0 = Mathf.Clamp(ip - 2, 0, m_data.Count - 1);
            var i1 = Mathf.Clamp(ip - 1, 0, m_data.Count - 1);
            var i2 = Mathf.Clamp(ip, 0, m_data.Count - 1);
            var i3 = Mathf.Clamp(ip + 1, 0, m_data.Count - 1);
            if (i1 == i2)
            {
                return m_data[i1].Value;
            }
            //double ip0 = m_data[i1].Value;
            //double ip1 = m_data[i2].Value;
            var ip0 = m_data[i1].Key;
            var ip1 = m_data[i2].Key;
            var a = (smv - ip0) / (ip1 - ip0);
            return Utils.InterpolateCubic(m_data[i0].Value, m_data[i1].Value, m_data[i2].Value,
                m_data[i3].Value, a);
        }

        #endregion
    }
}