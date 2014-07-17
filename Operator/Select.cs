using System.Diagnostics;

namespace LibNoise.Unity.Operator
{
    /// <summary>
    /// Provides a noise module that outputs the value selected from one of two source
    /// modules chosen by the output value from a control module. [OPERATOR]
    /// </summary>
    public class Select : ModuleBase
    {
        #region Fields

        private double m_fallOff;
        private double m_raw;
        private double m_min = -1.0;
        private double m_max = 1.0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Select.
        /// </summary>
        public Select()
            : base(3)
        {
        }

        /// <summary>
        /// Initializes a new instance of Select.
        /// </summary>
        /// <param name="inputA">The first input module.</param>
        /// <param name="inputB">The second input module.</param>
        /// <param name="inputB">The controller module.</param>
        public Select(ModuleBase inputA, ModuleBase inputB, ModuleBase controller)
            : base(3)
        {
            m_modules[0] = inputA;
            m_modules[1] = inputB;
            m_modules[2] = controller;
        }

        /// <summary>
        /// Initializes a new instance of Select.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="fallOff">The falloff value at the edge transition.</param>
        /// <param name="inputA">The first input module.</param>
        /// <param name="inputB">The second input module.</param>
        public Select(double min, double max, double fallOff, ModuleBase inputA, ModuleBase inputB)
            : this(inputA, inputB, null)
        {
            m_min = min;
            m_max = max;
            FallOff = fallOff;
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

        /// <summary>
        /// Gets or sets the falloff value at the edge transition.
        /// </summary>
        public double FallOff
        {
            get { return m_fallOff; }
            set
            {
                var bs = m_max - m_min;
                m_raw = value;
                m_fallOff = (value > bs / 2) ? bs / 2 : value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        public double Maximum
        {
            get { return m_max; }
            set
            {
                m_max = value;
                FallOff = m_raw;
            }
        }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        public double Minimum
        {
            get { return m_min; }
            set
            {
                m_min = value;
                FallOff = m_raw;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the bounds.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        public void SetBounds(double min, double max)
        {
            Debug.Assert(min < max);
            m_min = min;
            m_max = max;
            FallOff = m_fallOff;
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
            var cv = m_modules[2].GetValue(x, y, z);
            double a;
            if (m_fallOff > 0.0)
            {
                if (cv < (m_min - m_fallOff))
                {
                    return m_modules[0].GetValue(x, y, z);
                }
                if (cv < (m_min + m_fallOff))
                {
                    var lc = (m_min - m_fallOff);
                    var uc = (m_min + m_fallOff);
                    a = Utils.MapCubicSCurve((cv - lc) / (uc - lc));
                    return Utils.InterpolateLinear(m_modules[0].GetValue(x, y, z),
                        m_modules[1].GetValue(x, y, z), a);
                }
                if (cv < (m_max - m_fallOff))
                {
                    return m_modules[1].GetValue(x, y, z);
                }
                if (cv < (m_max + m_fallOff))
                {
                    var lc = (m_max - m_fallOff);
                    var uc = (m_max + m_fallOff);
                    a = Utils.MapCubicSCurve((cv - lc) / (uc - lc));
                    return Utils.InterpolateLinear(m_modules[1].GetValue(x, y, z),
                        m_modules[0].GetValue(x, y, z), a);
                }
                return m_modules[0].GetValue(x, y, z);
            }
            if (cv < m_min || cv > m_max)
            {
                return m_modules[0].GetValue(x, y, z);
            }
            return m_modules[1].GetValue(x, y, z);
        }

        #endregion
    }
}