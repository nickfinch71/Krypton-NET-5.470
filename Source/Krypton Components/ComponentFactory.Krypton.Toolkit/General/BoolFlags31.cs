﻿// *****************************************************************************
// 
//  © Component Factory Pty Ltd, modifications by Peter Wagner & Simon Coghlan 2010 - 2018. All rights reserved. (https://github.com/Wagnerp/Krypton-NET-4.7)
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Version 4.7.0.0 	www.ComponentFactory.com
// *****************************************************************************

namespace ComponentFactory.Krypton.Toolkit
{
    /// <summary>
    /// Manages a collection of 31 boolean flags.
    /// </summary>
    public struct BoolFlags31
    {
        #region Instance Fields

        #endregion

        #region Public
        /// <summary>
        /// Gets and sets the entire flags value.
        /// </summary>
        public int Flags { get; set; }

        /// <summary>
        /// Set all the provided flags to true.
        /// </summary>
        /// <param name="flags">Flags to set.</param>
        /// <return>Set of flags that have changed in value.</return>
        public int SetFlags(int flags)
        {
            int before = Flags;

            // Set all the provided flags
            Flags |= flags;

            // Return set of flags that have changed value
            return (before ^ Flags);
        }

        /// <summary>
        /// Clear all the provided flags to false.
        /// </summary>
        /// <param name="flags">Flags to clear.</param>
        /// <return>Set of flags that have changed in value.</return>
        public int ClearFlags(int flags)
        {
            int before = Flags;

            // Clear all the provided flags
            Flags &= ~flags;

            // Return set of flags that have changed value
            return (before ^ Flags);
        }

        /// <summary>
        /// Are all the provided flags set to true.
        /// </summary>
        /// <param name="flags">Flags to test.</param>
        /// <returns>True if all flags are set; otherwise false.</returns>
        public bool AreFlagsSet(int flags)
        {
            return ((Flags & flags) == flags);
        }
        #endregion
    }
}
