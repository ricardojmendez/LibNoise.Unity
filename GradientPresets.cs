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
        private static Gradient _island;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Gradient.
        /// </summary>
        static GradientPresets()
        {
            var alphaKeys = new List<GradientAlphaKey>();
            alphaKeys.Add(new GradientAlphaKey(1, 0));
            alphaKeys.Add(new GradientAlphaKey(1, 1));

            var islandKeys = new List<GradientColorKey>();
            islandKeys.Add(new GradientColorKey(new Color(  3/255f,  29/255f,  63/255f), 0));
            islandKeys.Add(new GradientColorKey(new Color(  7/255f, 106/255f, 127/255f), 0.250f));
            islandKeys.Add(new GradientColorKey(new Color( 62/255f,  86/255f,  30/255f), 0.375f));
            islandKeys.Add(new GradientColorKey(new Color( 84/255f,  96/255f,  50/255f), 0.500f));
            islandKeys.Add(new GradientColorKey(new Color(130/255f, 127/255f,  97/255f), 0.625f));
            islandKeys.Add(new GradientColorKey(new Color(184/255f, 163/255f, 141/255f), 0.750f));
            islandKeys.Add(new GradientColorKey(new Color(212/255f, 202/255f, 204/255f), 0.875f));
            islandKeys.Add(new GradientColorKey(new Color(234/255f, 232/255f, 236/255f), 1));

            _island = new Gradient();
            _island.SetKeys(islandKeys.ToArray(), alphaKeys.ToArray());

            var terrainKeys = new List<GradientColorKey>();
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

            var rgbKeys = new List<GradientColorKey>();
            rgbKeys.Add(new GradientColorKey(new Color(0, 0, 1), 0));
            rgbKeys.Add(new GradientColorKey(new Color(0, 1, 0), 0.5f));
            rgbKeys.Add(new GradientColorKey(new Color(1, 0, 0), 1));

            _rgb = new Gradient();
            _rgb.SetKeys(rgbKeys.ToArray(), alphaKeys.ToArray());

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

        /// <summary>
        /// Gets the island instance of Gradient.
        /// </summary>
        public static Gradient Island
        {
            get { return _island; }
        }

        #endregion
    }
}