using Cocos2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecalia.Common.Imaging
{
    public class CParallaxCamera
    {
        protected float _zoom; //Camera Zoom Value

        protected Matrix _transform; //Camera Transform Matrix

        protected Matrix _inverseTransform; //Inverse of Transform Matrix

        protected Vector2 _pos; //Camera Position

        protected float _rotation; //Camera Rotation Value (Radians)

        protected Viewport _viewport; //Cameras Viewport

        protected MouseState _mState; //Mouse state

        protected KeyboardState _keyState; //Keyboard state

        protected Int32 _scroll; //Previous Mouse Scroll Wheel Value

        public float Zoom
        {

            get { return _zoom; }

            set { _zoom = value; }

        }

        /// <summary>

        /// Camera View Matrix Property

        /// </summary>

        public Matrix Transform
        {

            get { return _transform; }

            set { _transform = value; }

        }

        /// <summary>

        /// Inverse of the view matrix, can be used to get objects screen coordinates

        /// from its object coordinates

        /// </summary>

        public Matrix InverseTransform
        {

            get { return _inverseTransform; }

        }

        public Vector2 Pos
        {

            get { return _pos; }

            set { _pos = value; }

        }

        public float Rotation
        {

            get { return _rotation; }

            set { _rotation = value; }

        }
        public CParallaxCamera(Viewport viewport)

        {

            _zoom = 1.0f;

            _scroll = 1;

            _rotation = 0.0f;

            _pos = Vector2.Zero;

            _viewport = viewport;

        }

        public void Update()

        {

            //Call Camera Input

            // Input();

            //Clamp zoom value

            MathHelper.Clamp(_zoom, 0.01f, 10.0f);

            //Clamp rotation value

            _rotation = ClampAngle(_rotation);

            //Create view matrix

            _transform = Matrix.CreateRotationZ(_rotation) *

                              Matrix.CreateScale(new Vector3(_zoom, _zoom, 1)) *
                              Matrix.CreateTranslation(_pos.X, _pos.Y, 0);

            //Update inverse matrix

            _inverseTransform = Matrix.Invert(_transform);

        }
        protected virtual void Input()
        {

            _mState = Mouse.GetState();

            _keyState = Keyboard.GetState();

            //Check zoom

            if (_mState.ScrollWheelValue > _scroll)

            {

                _zoom += 0.1f;

                _scroll = _mState.ScrollWheelValue;

            }

            else if (_mState.ScrollWheelValue < _scroll)

            {

                _zoom -= 0.1f;

                _scroll = _mState.ScrollWheelValue;

            }

            //Check rotation

            if (_keyState.IsKeyDown(Keys.Left))

            {

                _rotation -= 0.1f;

            }
            if (_keyState.IsKeyDown(Keys.Right))

            {

                _rotation += 0.1f;

            }

            //Check Move

            if (_keyState.IsKeyDown(Keys.A))

            {

                _pos.X += 0.5f;

            }

            if (_keyState.IsKeyDown(Keys.D))

            {

                _pos.X -= 0.5f;

            }

            if (_keyState.IsKeyDown(Keys.W))

            {

                _pos.Y += 0.5f;

            }

            if (_keyState.IsKeyDown(Keys.S))

            {

                _pos.Y -= 0.5f;

            }

        }
        protected float ClampAngle(float radians)

        {

            while (radians < -MathHelper.Pi)

            {

                radians += MathHelper.TwoPi;

            }

            while (radians > MathHelper.Pi)

            {

                radians -= MathHelper.TwoPi;

            }

            return radians;

        }


    }
}
