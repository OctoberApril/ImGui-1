﻿using System;
using System.Runtime.CompilerServices;
using ImGui.GraphicsAbstraction;
using ImGui.GraphicsImplementation;
using ImGui.OSAbstraction.Text;
using ImGui.Rendering;
using Xunit;

namespace ImGui.UnitTest.Rendering
{
    public class BuiltinPrimitiveRendererFacts
    {
        private const string RootDir = @"GraphicsImplementation\Builtin\images\BuiltinPrimitiveRendererFacts\";

        public class DrawLine
        {
            internal static void CheckLine(Pen pen, Point point0, Point point1,
                int width, int height,
                [CallerMemberName] string methodName = "unknown")
            {
                Application.EnableMSAA = false;

                MeshBuffer meshBuffer = new MeshBuffer();
                MeshList meshList = new MeshList();
                BuiltinGeometryRenderer renderer = new BuiltinGeometryRenderer();
                byte[] bytes;

                using (var context = new RenderContextForTest(width, height))
                {
                    renderer.OnBeforeRead();
                    renderer.DrawLine(pen, point0, point1);//This must be called after the RenderContextForTest is created, for uploading textures to GPU via OpenGL.
                    renderer.OnAfterRead(meshList);

                    //rebuild mesh buffer
                    meshBuffer.Clear();
                    meshBuffer.Init();
                    meshBuffer.Build(meshList);

                    //draw mesh buffer to screen
                    context.Clear();
                    context.DrawMeshes(meshBuffer);

                    bytes = context.GetRenderedRawBytes();
                }

                Util.CheckExpectedImage(bytes, width, height, $"{RootDir}{nameof(DrawLine)}\\{methodName}.png");
            }

            [Fact]
            public void DrawALine()
            {
                Pen pen = new Pen(Color.Black, 1);
                Point p0 = new Point(10, 10);
                Point p1 = new Point(90, 10);

                CheckLine(pen, p0, p1, 100, 100);
            }

            [Fact]
            public void DrawAThickLine()
            {
                Pen pen = new Pen(Color.Black, 5);
                Point p0 = new Point(10, 10);
                Point p1 = new Point(90, 10);

                CheckLine(pen, p0, p1, 100, 100);
            }
        }

        public class DrawRectangle
        {
            internal static void CheckRectangle(Brush brush, Pen pen, Rect rectangle,
                int width, int height,
                [CallerMemberName] string methodName = "unknown")
            {
                Application.EnableMSAA = false;

                MeshBuffer meshBuffer = new MeshBuffer();
                MeshList meshList = new MeshList();
                BuiltinGeometryRenderer renderer = new BuiltinGeometryRenderer();
                byte[] bytes;

                using (var context = new RenderContextForTest(width, height))
                {
                    renderer.OnBeforeRead();
                    renderer.DrawRectangle(brush, pen, rectangle);//This must be called after the RenderContextForTest is created, for uploading textures to GPU via OpenGL.
                    renderer.OnAfterRead(meshList);

                    //rebuild mesh buffer
                    meshBuffer.Clear();
                    meshBuffer.Init();
                    meshBuffer.Build(meshList);

                    //draw mesh buffer to screen
                    context.Clear();
                    context.DrawMeshes(meshBuffer);

                    bytes = context.GetRenderedRawBytes();
                }

                Util.CheckExpectedImage(bytes, width, height, $"{RootDir}{nameof(DrawRectangle)}\\{methodName}.png");
            }

            [Fact]
            public void DrawARectangle()
            {
                Brush brush = new Brush(Color.Aqua);
                Pen pen = new Pen(Color.Black, 4);
                Rect rectangle = new Rect(new Point(20, 20), new Point(80, 80));

                CheckRectangle(brush, pen, rectangle, 100, 100);
            }
        }

