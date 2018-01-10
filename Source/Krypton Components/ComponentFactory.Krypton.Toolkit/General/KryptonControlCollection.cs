﻿// *****************************************************************************
// 
//  © Component Factory Pty Ltd, modifications by Peter Wagner & Simon Coghlan 2010 - 2018. All rights reserved. (https://github.com/Wagnerp/Krypton-NET-4.7)
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Version 4.7.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System.ComponentModel;
using System.Windows.Forms;

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Base class for krypton specific control collections.
    /// </summary>
    public class KryptonControlCollection : Control.ControlCollection
    {
        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonControlCollection class.
        /// </summary>
        /// <param name="owner">Owning control.</param>
        public KryptonControlCollection(Control owner)
            : base(owner)
        {
        }
        #endregion

        #region AddInternal
        /// <summary>
        /// Add a control to the collection overriding the normal checks.
        /// </summary>
        /// <param name="control">Control to be added.</param>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void AddInternal(Control control)
        {
            base.Add(control);
        }
        #endregion

        #region RemoveInternal
        /// <summary>
        /// Add a control to the collection overriding the normal checks.
        /// </summary>
        /// <param name="control">Control to be added.</param>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RemoveInternal(Control control)
        {
            base.Remove(control);
        }
        #endregion

        #region ClearInternal
        /// <summary>
        /// Clear out all the entries in the collection.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ClearInternal()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                RemoveInternal(this[i]);
            }
        }
        #endregion
    }
}
