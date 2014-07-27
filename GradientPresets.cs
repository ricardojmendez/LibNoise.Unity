using System.Collections.Generic;
using UnityEngine;

namespace LibNoise
{
    /// <summary>
    /// Provides a series of gradient presets
    /// </summary>
    public static class GradientPresets
    {
        #region Fields

        private static readonly Gradient _empty;
        private static readonly Gradient _grayscale;
        private static readonly Gradient _rgb;
        private static readonly Gradient _rgba;
        private static readonly Gradient _terrain;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Gradient.
        /// </summary>
        static GradientPresets()
        {
            // Grayscale gradient color keys
            var grayscaleColorKeys = new List<GradientColorKey>
            {
                new GradientColorKey(Color.black, 0),
                new GradientColorKey(Color.white, 1)
            };

            // RGB gradient color keys
            var rgbColorKeys = new List<GradientColorKey>
            {
                new GradientColorKey(Color.red, 0),
                new GradientColorKey(Color.green, 0.5f),
                new GradientColorKey(Color.blue, 1)
            };

            // RGBA gradient color keys
            var rgbaColorKeys = new List<GradientColorKey>
            {
                new GradientColorKey(Color.red, 0),
                new GradientColorKey(Color.green, 1 / 3f),
                new GradientColorKey(Color.blue, 2 / 3f),
                new GradientColorKey(Color.black, 1)
            };

            // RGBA gradient alpha keys
            var rgbaAlphaKeys = new List<GradientAlphaKey> {new GradientAlphaKey(0, 2 / 3f), new GradientAlphaKey(1, 1)};

            // Terrain gradient color keys
            var terrainColorKeys = new List<GradientColorKey>
            {
                new GradientColorKey(new Color(0, 0, 0.5f), 0),
                new GradientColorKey(new Color(0.125f, 0.25f, 0.5f), 0.4f),
                new GradientColorKey(new Color(0.25f, 0.375f, 0.75f), 0.48f),
                new GradientColorKey(new Color(0, 0.75f, 0), 0.5f),
                new GradientColorKey(new Color(0.75f, 0.75f, 0), 0.625f),
                new GradientColorKey(new Color(0.625f, 0.375f, 0.25f), 0.75f),
                new GradientColorKey(new Color(0.5f, 1, 1), 0.875f),
                new GradientColorKey(Color.white, 1)
            };

            // Generic gradient alpha keys
            var alphaKeys = new List<GradientAlphaKey> {new GradientAlphaKey(1, 0), new GradientAlphaKey(1, 1)};

            _empty = new Gradient();

            _rgb = new Gradient();
            _rgb.SetKeys(rgbColorKeys.ToArray(), alphaKeys.ToArray());

            _rgba = new Gradient();
            _rgba.SetKeys(rgbaColorKeys.ToArray(), rgbaAlphaKeys.ToArray());

            _grayscale = new Gradient();
            _grayscale.SetKeys(grayscaleColorKeys.ToArray(), alphaKeys.ToArray());

            _terrain = new Gradient();
            _terrain.SetKeys(terrainColorKeys.ToArray(), alphaKeys.ToArray());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the empty instance of Gradient.
        /// </summary>
        public static Gradient Empty
        {
            get { return _empty; }
        }

        /// <summary>
        /// Gets the grayscale instance of Gradient.
        /// </summary>
        public static Gradient Grayscale
        {
            get { return _grayscale; }
        }

        /// <summary>
        /// Gets the RGB instance of Gradient.
        /// </summary>
        public static Gradient RGB
        {
            get { return _rgb; }
        }

        /// <summary>
        /// Gets the RGBA instance of Gradient.
        /// </summary>
        public static Gradient RGBA
        {
            get { return _rgba; }
        }

        /// <summary>
        /// Gets the terrain instance of Gradient.
        /// </summary>
        public static Gradient Terrain
        {
            get { return _terrain; }
        }

        #endregion
    }
}