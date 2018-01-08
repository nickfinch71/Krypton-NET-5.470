﻿// *****************************************************************************
// 
//  © Component Factory Pty Ltd 2018. All rights reserved.
//	The software and associated documentation supplied hereunder are the 
//  proprietary information of Component Factory Pty Ltd, 13 Swallows Close, 
//  Mornington, Vic 3931, Australia and are supplied subject to licence terms.
// 
//  Version 4.7.0.0 	www.ComponentFactory.com
// *****************************************************************************

using System;
using System.Collections;
using System.Drawing;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Diagnostics;
using ComponentFactory.Krypton.Toolkit;

namespace ComponentFactory.Krypton.Ribbon
{
    internal class KryptonRibbonGroupRichTextBoxDesigner : ComponentDesigner, IKryptonDesignObject
    {
        #region Instance Fields
        private IDesignerHost _designerHost;
        private IComponentChangeService _changeService;
        private KryptonRibbonGroupRichTextBox _ribbonRichTextBox;
        private DesignerVerbCollection _verbs;
        private DesignerVerb _toggleHelpersVerb;
        private DesignerVerb _moveFirstVerb;
        private DesignerVerb _movePrevVerb;
        private DesignerVerb _moveNextVerb;
        private DesignerVerb _moveLastVerb;
        private DesignerVerb _deleteRichTextBoxVerb;
        private ContextMenuStrip _cms;
        private ToolStripMenuItem _toggleHelpersMenu;
        private ToolStripMenuItem _visibleMenu;
        private ToolStripMenuItem _moveFirstMenu;
        private ToolStripMenuItem _movePreviousMenu;
        private ToolStripMenuItem _moveNextMenu;
        private ToolStripMenuItem _moveLastMenu;
        private ToolStripMenuItem _deleteRichTextBoxMenu;

        #endregion

        #region Identity
        /// <summary>
        /// Initialize a new instance of the KryptonRibbonGroupRichTextBoxDesigner class.
        /// </summary>
        public KryptonRibbonGroupRichTextBoxDesigner()
        {
        }
        #endregion

        #region Public
        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The IComponent to associate the designer with.</param>
        public override void Initialize(IComponent component)
        {
            Debug.Assert(component != null);

            // Validate the parameter reference
            if (component == null)
            {
                throw new ArgumentNullException("component");
            }

            // Let base class do standard stuff
            base.Initialize(component);

            // Cast to correct type
            _ribbonRichTextBox = (KryptonRibbonGroupRichTextBox)component;
            _ribbonRichTextBox.RichTextBoxDesigner = this;

            // Update designer properties with actual starting values
            Visible = _ribbonRichTextBox.Visible;
            Enabled = _ribbonRichTextBox.Enabled;

            // Update visible/enabled to always be showing/enabled at design time
            _ribbonRichTextBox.Visible = true;
            _ribbonRichTextBox.Enabled = true;

            // Tell the embedded text box it is in design mode
            _ribbonRichTextBox.RichTextBox.InRibbonDesignMode = true;

            // Hook into events
            _ribbonRichTextBox.DesignTimeContextMenu += OnContextMenu;

            // Get access to the services
            _designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
            _changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));

