﻿using ImGui.Rendering;
using Xunit;

namespace ImGui.UnitTest.Rendering
{
    public partial class NodeFacts
    {
        public class ContainerRenderingFixture
        {
            public ContainerRenderingFixture()
            {
                //mark as running unit tests
                Application.IsRunningInUnitTest = true;

                //don't use box-model for all nodes
                Node.DefaultUseBoxModel = false;

                //reset the style for rendering the rectangle of a node
                GUIStyle.Default.BackgroundColor = Color.White;
                GUIStyle.Default.Border = (1, 1, 1, 1);
                GUIStyle.Default.BorderColor = Color.Black;
                GUIStyle.Default.Padding = (1, 1, 1, 1);
                GUIStyle.Default.CellSpacing = (1, 1);
            }
        }

        public class Container : IClassFixture<ContainerRenderingFixture>
        {
            private static void CheckExpectedImage(Node node, string expectedImageFilePath)
            {
                int width = (int)node.Rect.Width;
                int height = (int)node.Rect.Height;
                Util.DrawNodeTreeToImage(out var imageRawBytes, node, width, height);
                Util.CheckExpectedImage(imageRawBytes, width, height, expectedImageFilePath);
            }

            [Theory]
            [InlineData(10, 10, 50, 50)]
            [InlineData(35, 35, 50, 50)]
            [InlineData(55, 55, 50, 50)]
            [InlineData(75, 75, 50, 50)]
            [InlineData(95, 95, 50, 50)]
            public void DrawNodesClipped(int clipX, int clipY, int clipWidth, int clipHeight)
            {
                var group = new Node(0, new Rect(120, 120));
                group.UseBoxModel = true;
                int i = 0;
                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        Node node = new Node(++i, new Rect(10 + x*20, 10 + y*20, 15, 15));
                        node.UseBoxModel = true;
                        group.AppendChild(node);
                    }
                }

                int width = (int)group.Rect.Width;
                int height = (int)group.Rect.Height;
                Util.DrawNodeTreeToImage(out var imageRawBytes, group, width, height, new Rect(clipX, clipY, clipWidth, clipHeight));
                Util.CheckExpectedImage(imageRawBytes, width, height, $@"Rendering\images\NodeFacts.Container.DrawNodesClipped_{clipX}_{clipY}_{clipWidth}_{clipHeight}.png");
            }

            [Fact]
            public void DrawAndLayoutEmptyContainer()
            {
                Node node = new Node(1, "container");
                node.AttachLayoutGroup(true);
                node.RuleSet.ApplyOptions(GUILayout.Width(300).Height(40));
                node.UseBoxModel = true;
                StyleRuleSetBuilder b = new StyleRuleSetBuilder(node);
                b.Border(1)
                    .BorderColor(Color.Black)
                    .Border((top: 1, right: 2, bottom: 1, left: 2));
                node.Layout();

                CheckExpectedImage(node, @"Rendering\images\NodeFacts.Container.DrawAndLayoutEmptyContainer.png");
            }

            [Fact]
            public void DrawAndLayoutContainerWithElements()
            {
                var container = new Node(1, "container");
                container.AttachLayoutGroup(false);
                container.RuleSet.ApplyOptions(GUILayout.Width(300).Height(40));
                container.UseBoxModel = true;
                StyleRuleSetBuilder b = new StyleRuleSetBuilder(container);
                b.Border(1)
                    .BorderColor(Color.Black)
                    .Padding((top: 4, right: 3, bottom: 4, left: 3))
                    .AlignmentVertical(Alignment.Center)
                    .AlignmentHorizontal(Alignment.Center);

                var icon = new Node(2, "icon");
                icon.AttachLayoutEntry(new Size(20, 20));
                icon.RuleSet.ApplyOptions(GUILayout.Width(20).Height(20));
                icon.UseBoxModel = false;
                icon.Geometry = new ImageGeometry(@"assets\images\logo.png");

                var title = new Node(3, "title");
                var titleTextSize = GUIStyle.Default.CalcSize("title", GUIState.Normal);//TODO consider this
                title.AttachLayoutEntry(titleTextSize);
                title.RuleSet.ApplyOptions(GUILayout.Height(20).ExpandWidth(true));
                title.UseBoxModel = false;
                title.Geometry = new TextGeometry("title");

                var closeButton = new Node(4, "close button");
                closeButton.AttachLayoutEntry(new Size(20, 20));
                closeButton.UseBoxModel = false;
                PathGeometry path = new PathGeometry();
                path.PathRect(new Rect(0, 0, 20, 20));
                path.PathFill(Color.Black);

                closeButton.Geometry = path;

                container.AppendChild(icon);
                container.AppendChild(title);
                container.AppendChild(closeButton);

                container.Layout();

                CheckExpectedImage(container, @"Rendering\images\NodeFacts.Container.DrawAndLayoutContainerWithElements.png");
            }

            [Fact]
            public void DrawAWindow()
            {
                //window
                var windowContainer = new Node("#window");
                windowContainer.AttachLayoutGroup(true);
                windowContainer.RuleSet.ApplyOptions(GUILayout.Width(400));
                windowContainer.UseBoxModel = true;
                windowContainer.RuleSet.Border = (1, 1, 1, 1);
                windowContainer.RuleSet.BackgroundColor = Color.White;

                //title bar
                {
                    var titleBarContainer = new Node(1, "#titleBar");
                    titleBarContainer.AttachLayoutGroup(false);
                    titleBarContainer.RuleSet.ApplyOptions(GUILayout.ExpandWidth(true).Height(40));
                    titleBarContainer.UseBoxModel = true;
                    StyleRuleSetBuilder b = new StyleRuleSetBuilder(titleBarContainer);
                    b.Padding((top: 8, right: 8, bottom: 8, left: 8))
                        .FontColor(Color.Black)
                        .FontSize(12)
                        .BackgroundColor(Color.White)
                        .AlignmentVertical(Alignment.Center)
                        .AlignmentHorizontal(Alignment.Start);

                    var icon = new Node(2, "#icon");
                    icon.AttachLayoutEntry(new Size(20, 20));
                    icon.RuleSet.ApplyOptions(GUILayout.Width(20).Height(20));
                    icon.UseBoxModel = false;
                    icon.Geometry = new ImageGeometry(@"assets\images\logo.png");

                    var title = new Node(3, "#title");
                    title.AttachLayoutEntry(Size.Zero);
                    title.RuleSet.ApplyOptions(GUILayout.Height(20));
                    title.UseBoxModel = false;
                    title.Geometry = new TextGeometry("The Window Title");

                    var closeButton = new Node(4, "#close button");
                    closeButton.AttachLayoutEntry(new Size(20, 20));
                    closeButton.RuleSet.ApplyOptions(GUILayout.Width(20).Height(20));
                    closeButton.UseBoxModel = false;
                    PathGeometry path = new PathGeometry();
                    path.PathRect(new Rect(0, 0, 20, 20));
                    closeButton.Geometry = path;

                    titleBarContainer.AppendChild(icon);
                    titleBarContainer.AppendChild(title);
                    titleBarContainer.AppendChild(closeButton);
                    windowContainer.AppendChild(titleBarContainer);
                }

                //client area background
                {
                    var clientArea = new Node("#ClientArea_Background");
                    clientArea.AttachLayoutGroup(true);
                    clientArea.RuleSet.ApplyOptions(GUILayout.ExpandWidth(true).Height(200));
                    windowContainer.AppendChild(clientArea);
                }

                windowContainer.Layout();

                CheckExpectedImage(windowContainer, @"Rendering\images\NodeFacts.Container.DrawAWindow.png");
            }
        }
    }
}