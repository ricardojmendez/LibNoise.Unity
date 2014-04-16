namespace LibNoise.Unity.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    //using Microsoft.Xna.Framework;
    using UnityEngine;

    /// <summary>
    /// Provides a noise module that outputs a three-dimensional perlin noise. [GENERATOR]
    /// </summary>
    public class Perlin : ModuleBase
    {
        #region Fields

        private double m_frequency = 1.0;
        private double m_lacunarity = 2.0;
        private QualityMode m_quality = QualityMode.Medium;
        private int m_octaveCount = 6;
        private double m_persistence = 0.5;
        private int m_seed = 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Perlin.
        /// </summary>
        public Perlin()
            : base(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of Perlin.
        /// </summary>
        /// <param name="frequency">The frequency of the first octave.</param>
        /// <param name="lacunarity">The lacunarity of the perlin noise.</param>
        /// <param name="persistence">The persistence of the perlin noise.</param>
        /// <param name="octaves">The number of octaves of the perlin noise.</param>
        /// <param name="seed">The seed of the perlin noise.</param>
        /// <param name="quality">The quality of the perlin noise.</param>
        public Perlin(double frequency, double lacunarity, double persistence, int octaves, int seed, QualityMode quality)
            : base(0)
        {
            this.Frequency = frequency;
            this.Lacunarity = lacunarity;
            this.OctaveCount = octaves;
            this.Persistence = persistence;
            this.Seed = seed;
            this.Quality = quality;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the frequency of the first octave.
        /// </summary>
        public double Frequency
        {
            get { return this.m_frequency; }
            set { this.m_frequency = value; }
        }

        /// <summary>
        /// Gets or sets the lacunarity of the perlin noise.
        /// </summary>
        public double Lacunarity
        {
            get { return this.m_lacunarity; }
            set { this.m_lacunarity = value; }
        }

        /// <summary>
        /// Gets or sets the quality of the perlin noise.
        /// </summary>
        public QualityMode Quality
        {
            get { return this.m_quality; }
            set { this.m_quality = value; }
        }

        /// <summary>
        /// Gets or sets the number of octaves of the perlin noise.
        /// </summary>
        public int OctaveCount
        {
            get { return this.m_octaveCount; }
            set { this.m_octaveCount = (int)Mathf.Clamp(value, 1, Utils.OctavesMaximum); }
        }

        /// <summary>
        /// Gets or sets the persistence of the perlin noise.
        /// </summary>
        public double Persistence
        {
            get { return this.m_persistence; }
            set { this.m_persistence = value; }
        }

        /// <summary>
        /// Gets or sets the seed of the perlin noise.
        /// </summary>
        public int Seed
        {
            get { return this.m_seed; }
            set { this.m_seed = value; }
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
            double value = 0.0;
            double signal = 0.0;
            double cp = 1.0;
            double nx, ny, nz;
            long seed;
            x *= this.m_frequency;
            y *= this.m_frequency;
            z *= this.m_frequency;
            for (int i = 0; i < this.m_octaveCount; i++)
            {
                nx = Utils.MakeInt32Range(x);
                ny = Utils.MakeInt32Range(y);
                nz = Utils.MakeInt32Range(z);
                seed = (this.m_seed + i) & 0xffffffff;
                signal = Utils.GradientCoherentNoise3D(nx, ny, nz, seed, this.m_quality);
                value += signal * cp;
                x *= this.m_lacunarity;
                y *= this.m_lacunarity;
                z *= this.m_lacunarity;
                cp *= this.m_persistence;
            }
            return value;
        }

        #endregion
    }
}