// ***********************************************************************
// Assembly         : Zeroit.Framework.MaterialDesign
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="MaterialListView.cs" company="Zeroit Dev Technologies">
//    This program is for creating Material Design controls.
//    Copyright ©  2017  Zeroit Dev Technologies
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
//    You can contact me at zeroitdevnet@gmail.com or zeroitdev@outlook.com
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Zeroit.Framework.MaterialDesign.Controls
{
    /// <summary>
    /// A class collection for rendering Material List view.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.ListView" />
    /// <seealso cref="Zeroit.Framework.MaterialDesign.IMaterialControl" />
    public class ZeroitMaterialListView : ListView, IMaterialControl
    {
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
        /// Gets or sets the mouse location.
        /// </summary>
        /// <value>The mouse location.</value>
        [Browsable(false)]
        public Point MouseLocation { get; set; }
        /// <summary>
        /// Gets or sets the hovered item.
        /// </summary>
        /// <value>The hovered item.</value>
        [Browsable(false)]
        private ListViewItem HoveredItem { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMaterialListView" /> class.
        /// </summary>
        public ZeroitMaterialListView()
        {
            GridLines = false;
            FullRowSelect = true;
            HeaderStyle = ColumnHeaderStyle.Nonclickable;
            View = View.Details;
            OwnerDraw = true;
            ResizeRedraw = true;
            BorderStyle = BorderStyle.None;
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);

            //Fix for hovers, by default it doesn't redraw
            //TODO: should only redraw when the hovered line changed, this to reduce unnecessary redraws
            MouseLocation = new Point(-1, -1);
            MouseState = MouseState.OUT;
            MouseEnter += delegate
            {
                MouseState = MouseState.HOVER;
            };
            MouseLeave += delegate
            {
                MouseState = MouseState.OUT;
                MouseLocation = new Point(-1, -1);
                HoveredItem = null;
                Invalidate();
            };
            MouseDown += delegate { MouseState = MouseState.DOWN; };
            MouseUp += delegate { MouseState = MouseState.HOVER; };
            MouseMove += delegate (object sender, MouseEventArgs args)
            {
                MouseLocation = args.Location;
                var currentHoveredItem = this.GetItemAt(MouseLocation.X, MouseLocation.Y);
                if (HoveredItem != currentHoveredItem)
                {
                    HoveredItem = currentHoveredItem;
                    Invalidate();
                }
            };
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ListView.DrawColumnHeader" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DrawListViewColumnHeaderEventArgs" /> that contains the event data.</param>
        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(SkinManager.GetApplicationBackgroundColor()), new Rectangle(e.Bounds.X, e.Bounds.Y, Width, e.Bounds.Height));
            e.Graphics.DrawString(e.Header.Text,
                SkinManager.ROBOTO_MEDIUM_10,
                SkinManager.GetSecondaryTextBrush(),
                new Rectangle(e.Bounds.X + ITEM_PADDING, e.Bounds.Y + ITEM_PADDING, e.Bounds.Width - ITEM_PADDING * 2, e.Bounds.Height - ITEM_PADDING * 2),
                getStringFormat());
        }

        /// <summary>
        /// The item padding
        /// </summary>
        private const int ITEM_PADDING = 12;
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ListView.DrawItem" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DrawListViewItemEventArgs" /> that contains the event data.</param>
        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            //We draw the current line of items (= item with subitems) on a temp bitmap, then draw the bitmap at once. This is to reduce flickering.
            var b = new Bitmap(e.Item.Bounds.Width, e.Item.Bounds.Height);
            var g = Graphics.FromImage(b);

            //always draw default background
            g.FillRectangle(new SolidBrush(SkinManager.GetApplicationBackgroundColor()), new Rectangle(new Point(e.Bounds.X, 0), e.Bounds.Size));

            if (e.State.HasFlag(ListViewItemStates.Selected))
            {
                //selected background
                g.FillRectangle(SkinManager.GetFlatButtonPressedBackgroundBrush(), new Rectangle(new Point(e.Bounds.X, 0), e.Bounds.Size));
            }
            else if (e.Bounds.Contains(MouseLocation) && MouseState == MouseState.HOVER)
            {
                //hover background
                g.FillRectangle(SkinManager.GetFlatButtonHoverBackgroundBrush(), new Rectangle(new Point(e.Bounds.X, 0), e.Bounds.Size));
            }


            //Draw separator
            g.DrawLine(new Pen(SkinManager.GetDividersColor()), e.Bounds.Left, 0, e.Bounds.Right, 0);

            foreach (ListViewItem.ListViewSubItem subItem in e.Item.SubItems)
            {
                //Draw text
                g.DrawString(subItem.Text, SkinManager.ROBOTO_MEDIUM_10, SkinManager.GetPrimaryTextBrush(),
                                 new Rectangle(subItem.Bounds.X + ITEM_PADDING, ITEM_PADDING, subItem.Bounds.Width - 2 * ITEM_PADDING, subItem.Bounds.Height - 2 * ITEM_PADDING),
                                 getStringFormat());
            }

            e.Graphics.DrawImage((Image)b.Clone(), new Point(0, e.Item.Bounds.Location.Y));
            g.Dispose();
            b.Dispose();
        }

        /// <summary>
        /// Gets the string format.
        /// </summary>
        /// <returns>StringFormat.</returns>
        private StringFormat getStringFormat()
        {
            return new StringFormat
            {
                FormatFlags = StringFormatFlags.LineLimit,
                Trimming = StringTrimming.EllipsisCharacter,
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };
        }

        /// <summary>
        /// Class LogFont.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LogFont
        {
            /// <summary>
            /// The lf height
            /// </summary>
            public int lfHeight = 0;
            /// <summary>
            /// The lf width
            /// </summary>
            public int lfWidth = 0;
            /// <summary>
            /// The lf escapement
            /// </summary>
            public int lfEscapement = 0;
            /// <summary>
            /// The lf orientation
            /// </summary>
            public int lfOrientation = 0;
            /// <summary>
            /// The lf weight
            /// </summary>
            public int lfWeight = 0;
            /// <summary>
            /// The lf italic
            /// </summary>
            public byte lfItalic = 0;
            /// <summary>
            /// The lf underline
            /// </summary>
            public byte lfUnderline = 0;
            /// <summary>
            /// The lf strike out
            /// </summary>
            public byte lfStrikeOut = 0;
            /// <summary>
            /// The lf character set
            /// </summary>
            public byte lfCharSet = 0;
            /// <summary>
            /// The lf out precision
            /// </summary>
            public byte lfOutPrecision = 0;
            /// <summary>
            /// The lf clip precision
            /// </summary>
            public byte lfClipPrecision = 0;
            /// <summary>
            /// The lf quality
            /// </summary>
            public byte lfQuality = 0;
            /// <summary>
            /// The lf pitch and family
            /// </summary>
            public byte lfPitchAndFamily = 0;
            /// <summary>
            /// The lf face name
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string lfFaceName = string.Empty;
        }

        /// <summary>
        /// Raises the <see cref="M:System.Windows.Forms.Control.CreateControl" /> method.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // This hack tries to apply the Roboto (24) font to all ListViewItems in this ListView
            // It only succeeds if the font is installed on the system.
            // Otherwise, a default sans serif font is used.
            var roboto24 = new Font(SkinManager.ROBOTO_MEDIUM_12.FontFamily, 24);
            var roboto24Logfont = new LogFont();
            roboto24.ToLogFont(roboto24Logfont);

            try
            {
                // Font.FromLogFont is the method used when drawing ListViewItems. I 'test' it in this safer context to avoid unhandled exceptions later.
                Font = Font.FromLogFont(roboto24Logfont);
            }
            catch (ArgumentException)
            {
                Font = new Font(FontFamily.GenericSansSerif, 24);
            }
        }
    }
}