        public class DrawRoundedRectangle
        {
            internal static void Check(Rect rectangle, double radiusX, double radiusY, Brush brush, Pen pen, int width, int height,
                [CallerMemberName] string methodName = "unknown")
            {
                Application.EnableMSAA = false;

                MeshBuffer meshBuffer = new MeshBuffer();
                MeshList meshList = new MeshList();
                BuiltinGeometryRenderer renderer = new BuiltinGeometryRenderer();
                byte[] bytes;

                using (var context = new RenderContextForTest(width, height))
                {
                    renderer.OnBeforeRead();
                    renderer.DrawRoundedRectangle(brush, pen, rectangle, radiusX, radiusY);//This must be called after the RenderContextForTest is created, for uploading textures to GPU via OpenGL.
                    renderer.OnAfterRead(meshList);

                    //rebuild mesh buffer
                    meshBuffer.Clear();
                    meshBuffer.Init();
                    meshBuffer.Build(meshList);

                    //draw mesh buffer to screen
                    context.Clear();
                    context.DrawMeshes(meshBuffer);

                    bytes = context.GetRenderedRawBytes();
                }

                Util.CheckExpectedImage(bytes, width, height, $"{RootDir}{nameof(DrawRoundedRectangle)}\\{methodName}.png");
            }

            [Fact]
            public void DrawARectangle()
            {
                Brush brush = new Brush(Color.Aqua);
                Pen pen = new Pen(Color.Black, 1);
                Rect rectangle = new Rect(new Point(20, 20), new Point(160, 160));

                Check(rectangle, 20, 40, brush, pen, 200, 200);
            }
        }

        public class DrawEllipse
        {
            //TODO
        }

        public class DrawGeometry
        {
            internal static void CheckGeometry(Geometry geometry, Brush brush, Pen pen, int width, int height,
                [CallerMemberName] string methodName = "unknown")
            {
                Application.EnableMSAA = false;

                MeshBuffer meshBuffer = new MeshBuffer();
                MeshList meshList = new MeshList();
                BuiltinGeometryRenderer renderer = new BuiltinGeometryRenderer();
                byte[] bytes;

                using (var context = new RenderContextForTest(width, height))
                {
                    renderer.OnBeforeRead();
                    renderer.DrawGeometry(brush, pen, geometry);//This must be called after the RenderContextForTest is created, for uploading textures to GPU via OpenGL.
                    renderer.OnAfterRead(meshList);

                    //rebuild mesh buffer
                    meshBuffer.Clear();
                    meshBuffer.Init();
                    meshBuffer.Build(meshList);

                    //draw mesh buffer to screen
                    context.Clear();
                    context.DrawMeshes(meshBuffer);

                    bytes = context.GetRenderedRawBytes();
                }

                Util.CheckExpectedImage(bytes, width, height, $"{RootDir}{nameof(DrawGeometry)}\\{methodName}.png");
            }

            [Fact]
            public void StrokeAPathGeometry()
            {
                var geometry = new PathGeometry();
                var figure = new PathFigure();
                geometry.Figures.Add(figure);
                figure.StartPoint = new Point(10, 10);
                figure.Segments.Add(new LineSegment(new Point(10, 10), true));
                figure.Segments.Add(new LineSegment(new Point(10, 80), true));
                figure.Segments.Add(new LineSegment(new Point(80, 80), true));
                figure.Segments.Add(new LineSegment(new Point(80, 10), true));
                figure.Segments.Add(new LineSegment(new Point(10, 10), true));
                figure.IsClosed = true;
                Pen pen = new Pen(Color.Red, 2);
                CheckGeometry(geometry, null, pen, 100, 100);
            }

