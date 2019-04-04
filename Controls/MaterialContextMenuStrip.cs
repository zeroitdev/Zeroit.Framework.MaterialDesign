﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.MaterialDesign
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="MaterialContextMenuStrip.cs" company="Zeroit Dev Technologies">
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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Zeroit.Framework.MaterialDesign.Animations;

namespace Zeroit.Framework.MaterialDesign.Controls
{
    /// <summary>
    /// A class collection for rendering context menu strip using Material Design.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.ContextMenuStrip" />
    /// <seealso cref="Zeroit.Framework.MaterialDesign.IMaterialControl" />
    public class ZeroitMaterialContextMenuStrip : ContextMenuStrip, IMaterialControl
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
        /// The animation manager
        /// </summary>
        internal AnimationManager AnimationManager;
        /// <summary>
        /// The animation source
        /// </summary>
        internal Point AnimationSource;

        /// <summary>
        /// Delegate ItemClickStart
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ToolStripItemClickedEventArgs"/> instance containing the event data.</param>
        public delegate void ItemClickStart(object sender, ToolStripItemClickedEventArgs e);
        /// <summary>
        /// Occurs when [on item click start].
        /// </summary>
        public event ItemClickStart OnItemClickStart;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMaterialContextMenuStrip" /> class.
        /// </summary>
        public ZeroitMaterialContextMenuStrip()
        {
            Renderer = new MaterialToolStripRender();

            AnimationManager = new AnimationManager(false)
            {
                Increment = 0.07,
                AnimationType = AnimationType.Linear
            };
            AnimationManager.OnAnimationProgress += sender => Invalidate();
            AnimationManager.OnAnimationFinished += sender => OnItemClicked(_delayesArgs);

            BackColor = SkinManager.GetApplicationBackgroundColor();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp" /> event.
        /// </summary>
        /// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs mea)
        {
            base.OnMouseUp(mea);

            AnimationSource = mea.Location;
        }

        /// <summary>
        /// The delayes arguments
        /// </summary>
        private ToolStripItemClickedEventArgs _delayesArgs;
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ToolStrip.ItemClicked" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs" /> that contains the event data.</param>
        protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem != null && !(e.ClickedItem is ToolStripSeparator))
            {
                if (e == _delayesArgs)
                {
                    //The event has been fired manualy because the args are the ones we saved for delay
                    base.OnItemClicked(e);
                }
                else
                {
                    //Interrupt the default on click, saving the args for the delay which is needed to display the animaton
                    _delayesArgs = e;

                    //Fire custom event to trigger actions directly but keep cms open
                    OnItemClickStart?.Invoke(this, e);

                    //Start animation
                    AnimationManager.StartNewAnimation(AnimationDirection.In);
                }
            }
        }
    }

    /// <summary>
    /// A class collection for rendering Toolstrip menu item using Material Design.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.ToolStripMenuItem" />
    public class MaterialToolStripMenuItem : ToolStripMenuItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialToolStripMenuItem" /> class.
        /// </summary>
        public MaterialToolStripMenuItem()
        {
            AutoSize = false;
            Size = new Size(120, 30);
        }

        /// <summary>
        /// Creates a generic <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which events can be defined.
        /// </summary>
        /// <returns>A generic <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which can be defined.</returns>
        protected override ToolStripDropDown CreateDefaultDropDown()
        {
            var baseDropDown = base.CreateDefaultDropDown();
            if (DesignMode) return baseDropDown;

            var defaultDropDown = new ZeroitMaterialContextMenuStrip();
            defaultDropDown.Items.AddRange(baseDropDown.Items);

            return defaultDropDown;
        }
    }

    /// <summary>
    /// Class MaterialToolStripRender.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.ToolStripProfessionalRenderer" />
    /// <seealso cref="Zeroit.Framework.MaterialDesign.IMaterialControl" />
    internal class MaterialToolStripRender : ToolStripProfessionalRenderer, IMaterialControl
    {
        //Properties for managing the material design properties        
        /// <summary>
        /// Gets or sets the depth.
        /// </summary>
        /// <value>The depth.</value>
        public int Depth { get; set; }

        /// <summary>
        /// Gets the skin manager.
        /// </summary>
        /// <value>The skin manager.</value>
        public ZeroitMaterialSkinManager SkinManager => ZeroitMaterialSkinManager.Instance;

        /// <summary>
        /// Gets or sets the state of the mouse.
        /// </summary>
        /// <value>The state of the mouse.</value>
        public MouseState MouseState { get; set; }


        /// <summary>
        /// Handles the <see cref="E:RenderItemText" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemTextRenderEventArgs" /> that contains the event data.</param>
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            var g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            var itemRect = GetItemRect(e.Item);
            var textRect = new Rectangle(24, itemRect.Y, itemRect.Width - (24 + 16), itemRect.Height);
            g.DrawString(
                e.Text,
                SkinManager.ROBOTO_MEDIUM_10,
                e.Item.Enabled ? SkinManager.GetPrimaryTextBrush() : SkinManager.GetDisabledOrHintBrush(),
                textRect,
                new StringFormat { LineAlignment = StringAlignment.Center });
        }

        /// <summary>
        /// Handles the <see cref="E:RenderMenuItemBackground" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(SkinManager.GetApplicationBackgroundColor());

            //Draw background
            var itemRect = GetItemRect(e.Item);
            g.FillRectangle(e.Item.Selected && e.Item.Enabled ? SkinManager.GetCmsSelectedItemBrush() : new SolidBrush(SkinManager.GetApplicationBackgroundColor()), itemRect);

            //Ripple animation
            var toolStrip = e.ToolStrip as ZeroitMaterialContextMenuStrip;
            if (toolStrip != null)
            {
                var animationManager = toolStrip.AnimationManager;
                var animationSource = toolStrip.AnimationSource;
                if (toolStrip.AnimationManager.IsAnimating() && e.Item.Bounds.Contains(animationSource))
                {
                    for (int i = 0; i < animationManager.GetAnimationCount(); i++)
                    {
                        var animationValue = animationManager.GetProgress(i);
                        var rippleBrush = new SolidBrush(Color.FromArgb((int)(51 - (animationValue * 50)), Color.Black));
                        var rippleSize = (int)(animationValue * itemRect.Width * 2.5);
                        g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, itemRect.Y - itemRect.Height, rippleSize, itemRect.Height * 3));
                    }
                }
            }
        }

        /// <summary>
        /// Handles the <see cref="E:RenderImageMargin" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            //base.OnRenderImageMargin(e);
        }

        /// <summary>
        /// Handles the <see cref="E:RenderSeparator" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripSeparatorRenderEventArgs" /> that contains the event data.</param>
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            var g = e.Graphics;

            g.FillRectangle(new SolidBrush(SkinManager.GetApplicationBackgroundColor()), e.Item.Bounds);
            g.DrawLine(
                new Pen(SkinManager.GetDividersColor()),
                new Point(e.Item.Bounds.Left, e.Item.Bounds.Height / 2),
                new Point(e.Item.Bounds.Right, e.Item.Bounds.Height / 2));
        }

        /// <summary>
        /// Handles the <see cref="E:RenderToolStripBorder" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            var g = e.Graphics;

            g.DrawRectangle(
                new Pen(SkinManager.GetDividersColor()),
                new Rectangle(e.AffectedBounds.X, e.AffectedBounds.Y, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1));
        }

        /// <summary>
        /// Handles the <see cref="E:RenderArrow" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripArrowRenderEventArgs" /> that contains the event data.</param>
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            var g = e.Graphics;
            const int ARROW_SIZE = 4;

            var arrowMiddle = new Point(e.ArrowRectangle.X + e.ArrowRectangle.Width / 2, e.ArrowRectangle.Y + e.ArrowRectangle.Height / 2);
            var arrowBrush = e.Item.Enabled ? SkinManager.GetPrimaryTextBrush() : SkinManager.GetDisabledOrHintBrush();
            using (var arrowPath = new GraphicsPath())
            {
                arrowPath.AddLines(
                    new[] {
                        new Point(arrowMiddle.X - ARROW_SIZE, arrowMiddle.Y - ARROW_SIZE),
                        new Point(arrowMiddle.X, arrowMiddle.Y),
                        new Point(arrowMiddle.X - ARROW_SIZE, arrowMiddle.Y + ARROW_SIZE) });
                arrowPath.CloseFigure();

                g.FillPath(arrowBrush, arrowPath);
            }
        }

        /// <summary>
        /// Gets the item rect.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Rectangle.</returns>
        private Rectangle GetItemRect(ToolStripItem item)
        {
            return new Rectangle(0, item.ContentRectangle.Y, item.ContentRectangle.Width + 4, item.ContentRectangle.Height);
        }
    }
}
