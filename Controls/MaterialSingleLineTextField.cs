// ***********************************************************************
// Assembly         : Zeroit.Framework.MaterialDesign
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="MaterialSingleLineTextField.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Zeroit.Framework.MaterialDesign.Animations;

namespace Zeroit.Framework.MaterialDesign.Controls
{
    /// <summary>
    /// A class collection for rendering Material Design Single Line Text field.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    /// <seealso cref="Zeroit.Framework.MaterialDesign.IMaterialControl" />
    public class ZeroitMaterialLineTextField : Control, IMaterialControl
    {
        //Properties for managing the material design properties        
        /// <summary>
        /// Gets or sets the depth.
        /// </summary>
        /// <value>The depth.</value>
        [Browsable(false)]
        public int Depth { get; set; }

        /// <summary>
        /// Gets the skin manager.
        /// </summary>
        /// <value>The skin manager.</value>
        [Browsable(false)]
        public ZeroitMaterialSkinManager SkinManager => ZeroitMaterialSkinManager.Instance;

        /// <summary>
        /// Gets or sets the state of the mouse.
        /// </summary>
        /// <value>The state of the mouse.</value>
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        /// <summary>
        /// The base text box
        /// </summary>
        private readonly BaseTextBox _baseTextBox;
        /// <summary>
        /// The animation manager
        /// </summary>
        private readonly AnimationManager _animationManager;

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        /// <value>The text.</value>
        public override string Text { get { return _baseTextBox.Text; } set { _baseTextBox.Text = value; } }

        /// <summary>
        /// Gets or sets the object that contains data about the control.
        /// </summary>
        /// <value>The tag.</value>
        public new object Tag { get { return _baseTextBox.Tag; } set { _baseTextBox.Tag = value; } }

        /// <summary>
        /// Gets or sets the maximum length.
        /// </summary>
        /// <value>The maximum length.</value>
        public new int MaxLength { get { return _baseTextBox.MaxLength; } set { _baseTextBox.MaxLength = value; } }

        /// <summary>
        /// Gets or sets the selected text.
        /// </summary>
        /// <value>The selected text.</value>
        public string SelectedText { get { return _baseTextBox.SelectedText; } set { _baseTextBox.SelectedText = value; } }

        /// <summary>
        /// Gets or sets the hint.
        /// </summary>
        /// <value>The hint.</value>
        public string Hint { get { return _baseTextBox.Hint; } set { _baseTextBox.Hint = value; } }

        /// <summary>
        /// Gets or sets the selection start.
        /// </summary>
        /// <value>The selection start.</value>
        public int SelectionStart { get { return _baseTextBox.SelectionStart; } set { _baseTextBox.SelectionStart = value; } }

        /// <summary>
        /// Gets or sets the length of the selection.
        /// </summary>
        /// <value>The length of the selection.</value>
        public int SelectionLength { get { return _baseTextBox.SelectionLength; } set { _baseTextBox.SelectionLength = value; } }

        /// <summary>
        /// Gets the length of the text.
        /// </summary>
        /// <value>The length of the text.</value>
        public int TextLength => _baseTextBox.TextLength;

        /// <summary>
        /// Gets or sets a value indicating whether to use system password character.
        /// </summary>
        /// <value><c>true</c> if use system password character; otherwise, <c>false</c>.</value>
        public bool UseSystemPasswordChar { get { return _baseTextBox.UseSystemPasswordChar; } set { _baseTextBox.UseSystemPasswordChar = value; } }

        /// <summary>
        /// Gets or sets the password character.
        /// </summary>
        /// <value>The password character.</value>
        public char PasswordChar { get { return _baseTextBox.PasswordChar; } set { _baseTextBox.PasswordChar = value; } }

        /// <summary>
        /// Selects all.
        /// </summary>
        public void SelectAll() { _baseTextBox.SelectAll(); }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear() { _baseTextBox.Clear(); }

        /// <summary>
        /// Focuses this instance.
        /// </summary>
        public void Focus() { _baseTextBox.Focus(); }


        # region Forwarding events to baseTextBox
        /// <summary>
        /// Occurs when [accepts tab changed].
        /// </summary>
        public event EventHandler AcceptsTabChanged
        {
            add
            {
                _baseTextBox.AcceptsTabChanged += value;
            }
            remove
            {
                _baseTextBox.AcceptsTabChanged -= value;
            }
        }

