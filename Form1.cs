using Assimp;
using System;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Media3D;


namespace _3DModelRender {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
       
            LoadAndShowObjModel();
        }

        private void LoadAndShowObjModel() {
            var importer = new AssimpContext();

            string objFilePath = "C:/Users/trahm/Desktop/Projects/UnitySimulationMaker/Assets/TMRModels/govde.obj";

            Scene scene = importer.ImportFile(objFilePath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);

            if (scene == null || scene.RootNode == null) {
                MessageBox.Show("OBJ dosyası yüklenirken bir hata oluştu.");
                return;
            }

            Model3DGroup modelGroup = new Model3DGroup();

            foreach (var mesh in scene.Meshes) {
                var positions = new Point3DCollection();
                var normals = new Vector3DCollection();
                var indices = new Int32Collection();

                // Mesh'in vertex ve index bilgilerini alın.
                foreach (var vertex in mesh.Vertices) {
                    positions.Add(new Point3D(vertex.X, vertex.Y, vertex.Z));
                    normals.Add(new System.Windows.Media.Media3D.Vector3D(vertex.X, vertex.Y, vertex.Z));
                }

                foreach (var face in mesh.Faces) {
                    foreach (var index in face.Indices) {
                        indices.Add(index);
                    }
                }

                var meshGeometry3D = new MeshGeometry3D() {
                    Positions = positions,
                    Normals = normals,
                    TriangleIndices = indices
                };

                var geometryModel3D = new GeometryModel3D(meshGeometry3D, new DiffuseMaterial(System.Windows.Media.Brushes.Gray));
                modelGroup.Children.Add(geometryModel3D);
            }
            userControl11.modelVisual3D.Content = modelGroup;
            
        }
    }
}
