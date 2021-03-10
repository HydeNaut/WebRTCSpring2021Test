using System;
using System.Reactive.Linq;

using Microsoft.MixedReality.WebRTC;

namespace EventDelegateWrappers
{

    public class WebRTCFrameEventArgs : EventArgs
    {
        public byte[] frameBuf = new byte[0];
        public uint Width;
        public uint Height;
        public int StrideY;
        public int StrideU;
        public int StrideV;
        public int StrideA;
        public WebRTCFrameEventArgs(I420AVideoFrame frame)
        {
           Width = frame.width;
           Height = frame.height;
           StrideY = frame.strideY;
           StrideU = frame.strideU;
           StrideV = frame.strideV;
           StrideA = frame.strideA;
           frame.CopyTo(frameBuf);
        }
    }

    public class FrameRequestEventArgs : EventArgs
    {
        public ExternalVideoTrackSource Source;
        //
        // Summary:
        //     Unique request identifier, for error checking.
        public uint RequestId;
        //
        // Summary:
        //     Frame timestamp, in milliseconds. This corresponds to the time when the request
        //     was made to the native video track source.
        public long TimestampMs;

        public FrameRequestEventArgs(FrameRequest fr)
        {
            Source = fr.Source;
            RequestId = fr.RequestId;
            TimestampMs = fr.TimestampMs;
        }
    }

    public static class FrameHelper
    {

        public static IObservable<WebRTCFrameEventArgs> VideoFrameReady(LocalVideoTrack vidtrack)
        {
            return Observable.FromEvent<I420AVideoFrameDelegate, WebRTCFrameEventArgs>(handler =>
            {
                I420AVideoFrameDelegate vfReadyHandler = (frame) =>
                {
                    handler(new WebRTCFrameEventArgs(frame));
                };

                return vfReadyHandler;
            },
                    vfReadyHandler => vidtrack.I420AVideoFrameReady += vfReadyHandler,
                    vfReadyHandler => vidtrack.I420AVideoFrameReady -= vfReadyHandler);
        }

    }


}
