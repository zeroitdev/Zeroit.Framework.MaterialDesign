﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.MaterialDesign
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 08-04-2017
// ***********************************************************************
// <copyright file="AnimationManager.cs" company="Zeroit Dev Technologies">
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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Zeroit.Framework.MaterialDesign.Animations
{
    /// <summary>
    /// Class AnimationManager.
    /// </summary>
    class AnimationManager
    {
        /// <summary>
        /// Gets or sets a value indicating whether [interrupt animation].
        /// </summary>
        /// <value><c>true</c> if [interrupt animation]; otherwise, <c>false</c>.</value>
        public bool InterruptAnimation { get; set; }
        /// <summary>
        /// Gets or sets the increment.
        /// </summary>
        /// <value>The increment.</value>
        public double Increment { get; set; }
        /// <summary>
        /// Gets or sets the secondary increment.
        /// </summary>
        /// <value>The secondary increment.</value>
        public double SecondaryIncrement { get; set; }
        /// <summary>
        /// Gets or sets the type of the animation.
        /// </summary>
        /// <value>The type of the animation.</value>
        public AnimationType AnimationType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AnimationManager"/> is singular.
        /// </summary>
        /// <value><c>true</c> if singular; otherwise, <c>false</c>.</value>
        public bool Singular { get; set; }

        /// <summary>
        /// Delegate AnimationFinished
        /// </summary>
        /// <param name="sender">The sender.</param>
        public delegate void AnimationFinished(object sender);
        /// <summary>
        /// Occurs when [on animation finished].
        /// </summary>
        public event AnimationFinished OnAnimationFinished;

        /// <summary>
        /// Delegate AnimationProgress
        /// </summary>
        /// <param name="sender">The sender.</param>
        public delegate void AnimationProgress(object sender);
        /// <summary>
        /// Occurs when [on animation progress].
        /// </summary>
        public event AnimationProgress OnAnimationProgress;

        /// <summary>
        /// The animation progresses
        /// </summary>
        private readonly List<double> _animationProgresses;
        /// <summary>
        /// The animation sources
        /// </summary>
        private readonly List<Point> _animationSources;
        /// <summary>
        /// The animation directions
        /// </summary>
        private readonly List<AnimationDirection> _animationDirections;
        /// <summary>
        /// The animation datas
        /// </summary>
        private readonly List<object[]> _animationDatas;

        /// <summary>
        /// The minimum value
        /// </summary>
        private const double MIN_VALUE = 0.00;
        /// <summary>
        /// The maximum value
        /// </summary>
        private const double MAX_VALUE = 1.00;

        /// <summary>
        /// The animation timer
        /// </summary>
        private readonly Timer _animationTimer = new Timer { Interval = 5, Enabled = false };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="singular">If true, only one animation is supported. The current animation will be replaced with the new one. If false, a new animation is added to the list.</param>
        public AnimationManager(bool singular = true)
        {
            _animationProgresses = new List<double>();
            _animationSources = new List<Point>();
            _animationDirections = new List<AnimationDirection>();
            _animationDatas = new List<object[]>();

            Increment = 0.03;
            SecondaryIncrement = 0.03;
            AnimationType = AnimationType.Linear;
            InterruptAnimation = true;
            Singular = singular;

            if (Singular)
            {
                _animationProgresses.Add(0);
                _animationSources.Add(new Point(0, 0));
                _animationDirections.Add(AnimationDirection.In);
            }

            _animationTimer.Tick += AnimationTimerOnTick;
        }

        /// <summary>
        /// Animations the timer on tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AnimationTimerOnTick(object sender, EventArgs eventArgs)
        {
            for (var i = 0; i < _animationProgresses.Count; i++)
            {
                UpdateProgress(i);

                if (!Singular)
                {
                    if ((_animationDirections[i] == AnimationDirection.InOutIn && _animationProgresses[i] == MAX_VALUE))
                    {
                        _animationDirections[i] = AnimationDirection.InOutOut;
                    }
                    else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingIn && _animationProgresses[i] == MIN_VALUE))
                    {
                        _animationDirections[i] = AnimationDirection.InOutRepeatingOut;
                    }
                    else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingOut && _animationProgresses[i] == MIN_VALUE))
                    {
                        _animationDirections[i] = AnimationDirection.InOutRepeatingIn;
                    }
                    else if (
                        (_animationDirections[i] == AnimationDirection.In && _animationProgresses[i] == MAX_VALUE) ||
                        (_animationDirections[i] == AnimationDirection.Out && _animationProgresses[i] == MIN_VALUE) ||
                        (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] == MIN_VALUE))
                    {
                        _animationProgresses.RemoveAt(i);
                        _animationSources.RemoveAt(i);
                        _animationDirections.RemoveAt(i);
                        _animationDatas.RemoveAt(i);
                    }
                }
                else
                {
                    if ((_animationDirections[i] == AnimationDirection.InOutIn && _animationProgresses[i] == MAX_VALUE))
                    {
                        _animationDirections[i] = AnimationDirection.InOutOut;
                    }
                    else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingIn && _animationProgresses[i] == MAX_VALUE))
                    {
                        _animationDirections[i] = AnimationDirection.InOutRepeatingOut;
                    }
                    else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingOut && _animationProgresses[i] == MIN_VALUE))
                    {
                        _animationDirections[i] = AnimationDirection.InOutRepeatingIn;
                    }
                }
            }

            OnAnimationProgress?.Invoke(this);
        }

        /// <summary>
        /// Determines whether this instance is animating.
        /// </summary>
        /// <returns><c>true</c> if this instance is animating; otherwise, <c>false</c>.</returns>
        public bool IsAnimating()
        {
            return _animationTimer.Enabled;
        }

        /// <summary>
        /// Starts the new animation.
        /// </summary>
        /// <param name="animationDirection">The animation direction.</param>
        /// <param name="data">The data.</param>
        public void StartNewAnimation(AnimationDirection animationDirection, object[] data = null)
        {
            StartNewAnimation(animationDirection, new Point(0, 0), data);
        }

        /// <summary>
        /// Starts the new animation.
        /// </summary>
        /// <param name="animationDirection">The animation direction.</param>
        /// <param name="animationSource">The animation source.</param>
        /// <param name="data">The data.</param>
        /// <exception cref="System.Exception">Invalid AnimationDirection</exception>
        public void StartNewAnimation(AnimationDirection animationDirection, Point animationSource, object[] data = null)
        {
            if (!IsAnimating() || InterruptAnimation)
            {
                if (Singular && _animationDirections.Count > 0)
                {
                    _animationDirections[0] = animationDirection;
                }
                else
                {
                    _animationDirections.Add(animationDirection);
                }

                if (Singular && _animationSources.Count > 0)
                {
                    _animationSources[0] = animationSource;
                }
                else
                {
                    _animationSources.Add(animationSource);
                }

                if (!(Singular && _animationProgresses.Count > 0))
                {
                    switch (_animationDirections[_animationDirections.Count - 1])
                    {
                        case AnimationDirection.InOutRepeatingIn:
                        case AnimationDirection.InOutIn:
                        case AnimationDirection.In:
                            _animationProgresses.Add(MIN_VALUE);
                            break;
                        case AnimationDirection.InOutRepeatingOut:
                        case AnimationDirection.InOutOut:
                        case AnimationDirection.Out:
                            _animationProgresses.Add(MAX_VALUE);
                            break;
                        default:
                            throw new Exception("Invalid AnimationDirection");
                    }
                }

                if (Singular && _animationDatas.Count > 0)
                {
                    _animationDatas[0] = data ?? new object[] { };
                }
                else
                {
                    _animationDatas.Add(data ?? new object[] { });
                }

            }

            _animationTimer.Start();
        }

        /// <summary>
        /// Updates the progress.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <exception cref="System.Exception">No AnimationDirection has been set</exception>
        public void UpdateProgress(int index)
        {
            switch (_animationDirections[index])
            {
                case AnimationDirection.InOutRepeatingIn:
                case AnimationDirection.InOutIn:
                case AnimationDirection.In:
                    IncrementProgress(index);
                    break;
                case AnimationDirection.InOutRepeatingOut:
                case AnimationDirection.InOutOut:
                case AnimationDirection.Out:
                    DecrementProgress(index);
                    break;
                default:
                    throw new Exception("No AnimationDirection has been set");
            }
        }

        /// <summary>
        /// Increments the progress.
        /// </summary>
        /// <param name="index">The index.</param>
        private void IncrementProgress(int index)
        {
            _animationProgresses[index] += Increment;
            if (_animationProgresses[index] > MAX_VALUE)
            {
                _animationProgresses[index] = MAX_VALUE;

                for (int i = 0; i < GetAnimationCount(); i++)
                {
                    if (_animationDirections[i] == AnimationDirection.InOutIn) return;
                    if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn) return;
                    if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut) return;
                    if (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] != MAX_VALUE) return;
                    if (_animationDirections[i] == AnimationDirection.In && _animationProgresses[i] != MAX_VALUE) return;
                }

                _animationTimer.Stop();
                OnAnimationFinished?.Invoke(this);
            }
        }

        /// <summary>
        /// Decrements the progress.
        /// </summary>
        /// <param name="index">The index.</param>
        private void DecrementProgress(int index)
        {
            _animationProgresses[index] -= (_animationDirections[index] == AnimationDirection.InOutOut || _animationDirections[index] == AnimationDirection.InOutRepeatingOut) ? SecondaryIncrement : Increment;
            if (_animationProgresses[index] < MIN_VALUE)
            {
                _animationProgresses[index] = MIN_VALUE;

                for (var i = 0; i < GetAnimationCount(); i++)
                {
                    if (_animationDirections[i] == AnimationDirection.InOutIn) return;
                    if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn) return;
                    if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut) return;
                    if (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] != MIN_VALUE) return;
                    if (_animationDirections[i] == AnimationDirection.Out && _animationProgresses[i] != MIN_VALUE) return;
                }

                _animationTimer.Stop();
                OnAnimationFinished?.Invoke(this);
            }
        }

        /// <summary>
        /// Gets the progress.
        /// </summary>
        /// <returns>System.Double.</returns>
        /// <exception cref="System.Exception">
        /// Animation is not set to Singular.
        /// or
        /// Invalid animation
        /// </exception>
        public double GetProgress()
        {
            if (!Singular)
                throw new Exception("Animation is not set to Singular.");

            if (_animationProgresses.Count == 0)
                throw new Exception("Invalid animation");

            return GetProgress(0);
        }

        /// <summary>
        /// Gets the progress.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>System.Double.</returns>
        /// <exception cref="System.IndexOutOfRangeException">Invalid animation index</exception>
        /// <exception cref="System.NotImplementedException">The given AnimationType is not implemented</exception>
        public double GetProgress(int index)
        {
            if (!(index < GetAnimationCount()))
                throw new IndexOutOfRangeException("Invalid animation index");

            switch (AnimationType)
            {
                case AnimationType.Linear:
                    return AnimationLinear.CalculateProgress(_animationProgresses[index]);
                case AnimationType.EaseInOut:
                    return AnimationEaseInOut.CalculateProgress(_animationProgresses[index]);
                case AnimationType.EaseOut:
                    return AnimationEaseOut.CalculateProgress(_animationProgresses[index]);
                case AnimationType.CustomQuadratic:
                    return AnimationCustomQuadratic.CalculateProgress(_animationProgresses[index]);
                default:
                    throw new NotImplementedException("The given AnimationType is not implemented");
            }

        }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>Point.</returns>
        /// <exception cref="System.IndexOutOfRangeException">Invalid animation index</exception>
        public Point GetSource(int index)
        {
            if (!(index < GetAnimationCount()))
                throw new IndexOutOfRangeException("Invalid animation index");

            return _animationSources[index];
        }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <returns>Point.</returns>
        /// <exception cref="System.Exception">
        /// Animation is not set to Singular.
        /// or
        /// Invalid animation
        /// </exception>
        public Point GetSource()
        {
            if (!Singular)
                throw new Exception("Animation is not set to Singular.");

            if (_animationSources.Count == 0)
                throw new Exception("Invalid animation");

            return _animationSources[0];
        }

        /// <summary>
        /// Gets the direction.
        /// </summary>
        /// <returns>AnimationDirection.</returns>
        /// <exception cref="System.Exception">
        /// Animation is not set to Singular.
        /// or
        /// Invalid animation
        /// </exception>
        public AnimationDirection GetDirection()
        {
            if (!Singular)
                throw new Exception("Animation is not set to Singular.");

            if (_animationDirections.Count == 0)
                throw new Exception("Invalid animation");

            return _animationDirections[0];
        }

        /// <summary>
        /// Gets the direction.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>AnimationDirection.</returns>
        /// <exception cref="System.IndexOutOfRangeException">Invalid animation index</exception>
        public AnimationDirection GetDirection(int index)
        {
            if (!(index < _animationDirections.Count))
                throw new IndexOutOfRangeException("Invalid animation index");

            return _animationDirections[index];
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <returns>System.Object[].</returns>
        /// <exception cref="System.Exception">
        /// Animation is not set to Singular.
        /// or
        /// Invalid animation
        /// </exception>
        public object[] GetData()
        {
            if (!Singular)
                throw new Exception("Animation is not set to Singular.");

            if (_animationDatas.Count == 0)
                throw new Exception("Invalid animation");

            return _animationDatas[0];
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>System.Object[].</returns>
        /// <exception cref="System.IndexOutOfRangeException">Invalid animation index</exception>
        public object[] GetData(int index)
        {
            if (!(index < _animationDatas.Count))
                throw new IndexOutOfRangeException("Invalid animation index");

            return _animationDatas[index];
        }

        /// <summary>
        /// Gets the animation count.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetAnimationCount()
        {
            return _animationProgresses.Count;
        }

        /// <summary>
        /// Sets the progress.
        /// </summary>
        /// <param name="progress">The progress.</param>
        /// <exception cref="System.Exception">
        /// Animation is not set to Singular.
        /// or
        /// Invalid animation
        /// </exception>
        public void SetProgress(double progress)
        {
            if (!Singular)
                throw new Exception("Animation is not set to Singular.");

            if (_animationProgresses.Count == 0)
                throw new Exception("Invalid animation");

            _animationProgresses[0] = progress;
        }

        /// <summary>
        /// Sets the direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <exception cref="System.Exception">
        /// Animation is not set to Singular.
        /// or
        /// Invalid animation
        /// </exception>
        public void SetDirection(AnimationDirection direction)
        {
            if (!Singular)
                throw new Exception("Animation is not set to Singular.");

            if (_animationProgresses.Count == 0)
                throw new Exception("Invalid animation");

            _animationDirections[0] = direction;
        }

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <exception cref="System.Exception">
        /// Animation is not set to Singular.
        /// or
        /// Invalid animation
        /// </exception>
        public void SetData(object[] data)
        {
            if (!Singular)
                throw new Exception("Animation is not set to Singular.");

            if (_animationDatas.Count == 0)
                throw new Exception("Invalid animation");

            _animationDatas[0] = data;
        }
    }
}