            [Fact]
            public void FillAPathGeometry()
            {
                var geometry = new PathGeometry();
                var figure = new PathFigure();
                figure.IsFilled = true;
                geometry.Figures.Add(figure);
                figure.StartPoint = new Point(10, 10);
                figure.Segments.Add(new LineSegment(new Point(10, 10), false));
                figure.Segments.Add(new LineSegment(new Point(10, 80), false));
                figure.Segments.Add(new LineSegment(new Point(80, 80), false));
                figure.Segments.Add(new LineSegment(new Point(80, 10), false));
                figure.Segments.Add(new LineSegment(new Point(10, 10), false));
                figure.IsClosed = true;
                Brush brush = new Brush(Color.Red);
                CheckGeometry(geometry, brush, null, 100, 100);
            }

            [Fact]
            public void StrokePolyLine()
            {
                var geometry = new PathGeometry();
                PathFigure figure = new PathFigure();
                figure.StartPoint = new Point(97.000 ,127.000);
                figure.Segments.Add(new PolyLineSegment(new[]
                {
                    new Point(132.267, 145.541),
                    new Point(125.532, 106.271),
                    new Point(154.063, 78.459),
                    new Point(114.634, 72.729),
                    new Point(97.000 ,37.000),
                    new Point(79.366 ,72.729),
                    new Point(39.937 ,78.459),
                    new Point(68.468 ,106.271),
                    new Point(61.733 ,145.541),
                    new Point(97.000 ,127.000),
                }, true));
                figure.IsClosed = true;
                figure.IsFilled = false;
                geometry.Figures.Add(figure);
                Pen pen = new Pen(Color.Black, 1);
                CheckGeometry(geometry, null, pen, 194, 194);
            }

            [Fact]
            public void StrokeACubicBezierCurve()
            {
                var geometry = new PathGeometry();
                PathFigure figure = new PathFigure();
                //for pixel perfect, we need to add 0.5 and 0.51
                figure.StartPoint = new Point(10+0.5, 100+0.5);
                var segment = new CubicBezierSegment(
                    new Point(30+0.5, 40+0.5),
                    new Point(80+0.5, 40+0.5),
                    new Point(100+0.51, 100+0.51),
                    true);
                figure.Segments.Add(segment);
                figure.IsClosed = false;
                figure.IsFilled = false;
                geometry.Figures.Add(figure);
                Pen pen = new Pen(Color.Black, 1);
                CheckGeometry(geometry, null, pen, 110, 110);
            }

            [Fact]
            public void StrokeACubicBezierPolyLine()
            {
                var geometry = new PathGeometry();
                PathFigure figure = new PathFigure();
                //for pixel perfect, we need to add 0.51
                figure.StartPoint = new Point(10+0.51, 100+0.51);
                var segment = new PolyCubicBezierSegment(new[]
                    {
                        new Point(0 + 0.51, 0 + 0.51),
                        new Point(200 + 0.51, 0 + 0.51),
                        new Point(300 + 0.51, 100 + 0.51),
                        new Point(300 + 0.51, 0 + 0.51),
                        new Point(400 + 0.51, 0 + 0.51),
                        new Point(600 + 0.51, 100 + 0.51)
                    },
                    true);
                figure.Segments.Add(segment);
                figure.IsClosed = false;
                figure.IsFilled = false;
                geometry.Figures.Add(figure);
                Pen pen = new Pen(Color.Black, 1);
                CheckGeometry(geometry, null, pen, 610, 110);
            }

            [Fact]
            public void DrawANodeArc1()
            {
                var pathGeometry = new PathGeometry();
                PathFigure figure = new PathFigure();
                //for pixel perfect, we need to add 0.5 and 0.51
                figure.StartPoint = new Point(60,100);
                figure.Segments.Add(
                    new ArcSegment(
                        size: new Size(60, 40),
                        rotationAngle: 10,
                        isLargeArc: true,
                        sweepDirection: SweepDirection.Counterclockwise,
                        point: new Point(140, 100),
                        isStroked: true));
                figure.IsClosed = false;
                figure.IsFilled = false;
                pathGeometry.Figures.Add(figure);
                Pen pen = new Pen(Color.Black, 1);
                CheckGeometry(pathGeometry, null, pen, 200,200);
            }

