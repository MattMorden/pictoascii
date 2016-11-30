using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

/*

    Matthew Morden
    PicToAscii
    May 8th, 2015
    Used to convert a picture into ASCII characters
*/

namespace PicToAscii
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string file_ = "";

        public MainWindow()
        {
            InitializeComponent();
        }


        private void OpenFileDialogBox()
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.ShowDialog();
                filePathTextBox.Text = dlg.FileName;
                file_ = filePathTextBox.Text;
            }
            catch(Exception ep)
            {
                System.Windows.MessageBox.Show(ep.Message + "\nCouldnt open file");
            }
        }



        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if(filePathTextBox.Text != null && textBoxWidth.Text != null)
            {
                try
                {
                    int width;
                    width = Convert.ToInt32(textBoxWidth.Text);
                    FileInfo fi = new FileInfo(file_);
                    if (!fi.Exists)
                    {
                        throw new Exception(string.Format("File {0} not found", file_));
                    }

                    string outputFile = System.IO.Path.Combine(fi.DirectoryName, System.IO.Path.GetFileNameWithoutExtension(file_) + ".txt");
                    Bitmap bmInput = new Bitmap(file_);

                    if (width > bmInput.Width)
                    {
                        throw new Exception("Output width must be <= pixel width of image");
                    }

                    // Generate the ASCII art
                    AsciiArt.GenerateAsciiArt(bmInput, outputFile, width);
                }
                catch(Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message + "\nCouldn't generate ASCII from Pic.");
                }
            }
            System.Windows.MessageBox.Show("Picture Successfully Created!");
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialogBox();
        }
    }
}
