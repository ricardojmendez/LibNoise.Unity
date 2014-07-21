using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace LibNoise.Operator
{
    /// <summary>
    /// Provides a noise module that maps the output value from a source module onto a
    /// terrace-forming curve. [OPERATOR]
    /// </summary>
    public class Terrace : ModuleBase
    {
        #region Fields

        private readonly List<double> _data = new List<double>();
        private bool _inverted;

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
        /// <param name="input">The input module.</param>
        public Terrace(ModuleBase input)
            : base(1)
        {
            Modules[0] = input;
        }

        /// <summary>
        /// Initializes a new instance of Terrace.
        /// </summary>
        /// <param name="inverted">Indicates whether the terrace curve is inverted.</param>
        /// <param name="input">The input module.</param>
        public Terrace(bool inverted, ModuleBase input)
            : base(1)
        {
            Modules[0] = input;
            IsInverted = inverted;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of control points.
        /// </summary>
        public int ControlPointCount
        {
            get { return _data.Count; }
        }

        /// <summary>
        /// Gets the list of control points.
        /// </summary>
        public List<double> ControlPoints
        {
            get { return _data; }
        }

        /// <summary>
        /// Gets or sets a value whether the terrace curve is inverted.
        /// </summary>
        public bool IsInverted
        {
            get { return _inverted; }
            set { _inverted = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a control point to the curve.
        /// </summary>
        /// <param name="input">The curves input value.</param>
        public void Add(double input)
        {
            if (!_data.Contains(input))
            {
                _data.Add(input);
            }
            _data.Sort(delegate(double lhs, double rhs) { return lhs.CompareTo(rhs); });
        }

        /// <summary>
        /// Clears the control points.
        /// </summary>
        public void Clear()
        {
            _data.Clear();
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
            Clear();
            var ts = 2.0 / (steps - 1.0);
            var cv = -1.0;
            for (var i = 0; i < steps; i++)
            {
                Add(cv);
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
            Debug.Assert(Modules[0] != null);
            Debug.Assert(ControlPointCount >= 2);
            var smv = Modules[0].GetValue(x, y, z);
            int ip;
            for (ip = 0; ip < _data.Count; ip++)
            {
                if (smv < _data[ip])
                {
                    break;
                }
            }
            var i0 = Mathf.Clamp(ip - 1, 0, _data.Count - 1);
            var i1 = Mathf.Clamp(ip, 0, _data.Count - 1);
            if (i0 == i1)
            {
                return _data[i1];
            }
            var v0 = _data[i0];
            var v1 = _data[i1];
            var a = (smv - v0) / (v1 - v0);
            if (_inverted)
            {
                a = 1.0 - a;
                var t = v0;
                v0 = v1;
                v1 = t;
            }
            a *= a;
            return Utils.InterpolateLinear(v0, v1, a);
        }

        #endregion
    }
}