            [Fact]
            public void DrawANodeArc2()
            {
                var pathGeometry = new PathGeometry();
                PathFigure figure = new PathFigure();
                //for pixel perfect, we need to add 0.5 and 0.51
                figure.StartPoint = new Point(87.07, 45.86);
                figure.Segments.Add(
                    new ArcSegment(
                        point: new Point(80, 40),
                        size: new Size(10, 20),
                        rotationAngle: 0,
                        isLargeArc: false,
                        sweepDirection: SweepDirection.Counterclockwise,
                        isStroked: true));
                figure.IsClosed = false;
                figure.IsFilled = false;
                pathGeometry.Figures.Add(figure);
                Pen pen = new Pen(Color.Black, 1);
                CheckGeometry(pathGeometry, null, pen, 200,200);
            }
        }

        public class DrawImage
        {
            internal static void Check(Func<OSAbstraction.Graphics.ITexture> textureGettter, Rect rectangle, int width, int height,
                [CallerMemberName] string methodName = "unknown")
            {
                Application.EnableMSAA = false;

                MeshBuffer meshBuffer = new MeshBuffer();
                MeshList meshList = new MeshList();
                BuiltinGeometryRenderer renderer = new BuiltinGeometryRenderer();
                byte[] bytes;

                using (var context = new RenderContextForTest(width, height))
                {
                    renderer.OnBeforeRead();
                    renderer.DrawImage(textureGettter(), rectangle);//This must be called after the RenderContextForTest is created, for uploading textures to GPU via OpenGL.
                    renderer.OnAfterRead(meshList);

                    //rebuild mesh buffer
                    meshBuffer.Clear();
                    meshBuffer.Init();
                    meshBuffer.Build(meshList);

                    //draw mesh buffer to screen
                    context.Clear();
                    context.DrawMeshes(meshBuffer);

                    bytes = context.GetRenderedRawBytes();
                }

                Util.CheckExpectedImage(bytes, width, height, $"{RootDir}{nameof(DrawImage)}\\{methodName}.png");
            }

            internal static void Check(Func<OSAbstraction.Graphics.ITexture> textureGettter, Rect rectangle, (double top, double right, double bottom, double left) slice, int width, int height,
                [CallerMemberName] string methodName = "unknown")
            {
                Application.EnableMSAA = false;

                MeshBuffer meshBuffer = new MeshBuffer();
                MeshList meshList = new MeshList();
                BuiltinGeometryRenderer renderer = new BuiltinGeometryRenderer();
                byte[] bytes;

                using (var context = new RenderContextForTest(width, height))
                {
                    renderer.OnBeforeRead();
                    renderer.DrawImage(textureGettter(), rectangle, slice);//This must be called after the RenderContextForTest is created, for uploading textures to GPU via OpenGL.
                    renderer.OnAfterRead(meshList);

                    //rebuild mesh buffer
                    meshBuffer.Clear();
                    meshBuffer.Init();
                    meshBuffer.Build(meshList);

                    //draw mesh buffer to screen
                    context.Clear();
                    context.DrawMeshes(meshBuffer);

                    bytes = context.GetRenderedRawBytes();
                }

                Util.CheckExpectedImage(bytes, width, height, $"{RootDir}{nameof(DrawImage)}\\{methodName}.png");
            }

            [Fact]
            public void DrawOriginalImage()
            {
                var image = new Image(@"assets\images\logo.png");

                Check(() =>
                    {
                        var texture = Application.PlatformContext.CreateTexture();
                        texture.LoadImage(image.Data, image.Width, image.Height);
                        return texture;
                    },
                    new Rect(10, 10, image.Width, image.Height), 300, 300);
            }

            [Fact]
            public void DrawScaledImage()
            {
                var image = new Image(@"assets\images\logo.png");

                Check(() =>
                    {
                        var texture = Application.PlatformContext.CreateTexture();
                        texture.LoadImage(image.Data, image.Width, image.Height);
                        return texture;
                    },
                    new Rect(10, 10, 200, 100), 250, 250);
            }

