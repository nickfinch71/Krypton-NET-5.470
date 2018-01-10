﻿// *****************************************************************************
// 
//  © Component Factory Pty Ltd, modifications by Peter Wagner & Simon Coghlan 2010 - 2018. All rights reserved. (https://github.com/Wagnerp/Krypton-NET-4.7)
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Version 4.7.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Docking
{
	/// <summary>
    /// Event arguments for events that need to provide a colletion of pages.
	/// </summary>
	public class PagesEventArgs : EventArgs
	{
		#region Instance Fields

	    #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PagesEventArgs class.
		/// </summary>
        /// <param name="pages">Collection of pages.</param>
        public PagesEventArgs(KryptonPageCollection pages)
		{
            Pages = pages;
		}
        #endregion

		#region Public
        /// <summary>
        /// Gets access to a collection of pages.
        /// </summary>
        public KryptonPageCollection Pages { get; }

	    #endregion
	}
}
