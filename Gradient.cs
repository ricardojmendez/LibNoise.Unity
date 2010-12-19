using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

using UnityEngine;

namespace LibNoise.Unity
{
    /// <summary>
    /// Provides a color gradient.
    /// </summary>
    public struct Gradient
    {
        #region Fields

        private List<KeyValuePair<double, Color>> m_data;
        private bool m_inverted;

        private static Gradient _empty;
        private static Gradient _terrain;
        private static Gradient _grayscale;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Gradient.
        /// </summary>
        static Gradient()
        {
            Gradient._terrain.m_data = new List<KeyValuePair<double, Color>>();
            Gradient._terrain.m_data.Add(new KeyValuePair<double, Color>(-1.0, new Color(0, 0, 128)));
            Gradient._terrain.m_data.Add(new KeyValuePair<double, Color>(-0.2, new Color(32, 64, 128)));
            Gradient._terrain.m_data.Add(new KeyValuePair<double, Color>(-0.04, new Color(64, 96, 192)));
            Gradient._terrain.m_data.Add(new KeyValuePair<double, Color>(-0.02, new Color(192, 192, 128)));
            Gradient._terrain.m_data.Add(new KeyValuePair<double, Color>(0.0, new Color(0, 192, 0)));
            Gradient._terrain.m_data.Add(new KeyValuePair<double, Color>(0.25, new Color(192, 192, 0)));
            Gradient._terrain.m_data.Add(new KeyValuePair<double, Color>(0.5, new Color(160, 96, 64)));
            Gradient._terrain.m_data.Add(new KeyValuePair<double, Color>(0.75, new Color(128, 255, 255)));
            Gradient._terrain.m_data.Add(new KeyValuePair<double, Color>(1.0, new Color(255, 255, 255)));
            Gradient._terrain.m_inverted = false;
            Gradient._grayscale.m_data = new List<KeyValuePair<double, Color>>();
            Gradient._grayscale.m_data.Add(new KeyValuePair<double, Color>(-1.0, Color.black));
            Gradient._grayscale.m_data.Add(new KeyValuePair<double, Color>(1.0, Color.white));
            Gradient._grayscale.m_inverted = false;
            Gradient._empty.m_data = new List<KeyValuePair<double, Color>>();
            Gradient._empty.m_data.Add(new KeyValuePair<double, Color>(-1.0, Color.clear));
            Gradient._empty.m_data.Add(new KeyValuePair<double, Color>(1.0, Color.clear));
            Gradient._empty.m_inverted = false;
        }

        /// <summary>
        /// Initializes a new instance of Gradient.
        /// </summary>
        public Gradient(Color color)
        {
            this.m_data = new List<KeyValuePair<double, Color>>();
            this.m_data.Add(new KeyValuePair<double, Color>(-1.0, color));
            this.m_data.Add(new KeyValuePair<double, Color>(1.0, color));
            this.m_inverted = false;
        }

        /// <summary>
        /// Initializes a new instance of Gradient.
        /// </summary>
        public Gradient(Color start, Color end)
        {
            this.m_data = new List<KeyValuePair<double, Color>>();
            this.m_data.Add(new KeyValuePair<double, Color>(-1.0, start));
            this.m_data.Add(new KeyValuePair<double, Color>(1.0, end));
            this.m_inverted = false;
        }

        #endregion

        #region Indexers

        /// <summary>
        /// Gets or sets a gradient step by its position.
        /// </summary>
        /// <param name="position">The position of the gradient step.</param>
        /// <returns>The corresponding color value.</returns>
        public Color this[double position]
        {
            get
            {
                int i = 0;
                for (i = 0; i < this.m_data.Count; i++)
                {
                    if (position < this.m_data[i].Key)
                    {
                        break;
                    }
                }
                int i0 = (int)Mathf.Clamp(i - 1, 0, this.m_data.Count - 1);
                int i1 = (int)Mathf.Clamp(i, 0, this.m_data.Count - 1);
                if (i0 == i1)
                {
                    return this.m_data[i1].Value;
                }
                double ip0 = this.m_data[i0].Key;
                double ip1 = this.m_data[i1].Key;
                double a = (position - ip0) / (ip1 - ip0);
                if (this.m_inverted)
                {
                    a = 1.0 - a;
                    double t = ip0;
                    ip0 = ip1;
                    ip1 = t;
                }
                return Color.Lerp(this.m_data[i0].Value, this.m_data[i1].Value, (float)a);
            }
            set
            {
                for (int i = 0; i < this.m_data.Count; i++)
                {
                    if (this.m_data[i].Key == position)
                    {
                        this.m_data.RemoveAt(i);
                        break;
                    }
                }
                this.m_data.Add(new KeyValuePair<double, Color>(position, value));
                this.m_data.Sort(delegate(KeyValuePair<double, Color> lhs, KeyValuePair<double, Color> rhs)
                    { return lhs.Key.CompareTo(rhs.Key); });
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value whether the gradient is inverted.
        /// </summary>
        public bool IsInverted
        {
            get { return this.m_inverted; }
            set { this.m_inverted = value; }
        }

        /// <summary>
        /// Gets the empty instance of Gradient.
        /// </summary>
        public static Gradient Empty
        {
            get { return Gradient._empty; }
        }

        /// <summary>
        /// Gets the grayscale instance of Gradient.
        /// </summary>
        public static Gradient Grayscale
        {
            get { return Gradient._grayscale; }
        }

        /// <summary>
        /// Gets the terrain instance of Gradient.
        /// </summary>
        public static Gradient Terrain
        {
            get { return Gradient._terrain; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clears the gradient to transparent black.
        /// </summary>
        public void Clear()
        {
            this.m_data.Clear();
            this.m_data.Add(new KeyValuePair<double, Color>(0.0, Color.clear));
            this.m_data.Add(new KeyValuePair<double, Color>(1.0, Color.clear));
        }

        /// <summary>
        /// Inverts the gradient.
        /// </summary>
        public void Invert()
        {
            // UNDONE: public void Invert()
            throw new NotImplementedException();
        }

        #endregion
    }
}