﻿using ImGui.GraphicsAbstraction;
using ImGui.Rendering;
using ImGui.Rendering.Composition;

namespace ImGui.GraphicsImplementation
{
    internal partial class BuiltinGeometryRenderer : RecordReader
    {
        public override void DrawLine(Pen pen, Point point0, Point point1)
        {
            unsafe
            {
                Point* scratchForLine = stackalloc Point[2];
                scratchForLine[0] = point0;
                scratchForLine[1] = point1;
                AddPolyline(scratchForLine, 2, pen.LineColor, false, pen.LineWidth);
            }
        }

        public override void DrawRectangle(Brush brush, Pen pen, Rect rectangle)
        {
            unsafe
            {
                Point* scratchForRectangle = stackalloc Point[4];
                scratchForRectangle[0] = rectangle.TopLeft;
                scratchForRectangle[1] = rectangle.TopRight;
                scratchForRectangle[2] = rectangle.BottomRight;
                scratchForRectangle[3] = rectangle.BottomLeft;
                AddPolyline(scratchForRectangle, 4, pen.LineColor, true, pen.LineWidth);
                AddConvexPolyFilled(scratchForRectangle, 4, brush.FillColor, false);
            }
        }

        public override void DrawRoundedRectangle(Brush brush, Pen pen, Rect rectangle, double radiusX, double radiusY)
        {
            throw new System.NotImplementedException();
        }

        public override void DrawEllipse(Brush brush, Pen pen, Point center, double radiusX, double radiusY)
        {
            throw new System.NotImplementedException();
        }

        public override void DrawGeometry(Brush brush, Pen pen, Geometry geometry)
        {
            throw new System.NotImplementedException();
        }

        public override void DrawImage(Image image, Rect rectangle)
        {
            throw new System.NotImplementedException();
        }

        public override void DrawDrawing(Drawing drawing)
        {
            throw new System.NotImplementedException();
        }
    }
}