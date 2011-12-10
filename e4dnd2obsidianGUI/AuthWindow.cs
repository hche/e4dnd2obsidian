 using System;
 using System.Windows.Forms;
  
  namespace e4dnd2obsidianGUI
  {
      public partial class AuthWindow : System.Windows.Forms.Form
      {
          public string Pin { get; private set; }
  
          public AuthWindow()
          {
              InitializeComponent();
          }
  
          public AuthWindow(string url)
          {
              InitializeComponent();
  
              Load += new EventHandler(delegate(object sender, EventArgs e)
                      { wbExplorer.Navigate(url); });
          }
  
          private void Okay(object sender, EventArgs e)
          {
              Pin = txtPin.Text;
  
              DialogResult = DialogResult.OK;
  
              Close();
          }
  
          private void OnCancel(object sender, EventArgs e)
          {
              DialogResult = DialogResult.Cancel;
  
              Close();
          }
      }
  }

