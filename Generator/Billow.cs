using System;
using UnityEngine;

namespace LibNoise.Unity.Generator
{
    /// <summary>
    /// Provides a noise module that outputs a three-dimensional billowy noise. [GENERATOR]
    /// </summary>
    public class Billow : ModuleBase
    {
        #region Fields

        private double m_frequency = 1.0;
        private double m_lacunarity = 2.0;
        private QualityMode m_quality = QualityMode.Medium;
        private int m_octaveCount = 6;
        private double m_persistence = 0.5;
        private int m_seed;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Billow.
        /// </summary>
        public Billow()
            : base(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of Billow.
        /// </summary>
        /// <param name="frequency">The frequency of the first octave.</param>
        /// <param name="lacunarity">The lacunarity of the billowy noise.</param>
        /// <param name="persistence">The persistence of the billowy noise.</param>
        /// <param name="octaves">The number of octaves of the billowy noise.</param>
        /// <param name="seed">The seed of the billowy noise.</param>
        /// <param name="quality">The quality of the billowy noise.</param>
        public Billow(double frequency, double lacunarity, double persistence, int octaves, int seed,
            QualityMode quality)
            : base(0)
        {
            Frequency = frequency;
            Lacunarity = lacunarity;
            OctaveCount = octaves;
            Persistence = persistence;
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
        /// Gets or sets the lacunarity of the billowy noise.
        /// </summary>
        public double Lacunarity
        {
            get { return m_lacunarity; }
            set { m_lacunarity = value; }
        }

        /// <summary>
        /// Gets or sets the quality of the billowy noise.
        /// </summary>
        public QualityMode Quality
        {
            get { return m_quality; }
            set { m_quality = value; }
        }

        /// <summary>
        /// Gets or sets the number of octaves of the billowy noise.
        /// </summary>
        public int OctaveCount
        {
            get { return m_octaveCount; }
            set { m_octaveCount = Mathf.Clamp(value, 1, Utils.OctavesMaximum); }
        }

        /// <summary>
        /// Gets or sets the persistence of the billowy noise.
        /// </summary>
        public double Persistence
        {
            get { return m_persistence; }
            set { m_persistence = value; }
        }

        /// <summary>
        /// Gets or sets the seed of the billowy noise.
        /// </summary>
        public int Seed
        {
            get { return m_seed; }
            set { m_seed = value; }
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
            var value = 0.0;
            var signal = 0.0;
            var curp = 1.0;
            double nx, ny, nz;
            long seed;
            x *= m_frequency;
            y *= m_frequency;
            z *= m_frequency;
            for (var i = 0; i < m_octaveCount; i++)
            {
                nx = Utils.MakeInt32Range(x);
                ny = Utils.MakeInt32Range(y);
                nz = Utils.MakeInt32Range(z);
                seed = (m_seed + i) & 0xffffffff;
                signal = Utils.GradientCoherentNoise3D(nx, ny, nz, seed, m_quality);
                signal = 2.0 * Math.Abs(signal) - 1.0;
                value += signal * curp;
                x *= m_lacunarity;
                y *= m_lacunarity;
                z *= m_lacunarity;
                curp *= m_persistence;
            }
            return value + 0.5;
        }

        #endregion
    }
}