using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace AnemiaEngine
{
    class Sprite
    {
        Texture texture;

        Shader WorldShader;
        Shader UIShader;

        Game game;

        // square shape
        float[] vertices =
        {
            //Position          Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };
        uint[] indices = {  // note that we start from 0!
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        };

        int VertexBufferObject;
        int VertexArrayObject;
        int ElementBufferObject;

        public Sprite(Texture texture, float scale = 1)
        {
            Scale = scale;

            this.texture = texture;
            UIShader = new Shader("Shaders/TextShader.vert", "Shaders/TextShader.frag");
            WorldShader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            game = Game.Current;

            // VBO
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // VAO
            VertexArrayObject = GL.GenVertexArray();
            // ..:: Initialization code (done once (unless your object frequently changes)) :: ..
            // 1. bind Vertex Array Object
            GL.BindVertexArray(VertexArrayObject);
            // 2. copy our vertices array in a buffer for OpenGL to use
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            // 3. then set our vertex attributes pointers

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            int texCoordLocation = WorldShader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            // EBO
            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        }

        public float Scale = 1;


        public bool isUI = false;
        public void Draw(Vector3 drawPosition)
        {
            // activating shader and texture
            WorldShader.Use();
            texture.Use();

            // projection magic
            Matrix4 model = Matrix4.CreateTranslation(drawPosition / Scale) * Matrix4.CreateScale(Scale,Scale,0);
            Matrix4 view = Matrix4.CreateTranslation(game.cameraPosition - game.cameraPosition * 10);
            float Width = (game.Bounds.Max.X - game.Bounds.Min.X);
            float Height = (game.Bounds.Max.Y - game.Bounds.Min.Y);
            //Matrix4 projection = Matrix4.CreatePerspective(MathHelper.DegreesToRadians(45.0f), Width / Height, 0.1f, 100.0f);
            Matrix4 projection = Matrix4.CreateOrthographic(50, 50, 0.1f, 100.0f);
            

            int lModel = GL.GetUniformLocation(WorldShader.Handle, "model");
            GL.UniformMatrix4(lModel, true, ref model);
            int lView = GL.GetUniformLocation(WorldShader.Handle, "view");
            GL.UniformMatrix4(lView, true, ref view);
            int lProjection = GL.GetUniformLocation(WorldShader.Handle, "projection");
            GL.UniformMatrix4(lProjection, true, ref projection);
            
            
            // drawing
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }
        public void DrawUI(Vector3 screenPosition)
        {
            // activating shader and texture
            UIShader.Use();
            texture.Use();

            // projection magic
            Matrix4 model = Matrix4.CreateTranslation(screenPosition / Scale) * Matrix4.CreateScale(Scale, Scale, 0);
            int lModel = GL.GetUniformLocation(WorldShader.Handle, "model");
            GL.UniformMatrix4(lModel, true, ref model);

            // drawing
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