        /// <summary>
        /// This event is not relevant for this class.
        /// </summary>
        public new event EventHandler AutoSizeChanged
        {
            add
            {
                _baseTextBox.AutoSizeChanged += value;
            }
            remove
            {
                _baseTextBox.AutoSizeChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="P:System.Windows.Forms.Control.BackgroundImage" /> property changes.
        /// </summary>
        public new event EventHandler BackgroundImageChanged
        {
            add
            {
                _baseTextBox.BackgroundImageChanged += value;
            }
            remove
            {
                _baseTextBox.BackgroundImageChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the <see cref="P:System.Windows.Forms.Control.BackgroundImageLayout" /> property changes.
        /// </summary>
        public new event EventHandler BackgroundImageLayoutChanged
        {
            add
            {
                _baseTextBox.BackgroundImageLayoutChanged += value;
            }
            remove
            {
                _baseTextBox.BackgroundImageLayoutChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="T:System.Windows.Forms.BindingContext" /> property changes.
        /// </summary>
        public new event EventHandler BindingContextChanged
        {
            add
            {
                _baseTextBox.BindingContextChanged += value;
            }
            remove
            {
                _baseTextBox.BindingContextChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when [border style changed].
        /// </summary>
        public event EventHandler BorderStyleChanged
        {
            add
            {
                _baseTextBox.BorderStyleChanged += value;
            }
            remove
            {
                _baseTextBox.BorderStyleChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="P:System.Windows.Forms.Control.CausesValidation" /> property changes.
        /// </summary>
        public new event EventHandler CausesValidationChanged
        {
            add
            {
                _baseTextBox.CausesValidationChanged += value;
            }
            remove
            {
                _baseTextBox.CausesValidationChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the focus or keyboard user interface (UI) cues change.
        /// </summary>
        public new event UICuesEventHandler ChangeUICues
        {
            add
            {
                _baseTextBox.ChangeUICues += value;
            }
            remove
            {
                _baseTextBox.ChangeUICues -= value;
            }
        }

        /// <summary>
        /// Occurs when the control is clicked.
        /// </summary>
        public new event EventHandler Click
        {
            add
            {
                _baseTextBox.Click += value;
            }
            remove
            {
                _baseTextBox.Click -= value;
            }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="P:System.Windows.Forms.Control.ClientSize" /> property changes.
        /// </summary>
        public new event EventHandler ClientSizeChanged
        {
            add
            {
                _baseTextBox.ClientSizeChanged += value;
            }
            remove
            {
                _baseTextBox.ClientSizeChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="P:System.Windows.Forms.Control.ContextMenu" /> property changes.
        /// </summary>
        public new event EventHandler ContextMenuChanged
        {
            add
            {
                _baseTextBox.ContextMenuChanged += value;
            }
            remove
            {
                _baseTextBox.ContextMenuChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="P:System.Windows.Forms.Control.ContextMenuStrip" /> property changes.
        /// </summary>
        public new event EventHandler ContextMenuStripChanged
        {
            add
            {
                _baseTextBox.ContextMenuStripChanged += value;
            }
            remove
            {
                _baseTextBox.ContextMenuStripChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when a new control is added to the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.
        /// </summary>
        public new event ControlEventHandler ControlAdded
        {
            add
            {
                _baseTextBox.ControlAdded += value;
            }
            remove
            {
                _baseTextBox.ControlAdded -= value;
            }
        }

        /// <summary>
        /// Occurs when a control is removed from the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.
        /// </summary>
        public new event ControlEventHandler ControlRemoved
        {
            add
            {
                _baseTextBox.ControlRemoved += value;
            }
            remove
            {
                _baseTextBox.ControlRemoved -= value;
            }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="P:System.Windows.Forms.Control.Cursor" /> property changes.
        /// </summary>
        public new event EventHandler CursorChanged
        {
            add
            {
                _baseTextBox.CursorChanged += value;
            }
            remove
            {
                _baseTextBox.CursorChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the component is disposed by a call to the <see cref="M:System.ComponentModel.Component.Dispose" /> method.
        /// </summary>
        public new event EventHandler Disposed
        {
            add
            {
                _baseTextBox.Disposed += value;
            }
            remove
            {
                _baseTextBox.Disposed -= value;
            }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="P:System.Windows.Forms.Control.Dock" /> property changes.
        /// </summary>
        public new event EventHandler DockChanged
        {
            add
            {
                _baseTextBox.DockChanged += value;
            }
            remove
            {
                _baseTextBox.DockChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the control is double-clicked.
        /// </summary>
        public new event EventHandler DoubleClick
        {
            add
            {
                _baseTextBox.DoubleClick += value;
            }
            remove
            {
                _baseTextBox.DoubleClick -= value;
            }
        }

        /// <summary>
        /// Occurs when a drag-and-drop operation is completed.
        /// </summary>
        public new event DragEventHandler DragDrop
        {
            add
            {
                _baseTextBox.DragDrop += value;
            }
            remove
            {
                _baseTextBox.DragDrop -= value;
            }
        }

        /// <summary>
        /// Occurs when an object is dragged into the control's bounds.
        /// </summary>
        public new event DragEventHandler DragEnter
        {
            add
            {
                _baseTextBox.DragEnter += value;
            }
            remove
            {
                _baseTextBox.DragEnter -= value;
            }
        }

        /// <summary>
        /// Occurs when an object is dragged out of the control's bounds.
        /// </summary>
        public new event EventHandler DragLeave
        {
            add
            {
                _baseTextBox.DragLeave += value;
            }
            remove
            {
                _baseTextBox.DragLeave -= value;
            }
        }

        /// <summary>
        /// Occurs when an object is dragged over the control's bounds.
        /// </summary>
        public new event DragEventHandler DragOver
        {
            add
            {
                _baseTextBox.DragOver += value;
            }
            remove
            {
                _baseTextBox.DragOver -= value;
            }
        }

        /// <summary>
        /// Occurs when the <see cref="P:System.Windows.Forms.Control.Enabled" /> property value has changed.
        /// </summary>
        public new event EventHandler EnabledChanged
        {
            add
            {
                _baseTextBox.EnabledChanged += value;
            }
            remove
            {
                _baseTextBox.EnabledChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the control is entered.
        /// </summary>
        public new event EventHandler Enter
        {
            add
            {
                _baseTextBox.Enter += value;
            }
            remove
            {
                _baseTextBox.Enter -= value;
            }
        }

        /// <summary>
        /// Occurs when the <see cref="P:System.Windows.Forms.Control.Font" /> property value changes.
        /// </summary>
        public new event EventHandler FontChanged
        {
            add
            {
                _baseTextBox.FontChanged += value;
            }
            remove
            {
                _baseTextBox.FontChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the <see cref="P:System.Windows.Forms.Control.ForeColor" /> property value changes.
        /// </summary>
        public new event EventHandler ForeColorChanged
        {
            add
            {
                _baseTextBox.ForeColorChanged += value;
            }
            remove
            {
                _baseTextBox.ForeColorChanged -= value;
            }
        }

        /// <summary>
        /// Occurs during a drag operation.
        /// </summary>
        public new event GiveFeedbackEventHandler GiveFeedback
        {
            add
            {
                _baseTextBox.GiveFeedback += value;
            }
            remove
            {
                _baseTextBox.GiveFeedback -= value;
            }
        }

        /// <summary>
        /// Occurs when the control receives focus.
        /// </summary>
        public new event EventHandler GotFocus
        {
            add
            {
                _baseTextBox.GotFocus += value;
            }
            remove
            {
                _baseTextBox.GotFocus -= value;
            }
        }

        /// <summary>
        /// Occurs when a handle is created for the control.
        /// </summary>
        public new event EventHandler HandleCreated
        {
            add
            {
                _baseTextBox.HandleCreated += value;
            }
            remove
            {
                _baseTextBox.HandleCreated -= value;
            }
        }

        /// <summary>
        /// Occurs when the control's handle is in the process of being destroyed.
        /// </summary>
        public new event EventHandler HandleDestroyed
        {
            add
            {
                _baseTextBox.HandleDestroyed += value;
            }
            remove
            {
                _baseTextBox.HandleDestroyed -= value;
            }
        }

        /// <summary>
        /// Occurs when the user requests help for a control.
        /// </summary>
        public new event HelpEventHandler HelpRequested
        {
            add
            {
                _baseTextBox.HelpRequested += value;
            }
            remove
            {
                _baseTextBox.HelpRequested -= value;
            }
        }

        /// <summary>
        /// Occurs when [hide selection changed].
        /// </summary>
        public event EventHandler HideSelectionChanged
        {
            add
            {
                _baseTextBox.HideSelectionChanged += value;
            }
            remove
            {
                _baseTextBox.HideSelectionChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the <see cref="P:System.Windows.Forms.Control.ImeMode" /> property has changed.
        /// </summary>
        public new event EventHandler ImeModeChanged
        {
            add
            {
                _baseTextBox.ImeModeChanged += value;
            }
            remove
            {
                _baseTextBox.ImeModeChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when a control's display requires redrawing.
        /// </summary>
        public new event InvalidateEventHandler Invalidated
        {
            add
            {
                _baseTextBox.Invalidated += value;
            }
            remove
            {
                _baseTextBox.Invalidated -= value;
            }
        }

        /// <summary>
        /// Occurs when a key is pressed while the control has focus.
        /// </summary>
        public new event KeyEventHandler KeyDown
        {
            add
            {
                _baseTextBox.KeyDown += value;
            }
            remove
            {
                _baseTextBox.KeyDown -= value;
            }
        }

        /// <summary>
        /// Occurs when a character. space or backspace key is pressed while the control has focus.
        /// </summary>
        public new event KeyPressEventHandler KeyPress
        {
            add
            {
                _baseTextBox.KeyPress += value;
            }
            remove
            {
                _baseTextBox.KeyPress -= value;
            }
        }

        /// <summary>
        /// Occurs when a key is released while the control has focus.
        /// </summary>
        public new event KeyEventHandler KeyUp
        {
            add
            {
                _baseTextBox.KeyUp += value;
            }
            remove
            {
                _baseTextBox.KeyUp -= value;
            }
        }

        /// <summary>
        /// Occurs when a control should reposition its child controls.
        /// </summary>
        public new event LayoutEventHandler Layout
        {
            add
            {
                _baseTextBox.Layout += value;
            }
            remove
            {
                _baseTextBox.Layout -= value;
            }
        }

        /// <summary>
        /// Occurs when the input focus leaves the control.
        /// </summary>
        public new event EventHandler Leave
        {
            add
            {
                _baseTextBox.Leave += value;
            }
            remove
            {
                _baseTextBox.Leave -= value;
            }
        }

        /// <summary>
        /// Occurs when the <see cref="P:System.Windows.Forms.Control.Location" /> property value has changed.
        /// </summary>
        public new event EventHandler LocationChanged
        {
            add
            {
                _baseTextBox.LocationChanged += value;
            }
            remove
            {
                _baseTextBox.LocationChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the control loses focus.
        /// </summary>
        public new event EventHandler LostFocus
        {
            add
            {
                _baseTextBox.LostFocus += value;
            }
            remove
            {
                _baseTextBox.LostFocus -= value;
            }
        }

        /// <summary>
        /// Occurs when the control's margin changes.
        /// </summary>
        public new event EventHandler MarginChanged
        {
            add
            {
                _baseTextBox.MarginChanged += value;
            }
            remove
            {
                _baseTextBox.MarginChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when [modified changed].
        /// </summary>
        public event EventHandler ModifiedChanged
        {
            add
            {
                _baseTextBox.ModifiedChanged += value;
            }
            remove
            {
                _baseTextBox.ModifiedChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the control loses mouse capture.
        /// </summary>
        public new event EventHandler MouseCaptureChanged
        {
            add
            {
                _baseTextBox.MouseCaptureChanged += value;
            }
            remove
            {
                _baseTextBox.MouseCaptureChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the control is clicked by the mouse.
        /// </summary>
        public new event MouseEventHandler MouseClick
        {
            add
            {
                _baseTextBox.MouseClick += value;
            }
            remove
            {
                _baseTextBox.MouseClick -= value;
            }
        }

        /// <summary>
        /// Occurs when the control is double clicked by the mouse.
        /// </summary>
        public new event MouseEventHandler MouseDoubleClick
        {
            add
            {
                _baseTextBox.MouseDoubleClick += value;
            }
            remove
            {
                _baseTextBox.MouseDoubleClick -= value;
            }
        }

        /// <summary>
        /// Occurs when the mouse pointer is over the control and a mouse button is pressed.
        /// </summary>
        public new event MouseEventHandler MouseDown
        {
            add
            {
                _baseTextBox.MouseDown += value;
            }
            remove
            {
                _baseTextBox.MouseDown -= value;
            }
        }

        /// <summary>
        /// Occurs when the mouse pointer enters the control.
        /// </summary>
        public new event EventHandler MouseEnter
        {
            add
            {
                _baseTextBox.MouseEnter += value;
            }
            remove
            {
                _baseTextBox.MouseEnter -= value;
            }
        }

        /// <summary>
        /// Occurs when the mouse pointer rests on the control.
        /// </summary>
        public new event EventHandler MouseHover
        {
            add
            {
                _baseTextBox.MouseHover += value;
            }
            remove
            {
                _baseTextBox.MouseHover -= value;
            }
        }

        /// <summary>
        /// Occurs when the mouse pointer leaves the control.
        /// </summary>
        public new event EventHandler MouseLeave
        {
            add
            {
                _baseTextBox.MouseLeave += value;
            }
            remove
            {
                _baseTextBox.MouseLeave -= value;
            }
        }

        /// <summary>
        /// Occurs when the mouse pointer is moved over the control.
        /// </summary>
        public new event MouseEventHandler MouseMove
        {
            add
            {
                _baseTextBox.MouseMove += value;
            }
            remove
            {
                _baseTextBox.MouseMove -= value;
            }
        }

        /// <summary>
        /// Occurs when the mouse pointer is over the control and a mouse button is released.
        /// </summary>
        public new event MouseEventHandler MouseUp
        {
            add
            {
                _baseTextBox.MouseUp += value;
            }
            remove
            {
                _baseTextBox.MouseUp -= value;
            }
        }

        /// <summary>
        /// Occurs when the mouse wheel moves while the control has focus.
        /// </summary>
        public new event MouseEventHandler MouseWheel
        {
            add
            {
                _baseTextBox.MouseWheel += value;
            }
            remove
            {
                _baseTextBox.MouseWheel -= value;
            }
        }

        /// <summary>
        /// Occurs when the control is moved.
        /// </summary>
        public new event EventHandler Move
        {
            add
            {
                _baseTextBox.Move += value;
            }
            remove
            {
                _baseTextBox.Move -= value;
            }
        }

        /// <summary>
        /// Occurs when [multiline changed].
        /// </summary>
        public event EventHandler MultilineChanged
        {
            add
            {
                _baseTextBox.MultilineChanged += value;
            }
            remove
            {
                _baseTextBox.MultilineChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the control's padding changes.
        /// </summary>
        public new event EventHandler PaddingChanged
        {
            add
            {
                _baseTextBox.PaddingChanged += value;
            }
            remove
            {
                _baseTextBox.PaddingChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the control is redrawn.
        /// </summary>
        public new event PaintEventHandler Paint
        {
            add
            {
                _baseTextBox.Paint += value;
            }
            remove
            {
                _baseTextBox.Paint -= value;
            }
        }

        /// <summary>
        /// Occurs when the <see cref="P:System.Windows.Forms.Control.Parent" /> property value changes.
        /// </summary>
        public new event EventHandler ParentChanged
        {
            add
            {
                _baseTextBox.ParentChanged += value;
            }
            remove
            {
                _baseTextBox.ParentChanged -= value;
            }
        }

        /// <summary>
        /// Occurs before the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event when a key is pressed while focus is on this control.
        /// </summary>
        public new event PreviewKeyDownEventHandler PreviewKeyDown
        {
            add
            {
                _baseTextBox.PreviewKeyDown += value;
            }
            remove
            {
                _baseTextBox.PreviewKeyDown -= value;
            }
        }

        /// <summary>
        /// Occurs when <see cref="T:System.Windows.Forms.AccessibleObject" /> is providing help to accessibility applications.
        /// </summary>
        public new event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp
        {
            add
            {
                _baseTextBox.QueryAccessibilityHelp += value;
            }
            remove
            {
                _baseTextBox.QueryAccessibilityHelp -= value;
            }
        }

        /// <summary>
        /// Occurs during a drag-and-drop operation and enables the drag source to determine whether the drag-and-drop operation should be canceled.
        /// </summary>
        public new event QueryContinueDragEventHandler QueryContinueDrag
        {
            add
            {
                _baseTextBox.QueryContinueDrag += value;
            }
            remove
            {
                _baseTextBox.QueryContinueDrag -= value;
            }
        }

        /// <summary>
        /// Occurs when [read only changed].
        /// </summary>
        public event EventHandler ReadOnlyChanged
        {
            add
            {
                _baseTextBox.ReadOnlyChanged += value;
            }
            remove
            {
                _baseTextBox.ReadOnlyChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the value of the <see cref="P:System.Windows.Forms.Control.Region" /> property changes.
        /// </summary>
        public new event EventHandler RegionChanged
        {
            add
            {
                _baseTextBox.RegionChanged += value;
            }
            remove
            {
                _baseTextBox.RegionChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the control is resized.
        /// </summary>
        public new event EventHandler Resize
        {
            add
            {
                _baseTextBox.Resize += value;
            }
            remove
            {
                _baseTextBox.Resize -= value;
            }
        }

        /// <summary>
        /// Occurs when the <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property value changes.
        /// </summary>
        public new event EventHandler RightToLeftChanged
        {
            add
            {
                _baseTextBox.RightToLeftChanged += value;
            }
            remove
            {
                _baseTextBox.RightToLeftChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the <see cref="P:System.Windows.Forms.Control.Size" /> property value changes.
        /// </summary>
        public new event EventHandler SizeChanged
        {
            add
            {
                _baseTextBox.SizeChanged += value;
            }
            remove
            {
                _baseTextBox.SizeChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the control style changes.
        /// </summary>
        public new event EventHandler StyleChanged
        {
            add
            {
                _baseTextBox.StyleChanged += value;
            }
            remove
            {
                _baseTextBox.StyleChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the system colors change.
        /// </summary>
        public new event EventHandler SystemColorsChanged
        {
            add
            {
                _baseTextBox.SystemColorsChanged += value;
            }
            remove
            {
                _baseTextBox.SystemColorsChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the <see cref="P:System.Windows.Forms.Control.TabIndex" /> property value changes.
        /// </summary>
        public new event EventHandler TabIndexChanged
        {
            add
            {
                _baseTextBox.TabIndexChanged += value;
            }
            remove
            {
                _baseTextBox.TabIndexChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the <see cref="P:System.Windows.Forms.Control.TabStop" /> property value changes.
        /// </summary>
        public new event EventHandler TabStopChanged
        {
            add
            {
                _baseTextBox.TabStopChanged += value;
            }
            remove
            {
                _baseTextBox.TabStopChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when [text align changed].
        /// </summary>
        public event EventHandler TextAlignChanged
        {
            add
            {
                _baseTextBox.TextAlignChanged += value;
            }
            remove
            {
                _baseTextBox.TextAlignChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the <see cref="P:System.Windows.Forms.Control.Text" /> property value changes.
        /// </summary>
        public new event EventHandler TextChanged
        {
            add
            {
                _baseTextBox.TextChanged += value;
            }
            remove
            {
                _baseTextBox.TextChanged -= value;
            }
        }

        /// <summary>
        /// Occurs when the control is finished validating.
        /// </summary>
        public new event EventHandler Validated
        {
            add
            {
                _baseTextBox.Validated += value;
            }
            remove
            {
                _baseTextBox.Validated -= value;
            }
        }

        /// <summary>
        /// Occurs when the control is validating.
        /// </summary>
        public new event CancelEventHandler Validating
        {
            add
            {
                _baseTextBox.Validating += value;
            }
            remove
            {
                _baseTextBox.Validating -= value;
            }
        }

        /// <summary>
        /// Occurs when the <see cref="P:System.Windows.Forms.Control.Visible" /> property value changes.
        /// </summary>
        public new event EventHandler VisibleChanged
        {
            add
            {
                _baseTextBox.VisibleChanged += value;
            }
            remove
            {
                _baseTextBox.VisibleChanged -= value;
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMaterialLineTextField" /> class.
        /// </summary>
        public ZeroitMaterialLineTextField()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer, true);

            _animationManager = new AnimationManager
            {
                Increment = 0.06,
                AnimationType = AnimationType.EaseInOut,
                InterruptAnimation = false
            };
            _animationManager.OnAnimationProgress += sender => Invalidate();

            _baseTextBox = new BaseTextBox
            {
                BorderStyle = BorderStyle.None,
                Font = SkinManager.ROBOTO_REGULAR_11,
                ForeColor = SkinManager.GetPrimaryTextColor(),
                Location = new Point(0, 0),
                Width = Width,
                Height = Height - 5
            };

            if (!Controls.Contains(_baseTextBox) && !DesignMode)
            {
                Controls.Add(_baseTextBox);
            }

            _baseTextBox.GotFocus += (sender, args) => _animationManager.StartNewAnimation(AnimationDirection.In);
            _baseTextBox.LostFocus += (sender, args) => _animationManager.StartNewAnimation(AnimationDirection.Out);
            BackColorChanged += (sender, args) =>
            {
                _baseTextBox.BackColor = BackColor;
                _baseTextBox.ForeColor = SkinManager.GetPrimaryTextColor();
            };

            //Fix for tabstop
            _baseTextBox.TabStop = true;
            this.TabStop = false;
        }

        /// <summary>
        /// Handles the <see cref="E:Paint" /> event.
        /// </summary>
        /// <param name="pevent">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.Clear(Parent.BackColor);

            var lineY = _baseTextBox.Bottom + 3;

            if (!_animationManager.IsAnimating())
            {
                //No animation
                g.FillRectangle(_baseTextBox.Focused ? SkinManager.ColorScheme.PrimaryBrush : SkinManager.GetDividersBrush(), _baseTextBox.Location.X, lineY, _baseTextBox.Width, _baseTextBox.Focused ? 2 : 1);
            }
            else
            {
                //Animate
                int animationWidth = (int)(_baseTextBox.Width * _animationManager.GetProgress());
                int halfAnimationWidth = animationWidth / 2;
                int animationStart = _baseTextBox.Location.X + _baseTextBox.Width / 2;

                //Unfocused background
                g.FillRectangle(SkinManager.GetDividersBrush(), _baseTextBox.Location.X, lineY, _baseTextBox.Width, 1);

                //Animated focus transition
                g.FillRectangle(SkinManager.ColorScheme.PrimaryBrush, animationStart - halfAnimationWidth, lineY, animationWidth, 2);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            _baseTextBox.Location = new Point(0, 0);
            _baseTextBox.Width = Width;

            Height = _baseTextBox.Height + 5;
        }

        /// <summary>
        /// Raises the <see cref="M:System.Windows.Forms.Control.CreateControl" /> method.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            _baseTextBox.BackColor = Parent.BackColor;
            _baseTextBox.ForeColor = SkinManager.GetPrimaryTextColor();
        }

        /// <summary>
        /// Class BaseTextBox.
        /// </summary>
        /// <seealso cref="System.Windows.Forms.TextBox" />
        private class BaseTextBox : TextBox
        {
            /// <summary>
            /// Sends the message.
            /// </summary>
            /// <param name="hWnd">The h WND.</param>
            /// <param name="msg">The MSG.</param>
            /// <param name="wParam">The w parameter.</param>
            /// <param name="lParam">The l parameter.</param>
            /// <returns>IntPtr.</returns>
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

            /// <summary>
            /// The em setcuebanner
            /// </summary>
            private const int EM_SETCUEBANNER = 0x1501;
            /// <summary>
            /// The empty character
            /// </summary>
            private const char EmptyChar = (char)0;
            /// <summary>
            /// The visual style password character
            /// </summary>
            private const char VisualStylePasswordChar = '\u25CF';
            /// <summary>
            /// The non visual style password character
            /// </summary>
            private const char NonVisualStylePasswordChar = '\u002A';

            /// <summary>
            /// The hint
            /// </summary>
            private string hint = string.Empty;
            /// <summary>
            /// Gets or sets the hint.
            /// </summary>
            /// <value>The hint.</value>
            public string Hint
            {
                get { return hint; }
                set
                {
                    hint = value;
                    SendMessage(Handle, EM_SETCUEBANNER, (int)IntPtr.Zero, Hint);
                }
            }

            /// <summary>
            /// The password character
            /// </summary>
            private char _passwordChar = EmptyChar;
            /// <summary>
            /// Gets or sets the character used to mask characters of a password in a single-line <see cref="T:System.Windows.Forms.TextBox" /> control.
            /// </summary>
            /// <value>The password character.</value>
            public new char PasswordChar
            {
                get { return _passwordChar; }
                set
                {
                    _passwordChar = value;
                    SetBasePasswordChar();
                }
            }

            /// <summary>
            /// Selects all text in the text box.
            /// </summary>
            public new void SelectAll()
            {
                BeginInvoke((MethodInvoker)delegate ()
                {
                    base.Focus();
                    base.SelectAll();
                });
            }

            /// <summary>
            /// Focuses this instance.
            /// </summary>
            public new void Focus()
            {
                BeginInvoke((MethodInvoker)delegate ()
                {
                    base.Focus();
                });
            }

            /// <summary>
            /// The use system password character
            /// </summary>
            private char _useSystemPasswordChar = EmptyChar;
            /// <summary>
            /// Gets or sets a value indicating whether the text in the <see cref="T:System.Windows.Forms.TextBox" /> control should appear as the default password character.
            /// </summary>
            /// <value><c>true</c> if [use system password character]; otherwise, <c>false</c>.</value>
            public new bool UseSystemPasswordChar
            {
                get { return _useSystemPasswordChar != EmptyChar; }
                set
                {
                    if (value)
                    {
                        _useSystemPasswordChar = Application.RenderWithVisualStyles ? VisualStylePasswordChar : NonVisualStylePasswordChar;
                    }
                    else
                    {
                        _useSystemPasswordChar = EmptyChar;
                    }

                    SetBasePasswordChar();
                }
            }

            /// <summary>
            /// Sets the base password character.
            /// </summary>
            private void SetBasePasswordChar()
            {
                base.PasswordChar = UseSystemPasswordChar ? _useSystemPasswordChar : _passwordChar;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="BaseTextBox"/> class.
            /// </summary>
            public BaseTextBox()
            {
                ZeroitMaterialContextMenuStrip cms = new TextBoxContextMenuStrip();
                cms.Opening += ContextMenuStripOnOpening;
                cms.OnItemClickStart += ContextMenuStripOnItemClickStart;

                ContextMenuStrip = cms;
            }

            /// <summary>
            /// Contexts the menu strip on item click start.
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="toolStripItemClickedEventArgs">The <see cref="ToolStripItemClickedEventArgs"/> instance containing the event data.</param>
            private void ContextMenuStripOnItemClickStart(object sender, ToolStripItemClickedEventArgs toolStripItemClickedEventArgs)
            {
                switch (toolStripItemClickedEventArgs.ClickedItem.Text)
                {
                    case "Undo":
                        Undo();
                        break;
                    case "Cut":
                        Cut();
                        break;
                    case "Copy":
                        Copy();
                        break;
                    case "Paste":
                        Paste();
                        break;
                    case "Delete":
                        SelectedText = string.Empty;
                        break;
                    case "Select All":
                        SelectAll();
                        break;
                }
            }

            /// <summary>
            /// Contexts the menu strip on opening.
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="cancelEventArgs">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
            private void ContextMenuStripOnOpening(object sender, CancelEventArgs cancelEventArgs)
            {
                var strip = sender as TextBoxContextMenuStrip;
                if (strip != null)
                {
                    strip.Undo.Enabled = CanUndo;
                    strip.Cut.Enabled = !string.IsNullOrEmpty(SelectedText);
                    strip.Copy.Enabled = !string.IsNullOrEmpty(SelectedText);
                    strip.Paste.Enabled = Clipboard.ContainsText();
                    strip.Delete.Enabled = !string.IsNullOrEmpty(SelectedText);
                    strip.SelectAll.Enabled = !string.IsNullOrEmpty(Text);
                }
            }
        }

        /// <summary>
        /// Class TextBoxContextMenuStrip.
        /// </summary>
        /// <seealso cref="Zeroit.Framework.MaterialDesign.Controls.ZeroitMaterialContextMenuStrip" />
        private class TextBoxContextMenuStrip : ZeroitMaterialContextMenuStrip
        {
            /// <summary>
            /// The undo
            /// </summary>
            public readonly ToolStripItem Undo = new MaterialToolStripMenuItem { Text = "Undo" };
            /// <summary>
            /// The seperator1
            /// </summary>
            public readonly ToolStripItem Seperator1 = new ToolStripSeparator();
            /// <summary>
            /// The cut
            /// </summary>
            public readonly ToolStripItem Cut = new MaterialToolStripMenuItem { Text = "Cut" };
            /// <summary>
            /// The copy
            /// </summary>
            public readonly ToolStripItem Copy = new MaterialToolStripMenuItem { Text = "Copy" };
            /// <summary>
            /// The paste
            /// </summary>
            public readonly ToolStripItem Paste = new MaterialToolStripMenuItem { Text = "Paste" };
            /// <summary>
            /// The delete
            /// </summary>
            public readonly ToolStripItem Delete = new MaterialToolStripMenuItem { Text = "Delete" };
            /// <summary>
            /// The seperator2
            /// </summary>
            public readonly ToolStripItem Seperator2 = new ToolStripSeparator();
            /// <summary>
            /// The select all
            /// </summary>
            public readonly ToolStripItem SelectAll = new MaterialToolStripMenuItem { Text = "Select All" };

            /// <summary>
            /// Initializes a new instance of the <see cref="TextBoxContextMenuStrip"/> class.
            /// </summary>
            public TextBoxContextMenuStrip()
            {
                Items.AddRange(new[]
                {
                    Undo,
                    Seperator1,
                    Cut,
                    Copy,
                    Paste,
                    Delete,
                    Seperator2,
                    SelectAll
                });
            }
        }
    }
}
