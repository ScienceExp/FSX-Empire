using DirectShowLib;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WebCam
{
    public partial class WebCapture : UserControl, ISampleGrabberCB
    {
        public bool isEnabled = true ;
        static Procesor procesor;
        //private volatile bool isFinished = true; //Could work on one frame and let others pass...


        #region Properties Box
        [Category("DirectShow"),
         Description("The width of the image captured by Direct Show")]
        public int HorizontalResoltion { get; set; } = 320;

        [Category("DirectShow"),
        Description("The height of the image captured by Direct Show")]
        public int VerticalResoltion { get; set; } = 240;

        [Category("DirectShow"),
        Description("The frames per second that image is captured by Direct Show")]
        public int CaptureFPS { get; set; } = 30;

        [Category("DirectShow"),
        Description("Size of the tracker marker to be generated and searched for")]
        public int MarkerSize { get; set; } = 8;

        [Category("DirectShow"),
        Description("Shows the marker being tracked and all possible matches")]
        public bool ShowDebug { get; set; } = false;

        [Category("DirectShow"),
        Description("Minimum match value that will be considered matching the tracker")]
        public float MarkerMinMatch { get; set; } = 0.3f;
        #endregion

        #region Marker Locations
        [Browsable(false)]
        public Point TopMarker
        { get { return procesor.tracker.MarkerLocations[0]; } }

        [Browsable(false)]
        public Point BottomMarker
        { get { return procesor.tracker.MarkerLocations[1]; } }
        #endregion

        #region Head Tracking Calibration (Todo: find way to make this more seperate from usercontrol)
        public Calibration calibration = new Calibration();

        /// <summary>Just a simple way to do calibration</summary>
        public void DoCalibration()
        {
            calibration.DoCalibration(ref procesor.tracker);
            CalibrationToTracker();
        }

        void CreateTracker()
        {
            procesor = new Procesor(HorizontalResoltion, VerticalResoltion, MarkerSize)
            { ShowDebug = ShowDebug };
            procesor.tracker.minimumPositive = MarkerMinMatch;
            procesor.tracker.markerSize = MarkerSize;

            calibration.ReadINI();

            CalibrationToTracker();
        }

        void CalibrationToTracker()
        {
            procesor.tracker.boundingBox.left = calibration.boundingBox.left + calibration.boundingBoxPadding;
            procesor.tracker.boundingBox.right = calibration.boundingBox.right - calibration.boundingBoxPadding;
            procesor.tracker.boundingBox.top = calibration.boundingBox.top + calibration.boundingBoxPadding;
            procesor.tracker.boundingBox.bottom = calibration.boundingBox.bottom - calibration.boundingBoxPadding;

            #region Fix off screen bounding box
            if (procesor.tracker.boundingBox.right < 0)
                procesor.tracker.boundingBox.right = 0;
            if (procesor.tracker.boundingBox.bottom < 0)
                procesor.tracker.boundingBox.bottom = 0;
            if (procesor.tracker.boundingBox.left > HorizontalResoltion)
                procesor.tracker.boundingBox.left = HorizontalResoltion;
            if (procesor.tracker.boundingBox.top > VerticalResoltion)
                procesor.tracker.boundingBox.top = VerticalResoltion;
            #endregion
        }
        #endregion

        #region Direct Show Declerations
        //private int _previewStride;

        enum PlayState
        {
            Stopped,
            Paused,
            Running,
            Init
        }
        PlayState CurrentState = PlayState.Stopped;

        // Application-defined message to notify app of filtergraph events
        const int WM_GRAPHNOTIFY = 0x8000 + 1;
        //const int WM_NCHITTEST = 0x0084;
        //const int HTTRANSPARENT = -1;

        IVideoWindow VideoWindow = null;
        IMediaControl MediaControl = null;
        IMediaEventEx MediaEventEx = null;
        IGraphBuilder GraphBuilder = null;
        ICaptureGraphBuilder2 CaptureGraphBuilder = null;
        //IBasicVideo BasicVideo = null;

        DsROTEntry rot = null;
        #endregion

        #region Constructor
        public WebCapture()
        {
            InitializeComponent();
        }
        #endregion

        public void Start()
        {
            if (!isEnabled)
                return;

            CreateTracker();

            DsDevice[] devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            // foreach (DsDevice device in devices)
            // Console.WriteLine("Device Found: " + device.Name);

            if (devices.Length == 0)
                MessageBox.Show("No Video Input Device Detected.");
            else
                CaptureVideo();
        }

        #region Direct Show

        #region Callbacks
        /// <summary> sample callback, NOT USED. </summary>
        public int SampleCB(double SampleTime, IMediaSample pSample)
        {
            Marshal.ReleaseComObject(pSample);
            return 0;
        }
        /// <summary> buffer callback, COULD BE FROM FOREIGN THREAD. </summary>
        public int BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
        {
            //    Bitmap b = new Bitmap(_previewWidth, _previewHeight, _previewStride,
            //      PixelFormat.Format24bppRgb, pBuffer);
            //        b.RotateFlip(RotateFlipType.Rotate180FlipX);

            //if (isFinished)
            //{
            //    this.BeginInvoke((MethodInvoker)delegate
            //{
                //isFinished = false;
                //Stopwatch sw = Stopwatch.StartNew();
                //sw.Start();

                procesor.StartDraw(pBuffer);

                //img.GetPixelIntensity();
                procesor.EndDraw();
                //sw.Stop();
                //isFinished = true;
            //}
            //   );
            //}

            return 0;
        }

        /// <summary>User Control Callback</summary>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_GRAPHNOTIFY:
                    {
                        HandleGraphEvent();
                        break;
                    }
                    //case WM_NCHITTEST:
                    //    m.Result = new IntPtr(HTTRANSPARENT);
                    //    return;
            }

            // Pass this message to the video window for notification of system changes
            if (VideoWindow != null)
                VideoWindow.NotifyOwnerMessage(m.HWnd, m.Msg, m.WParam, m.LParam);

            base.WndProc(ref m);
        }
        #endregion

        #region Setup
        /// <summary>Initializes Capture</summary>
        void CaptureVideo()
        {
            IBaseFilter sourceFilter = null;
            ISampleGrabber sampleGrabber = null;

            try
            {
                // Get DirectShow interfaces
                GetInterfaces();
                // Attach the filter graph to the capture graph
                int hr = CaptureGraphBuilder.SetFiltergraph(GraphBuilder);
                // Console.WriteLine("Attach the filter graph to the capture graph : " + DsError.GetErrorText(hr));
                DsError.ThrowExceptionForHR(hr);

                // Use the system device enumerator and class enumerator to find
                // a video capture/preview device, such as a desktop USB video camera.
                sourceFilter = FindCaptureDevice();

                // Add Capture filter to our graph.
                hr = GraphBuilder.AddFilter(sourceFilter, "Video Capture");
                // Console.WriteLine("Add Capture filter to our graph : " + DsError.GetErrorText(hr));
                DsError.ThrowExceptionForHR(hr);

                // Initialize SampleGrabber.
                sampleGrabber = new SampleGrabber() as ISampleGrabber;
                // Configure SampleGrabber. Add preview callback.
                ConfigureSampleGrabber(sampleGrabber);
                // Add SampleGrabber to graph.
                hr = GraphBuilder.AddFilter(sampleGrabber as IBaseFilter, "Frame Callback");
                DsError.ThrowExceptionForHR(hr);

                // Configure preview settings.
                SetConfigParams(CaptureGraphBuilder, sourceFilter, CaptureFPS, HorizontalResoltion, VerticalResoltion);

                // Render the preview pin on the video capture filter
                // Use this instead of this.graphBuilder.RenderFile
                hr = CaptureGraphBuilder.RenderStream(PinCategory.Preview, MediaType.Video, sourceFilter, (sampleGrabber as IBaseFilter), null);
                // Console.WriteLine("Render the preview pin on the video capture filter : " + DsError.GetErrorText(hr));
                DsError.ThrowExceptionForHR(hr);

                SaveSizeInfo(sampleGrabber);

                // Now that the filter has been added to the graph and we have
                // rendered its stream, we can release this reference to the filter.
                Marshal.ReleaseComObject(sourceFilter);

                // Set video window style and position
                SetupVideoWindow();

                // Add our graph to the running object table, which will allow
                // the GraphEdit application to "spy" on our graph
                rot = new DsROTEntry(GraphBuilder);

                // Start previewing video data
                hr = MediaControl.Run();
                // Console.WriteLine("Start previewing video data : " + DsError.GetErrorText(hr));
                DsError.ThrowExceptionForHR(hr);

                // Remember current state
                CurrentState = PlayState.Running;
                // Console.WriteLine("The currentstate : " + CurrentState.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unrecoverable error has occurred.With error : " + ex.ToString());
            }
            finally
            {
                if (sourceFilter != null)
                {
                    Marshal.ReleaseComObject(sourceFilter);
                }

                if (sampleGrabber != null)
                {
                    Marshal.ReleaseComObject(sampleGrabber);
                }
            }
        }
        /// <summary>Returns the 1st capture device</summary>
        IBaseFilter FindCaptureDevice()
        {
            //System.Collections.ArrayList devices;

            // Get all video input devices
            DsDevice[] devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            //devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            // Take the first device
            DsDevice device = devices[0];

            // Bind Moniker to a filter object
            Guid iid = typeof(IBaseFilter).GUID;
            device.Mon.BindToObject(null, null, ref iid, out object source);

            // An exception is thrown if cast fail
            return (IBaseFilter)source;
        }
        /// <summary>Gets references to intefaces</summary>
        void GetInterfaces()
        {

            // An exception is thrown if cast fail
            // GraphBuilder = (IGraphBuilder)new FilterGraph();
            GraphBuilder = (IFilterGraph2)new FilterGraph();
            CaptureGraphBuilder = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();
            MediaControl = (IMediaControl)GraphBuilder;
            VideoWindow = (IVideoWindow)GraphBuilder;
            MediaEventEx = (IMediaEventEx)GraphBuilder;

            // This method designates a window as the recipient of messages generated by or sent to the current DirectShow object
            int hr = MediaEventEx.SetNotifyWindow(Handle, WM_GRAPHNOTIFY, IntPtr.Zero);
            // ThrowExceptionForHR is a wrapper for Marshal.ThrowExceptionForHR, but additionally provides descriptions for any DirectShow specific error messages.
            //If the hr value is not a fatal error, no exception will be thrown:
            DsError.ThrowExceptionForHR(hr);
            // Console.WriteLine("I started Sub Get interfaces , the result is : " + DsError.GetErrorText(hr));
        }
        /// <summary> Called in the Designer.cs Dispose function</summary>
        public void Closeinterfaces()
        {
            // stop previewing data
            if (MediaControl != null)
                MediaControl.StopWhenReady();

            CurrentState = PlayState.Stopped;

            // stop recieving events
            if (MediaEventEx != null)
                MediaEventEx.SetNotifyWindow(IntPtr.Zero, WM_GRAPHNOTIFY, IntPtr.Zero);

            // Relinquish ownership (IMPORTANT!) of the video window.
            // Failing to call put_Owner can lead to assert failures within
            // the video renderer, as it still assumes that it has a valid
            // parent window.
            if (VideoWindow != null)
            {
                VideoWindow.put_Visible(OABool.False);
                VideoWindow.put_Owner(IntPtr.Zero);
            }

            // // Remove filter graph from the running object table
            if (rot != null)
            {
                rot.Dispose();
                rot = null;
            }

            // Release DirectShow interfaces
            if (MediaControl != null)
            {
                Marshal.ReleaseComObject(MediaControl);
                MediaControl = null;
            }
            if (MediaEventEx != null)
            {
                Marshal.ReleaseComObject(MediaEventEx);
                MediaEventEx = null;
            }
            if (VideoWindow != null)
            {
                Marshal.ReleaseComObject(VideoWindow);
                VideoWindow = null;
            }
            if (GraphBuilder != null)
            {
                Marshal.ReleaseComObject(GraphBuilder);
                GraphBuilder = null;
            }
            if (CaptureGraphBuilder != null)
            {
                Marshal.ReleaseComObject(CaptureGraphBuilder);
                CaptureGraphBuilder = null;
            }
        }

        /// <summary> Setup window</summary>
        public void SetupVideoWindow()
        {
            // set the video window to be a child of the main window
            // putowner : Sets the owning parent window for the video playback window. 
            int hr = VideoWindow.put_Owner(Handle);
            DsError.ThrowExceptionForHR(hr);

            hr = VideoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipChildren);
            DsError.ThrowExceptionForHR(hr);

            // Use helper function to position video window in client rect of main application window
            ResizeVideoWindow();

            // Make the video window visible, now that it is properly positioned
            // put_visible : This method changes the visibility of the video window. 
            hr = VideoWindow.put_Visible(OABool.True);
            DsError.ThrowExceptionForHR(hr);
        }
        /// <summary> Call ResizeVideoWindow</summary>
        private void WebCapture_Resize(object sender, EventArgs e)
        {
            //// Stop graph when Form is iconic
            //if (this.WindowState == FormWindowState.Minimized)
            //    ChangePreviewState(false);

            //// Restart Graph when window come back to normal state
            //if (this.WindowState == FormWindowState.Normal)
            //    ChangePreviewState(true);

            ResizeVideoWindow();
        }
        /// <summary> Resize video window to match owner window size</summary>
        public void ResizeVideoWindow()
        {
            // Resize the video preview window to match this user control size size.
            if (VideoWindow != null)
                VideoWindow.SetWindowPosition(0, 0, Width, ClientSize.Height);
        }
        /// <summary> Show or hide preview window </summary>
        public void ChangePreviewState(bool showVideo)
        {

            // If the media control interface isn't ready, don't call it
            if (MediaControl == null)
            {
                // Console.WriteLine("MediaControl is nothing");
                return;
            }

            if (showVideo == true)
            {
                if (CurrentState != PlayState.Running)
                {
                    // Start previewing video data
                    // Console.WriteLine("Start previewing video data");
                    _ = MediaControl.Run();
                    CurrentState = PlayState.Running;
                }
            }
            else
            {
                // Stop previewing video data
                // Console.WriteLine("Stop previewing video data");
                _ = MediaControl.StopWhenReady();
                CurrentState = PlayState.Stopped;
            }
        }
        /// <summary> Hangle Events</summary>
        public void HandleGraphEvent()
        {

            if (MediaEventEx == null)
                return;

            while (MediaEventEx.GetEvent(out EventCode evCode, out IntPtr evParam1, out IntPtr evParam2, 0) == 0)
            {
                // Free event parameters to prevent memory leaks associated with
                // event parameter data.  While this application is not interested
                // in the received events, applications should always process them.
                int hr = MediaEventEx.FreeEventParams(evCode, evParam1, evParam2);
                DsError.ThrowExceptionForHR(hr);

                // Insert event processing code here, if desired
            }
        }
        /// <summary> Config Sample Grabber so we can grab a image frame to work with </summary>
        private void ConfigureSampleGrabber(ISampleGrabber sampleGrabber)
        {
            AMMediaType media;
            int hr;

            // Set the media type to Video/RBG24
            media = new AMMediaType
            {
                majorType = MediaType.Video,
                subType = MediaSubType.RGB24,
                formatType = FormatType.VideoInfo
            };

            hr = sampleGrabber.SetMediaType(media);
            DsError.ThrowExceptionForHR(hr);

            DsUtils.FreeAMMediaType(media);
            hr = sampleGrabber.SetCallback(this, 1);
            DsError.ThrowExceptionForHR(hr);
        }
        /// <summary> Set Config </summary>
        private void SetConfigParams(ICaptureGraphBuilder2 capGraph, IBaseFilter capFilter, int iFrameRate, int iWidth, int iHeight)
        {
            // Find the stream config interface
            _ = capGraph.FindInterface(
                PinCategory.Capture, MediaType.Video, capFilter, typeof(IAMStreamConfig).GUID, out object config);


            if (!(config is IAMStreamConfig videoStreamConfig))
                throw new Exception("Failed to get IAMStreamConfig");

            // Get the existing format block
            int hr = videoStreamConfig.GetFormat(out AMMediaType mediaType);
            DsError.ThrowExceptionForHR(hr);

            // copy out the videoinfoheader
            VideoInfoHeader videoInfoHeader = new VideoInfoHeader();
            Marshal.PtrToStructure(mediaType.formatPtr, videoInfoHeader);

            // if overriding the framerate, set the frame rate
            if (iFrameRate > 0)
                videoInfoHeader.AvgTimePerFrame = 10000000 / iFrameRate;

            // if overriding the width, set the width
            if (iWidth > 0)
                videoInfoHeader.BmiHeader.Width = iWidth;

            // if overriding the Height, set the Height
            if (iHeight > 0)
                videoInfoHeader.BmiHeader.Height = iHeight;

            // Copy the media structure back
            Marshal.StructureToPtr(videoInfoHeader, mediaType.formatPtr, false);

            // Set the new format
            hr = videoStreamConfig.SetFormat(mediaType);
            DsError.ThrowExceptionForHR(hr);

            DsUtils.FreeAMMediaType(mediaType);
        }
        /// <summary> Save Size Info</summary>
        private void SaveSizeInfo(ISampleGrabber sampleGrabber)
        {
            // Get the media type from the SampleGrabber
            AMMediaType media = new AMMediaType();
            int hr = sampleGrabber.GetConnectedMediaType(media);
            DsError.ThrowExceptionForHR(hr);

            if ((media.formatType != FormatType.VideoInfo) || (media.formatPtr == IntPtr.Zero))
                throw new NotSupportedException("Unknown Grabber Media Format");

            // Grab the size info
            _ = (VideoInfoHeader)Marshal.PtrToStructure(media.formatPtr, typeof(VideoInfoHeader));
            // VideoInfoHeader videoInfoHeader = (VideoInfoHeader)Marshal.PtrToStructure(media.formatPtr, typeof(VideoInfoHeader));
            //_previewStride = HorizontalResoltion * (videoInfoHeader.BmiHeader.BitCount / 8);

            DsUtils.FreeAMMediaType(media);
        }
        #endregion

        #endregion
    }
}
