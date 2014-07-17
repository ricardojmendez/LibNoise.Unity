using System;
using UnityEngine;

namespace LibNoise.Unity.Generator
{
    /// <summary>
    /// Provides a noise module that outputs 3-dimensional ridged-multifractal noise. [GENERATOR]
    /// </summary>
    public class RidgedMultifractal : ModuleBase
    {
        #region Fields

        private double m_frequency = 1.0;
        private double m_lacunarity = 2.0;
        private QualityMode m_quality = QualityMode.Medium;
        private int m_octaveCount = 6;
        private int m_seed;
        private readonly double[] m_weights = new double[Utils.OctavesMaximum];

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of RidgedMultifractal.
        /// </summary>
        public RidgedMultifractal()
            : base(0)
        {
            UpdateWeights();
        }

        /// <summary>
        /// Initializes a new instance of RidgedMultifractal.
        /// </summary>
        /// <param name="frequency">The frequency of the first octave.</param>
        /// <param name="lacunarity">The lacunarity of the ridged-multifractal noise.</param>
        /// <param name="octaves">The number of octaves of the ridged-multifractal noise.</param>
        /// <param name="seed">The seed of the ridged-multifractal noise.</param>
        /// <param name="quality">The quality of the ridged-multifractal noise.</param>
        public RidgedMultifractal(double frequency, double lacunarity, int octaves, int seed, QualityMode quality)
            : base(0)
        {
            Frequency = frequency;
            Lacunarity = lacunarity;
            OctaveCount = octaves;
            Seed = seed;
            Quality = quality;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the frequency of the first octave.
        /// </summary>
        public double Frequency
        {
            get { return m_frequency; }
            set { m_frequency = value; }
        }

        /// <summary>
        /// Gets or sets the lacunarity of the ridged-multifractal noise.
        /// </summary>
        public double Lacunarity
        {
            get { return m_lacunarity; }
            set
            {
                m_lacunarity = value;
                UpdateWeights();
            }
        }

        /// <summary>
        /// Gets or sets the quality of the ridged-multifractal noise.
        /// </summary>
        public QualityMode Quality
        {
            get { return m_quality; }
            set { m_quality = value; }
        }

        /// <summary>
        /// Gets or sets the number of octaves of the ridged-multifractal noise.
        /// </summary>
        public int OctaveCount
        {
            get { return m_octaveCount; }
            set { m_octaveCount = Mathf.Clamp(value, 1, Utils.OctavesMaximum); }
        }

        /// <summary>
        /// Gets or sets the seed of the ridged-multifractal noise.
        /// </summary>
        public int Seed
        {
            get { return m_seed; }
            set { m_seed = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the weights of the ridged-multifractal noise.
        /// </summary>
        private void UpdateWeights()
        {
            var f = 1.0;
            for (var i = 0; i < Utils.OctavesMaximum; i++)
            {
                m_weights[i] = Math.Pow(f, -1.0);
                f *= m_lacunarity;
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
            x *= m_frequency;
            y *= m_frequency;
            z *= m_frequency;
            var signal = 0.0;
            var value = 0.0;
            var weight = 1.0;
            var offset = 1.0;
            var gain = 2.0;
            for (var i = 0; i < m_octaveCount; i++)
            {
                var nx = Utils.MakeInt32Range(x);
                var ny = Utils.MakeInt32Range(y);
                var nz = Utils.MakeInt32Range(z);
                long seed = (m_seed + i) & 0x7fffffff;
                signal = Utils.GradientCoherentNoise3D(nx, ny, nz, seed, m_quality);
                signal = Math.Abs(signal);
                signal = offset - signal;
                signal *= signal;
                signal *= weight;
                weight = signal * gain;
                weight = Mathf.Clamp01((float) weight);
                value += (signal * m_weights[i]);
                x *= m_lacunarity;
                y *= m_lacunarity;
                z *= m_lacunarity;
            }
            return (value * 1.25) - 1.0;
        }

        #endregion
    }
}