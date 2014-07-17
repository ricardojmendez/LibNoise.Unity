using System.Collections.Generic;
using UnityEngine;

namespace LibNoise.Unity
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
            var grayscaleColorKeys = new List<GradientColorKey>();
            grayscaleColorKeys.Add(new GradientColorKey(Color.black, 0));
            grayscaleColorKeys.Add(new GradientColorKey(Color.white, 1));

            // RGB gradient color keys
            var rgbColorKeys = new List<GradientColorKey>();
            rgbColorKeys.Add(new GradientColorKey(Color.red, 0));
            rgbColorKeys.Add(new GradientColorKey(Color.green, 0.5f));
            rgbColorKeys.Add(new GradientColorKey(Color.blue, 1));

            // RGBA gradient color keys
            var rgbaColorKeys = new List<GradientColorKey>();
            rgbaColorKeys.Add(new GradientColorKey(Color.red, 0));
            rgbaColorKeys.Add(new GradientColorKey(Color.green, 1 / 3f));
            rgbaColorKeys.Add(new GradientColorKey(Color.blue, 2 / 3f));
            rgbaColorKeys.Add(new GradientColorKey(Color.black, 1));

            // RGBA gradient alpha keys
            var rgbaAlphaKeys = new List<GradientAlphaKey>();
            rgbaAlphaKeys.Add(new GradientAlphaKey(0, 2 / 3f));
            rgbaAlphaKeys.Add(new GradientAlphaKey(1, 1));

            // Terrain gradient color keys
            var terrainColorKeys = new List<GradientColorKey>();
            terrainColorKeys.Add(new GradientColorKey(new Color(0, 0, 0.5f), 0));
            terrainColorKeys.Add(new GradientColorKey(new Color(0.125f, 0.25f, 0.5f), 0.4f));
            terrainColorKeys.Add(new GradientColorKey(new Color(0.25f, 0.375f, 0.75f), 0.48f));
            terrainColorKeys.Add(new GradientColorKey(new Color(0, 0.75f, 0), 0.5f));
            terrainColorKeys.Add(new GradientColorKey(new Color(0.75f, 0.75f, 0), 0.625f));
            terrainColorKeys.Add(new GradientColorKey(new Color(0.625f, 0.375f, 0.25f), 0.75f));
            terrainColorKeys.Add(new GradientColorKey(new Color(0.5f, 1, 1), 0.875f));
            terrainColorKeys.Add(new GradientColorKey(Color.white, 1));

            // Generic gradient alpha keys
            var alphaKeys = new List<GradientAlphaKey>();
            alphaKeys.Add(new GradientAlphaKey(1, 0));
            alphaKeys.Add(new GradientAlphaKey(1, 1));

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