            // We need to know when we are being removed/changed
            _changeService.ComponentChanged += OnComponentChanged;
        }

        /// <summary>
        /// Gets the design-time verbs supported by the component that is associated with the designer.
        /// </summary>
        public override DesignerVerbCollection Verbs
        {
            get
            {
                UpdateVerbStatus();
                return _verbs;
            }
        }

        /// <summary>
        /// Gets and sets if the object is enabled.
        /// </summary>
        public bool DesignEnabled 
        { 
            get => Enabled;
            set => Enabled = value;
        }

        /// <summary>
        /// Gets and sets if the object is visible.
        /// </summary>
        public bool DesignVisible 
        {
            get => Visible;
            set => Visible = value;
        }
        #endregion

        #region Protected
        /// <summary>
        /// Releases all resources used by the component. 
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    // Unhook from events
                    _ribbonRichTextBox.DesignTimeContextMenu -= OnContextMenu;
                    _changeService.ComponentChanged -= OnComponentChanged;
                }
            }
            finally
            {
                // Must let base class do standard stuff
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// Adjusts the set of properties the component exposes through a TypeDescriptor.
        /// </summary>
        /// <param name="properties">An IDictionary containing the properties for the class of the component.</param>
        protected override void PreFilterProperties(IDictionary properties)
        {
            base.PreFilterProperties(properties);

            // Setup the array of properties we override
            Attribute[] attributes = new Attribute[0];
            string[] strArray = { "Visible", "Enabled" };

            // Adjust our list of properties
            for (int i = 0; i < strArray.Length; i++)
            {
                PropertyDescriptor descrip = (PropertyDescriptor)properties[strArray[i]];
                if (descrip != null)
                {
                    properties[strArray[i]] = TypeDescriptor.CreateProperty(typeof(KryptonRibbonGroupRichTextBoxDesigner), descrip, attributes);
                }
            }
        }
        #endregion

        #region Internal
        internal bool Visible { get; set; }

        internal bool Enabled { get; set; }

        #endregion

        #region Implementation
        private void ResetVisible()
        {
            Visible = true;
        }

        private bool ShouldSerializeVisible()
        {
            return !Visible;
        }

        private void ResetEnabled()
        {
            Enabled = true;
        }

        private bool ShouldSerializeEnabled()
        {
            return !Enabled;
        }

        private void UpdateVerbStatus()
        {
            // Create verbs first time around
            if (_verbs == null)
            {
                _verbs = new DesignerVerbCollection();
                _toggleHelpersVerb = new DesignerVerb("Toggle Helpers", OnToggleHelpers);
                _moveFirstVerb = new DesignerVerb("Move RichTextBox First", OnMoveFirst);
                _movePrevVerb = new DesignerVerb("Move RichTextBox Previous", OnMovePrevious);
                _moveNextVerb = new DesignerVerb("Move RichTextBox Next", OnMoveNext);
                _moveLastVerb = new DesignerVerb("Move RichTextBox Last", OnMoveLast);
                _deleteRichTextBoxVerb = new DesignerVerb("Delete RichTextBox", OnDeleteTextBox);
                _verbs.AddRange(new DesignerVerb[] { _toggleHelpersVerb, _moveFirstVerb, _movePrevVerb, 
                                                     _moveNextVerb, _moveLastVerb, _deleteRichTextBoxVerb });
            }

            bool moveFirst = false;
            bool movePrev = false;
            bool moveNext = false;
            bool moveLast = false;

            if (_ribbonRichTextBox?.Ribbon != null)
            {
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;
                moveFirst = (items.IndexOf(_ribbonRichTextBox) > 0);
                movePrev = (items.IndexOf(_ribbonRichTextBox) > 0);
                moveNext = (items.IndexOf(_ribbonRichTextBox) < (items.Count - 1));
                moveLast = (items.IndexOf(_ribbonRichTextBox) < (items.Count - 1));
            }

            _moveFirstVerb.Enabled = moveFirst;
            _movePrevVerb.Enabled = movePrev;
            _moveNextVerb.Enabled = moveNext;
            _moveLastVerb.Enabled = moveLast;
        }

        private void OnToggleHelpers(object sender, EventArgs e)
        {
            // Invert the current toggle helper mode
            if (_ribbonRichTextBox?.Ribbon != null)
            {
                _ribbonRichTextBox.Ribbon.InDesignHelperMode = !_ribbonRichTextBox.Ribbon.InDesignHelperMode;
            }
        }

        private void OnMoveFirst(object sender, EventArgs e)
        {
            if (_ribbonRichTextBox?.Ribbon != null)
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupRichTextBox MoveFirst");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonRichTextBox.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the richtextbox
                    items.Remove(_ribbonRichTextBox);
                    items.Insert(0, _ribbonRichTextBox);
                    UpdateVerbStatus();

                    RaiseComponentChanged(propertyItems, null, null);
                }
                finally
                {
                    // If we managed to create the transaction, then do it
                    transaction?.Commit();
                }
            }
        }

        private void OnMovePrevious(object sender, EventArgs e)
        {
            if (_ribbonRichTextBox?.Ribbon != null)
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupRichTextBox MovePrevious");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonRichTextBox.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the richtextbox
                    int index = items.IndexOf(_ribbonRichTextBox) - 1;
                    index = Math.Max(index, 0);
                    items.Remove(_ribbonRichTextBox);
                    items.Insert(index, _ribbonRichTextBox);
                    UpdateVerbStatus();

                    RaiseComponentChanged(propertyItems, null, null);
                }
                finally
                {
                    // If we managed to create the transaction, then do it
                    transaction?.Commit();
                }
            }
        }

        private void OnMoveNext(object sender, EventArgs e)
        {
            if (_ribbonRichTextBox?.Ribbon != null)
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupRichTextBox MoveNext");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonRichTextBox.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the richtextbox
                    int index = items.IndexOf(_ribbonRichTextBox) + 1;
                    index = Math.Min(index, items.Count - 1);
                    items.Remove(_ribbonRichTextBox);
                    items.Insert(index, _ribbonRichTextBox);
                    UpdateVerbStatus();

                    RaiseComponentChanged(propertyItems, null, null);
                }
                finally
                {
                    // If we managed to create the transaction, then do it
                    transaction?.Commit();
                }
            }
        }

        private void OnMoveLast(object sender, EventArgs e)
        {
            if (_ribbonRichTextBox?.Ribbon != null)
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupRichTextBox MoveLast");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonRichTextBox.RibbonContainer)["Items"];

                    RaiseComponentChanging(propertyItems);

                    // Move position of the richtextbox
                    items.Remove(_ribbonRichTextBox);
                    items.Insert(items.Count, _ribbonRichTextBox);
                    UpdateVerbStatus();

                    RaiseComponentChanged(propertyItems, null, null);
                }
                finally
                {
                    // If we managed to create the transaction, then do it
                    transaction?.Commit();
                }
            }
        }

        private void OnDeleteTextBox(object sender, EventArgs e)
        {
            if (_ribbonRichTextBox?.Ribbon != null)
            {
                // Get access to the parent collection of items
                TypedRestrictCollection<KryptonRibbonGroupItem> items = ParentItems;

                // Use a transaction to support undo/redo actions
                DesignerTransaction transaction = _designerHost.CreateTransaction("KryptonRibbonGroupRichTextBox DeleteRichTextBox");

                try
                {
                    // Get access to the Items property
                    MemberDescriptor propertyItems = TypeDescriptor.GetProperties(_ribbonRichTextBox.RibbonContainer)["Items"];

                    RaiseComponentChanging(null);
                    RaiseComponentChanging(propertyItems);

                    // Remove the richtextbox from the group
                    items.Remove(_ribbonRichTextBox);

                    // Get designer to destroy it
                    _designerHost.DestroyComponent(_ribbonRichTextBox);

                    RaiseComponentChanged(propertyItems, null, null);
                    RaiseComponentChanged(null, null, null);
                }
                finally
                {
                    // If we managed to create the transaction, then do it
                    transaction?.Commit();
                }
            }
        }

        private void OnEnabled(object sender, EventArgs e)
        {
            if (_ribbonRichTextBox?.Ribbon != null)
            {
                PropertyDescriptor propertyEnabled = TypeDescriptor.GetProperties(_ribbonRichTextBox)["Enabled"];
                bool oldValue = (bool)propertyEnabled.GetValue(_ribbonRichTextBox);
                bool newValue = !oldValue;
                _changeService.OnComponentChanged(_ribbonRichTextBox, null, oldValue, newValue);
                propertyEnabled.SetValue(_ribbonRichTextBox, newValue);
            }
        }

        private void OnVisible(object sender, EventArgs e)
        {
            if (_ribbonRichTextBox?.Ribbon != null)
            {
                PropertyDescriptor propertyVisible = TypeDescriptor.GetProperties(_ribbonRichTextBox)["Visible"];
                bool oldValue = (bool)propertyVisible.GetValue(_ribbonRichTextBox);
                bool newValue = !oldValue;
                _changeService.OnComponentChanged(_ribbonRichTextBox, null, oldValue, newValue);
                propertyVisible.SetValue(_ribbonRichTextBox, newValue);
            }
        }

        private void OnComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            UpdateVerbStatus();
        }

        private void OnContextMenu(object sender, MouseEventArgs e)
        {
            if (_ribbonRichTextBox?.Ribbon != null)
            {
                // Create the menu strip the first time around
                if (_cms == null)
                {
                    _cms = new ContextMenuStrip();
                    _toggleHelpersMenu = new ToolStripMenuItem("Design Helpers", null, OnToggleHelpers);
                    _visibleMenu = new ToolStripMenuItem("Visible", null, OnVisible);
                    _moveFirstMenu = new ToolStripMenuItem("Move RichTextBox First", ComponentFactory.Krypton.Design.Properties.Resources.MoveFirst, OnMoveFirst);
                    _movePreviousMenu = new ToolStripMenuItem("Move RichTextBox Previous", ComponentFactory.Krypton.Design.Properties.Resources.MovePrevious, OnMovePrevious);
                    _moveNextMenu = new ToolStripMenuItem("Move RichTextBox Next", ComponentFactory.Krypton.Design.Properties.Resources.MoveNext, OnMoveNext);
                    _moveLastMenu = new ToolStripMenuItem("Move RichTextBox Last", ComponentFactory.Krypton.Design.Properties.Resources.MoveLast, OnMoveLast);
                    _deleteRichTextBoxMenu = new ToolStripMenuItem("Delete RichTextBox", ComponentFactory.Krypton.Design.Properties.Resources.delete2, OnDeleteTextBox);
                    _cms.Items.AddRange(new ToolStripItem[] { _toggleHelpersMenu, new ToolStripSeparator(),
                                                              _visibleMenu, new ToolStripSeparator(),
                                                              _moveFirstMenu, _movePreviousMenu, _moveNextMenu, _moveLastMenu, new ToolStripSeparator(),
                                                              _deleteRichTextBoxMenu });
                }

                // Update verbs to work out correct enable states
                UpdateVerbStatus();

                // Update menu items state from versb
                _toggleHelpersMenu.Checked = _ribbonRichTextBox.Ribbon.InDesignHelperMode;
                _visibleMenu.Checked = Visible;
                _moveFirstMenu.Enabled = _moveFirstVerb.Enabled;
                _movePreviousMenu.Enabled = _movePrevVerb.Enabled;
                _moveNextMenu.Enabled = _moveNextVerb.Enabled;
                _moveLastMenu.Enabled = _moveLastVerb.Enabled;

                // Show the context menu
                if (CommonHelper.ValidContextMenuStrip(_cms))
                {
                    Point screenPt = _ribbonRichTextBox.Ribbon.ViewRectangleToPoint(_ribbonRichTextBox.RichTextBoxView);
                    VisualPopupManager.Singleton.ShowContextMenuStrip(_cms, screenPt);
                }
            }
        }

        private TypedRestrictCollection<KryptonRibbonGroupItem> ParentItems
        {
            get
            {
                switch (_ribbonRichTextBox.RibbonContainer)
                {
                    case KryptonRibbonGroupTriple triple:
                        return triple.Items;
                    case KryptonRibbonGroupLines lines:
                        return lines.Items;
                    default:
                        // Should never happen!
                        Debug.Assert(false);
                        return null;
                }
            }
        }
        #endregion
    }
}
