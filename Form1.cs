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

        Model3DGroup modelGroup = new Model3DGroup();
        private void LoadAndShowObjModel() {
            var importer = new AssimpContext();

            string objFilePath = "C:/Users/trahm/Desktop/Projects/UnitySimulationMaker/Assets/TMRModels/govde.obj";
           
            string objFilePath1 = "C:/Users/trahm/Desktop/Projects/UnitySimulationMaker/Assets/TMRModels/bilek.obj";

            Scene scene = importer.ImportFile(objFilePath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);

            Scene scene1 = importer.ImportFile(objFilePath1, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);
            if (scene == null || scene.RootNode == null) {
                MessageBox.Show("OBJ dosyası yüklenirken bir hata oluştu.");
                return;
            }


            foreach (var mesh in scene.Meshes) {
                var positions = new Point3DCollection();
                var normals = new Vector3DCollection();
                var indices = new Int32Collection();

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
                    TriangleIndices = indices,
                    
                };
                
                var geometryModel3D = new GeometryModel3D(meshGeometry3D, new DiffuseMaterial(System.Windows.Media.Brushes.Gray));
              
                modelGroup.Children.Add(geometryModel3D);
            }

            foreach (var mesh in scene1.Meshes) {
                var positions = new Point3DCollection();
                var normals = new Vector3DCollection();
                var indices = new Int32Collection();

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
                
                // Apply transformations to the model here
                var transformGroup = new Transform3DGroup();

                // Example: Translation
                var translation = new TranslateTransform3D(0, 50, 0); // Move 10 units along X-axis
                transformGroup.Children.Add(translation);

                // Example: Rotation
                var rotation = new AxisAngleRotation3D(new System.Windows.Media.Media3D.Vector3D(0, 1, 0), 45); // Rotate 45 degrees around Y-axis
                var rotationTransform = new RotateTransform3D(rotation);
                transformGroup.Children.Add(rotationTransform);

                // Apply the transform group to the model
                geometryModel3D.Transform = transformGroup;
                modelGroup.Children.Add(geometryModel3D);
            }
            userControl11.modelVisual3D.Content = modelGroup;
            
        }



        private void button1_Click(object sender, EventArgs e) {
            
        }
    }
}
