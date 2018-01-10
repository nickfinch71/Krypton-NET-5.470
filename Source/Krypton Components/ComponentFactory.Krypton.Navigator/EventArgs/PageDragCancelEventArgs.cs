﻿// *****************************************************************************
// 
//  © Component Factory Pty Ltd, modifications by Peter Wagner & Simon Coghlan 2010 - 2018. All rights reserved. (https://github.com/Wagnerp/Krypton-NET-4.7)
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Version 4.7.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace ComponentFactory.Krypton.Navigator
{
	/// <summary>
	/// Details for an cancellable event that provides pages associated with a page dragging event.
	/// </summary>
	public class PageDragCancelEventArgs : CancelEventArgs
	{
		#region Instance Fields

	    #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PageDragCancelEventArgs class.
		/// </summary>
        /// <param name="elementOffset">Offset from the top left of the element.</param>
        /// <param name="screenPoint">Screen point of the mouse.</param>
        /// <param name="c">Control that started the drag operation.</param>
        /// <param name="pages">Array of event associated pages.</param>
        public PageDragCancelEventArgs(Point screenPoint,
                                       Point elementOffset,
                                       Control c,
                                       KryptonPage[] pages)
		{
            ScreenPoint = screenPoint;
            ElementOffset = elementOffset;
            Control = c;
            Pages = new KryptonPageCollection();

            if (pages != null)
            {
                Pages.AddRange(pages);
            }
		}

        /// <summary>
        /// Initialize a new instance of the PageDragCancelEventArgs class.
        /// </summary>
        /// <param name="screenPoint">Screen point of the mouse.</param>
        /// <param name="elementOffset">Offset from the top left of the element.</param>
        /// <param name="c">Control that started the drag operation.</param>
        /// <param name="pages">Collection of event associated pages.</param>
        public PageDragCancelEventArgs(Point screenPoint,
                                       Point elementOffset,
                                       Control c,
                                       KryptonPageCollection pages)
        {
            ScreenPoint = screenPoint;
            ElementOffset = elementOffset;
            Control = c;
            Pages = pages;
        }
        #endregion

        #region ScreenPoint
        /// <summary>
        /// Gets access to the associated screen point.
        /// </summary>
        public Point ScreenPoint { get; }

	    #endregion

        #region ElementOffset
        /// <summary>
        /// Gets access to the associated element offset.
        /// </summary>
        public Point ElementOffset { get; }

	    #endregion

        #region ElementOffset
        /// <summary>
        /// Gets access to the control that started the drag operation.
        /// </summary>
        public Control Control { get; }

	    #endregion

        #region Pages
        /// <summary>
        /// Gets access to the collection of pages.
        /// </summary>
        public KryptonPageCollection Pages { get; }

	    #endregion
    }
}
