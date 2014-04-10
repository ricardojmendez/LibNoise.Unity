namespace LibNoise.Unity
{
    using System;
    using System.Collections.Generic;

    using UnityEngine;

    /// <summary>
    /// Provides a series of gradient presets
    /// </summary>
    public static class GradientPresets
    {
        #region Fields

        private static Gradient _empty;
        private static Gradient _grayscale;
        private static Gradient _rgb;
        private static Gradient _terrain;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Gradient.
        /// </summary>
        static GradientPresets()
        {
            _empty = new Gradient();

            List<GradientAlphaKey> alphaKeys = new List<GradientAlphaKey>();
            alphaKeys.Add(new GradientAlphaKey(1, 0));
            alphaKeys.Add(new GradientAlphaKey(1, 1));

            List<GradientColorKey> grayscaleKeys = new List<GradientColorKey>();
            grayscaleKeys.Add(new GradientColorKey(Color.black, 0));
            grayscaleKeys.Add(new GradientColorKey(Color.white, 1));

            _grayscale = new Gradient();
            _grayscale.SetKeys(grayscaleKeys.ToArray(), alphaKeys.ToArray());

            List<GradientColorKey> rgbKeys = new List<GradientColorKey>();
            rgbKeys.Add(new GradientColorKey(new Color(1, 0, 0), 0));
            rgbKeys.Add(new GradientColorKey(new Color(0, 1, 0), 0.5f));
            rgbKeys.Add(new GradientColorKey(new Color(0, 0, 1), 1));

            _rgb = new Gradient();
            _rgb.SetKeys(rgbKeys.ToArray(), alphaKeys.ToArray());

            List<GradientColorKey> terrainKeys = new List<GradientColorKey>();
            terrainKeys.Add(new GradientColorKey(new Color(0, 0, 0.5f), 0));
            terrainKeys.Add(new GradientColorKey(new Color(0.125f, 0.25f, 0.5f), 0.4f));
            terrainKeys.Add(new GradientColorKey(new Color(0.25f, 0.375f, 0.75f), 0.48f));
            terrainKeys.Add(new GradientColorKey(new Color(0, 0.75f, 0), 0.5f));
            terrainKeys.Add(new GradientColorKey(new Color(0.75f, 0.75f, 0), 0.625f));
            terrainKeys.Add(new GradientColorKey(new Color(0.625f, 0.375f, 0.25f), 0.75f));
            terrainKeys.Add(new GradientColorKey(new Color(0.5f, 1, 1), 0.875f));
            terrainKeys.Add(new GradientColorKey(Color.white, 1));

            _terrain = new Gradient();
            _terrain.SetKeys(terrainKeys.ToArray(), alphaKeys.ToArray());
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
        /// Gets the terrain instance of Gradient.
        /// </summary>
        public static Gradient Terrain
        {
            get { return _terrain; }
        }

        #endregion
    }
}