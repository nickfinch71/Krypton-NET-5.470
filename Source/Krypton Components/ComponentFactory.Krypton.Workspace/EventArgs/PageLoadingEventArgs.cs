﻿// *****************************************************************************
// 
//  © Component Factory Pty Ltd, modifications by Peter Wagner & Simon Coghlan 2010 - 2018. All rights reserved. (https://github.com/Wagnerp/Krypton-NET-4.7)
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Version 4.7.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System.Xml;
using ComponentFactory.Krypton.Navigator;

namespace ComponentFactory.Krypton.Workspace
{
	/// <summary>
	/// Event data for persisting extra data for a workspace cell page.
	/// </summary>
	public class PageLoadingEventArgs : XmlLoadingEventArgs
	{
		#region Instance Fields

	    #endregion

		#region Identity
		/// <summary>
        /// Initialize a new instance of the PageLoadingEventArgs class.
		/// </summary>
        /// <param name="workspace">Reference to owning workspace control.</param>
        /// <param name="page">Reference to owning workspace cell page.</param>
        /// <param name="xmlReader">Xml reader for persisting custom data.</param>
        public PageLoadingEventArgs(KryptonWorkspace workspace,
                                    KryptonPage page,
                                    XmlReader xmlReader)
            : base(workspace, xmlReader)
		{
            Page = page;
		}
		#endregion

		#region Public
		/// <summary>
        /// Gets the workspace cell page reference.
		/// </summary>
        public KryptonPage Page { get; set; }

	    #endregion
	}
}
