using System;
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
        private static Gradient _empty;
        private static Gradient _terrain;
        private static Gradient _grayscale;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Gradient.
        /// </summary>
        static GradientPresets()
        {
            var terrainKeys = new List<GradientColorKey>();
            terrainKeys.Add(new GradientColorKey(new Color(0, 0, 0.5f), 0));
            terrainKeys.Add(new GradientColorKey(new Color(0.125f, 0.25f, 0.5f), 0.4f));
            terrainKeys.Add(new GradientColorKey(new Color(0.25f, 0.375f, 0.75f), 0.48f));
            terrainKeys.Add(new GradientColorKey(new Color(0, 0.75f, 0), 0.5f));
            terrainKeys.Add(new GradientColorKey(new Color(0.75f, 0.75f, 0), 0.625f));
            terrainKeys.Add(new GradientColorKey(new Color(0.625f, 0.375f, 0.25f), 0.75f));
            terrainKeys.Add(new GradientColorKey(new Color(0.5f, 1, 1), 0.875f));
            terrainKeys.Add(new GradientColorKey(Color.white, 1));

            var alphaKeys = new List<GradientAlphaKey>();
            alphaKeys.Add(new GradientAlphaKey(1, 0));
            alphaKeys.Add(new GradientAlphaKey(1, 1));

            _terrain = new Gradient();
            _terrain.SetKeys(terrainKeys.ToArray(), alphaKeys.ToArray());

            var grayscaleKeys = new List<GradientColorKey>();
            grayscaleKeys.Add(new GradientColorKey(Color.black, 0));
            grayscaleKeys.Add(new GradientColorKey(Color.white, 1));

            _grayscale = new Gradient();
            _grayscale.SetKeys(grayscaleKeys.ToArray(), alphaKeys.ToArray());

            _empty = new Gradient();
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
        /// Gets the terrain instance of Gradient.
        /// </summary>
        public static Gradient Terrain
        {
            get { return _terrain; }
        }

        #endregion

    }
}