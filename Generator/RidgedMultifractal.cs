using System;
using UnityEngine;

namespace LibNoise.Generator
{
    /// <summary>
    /// Provides a noise module that outputs 3-dimensional ridged-multifractal noise. [GENERATOR]
    /// </summary>
    public class RidgedMultifractal : ModuleBase
    {
        #region Fields

        private double _frequency = 1.0;
        private double _lacunarity = 2.0;
        private QualityMode _quality = QualityMode.Medium;
        private int _octaveCount = 6;
        private int _seed;
        private readonly double[] _weights = new double[Utils.OctavesMaximum];

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
            get { return _frequency; }
            set { _frequency = value; }
        }

        /// <summary>
        /// Gets or sets the lacunarity of the ridged-multifractal noise.
        /// </summary>
        public double Lacunarity
        {
            get { return _lacunarity; }
            set
            {
                _lacunarity = value;
                UpdateWeights();
            }
        }

        /// <summary>
        /// Gets or sets the quality of the ridged-multifractal noise.
        /// </summary>
        public QualityMode Quality
        {
            get { return _quality; }
            set { _quality = value; }
        }

        /// <summary>
        /// Gets or sets the number of octaves of the ridged-multifractal noise.
        /// </summary>
        public int OctaveCount
        {
            get { return _octaveCount; }
            set { _octaveCount = Mathf.Clamp(value, 1, Utils.OctavesMaximum); }
        }

        /// <summary>
        /// Gets or sets the seed of the ridged-multifractal noise.
        /// </summary>
        public int Seed
        {
            get { return _seed; }
            set { _seed = value; }
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
                _weights[i] = Math.Pow(f, -1.0);
                f *= _lacunarity;
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
            x *= _frequency;
            y *= _frequency;
            z *= _frequency;
            var value = 0.0;
            var weight = 1.0;
            var offset = 1.0; // TODO: Review why Offset is never assigned
            var gain = 2.0;   // TODO: Review why gain is never assigned
            for (var i = 0; i < _octaveCount; i++)
            {
                var nx = Utils.MakeInt32Range(x);
                var ny = Utils.MakeInt32Range(y);
                var nz = Utils.MakeInt32Range(z);
                long seed = (_seed + i) & 0x7fffffff;
                var signal = Utils.GradientCoherentNoise3D(nx, ny, nz, seed, _quality);
                signal = Math.Abs(signal);
                signal = offset - signal;
                signal *= signal;
                signal *= weight;
                weight = signal * gain;
                weight = Mathf.Clamp01((float) weight);
                value += (signal * _weights[i]);
                x *= _lacunarity;
                y *= _lacunarity;
                z *= _lacunarity;
            }
            return (value * 1.25) - 1.0;
        }

        #endregion
    }
}