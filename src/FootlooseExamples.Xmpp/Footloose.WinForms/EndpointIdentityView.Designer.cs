namespace Footloose.WinForms
{
    partial class EndpointIdentityView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EndpointTreeView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // EndpointTreeView
            // 
            this.EndpointTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EndpointTreeView.Location = new System.Drawing.Point(0, 0);
            this.EndpointTreeView.Name = "EndpointTreeView";
            this.EndpointTreeView.Size = new System.Drawing.Size(186, 302);
            this.EndpointTreeView.TabIndex = 0;
            // 
            // EndpointIdentityView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EndpointTreeView);
            this.Name = "EndpointIdentityView";
            this.Size = new System.Drawing.Size(186, 302);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView EndpointTreeView;
    }
}
