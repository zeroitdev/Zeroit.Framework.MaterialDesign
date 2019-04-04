// ***********************************************************************
// Assembly         : Zeroit.Framework.MaterialDesign
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="MaterialDivider.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;
using System.Windows.Forms;

namespace Zeroit.Framework.MaterialDesign.Controls
{
    /// <summary>
    /// Class ZeroitMaterialDivider. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    /// <seealso cref="Zeroit.Framework.MaterialDesign.IMaterialControl" />
    public sealed class ZeroitMaterialDivider : Control, IMaterialControl
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
        /// Initializes a new instance of the <see cref="ZeroitMaterialDivider" /> class.
        /// </summary>
        public ZeroitMaterialDivider()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Height = 1;
            BackColor = SkinManager.GetDividersColor();
        }
    }
}
