using DirectShowLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebCam
{
    public partial class WebCapture : UserControl, ISampleGrabberCB
    {
        static Procesor procesor;
        //private volatile bool isFinished = true; //Could work on one frame and let others pass...

        #region Properties Box
        [Category("DirectShow"),
         Description("The width of the image captured by Direct Show")]
        public int CaptureWidth { get; set; } = 320;

        [Category("DirectShow"),
        Description("The height of the image captured by Direct Show")]
        public int CaptureHeight { get; set; } = 240;

        [Category("DirectShow"),
        Description("The frames per second that image is captured by Direct Show")]
        public int CaptureFPS { get; set; } = 30;

        [Category("DirectShow"),
        Description("Size of the tracker marker to be generated and searched for")]
        public int TrackerMarkerSize { get; set; } = 8;

        [Category("DirectShow"),
        Description("Shows the marker being tracked and all possible matches")]
        public bool ShowTrackerDebug { get; set; } = false;

        [Category("DirectShow"),
        Description("Minimum match value that will be considered matching the tracker")]
        public float TrackerMinimumMatch { get; set; } = 0.3f;
        #endregion

        #region Marker Locations
        [Browsable(false)]
        public Point TopMarker
        { get {return procesor.tracker.MarkerLocations[0]; } }

        [Browsable(false)]
        public Point BottomMarker
        { get { return procesor.tracker.MarkerLocations[1]; } }
        #endregion

        #region Head Tracking Calibration
        public Calibration calibration = new Calibration();

        /// <summary>Just a simple way to do calibration</summary>
        public void DoCalibration()
        {
            if (MessageBox.Show("Look straight ahead at your screen.\n Click 'OK' to capture orientation.", "Calibration Step 1", MessageBoxButtons.OK) == DialogResult.OK)
                calibration.SetCenter(TopMarker, BottomMarker);

            if (MessageBox.Show("Look as far left as you normally would in game.\n Make sure trackers are still being read. \nClick 'OK' to capture orientation.", "Calibration Step 2", MessageBoxButtons.OK) == DialogResult.OK)
                calibration.SetMaxLeftRotation(TopMarker, BottomMarker);

            if (MessageBox.Show("Look as far right as you normally would in game.\n Make sure trackers are still being read. \nClick 'OK' to capture orientation.", "Calibration Step 3", MessageBoxButtons.OK) == DialogResult.OK)
                calibration.SetMaxRightRotation(TopMarker, BottomMarker);

            if (MessageBox.Show("Look as far up as you normally would in game.\n Make sure both trackers are still being read. \nClick 'OK' to capture orientation.", "Calibration Step 4", MessageBoxButtons.OK) == DialogResult.OK)
                calibration.SetMaxUpRotation(TopMarker, BottomMarker);

            if (MessageBox.Show("Look as far down as you normally would in game.\n Make sure both trackers are still being read. \nClick 'OK' to capture orientation.", "Calibration Step 5", MessageBoxButtons.OK) == DialogResult.OK)
                calibration.SetMaxDownRotation(TopMarker, BottomMarker);

            if (MessageBox.Show("Would you like to enable auto updating calibration?\n If you rotate more than the calibration max a new max will be set.", "Calibration Step 6", MessageBoxButtons.YesNo) == DialogResult.Yes)
                calibration.EnableContinualCalibration = true;
            else
                calibration.EnableContinualCalibration = false;

            Console.WriteLine(calibration.boundingBox.left + " " + calibration.boundingBox.right + " " + calibration.boundingBox.top + " " + calibration.boundingBox.bottom);
            int padding = 100;
            procesor.tracker.boundingBox.left = calibration.boundingBox.left + padding;
            procesor.tracker.boundingBox.right = calibration.boundingBox.right - padding;
            procesor.tracker.boundingBox.top = calibration.boundingBox.top + padding;
            procesor.tracker.boundingBox.bottom = calibration.boundingBox.bottom - padding;

            #region Fix off screen bounding box
            if (procesor.tracker.boundingBox.right < 0)
                procesor.tracker.boundingBox.right = 0;
            if (procesor.tracker.boundingBox.bottom < 0)
                procesor.tracker.boundingBox.bottom = 0;
            if (procesor.tracker.boundingBox.left > CaptureWidth)
                procesor.tracker.boundingBox.left = CaptureWidth;
            if (procesor.tracker.boundingBox.top > CaptureHeight)
                procesor.tracker.boundingBox.top = CaptureHeight;
            #endregion 
        }
        #endregion 

        #region Direct Show Declerations
        int _previewStride = 0;

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
        const int WM_NCHITTEST = 0x0084;
        const int HTTRANSPARENT = -1;

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
            procesor = new Procesor(CaptureWidth, CaptureHeight, TrackerMarkerSize);
            procesor.ShowDebug = ShowTrackerDebug;
            procesor.tracker.minimumPositive = TrackerMinimumMatch;
            procesor.tracker.markerSize = TrackerMarkerSize;

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
            int hr = 0;
            IBaseFilter sourceFilter = null;
            ISampleGrabber sampleGrabber = null;

            try
            {
                // Get DirectShow interfaces
                GetInterfaces();

                // Attach the filter graph to the capture graph
                hr = CaptureGraphBuilder.SetFiltergraph(GraphBuilder);
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
                SetConfigParams(CaptureGraphBuilder, sourceFilter, CaptureFPS, CaptureWidth, CaptureHeight);

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
                    sourceFilter = null;
                }

                if (sampleGrabber != null)
                {
                    Marshal.ReleaseComObject(sampleGrabber);
                    sampleGrabber = null;
                }
            }
        }
        /// <summary>Returns the 1st capture device</summary>
        IBaseFilter FindCaptureDevice()
        {
            //System.Collections.ArrayList devices;
            object source;

            // Get all video input devices
            DsDevice[] devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            //devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            // Take the first device
            DsDevice device = devices[0];

            // Bind Moniker to a filter object
            Guid iid = typeof(IBaseFilter).GUID;
            device.Mon.BindToObject(null, null, ref iid, out source);

            // An exception is thrown if cast fail
            return (IBaseFilter)source;
        }
        /// <summary>Gets references to intefaces</summary>
        void GetInterfaces()
        {
            int hr = 0;

            // An exception is thrown if cast fail
            // GraphBuilder = (IGraphBuilder)new FilterGraph();
            GraphBuilder = (IFilterGraph2)new FilterGraph();
            CaptureGraphBuilder = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();
            MediaControl = (IMediaControl)GraphBuilder;
            VideoWindow = (IVideoWindow)GraphBuilder;
            MediaEventEx = (IMediaEventEx)GraphBuilder;

            // This method designates a window as the recipient of messages generated by or sent to the current DirectShow object
            hr = MediaEventEx.SetNotifyWindow(Handle, WM_GRAPHNOTIFY, IntPtr.Zero);
            // ThrowExceptionForHR is a wrapper for Marshal.ThrowExceptionForHR, but additionally provides descriptions for any DirectShow specific error messages.
            //If the hr value is not a fatal error, no exception will be thrown:
            DsError.ThrowExceptionForHR(hr);
            // Console.WriteLine("I started Sub Get interfaces , the result is : " + DsError.GetErrorText(hr));
        }
        /// <summary> Called in the Designer.cs Dispose function</summary>
        public void Closeinterfaces()
        {
            try
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
                Marshal.ReleaseComObject(MediaControl);
                MediaControl = null;
                Marshal.ReleaseComObject(MediaEventEx);
                MediaEventEx = null;
                Marshal.ReleaseComObject(VideoWindow);
                VideoWindow = null;
                Marshal.ReleaseComObject(GraphBuilder);
                GraphBuilder = null;
                Marshal.ReleaseComObject(CaptureGraphBuilder);
                CaptureGraphBuilder = null;
            }
            catch (Exception ex)
            {
                //todo figure out exception on close
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary> Setup window</summary>
        public void SetupVideoWindow()
        {
            int hr = 0;
            // set the video window to be a child of the main window
            // putowner : Sets the owning parent window for the video playback window. 
            hr = VideoWindow.put_Owner(Handle);
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
            // Resize the video preview window to match owner window size. left , top , width , height
            if (VideoWindow != null)
                VideoWindow.SetWindowPosition(0, 0, Width, ClientSize.Height);
        }
        /// <summary> Show or hide preview window </summary>
        public void ChangePreviewState(bool showVideo)
        {
            int hr = 0;

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
                    hr = MediaControl.Run();
                    CurrentState = PlayState.Running;
                }
            }
            else
            {
                // Stop previewing video data
                // Console.WriteLine("Stop previewing video data");
                hr = MediaControl.StopWhenReady();
                CurrentState = PlayState.Stopped;
            }
        }
        /// <summary> Hangle Events</summary>
        public void HandleGraphEvent()
        {
            int hr = 0;
            EventCode evCode;
            IntPtr evParam1;
            IntPtr evParam2;

            if (MediaEventEx == null)
                return;

            while (MediaEventEx.GetEvent(out evCode, out evParam1, out evParam2, 0) == 0)
            {
                // Free event parameters to prevent memory leaks associated with
                // event parameter data.  While this application is not interested
                // in the received events, applications should always process them.
                hr = MediaEventEx.FreeEventParams(evCode, evParam1, evParam2);
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
            media = new AMMediaType();
            media.majorType = MediaType.Video;
            media.subType = MediaSubType.RGB24;
            media.formatType = FormatType.VideoInfo;

            hr = sampleGrabber.SetMediaType(media);
            DsError.ThrowExceptionForHR(hr);

            DsUtils.FreeAMMediaType(media);
            media = null;

            hr = sampleGrabber.SetCallback(this, 1);
            DsError.ThrowExceptionForHR(hr);
        }
        /// <summary> Set Config </summary>
        private void SetConfigParams(ICaptureGraphBuilder2 capGraph, IBaseFilter capFilter, int iFrameRate, int iWidth, int iHeight)
        {
            int hr;
            object config;
            AMMediaType mediaType;
            // Find the stream config interface
            hr = capGraph.FindInterface(
                PinCategory.Capture, MediaType.Video, capFilter, typeof(IAMStreamConfig).GUID, out config);

            IAMStreamConfig videoStreamConfig = config as IAMStreamConfig;

            if (videoStreamConfig == null)
                throw new Exception("Failed to get IAMStreamConfig");

            // Get the existing format block
            hr = videoStreamConfig.GetFormat(out mediaType);
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
            mediaType = null;
        }
        /// <summary> Save Size Info</summary>
        private void SaveSizeInfo(ISampleGrabber sampleGrabber)
        {
            int hr;

            // Get the media type from the SampleGrabber
            AMMediaType media = new AMMediaType();
            hr = sampleGrabber.GetConnectedMediaType(media);
            DsError.ThrowExceptionForHR(hr);

            if ((media.formatType != FormatType.VideoInfo) || (media.formatPtr == IntPtr.Zero))
                throw new NotSupportedException("Unknown Grabber Media Format");

            // Grab the size info
            VideoInfoHeader videoInfoHeader = (VideoInfoHeader)Marshal.PtrToStructure(media.formatPtr, typeof(VideoInfoHeader));
            _previewStride = CaptureWidth * (videoInfoHeader.BmiHeader.BitCount / 8);

            DsUtils.FreeAMMediaType(media);
            media = null;
        }
        #endregion
        
        #endregion
    }
}
