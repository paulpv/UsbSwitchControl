using HidLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    public class HidLibraryExt
    {
        public class HidDeviceId
        {
            public int VendorId { get; private set; }
            public int ProductId { get; private set; }
            public Version Version { get; private set; }

            public HidDeviceId(int vendorId, int productId, Version version)
            {
                VendorId = vendorId;
                ProductId = productId;
                Version = version;
            }
        }

        public static Version newVersion(int version)
        {
            var major = (int)((version & 0xFF000000) >> 24);
            var minor = (version & 0x00FF0000) >> 16;
            var build = (version & 0x0000FF00) >> 8;
            var revision = version & 0x000000FF;
            return new Version(major, minor, build, revision);
        }

        public static IEnumerable<HidDevice> Enumerate(HidDeviceId hidDeviceId, bool includeGreaterThan = false)
        {
            return HidDevices.Enumerate(hidDeviceId.VendorId, hidDeviceId.ProductId)
                .Where(x => {
                    var xAttributesVersion = newVersion(x.Attributes.Version);
                    if (includeGreaterThan)
                    {
                        return xAttributesVersion >= hidDeviceId.Version;
                    }
                    else
                    {
                        return xAttributesVersion == hidDeviceId.Version;
                    }
                });
        }
    }
}