using System;
using System.Reactive.Linq;

using Microsoft.MixedReality.WebRTC;

namespace EventDelegateWrappers
{

    public class WebRTCFrameEventArgs : EventArgs
    {   public byte[] frameBuf = new byte[0];
        public WebRTCFrameEventArgs(I420AVideoFrame frame)
        {
            var width = frame.width;
            var height = frame.height;
            var strideY = frame.strideY;
            var strideU = frame.strideU;
            var strideV = frame.strideV;
            var strideA = frame.strideA;
            frame.CopyTo(frameBuf);
        }
    }

    public static class FrameHelper
    {
        public static IObservable<WebRTCFrameEventArgs>(PeerConnection pc)
        {
            return Observable.FromEvent<>
        }
            
    }

}
