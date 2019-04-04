// ***********************************************************************
// Assembly         : Zeroit.Framework.MaterialDesign
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="MaterialLabel.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Zeroit.Framework.MaterialDesign.Controls
{


    /// <summary>
    /// A class collection for rendering a Material design label.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Label" />
    /// <seealso cref="Zeroit.Framework.MaterialDesign.IMaterialControl" />
    public class ZeroitMaterialLabel : Control, IMaterialControl
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
        /// Raises the <see cref="M:System.Windows.Forms.Control.CreateControl" /> method.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            ForeColor = SkinManager.GetPrimaryTextColor();
            Font = SkinManager.ROBOTO_REGULAR_11;

            BackColorChanged += (sender, args) => ForeColor = SkinManager.GetPrimaryTextColor();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            TransInPaint(e.Graphics);

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            
            g.DrawString(Text, Font, new SolidBrush(ForeColor), new PointF(0, 0));
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Graphics g = CreateGraphics();
            SizeF fS = g.MeasureString(Text, Font);

            Size = new Size((int)fS.Width, (int)fS.Height);

        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            Graphics g = CreateGraphics();
            SizeF fS = g.MeasureString(Text, Font);

            Size = new Size((int)fS.Width, (int)fS.Height);
        }


        #region Transparency


        #region Include in Paint

        private void TransInPaint(Graphics g)
        {
            if (AllowTransparency)
            {
                MakeTransparent(this, g);
            }
        }

        #endregion

        #region Include in Private Field

        private bool allowTransparency = true;

        #endregion

        #region Include in Public Properties

        public bool AllowTransparency
        {
            get { return allowTransparency; }
            set
            {
                allowTransparency = value;

                Invalidate();
            }
        }

        #endregion

        #region Method

        //-----------------------------Include in Paint--------------------------//
        //
        // if(AllowTransparency)
        //  {
        //    MakeTransparent(this,g);
        //  }
        //
        //-----------------------------Include in Paint--------------------------//

        private static void MakeTransparent(Control control, Graphics g)
        {
            var parent = control.Parent;
            if (parent == null) return;
            var bounds = control.Bounds;
            var siblings = parent.Controls;
            int index = siblings.IndexOf(control);
            Bitmap behind = null;
            for (int i = siblings.Count - 1; i > index; i--)
            {
                var c = siblings[i];
                if (!c.Bounds.IntersectsWith(bounds)) continue;
                if (behind == null)
                    behind = new Bitmap(control.Parent.ClientSize.Width, control.Parent.ClientSize.Height);
                c.DrawToBitmap(behind, c.Bounds);
            }
            if (behind == null) return;
            g.DrawImage(behind, control.ClientRectangle, bounds, GraphicsUnit.Pixel);
            behind.Dispose();
        }

        #endregion


        #endregion



    }
}
