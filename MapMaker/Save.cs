using System;
using System.Windows.Forms;

namespace MapMaker
{
    public static class Save
    {
        public static SaveFileDialog saver;
        public static void saverSetup()
        {
            saver = new SaveFileDialog();
            saver.AddExtension = true;
            saver.DefaultExt = ".smf";
            saver.InitialDirectory = System.Environment.CurrentDirectory;
            saver.OverwritePrompt = true;
            saver.ShowHelp = false;
        }
    }
}
