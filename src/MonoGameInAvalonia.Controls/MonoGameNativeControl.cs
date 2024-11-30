using Avalonia.Controls;
using Avalonia.Platform;
using Microsoft.Xna.Framework;
using System;
using static SDL2.SDL;

namespace MonoGameInAvalonia.Controls;

internal class MonoGameNativeControl : NativeControlHost
{
    private Game _game;

    public MonoGameNativeControl(Game game) => _game = game;

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        if (!GetSystemWindowInfo(_game.Window.Handle, out SDL_SysWMinfo info))
            throw new Exception("Failed to retrive window manager information.");

        if (OperatingSystem.IsWindows())
            return new PlatformHandle(info.info.win.window, "HWND");
        else if (OperatingSystem.IsLinux())
            return new PlatformHandle(info.info.x11.window, "XID");
        else if (OperatingSystem.IsMacOS())
            return new PlatformHandle(info.info.cocoa.window, "NSView");

        throw new NotSupportedException("Platform is not supported");
    }

    private static bool GetSystemWindowInfo(nint windowHandle, out SDL_SysWMinfo info)
    {
        info = new();
        SDL_GetVersion(out SDL_version version);
        info.version = version;

        SDL_bool result = SDL_GetWindowWMInfo(windowHandle, ref info);
        return result == SDL_bool.SDL_TRUE;
    }
}
