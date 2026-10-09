using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Commons.Native
{
    public static class NativeUtil
    {
        public const byte NULL = 0;
        
        public static int strlen(byte[] aBytes)
        {
            var index = 0;
            while (aBytes[index] != 0) index++;
            return index;
        }
        
        public static int strlen(string szString)
        {
            return szString.Length;
        }
        
        public static int strcmp(byte[] aSourceBytes, byte[] aCompareBytes)
        {
            for (var i = 0; i < aCompareBytes.Length; i++)
            {
                if (aSourceBytes.Length == i) return 0 - aCompareBytes[i];
                
                if (aSourceBytes[i] == 0 ||
                    aSourceBytes[i] != aCompareBytes[i])
                {
                    return aSourceBytes[i] - aCompareBytes[i];
                }
            }

            return 0;
        }

        public static int strcmp(byte[] aSourceBytes, byte aCompareByte)
        {
            return strcmp(aSourceBytes, new[] {aCompareByte});
        }
        
        public static int strcmp(byte[] aSourceBytes, string szCompareString)
        {
            return strcmp(aSourceBytes, Encoding.Default.GetBytes(szCompareString));
        }
        
        public static int strcmp(string szToBeComparedString, string szCompareString)
        {
            return strcmp(Encoding.Default.GetBytes(szToBeComparedString), Encoding.Default.GetBytes(szCompareString));
        }
        
        public static string BArrToStr(byte[] aBytes)
        {
            aBytes[aBytes.Length - 1] = 0;
            return Global.Encoding.GetString(aBytes, 0, strlen(aBytes));
        }
        
        public static void strncpy(ref byte[] bytes, string source, int length)
        {
            bytes = new byte[length];
            if (string.IsNullOrEmpty(source) || length < 2) return;

            // The old char-count clamp overflowed the field: GB2312 names need 2 bytes per char.
            // flush:false makes the encoder leave a half-written double-byte char out.
            var aChars = source.ToCharArray(0, Math.Min(source.Length, length - 1));
            var aEncoded = new byte[length - 1];
            Global.Encoding.GetEncoder().Convert(aChars, 0, aChars.Length, aEncoded, 0, aEncoded.Length,
                false, out _, out var nBytesUsed, out _);

            Array.Copy(aEncoded, 0, bytes, 0, nBytesUsed);
        }

        public static unsafe void strncpy(byte* ptr, ref string source, int length)
        {
            if (string.IsNullOrEmpty(source))
            {
                *ptr = 0;
                return;
            }
            
            Marshal.Copy(Global.Encoding.GetBytes(source), 0, (IntPtr)ptr, source.Length > length ? length : source.Length);
            while (--length >= source.Length) *(ptr + length) = 0;
        }
        
        public static void strcpy(IntPtr ptr, ref string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                Marshal.WriteByte(ptr, source.Length, 0);
                return;
            }
            
            Marshal.Copy(Global.Encoding.GetBytes(source), 0, ptr, source.Length);
            Marshal.WriteByte(ptr, source.Length, 0);
        }
        
        public static unsafe void strcpy(byte* ptr, ref string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                *ptr = 0;
                return;
            }

            var sourceBytes = Global.Encoding.GetBytes(source);
            Marshal.Copy(sourceBytes, 0, (IntPtr)ptr, sourceBytes.Length);
            *(ptr + sourceBytes.Length) = 0;
        }
        
        public static unsafe void memset(byte* buffer, byte value, int size)
        {
            if (size == 0) return;
            while (size-- > 0)
            {
                buffer[size] = value;
            }
        }
        
        public static unsafe void memset(IntPtr buffer, byte value, int size)
        {
            if (size == 0) return;
            while (size-- > 0)
            {
                *(byte*)(buffer + size) = value;
            }
        }

        public static int GetTimestamp()
        {
            return (int) ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
        }
        
        public static long time64()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

        public static int GetTickCount()
        {
            return Environment.TickCount & int.MaxValue;
        }

        public static bool IsUnix()
        {
            return Environment.OSVersion.Platform == PlatformID.Unix;
        }

        public static uint inet_addr(string szAddr)
        {
            if (string.IsNullOrEmpty(szAddr)) return 0;
            var szSplitAddr = szAddr.Split('.');
            return (uint) (Convert.ToByte(szSplitAddr[3]) << 24 | Convert.ToByte(szSplitAddr[2]) << 16 |
                           Convert.ToByte(szSplitAddr[1]) << 8 | Convert.ToByte(szSplitAddr[0]));
        }

        public static unsafe string inet_ntoa(uint dwAddr)
        {
            var uiPtr = &dwAddr;
            return *(byte*) uiPtr + "." + *((byte*)uiPtr + 1) + "." + *((byte*)uiPtr + 2) + "." +
                   *((byte*)uiPtr + 3);
        }
        
        public static int DTToUnixTimestamp(DateTime target)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            var unixTimestamp = Convert.ToInt32((target - date).TotalSeconds);

            return unixTimestamp;
        }

        public static byte[] StructToByteArray<TClass>(TClass objData)
        {
            var structSize = Marshal.SizeOf(typeof(TClass));
            var buffer = new byte[structSize];
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            Marshal.StructureToPtr(objData, handle.AddrOfPinnedObject(), false);
            handle.Free();

            return buffer;
        }
    }
}