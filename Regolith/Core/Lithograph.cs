using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using Lithograph.Graphics;
using ImGuiNET;
using Lithograph.Windowing;
using Lithograph.Windowing.Windows;

namespace Lithograph.Core
{
    public static class Lithograph
    {
        public static IntPtr Window;
        public static IntPtr GLContext;

        public static bool WindowOpen = true;

        public class SDLBindingsContext : IBindingsContext
        {
            public IntPtr GetProcAddress(string procName)
            {
                return SDL.SDL_GL_GetProcAddress(procName);
            }
        }

        public static void Run()
        {
            Window = SDL.SDL_CreateWindow("Lithograph",
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                1600,
                900,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL);

            SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_CONTEXT_MAJOR_VERSION, 3);
            SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_CONTEXT_MINOR_VERSION, 2);
            SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_CONTEXT_PROFILE_MASK,
                (int)SDL.SDL_GLprofile.SDL_GL_CONTEXT_PROFILE_CORE);

            GLContext = SDL.SDL_GL_CreateContext(Window);
            GL.LoadBindings(new SDLBindingsContext());

            var igCtx = ImGui.CreateContext();
            ImGui.SetCurrentContext(igCtx);

            var igIo = ImGui.GetIO();
            igIo.DisplaySize.X = 1600;
            igIo.DisplaySize.Y = 900;

            ImGuiImplSDL2.Init(Window);
            ImGuiImplOpenGL3.Init();

            WindowManager.Open<MenuBar>();

            var sw = new Stopwatch();
            sw.Start();
            while (WindowOpen)
            {
                SDL.SDL_Event evt;
                while (SDL.SDL_PollEvent(out evt) != 0)
                {
                    ImGuiImplSDL2.ProcessEvent(evt);
                    switch (evt.type)
                    {
                        case SDL.SDL_EventType.SDL_QUIT:
                            WindowOpen = false;
                            break;
                    }
                }

                ImGuiImplSDL2.NewFrame();
                ImGui.NewFrame();

                var time = sw.Elapsed;
                sw.Restart();
                igIo.DeltaTime = (float)time.TotalSeconds;

                GL.ClearColor(0, 0, 0, 1);
                GL.Clear(ClearBufferMask.ColorBufferBit |
                         ClearBufferMask.DepthBufferBit);

                WindowManager.RenderAll();

                ImGui.Render();
                ImGuiImplOpenGL3.Render();

                SDL.SDL_GL_SwapWindow(Window);
            }
        }
    }
}
