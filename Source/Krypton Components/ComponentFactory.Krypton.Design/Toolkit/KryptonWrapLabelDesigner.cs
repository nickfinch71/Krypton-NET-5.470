﻿// *****************************************************************************
// 
//  © Component Factory Pty Ltd, modifications by Peter Wagner & Simon Coghlan 2010 - 2018. All rights reserved. (https://github.com/Wagnerp/Krypton-NET-4.7)
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Version 4.7.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace ComponentFactory.Krypton.Toolkit
{
    internal class KryptonWrapLabelDesigner : ControlDesigner
	{
		#region Identity
		/// <summary>
        /// Initialize a new instance of the KryptonWrapLabelDesigner class.
		/// </summary>
        public KryptonWrapLabelDesigner()
        {
            // The resizing handles around the control need to change depending on the
            // value of the AutoSize and AutoSizeMode properties. When in AutoSize you
            // do not get the resizing handles, otherwise you do.
            AutoResizeHandles = true;
        }            
		#endregion

        #region Public Overrides
        /// <summary>
        ///  Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                // Create a collection of action lists
                DesignerActionListCollection actionLists = new DesignerActionListCollection
                {

                    // Add the wrpa label specific list
                    new KryptonWrapLabelActionList(this)
                };

                return actionLists;
            }
        }
        #endregion
    }
}
