using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MonoGameInAvalonia.Controls
{
    internal partial class Sdl
    {
        private const string LibraryName = "SDL2";

        public enum SysWMType
        {
            Unknown,
            Windows,
            X11,
            DirectFB,
            Cocoa,
            UIKit,
            Wayland,
            Mir,
            WinRT,
            Android,
            Vivante,
            OS2,
            Haiku,
            Kmsdrm
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Version
        {
            public byte Major;
            public byte Minor;
            public byte Patch;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WindowsWMInfo
        {
            public IntPtr Window; // Refers to an HWND
            public IntPtr Hdc; // Refers to an HDC
            public IntPtr HInstance; // Refers to an HINSTANCE
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct X11WMInfo
        {
            public IntPtr Display; // Refers to a Display*
            public IntPtr Window; // Refers to a Window (XID, use ToInt64!)
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CocoaWMInfo
        {
            public IntPtr Window; // Refers to an NSWindow*
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct SysWMDriverUnion
        {
            [FieldOffset(0)]
            public WindowsWMInfo Windows;
            [FieldOffset(0)]
            public X11WMInfo X11;
            [FieldOffset(0)]
            public CocoaWMInfo Cocoa;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SysWMinfo
        {
            public Version Version;
            public SysWMType Subsystem;
            public SysWMDriverUnion Info;
        }

        [LibraryImport(LibraryName, EntryPoint = "SDL_GetWindowWMInfo")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool GetWindowWMInfo(IntPtr window, out SysWMinfo info);

        [LibraryImport(LibraryName, EntryPoint = "SDL_GetError", StringMarshalling = StringMarshalling.Utf8)]
        private static partial IntPtr GetErrorInternal();

        public static string? GetError()
        {
            IntPtr error = GetErrorInternal();
            return Marshal.PtrToStringUTF8(error);
        }

        private static void SetCustomResolver()
        {
            NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), (string libraryName, Assembly assembly, DllImportSearchPath? searchPath) =>
            {
                if (libraryName == "SDL")
                {
                    if (OperatingSystem.IsLinux())
                        return NativeLibrary.Load("libSDL2-2.0.so.0", assembly, searchPath);
                }
                return NativeLibrary.Load(libraryName, assembly, searchPath);
            });
        }

        static Sdl() => SetCustomResolver();
    }
}
