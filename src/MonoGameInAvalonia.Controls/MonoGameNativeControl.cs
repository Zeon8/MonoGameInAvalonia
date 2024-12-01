﻿using Avalonia.Controls;
using Avalonia.Platform;
using Microsoft.Xna.Framework;
using System;

namespace MonoGameInAvalonia.Controls;

internal class MonoGameNativeControl : NativeControlHost
{
    private Game _game;

    public MonoGameNativeControl(Game game) => _game = game;

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        if (!SDL.GetWindowWMInfo(_game.Window.Handle, out SDL.SysWMinfo info))
            throw new Exception($"Failed to retrive window manager information. Reason: {SDL.GetError()}");

        if (OperatingSystem.IsWindows())
            return new PlatformHandle(info.Info.Win.Window, "HWND");
        else if (OperatingSystem.IsLinux())
            return new PlatformHandle(info.Info.X11.Window, "XID");
        else if (OperatingSystem.IsMacOS())
            return new PlatformHandle(info.Info.Cocoa.Window, "NSView");

        throw new NotSupportedException("Platform is not supported.");
    }
}
