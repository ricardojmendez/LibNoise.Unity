using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Microsoft.Xna.Framework;
using UnityEngine;

namespace LibNoise.Unity
{
    #region Enumerations

    /// <summary>
    /// Defines a collection of quality modes.
    /// </summary>
    public enum QualityMode
    {
        Low,
        Medium,
        High,
    }

    #endregion

    /// <summary>
    /// Base class for noise modules.
    /// </summary>
    public abstract class ModuleBase : IDisposable
    {
        #region Fields

        protected ModuleBase[] m_modules = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of Helpers.
        /// </summary>
        /// <param name="count">The number of source modules.</param>
        protected ModuleBase(int count)
        {
            if (count > 0)
            {
                this.m_modules = new ModuleBase[count];
            }
        }

        #endregion

        #region Indexers

        /// <summary>
        /// Gets or sets a source module by index.
        /// </summary>
        /// <param name="index">The index of the source module to aquire.</param>
        /// <returns>The requested source module.</returns>
        public virtual ModuleBase this[int index]
        {
            get
            {
                System.Diagnostics.Debug.Assert(this.m_modules != null);
                System.Diagnostics.Debug.Assert(this.m_modules.Length > 0);
                if (index < 0 || index >= this.m_modules.Length)
                {
                    throw new ArgumentOutOfRangeException("Index out of valid module range");
                }
                if (this.m_modules[index] == null)
                {
                    throw new ArgumentNullException("Desired element is null");
                }
                return this.m_modules[index];
            }
            set
            {
                System.Diagnostics.Debug.Assert(this.m_modules.Length > 0);
                if (index < 0 || index >= this.m_modules.Length)
                {
                    throw new ArgumentOutOfRangeException("Index out of valid module range");
                }
                if (value == null)
                {
                    throw new ArgumentNullException("Value should not be null");
                }
                this.m_modules[index] = value;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of source modules required by this noise module.
        /// </summary>
        public int SourceModuleCount
        {
            get { return (this.m_modules == null) ? 0 : this.m_modules.Length; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the output value for the given input coordinates.
        /// </summary>
        /// <param name="x">The input coordinate on the x-axis.</param>
        /// <param name="y">The input coordinate on the y-axis.</param>
        /// <param name="z">The input coordinate on the z-axis.</param>
        /// <returns>The resulting output value.</returns>
        public abstract double GetValue(double x, double y, double z);

        /// <summary>
        /// Returns the output value for the given input coordinates.
        /// </summary>
        /// <param name="coordinate">The input coordinate.</param>
        /// <returns>The resulting output value.</returns>
        public double GetValue(Vector3 coordinate)
        {
            return this.GetValue((double)coordinate.x, (double)coordinate.y, (double)coordinate.z);
        }

        /// <summary>
        /// Returns the output value for the given input coordinates.
        /// </summary>
        /// <param name="coordinate">The input coordinate.</param>
        /// <returns>The resulting output value.</returns>
        public double GetValue(ref Vector3 coordinate)
        {
            return this.GetValue((double)coordinate.x, (double)coordinate.y, (double)coordinate.z);
        }

        #endregion

        #region IDisposable Members

        [System.Xml.Serialization.XmlIgnore]
#if !XBOX360 && !ZUNE
        [NonSerialized]
#endif
        private bool m_disposed = false;

        /// <summary>
        /// Gets a value whether the object is disposed.
        /// </summary>
        public bool IsDisposed
        {
            get { return this.m_disposed; }
        }

        /// <summary>
        /// Immediately releases the unmanaged resources used by this object.
        /// </summary>
        public void Dispose()
        {
            if (!this.m_disposed) { this.m_disposed = this.Disposing(); }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Immediately releases the unmanaged resources used by this object.
        /// </summary>
        /// <returns>True if the object is completely disposed.</returns>
        protected virtual bool Disposing()
        {
            if (this.m_modules != null)
            {
                for (int i = 0; i < this.m_modules.Length; i++)
                {
                    this.m_modules[i].Dispose();
                    this.m_modules[i] = null;
                }
                this.m_modules = null;
            }
            return true;
        }

        #endregion
    }
}