using HidLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace HidLibrary
{
    public static class HidLibraryExt
    {
        public static Version newVersion(int version)
        {
            var major = (int)((version & 0xFF000000) >> 6);
            var minor = (version & 0x00FF0000) >> 4;
            var build = (version & 0x0000FF00) >> 2;
            var revision = version & 0x000000FF;
            return new Version(major, minor, build, revision);
        }

        public static IEnumerable<HidDevice> Enumerate(int vendorId, int productId, int version)
        {
            return HidDevices.Enumerate(vendorId, productId)
                .Where(x => {
                    if (x.Attributes.VendorId != vendorId)
                    {
                        return false;
                    }
                    if (x.Attributes.ProductId != productId)
                    {
                        return false;
                    }
                    return newVersion(x.Attributes.Version) >= newVersion(version);
                });
        }
    }
}