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
	/// Details for close reason event handlers.
	/// </summary>
	public class CloseReasonEventArgs : CancelEventArgs
	{
		#region Instance Fields

	    #endregion

		#region Identity
        /// <summary>
        /// Initialize a new instance of the CloseReasonEventArgs class.
		/// </summary>
        /// <param name="closeReason">Reason for the close action occuring.</param>
        public CloseReasonEventArgs(ToolStripDropDownCloseReason closeReason)
		{
            CloseReason = closeReason;
		}
        #endregion

		#region Public
		/// <summary>
		/// Gets access to the reason for the context menu closing.
		/// </summary>
        public ToolStripDropDownCloseReason CloseReason { get; }

	    #endregion
	}
}
