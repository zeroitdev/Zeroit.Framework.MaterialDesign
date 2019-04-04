// ***********************************************************************
// Assembly         : Zeroit.Framework.MaterialDesign
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="MaterialFlatButton.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Zeroit.Framework.MaterialDesign.Animations;

namespace Zeroit.Framework.MaterialDesign.Controls
{
    /// <summary>
    /// A class collection for rendering Material design flat button.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Button" />
    /// <seealso cref="Zeroit.Framework.MaterialDesign.IMaterialControl" />
    public class ZeroitMaterialFlatButton : Control, IMaterialControl
    {

        /// <summary>
        /// The automatic size mode
        /// </summary>
        private AutoSizeMode autoSizeMode = AutoSizeMode.GrowAndShrink;
        /// <summary>
        /// The animation manager
        /// </summary>
        private readonly AnimationManager _animationManager;
        /// <summary>
        /// The hover animation manager
        /// </summary>
        private readonly AnimationManager _hoverAnimationManager;

        /// <summary>
        /// The text size
        /// </summary>
        private SizeF _textSize;

        /// <summary>
        /// The icon
        /// </summary>
        private Image _icon;

        /// <summary>
        /// The upper case
        /// </summary>
        private bool upperCase = false;

        /// <summary>
        /// Gets or sets the automatic size mode.
        /// </summary>
        /// <value>The automatic size mode.</value>
        public AutoSizeMode AutoSizeMode
        {
            get { return autoSizeMode; }
            set
            {
                autoSizeMode = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public Image Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                if (AutoSize)
                    Size = GetPreferredSize();
                Invalidate();
            }
        }

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
        [Browsable(true)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public ZeroitMaterialSkinManager SkinManager => ZeroitMaterialSkinManager.Instance;

        /// <summary>
        /// Gets or sets the state of the mouse.
        /// </summary>
        /// <value>The state of the mouse.</value>
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitMaterialFlatButton" /> has primary enabled.
        /// </summary>
        /// <value><c>true</c> if primary; otherwise, <c>false</c>.</value>
        public bool Primary { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMaterialFlatButton" /> class.
        /// </summary>
        public ZeroitMaterialFlatButton()
        {

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            
            Primary = false;

            _animationManager = new AnimationManager(false)
            {
                Increment = 0.03,
                AnimationType = AnimationType.EaseOut
            };
            _hoverAnimationManager = new AnimationManager
            {
                Increment = 0.07,
                AnimationType = AnimationType.Linear
            };

            _hoverAnimationManager.OnAnimationProgress += sender => Invalidate();
            _animationManager.OnAnimationProgress += sender => Invalidate();

            //AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AutoSize = true;
            Margin = new Padding(4, 6, 4, 6);
            Padding = new Padding(0);
        }


        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;

                if (UpperCase)
                {
                    _textSize = CreateGraphics().MeasureString(value.ToUpper(), SkinManager.ROBOTO_MEDIUM_10);

                }
                else
                {
                    _textSize = CreateGraphics().MeasureString(value, SkinManager.ROBOTO_MEDIUM_10);

                }

                if (AutoSize)
                    Size = GetPreferredSize();
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether text should be upper case.
        /// </summary>
        /// <value><c>true</c> if upper case; otherwise, <c>false</c>.</value>
        public bool UpperCase
        {
            get { return upperCase; }
            set
            {
                upperCase = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnPaint(System.Windows.Forms.PaintEventArgs)" /> event.
        /// </summary>
        /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            TransInPaint(pevent.Graphics);

            var g = pevent.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            //g.Clear(BackColor);

            //Hover
            Color c = SkinManager.GetFlatButtonHoverBackgroundColor();
            using (Brush b = new SolidBrush(Color.FromArgb((int)(_hoverAnimationManager.GetProgress() * c.A), c.RemoveAlpha())))
                g.FillRectangle(b, ClientRectangle);
            

            //Ripple
            if (_animationManager.IsAnimating())
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                for (var i = 0; i < _animationManager.GetAnimationCount(); i++)
                {
                    var animationValue = _animationManager.GetProgress(i);
                    var animationSource = _animationManager.GetSource(i);

                    using (Brush rippleBrush = new SolidBrush(Color.FromArgb((int)(101 - (animationValue * 100)), Color.Black)))
                    {
                        var rippleSize = (int)(animationValue * Width * 2);
                        g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                    }
                }
                //g.SmoothingMode = SmoothingMode.None;
            }

            //Icon
            var iconRect = new Rectangle(8, 6, 24, 24);

            if (string.IsNullOrEmpty(Text))
                // Center Icon
                iconRect.X += 2;

            if (Icon != null)
                g.DrawImage(Icon, iconRect);

            //Text
            var textRect = ClientRectangle;

            if (Icon != null)
            {
                //
                // Resize and move Text container
                //

                // First 8: left padding
                // 24: icon width
                // Second 4: space between Icon and Text
                // Third 8: right padding
                textRect.Width -= 8 + 24 + 4 + 8;

                // First 8: left padding
                // 24: icon width
                // Second 4: space between Icon and Text
                textRect.X += 8 + 24 + 4;
            }

            if (UpperCase)
            {
                g.DrawString(
                    Text.ToUpper(),
                    SkinManager.ROBOTO_MEDIUM_10,
                    Enabled ? (Primary ? SkinManager.ColorScheme.PrimaryBrush : SkinManager.GetPrimaryTextBrush()) : SkinManager.GetFlatButtonDisabledTextBrush(),
                    textRect,
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
            }
            else
            {
                g.DrawString(
                    Text,
                    SkinManager.ROBOTO_MEDIUM_10,
                    new SolidBrush(ForeColor),
                    textRect,
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
            }
            
        }

        /// <summary>
        /// Gets the size of the preferred.
        /// </summary>
        /// <returns>Size.</returns>
        private Size GetPreferredSize()
        {
            return GetPreferredSize(new Size(0, 0));
        }

        /// <summary>
        /// Retrieves the size of a rectangular area into which a control can be fitted.
        /// </summary>
        /// <param name="proposedSize">The custom-sized area for a control.</param>
        /// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
        public override Size GetPreferredSize(Size proposedSize)
        {
            // Provides extra space for proper padding for content
            var extra = 16;

            if (Icon != null)
                // 24 is for icon size
                // 4 is for the space between icon & text
                extra += 24 + 4;

            return new Size((int)Math.Ceiling(_textSize.Width) + extra, 36);
        }

        /// <summary>
        /// Raises the <see cref="M:System.Windows.Forms.Control.CreateControl" /> method.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DesignMode) return;

            MouseState = MouseState.OUT;
            MouseEnter += (sender, args) =>
            {
                MouseState = MouseState.HOVER;
                _hoverAnimationManager.StartNewAnimation(AnimationDirection.In);
                Invalidate();
            };
            MouseLeave += (sender, args) =>
            {
                MouseState = MouseState.OUT;
                _hoverAnimationManager.StartNewAnimation(AnimationDirection.Out);
                Invalidate();
            };
            MouseDown += (sender, args) =>
            {
                if (args.Button == MouseButtons.Left)
                {
                    MouseState = MouseState.DOWN;

                    _animationManager.StartNewAnimation(AnimationDirection.In, args.Location);
                    Invalidate();
                }
            };
            MouseUp += (sender, args) =>
            {
                MouseState = MouseState.HOVER;

                Invalidate();
            };
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            //if (AutoSizeMode != AutoSizeMode.GrowOnly)
            //{
            //    Size = UnionSizes(GetPreferredSize(), this.Size);
            //}
            
        }

        public static Size UnionSizes(Size a, Size b)
        {
            return new Size(Math.Max(a.Width, b.Width), Math.Max(a.Height, b.Height));
        }

        public static Size IntersectSizes(Size a, Size b)
        {
            return new Size(Math.Min(a.Width, b.Width), Math.Min(a.Height, b.Height));
        }

        public static bool IsIntersectHorizontally(Rectangle rect1, Rectangle rect2)
        {
            return rect1.IntersectsWith(rect2) && (rect1.X <= rect2.X && rect1.X + rect1.Width >= rect2.X + rect2.Width || rect2.X <= rect1.X && rect2.X + rect2.Width >= rect1.X + rect1.Width);
        }

        public static bool IsIntersectVertically(Rectangle rect1, Rectangle rect2)
        {
            return rect1.IntersectsWith(rect2) && (rect1.Y <= rect2.Y && rect1.Y + rect1.Width >= rect2.Y + rect2.Width || rect2.Y <= rect1.Y && rect2.Y + rect2.Width >= rect1.Y + rect1.Width);
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
