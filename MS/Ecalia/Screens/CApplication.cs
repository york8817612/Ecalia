using Ecalia.Common;
using Ecalia.Engine.Graphics;
using reWZ;
using reWZ.WZProperties;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.Mathematics.Interop;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecalia.Screens
{
    public class CApplication : RenderForm, IDisposable
    {
        private SharpDX.DXGI.SwapChain swapChain;
        private Device device;
        private DeviceContext deviceContext;
        private RenderTargetView renderTarget;
        private RawColor4 screenColor = new RawColor4(0, 0, 0, 0);


        private CGraphicsEngine graphics;
        private CDrawManager drawMan;

        public CApplication()
            : base()
        {
            InitializeDevice();
            InitializeComponent();
            InitializeEngine();
        }

        private void InitializeDevice()
        {
            if (!InitDevice())
            {
                MessageBox.Show("[Init Error] DirectX Failed to Initialize! - System shutting down.");
                Environment.Exit(0);
            }

            if (!InitScene())
            {
                MessageBox.Show("[Init Error] Scene failed to Initialize! - System shutting down.");
                Environment.Exit(0);
            }
        }

        private bool InitDevice()
        {
            var mode = new SharpDX.DXGI.ModeDescription()
            {
                Width = Width,
                Height = Height,
                RefreshRate = new SharpDX.DXGI.Rational(60, 1),
                Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm,
                ScanlineOrdering = SharpDX.DXGI.DisplayModeScanlineOrder.Unspecified,
                Scaling = SharpDX.DXGI.DisplayModeScaling.Unspecified,
            };

            var swapDesc = new SharpDX.DXGI.SwapChainDescription()
            {
                BufferCount = 1,
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = SharpDX.DXGI.Usage.RenderTargetOutput,
                ModeDescription = mode,
                OutputHandle = Handle,
                IsWindowed = true,
                SwapEffect = SharpDX.DXGI.SwapEffect.Discard,
            };

            Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, swapDesc, out device, out swapChain);

            Texture2D backbuffer = swapChain.GetBackBuffer<Texture2D>(0);
            renderTarget = new RenderTargetView(device, backbuffer);

            deviceContext = device.ImmediateContext;

            deviceContext.OutputMerger.SetRenderTargets(renderTarget);

            deviceContext.Rasterizer.SetViewport(0, 0, Width, Height);

            return true;
        }

        private bool InitScene()
        {
            return true;
        }

        private void InitializeEngine()
        {
            graphics = new CGraphicsEngine() { ID = 10000 };
            drawMan = new CDrawManager(device, new Viewport());
        }

        public void Run()
        {
            RenderLoop.Run(this, RenderCallback);
        }

        private void RenderCallback()
        {
            if (!Focused)
                Application.DoEvents();
            else
            {
            }

            deviceContext.ClearRenderTargetView(renderTarget, screenColor);
            swapChain.Present(1, SharpDX.DXGI.PresentFlags.None);
        }

        private void RenderSurface()
        {

        }


        public new void Dispose()
        {
            device.Dispose();
            deviceContext.Dispose();
            renderTarget.Dispose();
            swapChain.Dispose();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CApplication
            // 
            ClientSize = new System.Drawing.Size(800, 600);
            Name = "CApplication";
            Text = "Ecalia Client";
            ResumeLayout(false);

        }

        public void Render()
        {
            graphics.OnEnter();
        }
    }
}
