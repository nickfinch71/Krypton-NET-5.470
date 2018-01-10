﻿// *****************************************************************************
// 
//  © Component Factory Pty Ltd, modifications by Peter Wagner & Simon Coghlan 2010 - 2018. All rights reserved. (https://github.com/Wagnerp/Krypton-NET-4.7)
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Version 4.7.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System.Drawing;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Details for a popup page event.
	/// </summary>
    public class PopupPageEventArgs : KryptonPageCancelEventArgs
	{
		#region Instance Fields

	    #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PopupPageEventArgs class.
		/// </summary>
		/// <param name="page">Page effected by event.</param>
		/// <param name="index">Index of page in the owning collection.</param>
        /// <param name="screenRect">Screen rectangle for showing the popup.</param>
        public PopupPageEventArgs(KryptonPage page, 
                                  int index, 
                                  Rectangle screenRect)
			: base(page, index)
		{
            ScreenRect = screenRect;
		}
		#endregion

        #region ScreenRect
        /// <summary>
		/// Gets and sets the screen rectangle for showing the popup page.
		/// </summary>
        public Rectangle ScreenRect { get; set; }

	    #endregion
	}
}