            [Fact]
            public void DrawSlicedImage1()
            {
                var image = new Image(@"assets\images\button.png");

                Check(() =>
                    {
                        var texture = Application.PlatformContext.CreateTexture();
                        texture.LoadImage(image.Data, image.Width, image.Height);
                        return texture;
                    },
                    new Rect(2, 2, image.Width + 50, image.Height*0.8),
                    (83, 54, 54, 54),
                    500, 500);
            }

            [Fact]
            public void DrawSlicedImage2()
            {
                var image = new Image(@"assets\images\button.png");

                Check(() =>
                    {
                        var texture = Application.PlatformContext.CreateTexture();
                        texture.LoadImage(image.Data, image.Width, image.Height);
                        return texture;
                    },
                    new Rect(2, 2, image.Width*1.5, image.Height*1.8),
                    (83, 54, 54, 54),
                    500, 500);
            }
        }

        public class DrawGlyphRun
        {
            internal static void Check(Rect rectangle, GlyphRun glyphRun, Brush brush, int width, int height,
                [CallerMemberName] string methodName = "unknown")
            {
                Application.EnableMSAA = false;

                MeshBuffer meshBuffer = new MeshBuffer();
                MeshList meshList = new MeshList();
                BuiltinGeometryRenderer renderer = new BuiltinGeometryRenderer();
                byte[] bytes;

                using (var context = new RenderContextForTest(width, height))
                {
                    renderer.OnBeforeRead();
                    renderer.DrawGlyphRun(brush, glyphRun, rectangle);//This must be called after the RenderContextForTest is created, for uploading textures to GPU via OpenGL.
                    renderer.OnAfterRead(meshList);

                    //rebuild mesh buffer
                    meshBuffer.Clear();
                    meshBuffer.Init();
                    meshBuffer.Build(meshList);

                    //draw mesh buffer to screen
                    context.Clear();
                    context.DrawMeshes(meshBuffer);

                    bytes = context.GetRenderedRawBytes();
                }

                Util.CheckExpectedImage(bytes, width, height, $"{RootDir}{nameof(DrawGlyphRun)}\\{methodName}.png");
            }

            [Fact]
            public void DrawOneLineText()
            {
                GlyphRun glyphRun = new GlyphRun("Hello你好こんにちは", GUIStyle.Default.FontFamily, 24, FontStyle.Normal, FontWeight.Normal);
                Brush brush = new Brush(Color.Black);

                Check(new Rect(10, 10, 400, 40), glyphRun, brush, 400, 50);
            }

            [Fact]
            public void DrawOneLineTextWithoutSpace()
            {
                GlyphRun glyphRun = new GlyphRun("textwithoutspace", GUIStyle.Default.FontFamily, 24, FontStyle.Normal, FontWeight.Normal);
                Brush brush = new Brush(Color.Black);

                Check(new Rect(10, 10, 400, 40), glyphRun, brush, 400, 50);
            }

            [Fact]
            public void DrawOneLineTextWithSpace()
            {
                GlyphRun glyphRun = new GlyphRun("text with space", GUIStyle.Default.FontFamily, 24, FontStyle.Normal, FontWeight.Normal);
                Brush brush = new Brush(Color.Black);

                Check(new Rect(10, 10, 400, 40), glyphRun, brush, 400, 50);
            }

            [Fact]
            public void DrawMultipleLineText()
            {
                throw new Exception("The result is incorrect. FIXME.");

                GlyphRun glyphRun = new GlyphRun("Hello\n你好\nこんにちは", GUIStyle.Default.FontFamily, 24, FontStyle.Normal, FontWeight.Normal);
                Brush brush = new Brush(Color.Black);

                Check(new Rect(10, 10, 400, 120), glyphRun, brush, 400, 130);
            }

        }

        public class DrawDrawing
        {
            //TODO
        }

    }
}
