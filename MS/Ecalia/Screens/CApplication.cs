using Ecalia.Common;
using Ecalia.Engine.Graphics;
using reWZ;
using reWZ.WZProperties;
using SharpDX;
using SharpDX.Direct3D9;
using SharpDX.Mathematics.Interop;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecalia.Screens
{
    public class CApplication : RenderForm, IDisposable
    {
        private Direct3D D3D;
        public static Device device;

        private CGraphicsEngine graphics;

        public CApplication()
        {
            InitializeDevice();
            InitializeComponent();
            InitializeEngine();
        }

        private void InitializeDevice()
        {
            D3D = new Direct3D();
            device = new Device(D3D, 0, DeviceType.Hardware, Handle, CreateFlags.HardwareVertexProcessing, new PresentParameters(Width, Height));
        }

        private void InitializeEngine()
        {
            graphics = new CGraphicsEngine();
        }

        public void Run()
        {
            Initialize();
            RenderLoop.Run(this, RenderCallback);
        }

        private void RenderCallback()
        {
            device.Clear(ClearFlags.Target, new RawColorBGRA(0, 0, 0, 0), 1.0f, 0);

            device.Present();

        }

        public new void Dispose()
        {
            D3D.Dispose();
            device.Dispose();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CApplication
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Name = "CApplication";
            this.Text = "Ecalia Client";
            this.ResumeLayout(false);

        }

        public static Device GraphicsDevice
        {
            get { return device; }
        }

        private void Initialize()
        {
            graphics.Render(000010000);
        }


        /*private SharpDX.DXGI.ModeDescription modeDesc;
        private SharpDX.DXGI.SwapChain swapChain;
        private SharpDX.DXGI.SwapChainDescription swapChainDesc;
        private SharpDX.Direct3D11.Device device;
        private SharpDX.Direct3D11.DeviceContext deviceContext;
        private SharpDX.Direct3D11.RenderTargetView renderTarget;

        public CApplication()
        {
            InitializeComponent();
        }

        public void Run()
        {
            RenderLoop.Run(this, RenderCallback);
        }

        private void RenderCallback()
        {
            deviceContext.ClearRenderTargetView(renderTarget, new SharpDX.Mathematics.Interop.RawColor4(0, 0, 0, 0));
            swapChain.Present(1, SharpDX.DXGI.PresentFlags.None);
        }

        private void InitializeComponent()
        {
            Width = GameConstants.WINDOW_WIDTH; // Window Width
            Height = GameConstants.WINDOW_HEIGHT; // Window Height
            modeDesc = new SharpDX.DXGI.ModeDescription(Width,
                Height,
                new SharpDX.DXGI.Rational(60, 1), // 60 frames per sec 
                SharpDX.DXGI.Format.R8G8B8A8_UNorm);
            swapChainDesc = new SharpDX.DXGI.SwapChainDescription()
            {
                ModeDescription = modeDesc,
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = SharpDX.DXGI.Usage.RenderTargetOutput,
                BufferCount = 1,
                OutputHandle = Handle,
                IsWindowed = true,
            };
            SharpDX.Direct3D11.Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, SharpDX.Direct3D11.DeviceCreationFlags.None, swapChainDesc, out device, out swapChain);
            deviceContext = device.ImmediateContext;

            using (SharpDX.Direct3D11.Texture2D backBuffer = swapChain.GetBackBuffer<SharpDX.Direct3D11.Texture2D>(0))
            {
                renderTarget = new SharpDX.Direct3D11.RenderTargetView(device, backBuffer);
            }

            deviceContext.OutputMerger.SetRenderTargets(renderTarget);
        }

        public new void Dispose()
        {
            swapChain.Dispose();
            renderTarget.Dispose();
            device.Dispose();
            deviceContext.Dispose();
        }*/
    }
}
