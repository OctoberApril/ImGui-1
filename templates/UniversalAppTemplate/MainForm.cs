﻿using ImGui;

namespace UniversalAppTemplate
{
    public class MainForm : Form
    {
        public MainForm() : base(new Rect(320, 180, 1280, 720))
        {
        }

        private bool open;
        private bool clicked;
        protected override void OnGUI()
        {
            if (GUILayout.Button("Button"))
            {
                clicked = !clicked;
            }
            if (clicked)
            {
                GUILayout.Button("123");
                GUILayout.Label("345");
            }
            GUI.Button(new Rect(100, 10, 200,40), "123");

            //GUILayout.Button("123");
            //GUI.Begin("test window", ref open);
            //GUI.End();
        }
    }
}

