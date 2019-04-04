// ***********************************************************************
// Assembly         : Zeroit.Framework.MaterialDesign
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 08-04-2017
// ***********************************************************************
// <copyright file="DrawHelper.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Zeroit.Framework.MaterialDesign
{
    /// <summary>
    /// Class DrawHelper.
    /// </summary>
    static class DrawHelper
    {
        /// <summary>
        /// Creates the round rect.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="radius">The radius.</param>
        /// <returns>GraphicsPath.</returns>
        public static GraphicsPath CreateRoundRect(float x, float y, float width, float height, float radius)
        {
            var gp = new GraphicsPath();
            gp.AddLine(x + radius, y, x + width - (radius * 2), y);
            gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
            gp.AddLine(x + width, y + radius, x + width, y + height - (radius * 2));
            gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
            gp.AddLine(x + width - (radius * 2), y + height, x + radius, y + height);
            gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
            gp.AddLine(x, y + height - (radius * 2), x, y + radius);
            gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);
            gp.CloseFigure();
            return gp;
        }

        /// <summary>
        /// Creates the round rect.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="radius">The radius.</param>
        /// <returns>GraphicsPath.</returns>
        public static GraphicsPath CreateRoundRect(Rectangle rect, float radius)
        {
            return CreateRoundRect(rect.X, rect.Y, rect.Width, rect.Height, radius);
        }

        /// <summary>
        /// Blends the color.
        /// </summary>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="frontColor">Color of the front.</param>
        /// <param name="blend">The blend.</param>
        /// <returns>Color.</returns>
        public static Color BlendColor(Color backgroundColor, Color frontColor, double blend)
        {
            var ratio = blend / 255d;
            var invRatio = 1d - ratio;
            var r = (int)((backgroundColor.R * invRatio) + (frontColor.R * ratio));
            var g = (int)((backgroundColor.G * invRatio) + (frontColor.G * ratio));
            var b = (int)((backgroundColor.B * invRatio) + (frontColor.B * ratio));
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Blends the color.
        /// </summary>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="frontColor">Color of the front.</param>
        /// <returns>Color.</returns>
        public static Color BlendColor(Color backgroundColor, Color frontColor)
        {
            return BlendColor(backgroundColor, frontColor, frontColor.A);
        }
    }
}
