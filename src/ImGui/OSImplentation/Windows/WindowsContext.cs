﻿using ImGui.Input;
using ImGui.OSAbstraction;
using ImGui.OSAbstraction.Graphics;
using ImGui.OSAbstraction.Text;
using ImGui.OSAbstraction.Window;

namespace ImGui.OSImplentation.Windows
{
    class WindowsContext : PlatformContext
    {
        public static PlatformContext MapFactory()
        {
            return new WindowsContext
            {
                CreateTextContext = CTextContext,
                CreateWindow = CWindow,
                ChangeCursor = DoChangeCursor,
                CreateRenderer = CRenderer,
                CreateTexture = CTexture,
            };
        }

        internal static ITextContext CTextContext(
            string text, string fontFamily, int fontSize,
            FontStretch stretch, FontStyle style, FontWeight weight,
            int maxWidth, int maxHeight,
            TextAlignment alignment)
        {
            return new TypographyTextContext(
                text, fontFamily, fontSize,
                stretch, style, weight,
                maxWidth, maxHeight, alignment);
        }

        private static IWindow CWindow(Point position, Size size, WindowTypes windowType)
        {
            Win32Window window = new Win32Window();
            window.Init(position, size, windowType);
            return window;
        }

        private static void DoChangeCursor(Cursor cursor)
        {
            Win32Cursor.ChangeCursor(cursor);
        }

        private static IRenderer CRenderer()
        {
            return new Win32OpenGLRenderer();
        }

        private static ITexture CTexture()
        {
            return new OpenGLTexture();
        }
    }
}